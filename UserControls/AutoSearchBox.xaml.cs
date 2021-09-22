using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // SEARCH LIST DP

        public List<string> SearchList
        {
            get { return (List<string>)GetValue(SearchListProperty); }
            set { SetValue(SearchListProperty, value); }
        }

        public static readonly DependencyProperty SearchListProperty = DependencyProperty.Register("SearchList", typeof(List<string>), typeof(AutoSearchBox), new PropertyMetadata(default(List<string>)));

        // SELECTED ITEM DP

        public string SelectedItem 
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set{ SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(string), typeof(AutoSearchBox), new PropertyMetadata(""));

        #endregion

        private ObservableCollection<string> _resultList;
        public ObservableCollection<string> ResultList
        {
            get { return _resultList; }
            set { _resultList = value; }
        }

        private bool updatingResultList;

        public AutoSearchBox()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
            SearchList = new List<string>();
            ResultList = new ObservableCollection<string>();
            foreach(string item in SearchList)
            {
                ResultList.Add(item);
            }
        }

        private void SearchTextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SearchPopup.IsOpen = true;
            SearchTextBox.SelectAll();
        }

        private void SearchTextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SearchPopup.IsOpen = false;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!updatingResultList)
            {
                SearchTextBox.Text = SelectedItem;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == null)
            {
                return;
            }

            updatingResultList = true;

            ResultList.Clear();

            if (SearchTextBox.Text.Length > 0)
            {
                foreach (string item in SearchList)
                {
                    if (item.ToUpper().Contains(SearchTextBox.Text.ToUpper()))
                    {
                        ResultList.Add(item);
                    }
                }
            }
            else
            {
                foreach (string item in SearchList)
                {
                    ResultList.Add(item);
                }
            }

            updatingResultList = false;
        }
    }
}
