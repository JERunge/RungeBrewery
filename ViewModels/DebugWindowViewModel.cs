using BrewUI.Data;
using BrewUI.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrewUI.ViewModels
{
    public class DebugWindowViewModel : Screen, IHandle<DebugDataUpdatedEvent>, IHandle<SerialReceivedEvent>, IHandle<SerialToSendEvent>
    {
        private IEventAggregator _events;

        private string _currentProcess;
        public string CurrentProcess
        {
            get { return _currentProcess; }
            set
            { 
                _currentProcess = value;
                NotifyOfPropertyChange(() => CurrentProcess);
            }
        }

        private bool _sessionRunning;
        public bool SessionRunning
        {
            get { return _sessionRunning; }
            set 
            { 
                _sessionRunning = value;
                NotifyOfPropertyChange(() => SessionRunning);
            }
        }

        private string _currentStep;
        public string CurrentStep
        {
            get { return _currentStep; }
            set
            {
                _currentStep = value;
                NotifyOfPropertyChange(() => CurrentStep);
            }
        }

        private bool _mashRunning;
        public bool MashRunning
        {
            get { return _mashRunning; }
            set
            {
                _mashRunning = value;
                NotifyOfPropertyChange(() => MashRunning);
            }
        }

        private bool _spargeRunning;
        public bool SpargeRunning
        {
            get { return _spargeRunning; }
            set
            { 
                _spargeRunning = value;
                NotifyOfPropertyChange(() => SpargeRunning);
            }
        }

        private bool _boilRunning;
        public bool BoilRunning
        {
            get { return _boilRunning; }
            set
            { 
                _boilRunning = value;
                NotifyOfPropertyChange(() => BoilRunning);
            }
        }

        private string _receiveText;
        public string ReceiveText
        {
            get { return _receiveText; }
            set 
            {
                _receiveText = value;
                NotifyOfPropertyChange(() => ReceiveText);
            }
        }

        private string _receivedData;
        public string ReceivedData
        {
            get { return _receivedData; }
            set 
            {
                _receivedData = value;
                NotifyOfPropertyChange(() => ReceivedData);
            }
        }

        private string _sentData;
        public string SentData
        {
            get { return _sentData; }
            set
            { 
                _sentData = value;
                NotifyOfPropertyChange(() => SentData);
            }
        }

        private string _debugText;
        public string DebugText
        {
            get { return _debugText; }
            set 
            { 
                _debugText = value;
                NotifyOfPropertyChange(() => DebugText);
            }
        }

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set
            { 
                _connected = value;
                NotifyOfPropertyChange(() => Connected);
            }
        }


        public DebugWindowViewModel(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);
        }

        public void ReceiveCommand()
        {
            ArduinoMessage AM = new ArduinoMessage();

            AM.AIndex = ReceiveText[0];
            AM.AMessage = ReceiveText.Substring(1, ReceiveText.Length - 1);
            _events.PublishOnUIThread(new SerialReceivedEvent { arduinoMessage = AM });
        }

        #region Event handlers
        public void Handle(DebugDataUpdatedEvent message)
        {
            switch (message.index)
            {
                case "SR":
                    SessionRunning = message.boolValue;
                    break;
                case "CP":
                    CurrentProcess = message.stringValue;
                    break;
                case "CS":
                    CurrentStep = message.stringValue;
                    break;
                case "MR":
                    MashRunning = message.boolValue;
                    break;
                case "SPR":
                    SpargeRunning = message.boolValue;
                    break;
                case "BR":
                    BoilRunning = message.boolValue;
                    break;
            }

            // Fill in debug log
            string msg = "";
            try
            {
                msg = message.stringValue;
            }
            catch (NullReferenceException e)
            {
            }
            if(msg == null)
            {
                msg = message.boolValue.ToString();
            }
            string timeStamp = DateTime.Now.ToString("hh\\:mm\\:ss");
            DebugText += timeStamp + ": " + message.index + " " + msg + "\n";
        }

        public void Handle(SerialReceivedEvent message)
        {
            char index = message.arduinoMessage.AIndex;
            string value = message.arduinoMessage.AMessage;
            string timeStamp = DateTime.Now.ToString("hh\\:mm\\:ss");
            ReceivedData += timeStamp + ": " + index + " " + value + "\n";
        }

        public void Handle(SerialToSendEvent message)
        {
            char index = message.arduinoMessage.AIndex;
            string value = message.arduinoMessage.AMessage;
            string timeStamp = DateTime.Now.ToString("hh\\:mm\\:ss");

            SentData += timeStamp + ": " + index + " " + value + "\n";
        }
        #endregion
    }
}
