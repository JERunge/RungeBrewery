﻿using System;
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
using BrewUI.Models;
using Caliburn.Micro;

namespace BrewUI.Views
{
    /// <summary>
    /// Interaction logic for SessionSettingsView.xaml
    /// </summary>
    public partial class SessionSettingsView : UserControl
    {

        // GUI variables

        public SessionSettingsView()
        {
            InitializeComponent();
        }

        private void GrainAutoText_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GrainPopup.IsOpen = true;
            GrainAutoText.SelectAll();
        }

        private void GrainAutoText_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            GrainPopup.IsOpen = false;
        }
    }
}
