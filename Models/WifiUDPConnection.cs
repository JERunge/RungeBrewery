using BrewUI.Data;
using BrewUI.EventModels;
using BrewUI.Properties;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.NetworkInformation;
using System.Windows.Threading;

namespace BrewUI.Models
{
    public class WifiUDPConnection : Conductor<object>, IHandle<ConnectionEvent>
    {
        private IEventAggregator _events;
        private int breweryPort;
        private bool commandVerified;
        private DispatcherTimer sendTimer;
        private List<ArduinoMessage> sendBuffer;
        private UdpClient senderUDP;
        IPEndPoint ep;

        public WifiUDPConnection(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            // Fetch UDP info of brewery
            breweryPort = Settings.Default.breweryPort;

            // Initiate UDP client
            senderUDP = new UdpClient();
            ep = new IPEndPoint(IPAddress.Any, breweryPort);

            // Setup send buffer and command verification from brewery
            sendTimer = new DispatcherTimer();
            sendTimer.Interval = TimeSpan.FromMilliseconds(500);
            sendTimer.Tick += SendTimer_Tick;
            sendTimer.IsEnabled = false;

            sendBuffer = new List<ArduinoMessage>();

            Task.Run(() => listenForUDP());
        }

        private async void SendTimer_Tick(object sender, EventArgs e)
        {
            if (sendBuffer.Count > 0)
            {
                commandVerified = false;
                TimeSpan timedOutSpan = TimeSpan.FromSeconds(10);
                DateTime endTime = DateTime.Now + timedOutSpan;
                int sendCounter = 0;
                while (true) // Resend command untill it is verified by brewery
                {
                    sendCounter++;
                    sendToUDP(sendBuffer[0]);
                    await Task.Delay(2000);
                    if(DateTime.Now > endTime)
                    {
                        _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
                        MessageBox.Show("No command verification received - connection timed out.");
                    }
                    if (commandVerified)
                    {
                        break;
                    }
                }

                if (commandVerified)
                {
                    sendBuffer.RemoveAt(0);
                }
                _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = $"Send count: {sendCounter}" });
            }
        }

        public void ConnectUDP()
        {
            try
            {
                senderUDP.Connect(ep);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            if (senderUDP.Client.Connected)
            {
                _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Connected });
            }
        }

        public void DisconnectUDP()
        {
            try
            {
                senderUDP.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
        }

        public void sendToUDP(ArduinoMessage msg)
        {
            byte[] data = Encoding.ASCII.GetBytes(ArduinoParse.ToParse(msg));
            try
            {
                if(true)
                {
                    senderUDP.Send(data, data.Length);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task listenForUDP()
        {
            int recPort = Properties.Settings.Default.receivePort;

            using (UdpClient listener = new UdpClient(recPort))
            {
                
                IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 0);

                while (true)
                {
                    byte[] receivedData = listener.Receive(ref listenEndPoint);
                    ArduinoMessage recAM = ArduinoParse.FromParse(Encoding.ASCII.GetString(receivedData));

                    if (recAM.AIndex == 'C' && recAM.AMessage == "Command ok")
                    {
                        commandVerified = true;
                        _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = "Command verified" });
                    }
                    else
                    {
                        if (commandVerified)
                        {
                            _events.PublishOnUIThread(new SerialReceivedEvent { arduinoMessage = recAM });
                        }
                    }
                }
            }
        }

        public void Handle(ConnectionEvent message)
        {
            switch (message.ConnectionStatus)
            {
                case MyEnums.ConnectionStatus.Connected:
                    try
                    {
                        sendTimer.Start();
                    }
                    catch { }
                    break;
                case MyEnums.ConnectionStatus.Disconnected:
                    try
                    {
                        sendTimer.Stop();
                        sendBuffer.Clear();
                    }
                    catch { }
                    break;
            }
        }
    }
}
