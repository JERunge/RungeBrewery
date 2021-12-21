using BrewUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BrewUI.UserControls
{
    /// <summary>
    /// Interaction logic for StepConfirmationWindow.xaml
    /// </summary>
    public partial class StepConfirmationWindow : UserControl
    {
        #region DP
        // ITEMS LIST DP
        public List<StepConfirmation> ItemsList
        {
            get { return (List<StepConfirmation>)GetValue(ItemsListProperty); }
            set { SetValue(ItemsListProperty, value); }
        }
        public static readonly DependencyProperty ItemsListProperty = DependencyProperty.Register("ItemsList", typeof(List<StepConfirmation>), typeof(StepConfirmationWindow), new PropertyMetadata(new List<StepConfirmation>()));

        #endregion

        #region Variables

        private DispatcherTimer checkedTimer;
        public bool Finished { get; set; }

        #endregion

        public StepConfirmationWindow()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;

            // Initialize timer
            checkedTimer = new DispatcherTimer();
            checkedTimer.Interval = TimeSpan.FromMilliseconds(100);
            checkedTimer.Tick += CheckedTimer_Tick;
            checkedTimer.Start();

            Finished = false;
            Done.IsEnabled = false;
        }

        private void CheckedTimer_Tick(object sender, EventArgs e)
        {
            int numberOfItems = ItemsList.Count;
            int numberOfCheckedItems = 0;

            foreach(StepConfirmation sc in ItemsList)
            {
                if (sc.Checked)
                {
                    numberOfCheckedItems++;
                }
            }

            if(numberOfCheckedItems == numberOfItems && !Done.IsEnabled)
            {
                Done.IsEnabled = true;
            }
            else if(numberOfCheckedItems < numberOfItems && Done.IsEnabled)
            {
                Done.IsEnabled = false;
            }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            Finished = true;
            Window.GetWindow(this).Close();
        }
    }
}
