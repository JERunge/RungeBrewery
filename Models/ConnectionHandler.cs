using BrewUI.Data;
using BrewUI.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Models
{
    public class ConnectionHandler : Conductor<object>, IHandle<SerialToSendEvent>
    {
        static WifiConnection wifi;
        static BluetoothConnection bluetooth;

        private IEventAggregator _events;

        public ConnectionHandler(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            // Initiate connection classes
            wifi = new WifiConnection(events);
            bluetooth = new BluetoothConnection(events);
        }

        public async Task ArduinoConnect()
        {
            if (Properties.Settings.Default.ConnectionType == "Wifi")
            {
                await Task.Run(() => wifi.StartClient());
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
                wifi.Closeclient();
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
            string message = arduinoMessage.AIndex.ToString() + arduinoMessage.AMessage;

            if (Properties.Settings.Default.ConnectionType == "Wifi")
            {
                wifi.SendToWifi(message);
            }
            else
            {
                bluetooth.SendToArduino(arduinoMessage);
            }
        }

        #region Event handlers
        public void Handle(SerialToSendEvent message)
        {
            SendToArduino(message.arduinoMessage);
        }
    }
    #endregion
}
