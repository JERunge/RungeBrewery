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

namespace BrewUI.Views
{
    /// <summary>
    /// Interaction logic for SessionView.xaml
    /// </summary>
    public partial class SessionView : UserControl
    {
        public SessionView()
        {
            InitializeComponent();
        }

        #region Toggle container click

        private void MouseDown_Mash(object sender, MouseButtonEventArgs e)
        {
            GUI.ToggleContainerHeight(MashContainer);
            GUI.ToggleIcon(MashContainer, MashIcon);
        }

        private void MouseDown_Sparge(object sender, MouseButtonEventArgs e)
        {
            GUI.ToggleContainerHeight(SpargeContainer);
            GUI.ToggleIcon(SpargeContainer, SpargeIcon);
        }

        private void MouseDown_Boil(object sender, MouseButtonEventArgs e)
        {
            GUI.ToggleContainerHeight(BoilContainer);
            GUI.ToggleIcon(BoilContainer, BoilIcon);
        }

        private void MouseDown_Cooldown(object sender, MouseButtonEventArgs e)
        {
            GUI.ToggleContainerHeight(CooldownContainer);
            GUI.ToggleIcon(CooldownContainer, CooldownIcon);
        }

        #endregion

        #region Color hover
        private void MouseEnter_Mash(object sender, MouseEventArgs e)
        {
            GUI.SetColorFromResource(MashContainer, "GrayHover");
        }

        private void MouseLeave_Mash(object sender, MouseEventArgs e)
        {
            MashContainer.Background = Brushes.White;
        }

        private void MouseEnter_Sparge(object sender, MouseEventArgs e)
        {
            GUI.SetColorFromResource(SpargeContainer, "GrayHover");
        }

        private void MouseLeave_Sparge(object sender, MouseEventArgs e)
        {
            SpargeContainer.Background = Brushes.White;
        }

        private void MouseEnter_Boil(object sender, MouseEventArgs e)
        {
            GUI.SetColorFromResource(BoilContainer, "GrayHover");
        }

        private void MouseLeave_Boil(object sender, MouseEventArgs e)
        {
            BoilContainer.Background = Brushes.White;
        }

        private void MouseEnter_Cooldown(object sender, MouseEventArgs e)
        {
            GUI.SetColorFromResource(CooldownContainer, "GrayHover");
        }

        private void MouseLeave_Cooldown(object sender, MouseEventArgs e)
        {
            CooldownContainer.Background = Brushes.White;
        }

        #endregion
    }
}
