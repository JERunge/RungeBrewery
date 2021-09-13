using BrewUI.Data;
using BrewUI.EventModels;
using Caliburn.Micro;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BrewUI.Models
{
    public class BluetoothConnection : Conductor<object>, IHandle<ConnectionEvent>
    {
        #region Variables and constants

        private IEventAggregator _events;

        private BluetoothClient BTClient;
        private NetworkStream BTStream;
        private readonly BackgroundWorker BW_ReceiveData = new BackgroundWorker();
        private readonly BackgroundWorker BW_RequestData = new BackgroundWorker();

        private ArduinoMessage AM = new ArduinoMessage();
        private string BTName = "Runge Brewery";
        public DateTime pingTimeStamp { get; set; }

        #endregion

        public BluetoothConnection(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            // Background worker that reads incomming data
            BW_ReceiveData.DoWork += BW_BT;
            BW_ReceiveData.WorkerSupportsCancellation = true;

            // Backgroundworker that requests update data from Arduino
            BW_RequestData.DoWork += BW_RD;
            BW_RequestData.WorkerSupportsCancellation = true;
        }

        #region Methods

        public async Task ArduinoConnect()
        {
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Searching });
            BTClient = new BluetoothClient();
            BluetoothDeviceInfo[] devices = await Task.Run(() => BTClient.DiscoverDevices());
            BluetoothDeviceInfo device = null;

            foreach (var dev in devices)
            {
                if (dev.DeviceName == BTName)
                {
                    device = dev;
                    break;
                }
            }

            if (!device.Authenticated)
            {
                try
                {
                    await Task.Run(() => BluetoothSecurity.PairRequest(device.DeviceAddress, "0000"));
                }
                catch
                {
                    MessageBox.Show("Could not pair to Brewery. Please try again.", "Pairing failed");
                    _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
                    return;
                }
                
            }

            device.Refresh();

            try
            {
                _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Connecting });
                await Task.Run(() => BTClient.Connect(device.DeviceAddress, BluetoothService.SerialPort));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection failed");
                _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
                return;
            }
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Connected });
        }

        public void ArduinoDisconnect()
        {
            AM.AIndex = 'H';
            AM.AMessage = "0";
            _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = AM });
            Thread.Sleep(100);

            AM.AIndex = 'P';
            _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = AM });
            Thread.Sleep(100);

            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
            BTClient.Close();
        }

        private void BW_BT(object sender, DoWorkEventArgs e) // Do this in background
        {
            TimeSpan _discTimeSpan = TimeSpan.FromMilliseconds(2000);
            BTStream = BTClient.GetStream();
            BTStream.ReadTimeout = 300;

            while (BTClient.Connected == true)
            {
                //if (DateTime.Now - pingTimeStamp > TimeSpan.FromSeconds(5)) // Check if connection is lost. If so, try to reconnect.
                //{
                //    _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Reconnecting });
                //    return;
                //}

                if (BW_ReceiveData.CancellationPending) // Check if cancellation is pending and exit if so.
                {
                    return;
                }

                try
                {
                    if (BTStream.DataAvailable)
                    {
                        // Prepare byte array and string for received data
                        byte[] receive = new byte[1024];
                        Array.Clear(receive, 0, receive.Length);
                        string readMessage = "";

                        BTStream.Read(receive, 0, receive.Length);

                        // Start reading data to _receivedMessage if startmarker is detected
                        if (Encoding.ASCII.GetString(receive)[0] == '<')
                        {
                            do // Add read byte to array until endmarker appears
                            {

                                BTStream.Read(receive, 0, receive.Length);

                                readMessage += Encoding.ASCII.GetString(receive);
                            }
                            while (BTStream.DataAvailable && Encoding.ASCII.GetString(receive)[0] != '>');

                            ArduinoMessage _arduinoMessage = new ArduinoMessage();

                            // Read index value from read message
                            _arduinoMessage.AIndex = readMessage[0];

                            // Read value from read message
                            for (int i = 1; i < readMessage.Length; i++)
                            {
                                if (readMessage[i] != '>')
                                {
                                    _arduinoMessage.AMessage += readMessage[i];
                                }
                                else if (readMessage[i] == '>')
                                {
                                    break;
                                }
                            }

                            // Publish message on event
                            _events.PublishOnUIThread(new SerialReceivedEvent { arduinoMessage = _arduinoMessage });
                            pingTimeStamp = DateTime.Now;
                        }
                    }
                }
                catch
                {
                    //MessageBox.Show("Could not read incoming data");
                }

            }
        }

        private void BW_Completed(object sender, RunWorkerCompletedEventArgs e) // Do this when worker is finished
        {

        }

        private void BW_RD(object sender, DoWorkEventArgs e) // Do this in background
        {
            while (true)
            {
                if (BW_RequestData.CancellationPending) // Check if cancellation is pending and exit if so.
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }

        public void SendToArduino(ArduinoMessage message)
        {
            if (BTClient != null)
            {
                try
                {
                    BTStream = BTClient.GetStream();
                    if (BTClient.Connected && BTStream != null)
                    {
                        var buffer = System.Text.Encoding.UTF8.GetBytes(ArduinoParse.ToParse(message));
                        BTStream.Write(buffer, 0, buffer.Length);
                        BTStream.Flush();
                    }
                }
                catch
                {

                }
            }
        }
        #endregion

        #region Event handlers

        public void Handle(ConnectionEvent message)
        {
            switch (message.ConnectionStatus)
            {
                case MyEnums.ConnectionStatus.Connected:
                    BW_ReceiveData.RunWorkerAsync();
                    BW_RequestData.RunWorkerAsync();
                    break;
                case MyEnums.ConnectionStatus.Disconnected:
                    BW_ReceiveData.CancelAsync();
                    BW_RequestData.CancelAsync();
                    break;
                case MyEnums.ConnectionStatus.Reconnecting:
                    BW_ReceiveData.CancelAsync();
                    BW_RequestData.CancelAsync();
                    BTClient.Close();
                    ArduinoConnect();
                    break;
            }
        }

        #endregion
    }
}
