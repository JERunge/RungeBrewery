using BrewUI.Data;
using BrewUI.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BrewUI.Models
{
    public class ConnectionHandler : Conductor<object>, IHandle<SerialToSendEvent>, IHandle<ConnectionEvent>
    {
        static BluetoothConnection bluetooth;
        static WifiTCPConnection wifi;

        private IEventAggregator _events;

        private List<ArduinoMessage> sendBuffer;

        private DispatcherTimer sendTimer;

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
                SendToArduino(sendBuffer[0]);
                sendBuffer.RemoveAt(0);
            }
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

        public void ArduinoReconnect()
        {

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

        #region Event handlers
        public void Handle(SerialToSendEvent message)
        {
            sendBuffer.Add(message.arduinoMessage);
        }

        public void Handle(ConnectionEvent message)
        {
            if(message.ConnectionStatus == MyEnums.ConnectionStatus.Connected)
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
    }
    #endregion
}
