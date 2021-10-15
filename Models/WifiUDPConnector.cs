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
using System.Net.NetworkInformation;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Forms;

namespace BrewUI.Models
{
    public class WifiUDPConnector : Conductor<object>
    {
        private IEventAggregator _events;
        private int breweryPort;
        private int receivePort;
        private UdpClient udpClient;
        private IPAddress breweryIP;
        private bool endSearch;
        private bool searchFinished;

        public WifiUDPConnector(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
        }

        public async Task<IPAddress> GetIP()
        {
            searchFinished = false;
            Task.Factory.StartNew(SearchForIP).ContinueWith(result => confirmSearchFinished());

            while (!searchFinished)
            {
                await Task.Delay(500);
            }

            //MessageBox.Show("Returning IP now");
            return breweryIP;
        }

        private void confirmSearchFinished()
        {
            searchFinished = true;
        }

        private async Task SearchForIP()
        {
            breweryPort = Settings.Default.breweryPort;
            receivePort = Settings.Default.receivePort;
            udpClient = new UdpClient();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, receivePort));
            breweryIP = null;
            endSearch = false;

            var tasks = new List<Task>();
            tasks.Add(Task.Run(() => timeoutHandler()));
            tasks.Add(Task.Run(() => sendRequest()));
            tasks.Add(listenForVerification());

            await Task.WhenAll(tasks);
        }

        private async Task sendRequest()
        {
            _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = $"Sender started" });
            TimeSpan requestInterval = TimeSpan.FromSeconds(2);
            DateTime requestTime = DateTime.Now + requestInterval;

            while (true)
            {
                // Send request every n seconds
                if(requestTime <= DateTime.Now)
                {
                    var data = Encoding.UTF8.GetBytes("Connection request");
                    udpClient.Send(data, data.Length, "255.255.255.255", breweryPort);
                    _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = $"To arduino: {Encoding.UTF8.GetString(data)}" });
                    requestTime = DateTime.Now + requestInterval;
                }

                // Exit request loop if cancelled
                if (endSearch)
                {
                    return;
                }
            }
        }

        private async Task listenForVerification()
        {
            _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = $"Listener started" });
            var from = new IPEndPoint(0, 0);
            string receivedString = "";
            while (true)
            {
                try
                {
                    if(udpClient.Available > 0)
                    {
                        var recvBuffer = udpClient.Receive(ref from);
                        receivedString = Encoding.UTF8.GetString(recvBuffer);
                        _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = receivedString });
                    }
                }
                catch { };

                // IP found
                if (receivedString == "Connection verified")
                {
                    breweryIP = from.Address;
                    endSearch = true;
                    return;
                }

                // IP not found - search timed out
                if (endSearch)
                {
                    return;
                }
            }
        }

        private async Task timeoutHandler()
        {
            _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = $"Timeout handler started" });
            DateTime now = DateTime.Now;
            DateTime endTime = now + TimeSpan.FromSeconds(15);

            while (true)
            {
                now = DateTime.Now;
                if (now >= endTime)
                {
                    endSearch = true;
                    _events.PublishOnUIThread(new DebugDataUpdatedEvent { stringValue = $"Timeout handler ended" });
                    return;
                }
            }
        }

    }
}
