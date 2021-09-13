using BrewUI.Data;
using BrewUI.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace BrewUI.Models
{
    public class WifiConnection : Conductor<object>
    {
        private IEventAggregator _events;
        static TcpClient client;

        public WifiConnection(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);
        }        

        public void StartClient()
        {
            //Connect to the server
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Connecting });
            try
            {
                client = new TcpClient();
                client.Connect("192.168.1.202", 80);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Connection failed");
                _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
                return;
            }

            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Connected });

            while (client.Connected)
            {
                if(client.Available > 0)
                {
                    //Initializing a new byte array the size of the available bytes on the network stream
                    byte[] readBytes = new byte[client.Available];
                    //Reading data from the stream
                    client.GetStream().Read(readBytes, 0, client.Available);
                    //Converting the byte array to string
                    string str = System.Text.Encoding.ASCII.GetString(readBytes);
                    //This should output "Hello world" to the console window

                    ArduinoMessage _arduinoMessage = new ArduinoMessage();

                    // Read index value from read message
                    _arduinoMessage.AIndex = str[1];

                    // Read value from read message
                    for (int i = 2; i < str.Length; i++)
                    {
                        if (str[i] != '>')
                        {
                            if (str[i] != ' ')
                            {
                                _arduinoMessage.AMessage += str[i];
                            }
                        }
                        else if (str[i] == '>')
                        {
                            break;
                        }
                    }

                    // Publish message on event
                    _events.PublishOnUIThread(new SerialReceivedEvent { arduinoMessage = _arduinoMessage });
                }
            }
        }

        public void Closeclient()
        {
            client.Close();
            client.Dispose();
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
        }

        public void SendToWifi(ArduinoMessage message)
        {
            if (client.Connected)
            {
                NetworkStream stream = client.GetStream();
                //Converting string to byte array
                if (message == null)
                {
                    return;
                }
                var buffer = System.Text.Encoding.UTF8.GetBytes(ArduinoParse.ToParse(message));
                //Sending the byte array to the server
                client.Client.Send(buffer);
            }
        }

        #region Event handlers

        #endregion
    }

}
