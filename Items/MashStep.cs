using Caliburn.Micro;
using System;
using System.Windows.Threading;

namespace BrewUI.Items
{
    public class MashStep : Conductor<object>
    {
        public string stepName { get; set; }
        public int index { get; set; }
        public double stepTemp { get; set; }
        public int progressShare { get; set; }

        public TimeSpan stepDuration { get; set; }

        public DispatcherTimer stepTimer { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public TimeSpan timeLeft { get; set; }

        private string _timerText;
        public string TimerText
        {
            get { return _timerText; }
            set
            {
                _timerText = value;
                NotifyOfPropertyChange(() => TimerText);
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        public MashStep()
        {
            stepTimer = new DispatcherTimer();
            stepTimer.Interval = TimeSpan.FromSeconds(1);
            stepTimer.Tick += StepTimer_Tick;
        }

        private void StepTimer_Tick(object sender, EventArgs e)
        {
            TimerText = endTime.Subtract(DateTime.Now).ToString("hh\\:mm\\:ss");
        }
    }
}
