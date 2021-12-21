using BrewUI.Data;
using BrewUI.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using static BrewUI.Models.MyEnums;

namespace BrewUI.Models
{
    public class ConnectionHandler : Conductor<object>, IHandle<SerialToSendEvent>, IHandle<ConnectionEvent>, IHandle<SerialReceivedEvent>
    {
        #region Data

        static BluetoothConnection bluetooth;
        static WifiTCPConnection wifi;

        private IEventAggregator _events;

        private List<ArduinoMessage> sendBuffer;

        private DispatcherTimer sendTimer;

        private ConnectionStatus connectionStatus;

        private bool dataVerified;

        #endregion

        #region Methods

        public ConnectionHandler(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            // Initiate connection classes
            bluetooth = new BluetoothConnection(_events);
            wifi = new WifiTCPConnection(_events);

            // Initiate send buffer to store outgoing messages
            sendBuffer = new List<ArduinoMessage>();

            // Initiate timer to send messages 
            sendTimer = new DispatcherTimer();
            sendTimer.Interval = TimeSpan.FromMilliseconds(500);
            sendTimer.Tick += SendTimer_Tick;
        }

        private void SendTimer_Tick(object sender, EventArgs e)
        {
            if(sendBuffer.Count > 0)
            {
                //SendToArduino(sendBuffer[0]);
                SendWithVerification(sendBuffer[0]);
                //sendBuffer.RemoveAt(0);
            }
        }

        private async Task SendWithVerification(ArduinoMessage am)
        {
            dataVerified = false;
            sendTimer.Stop();

            for(int i = 1; i <= 5; i++)
            {
                if (!dataVerified) // No verification received yet
                {
                    SendToArduino(am);
                }
                else // Verification received
                {
                    dataVerified = false;
                    sendBuffer.Clear();
                    sendTimer.Start();
                    return; 
                }

                await Task.Delay(2000);
            }

            sendTimer.Start();

        }

        public async Task ArduinoConnect()
        {
            if (Properties.Settings.Default.ConnectionType == "Wifi")
            {
                await Task.Run(() => wifi.ConnectClient());
            }
            else
            {
                await Task.Run(() => bluetooth.ArduinoConnect());
            }
        }

        public void ArduinoDisconnect()
        {
            if(Properties.Settings.Default.ConnectionType == "Wifi")
            {
                wifi.CloseClient();
            }
            else
            {
                bluetooth.ArduinoDisconnect();
            }
        }

        public void SendToArduino(ArduinoMessage arduinoMessage)
        {
            if (Properties.Settings.Default.ConnectionType == "Wifi")
            {
                wifi.SendToWifi(arduinoMessage);
            }
            else
            {
                bluetooth.SendToArduino(arduinoMessage);
            }
        }

        #endregion

        #region Event handlers
        public void Handle(SerialToSendEvent message)
        {
            sendBuffer.Add(message.arduinoMessage);
        }

        public void Handle(ConnectionEvent message)
        {
            connectionStatus = message.ConnectionStatus;

            if(connectionStatus == MyEnums.ConnectionStatus.Connected)
            {
                sendTimer.Start();
            }
            else
            {
                if (sendTimer.IsEnabled)
                {
                    sendTimer.Stop();
                }
            }
        }

        public void Handle(SerialReceivedEvent message)
        {
            ArduinoMessage am = message.arduinoMessage;
            if(am.AIndex == 'V' && am.AMessage == "OK")
            {
                dataVerified = true;
            }
        }
    }
    #endregion
}
