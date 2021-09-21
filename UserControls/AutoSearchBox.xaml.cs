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

namespace BrewUI.UserControls
{
    /// <summary>
    /// Interaction logic for AutoSearchBox.xaml
    /// </summary>
    public partial class AutoSearchBox : UserControl
    {
        #region Dependency Properties
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register("SearchText", typeof(string), typeof(AutoSearchBox), new PropertyMetadata(""));

        public List<string> SearchList
        {
            get { return (List<string>)GetValue(SearchListProperty); }
            set { SetValue(SearchListProperty, value); }
        }

        public static readonly DependencyProperty SearchListProperty = DependencyProperty.Register("SearchList", typeof(List<string>), typeof(AutoSearchBox), new PropertyMetadata(""));

        #endregion

        public AutoSearchBox()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }

        private void SearchTextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void SearchTextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        public void SearchTextChanged()
        {
            SearchList.Add(SearchText);
        }
    }
}
