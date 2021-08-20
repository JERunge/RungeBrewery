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
using System.Windows.Shapes;

namespace BrewUI.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        private TimeSpan timeSinceClick;
        private DateTime firstClickTime;
        private bool clicked = false;
        private bool maximized = false;

        public ShellView()
        {
            InitializeComponent();
        }

        private void HeaderGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();

            if (!clicked)
            {
                clicked = !clicked;
                firstClickTime = DateTime.Now;
            }
            else
            {
                timeSinceClick = DateTime.Now - firstClickTime;
                if(timeSinceClick > TimeSpan.FromMilliseconds(300))
                {
                    clicked = !clicked;
                    firstClickTime = DateTime.Now;
                }
                else
                {
                    if (maximized)
                    {
                        this.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        this.WindowState = WindowState.Maximized;
                    }
                    clicked = !clicked;
                    maximized = !maximized;
                }
            }
            
        }
    }
}
