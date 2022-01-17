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
    /// Interaction logic for TextBoxWithUnit.xaml
    /// </summary>
    public partial class TextBoxWithUnit : UserControl
    {
        #region Dependency properties

        // TEXT TITLE DP

        public string BoxLabel
        {
            get { return (string)GetValue(BoxLabelProperty); }
            set { SetValue(BoxLabelProperty, value); }
        }

        public static readonly DependencyProperty BoxLabelProperty = DependencyProperty.Register("BoxLabel", typeof(string), typeof(TextBoxWithUnit), new PropertyMetadata(""));

        // TEXT TITLE DP

        public double BoxValue
        {
            get { return (double)GetValue(BoxValueProperty); }
            set { SetValue(BoxValueProperty, value); }
        }

        public static readonly DependencyProperty BoxValueProperty = DependencyProperty.Register("BoxValue", typeof(double), typeof(TextBoxWithUnit), new PropertyMetadata(default(double)));

        // BOX VALUE WIDT DP

        public double BoxValueWidth
        {
            get { return (double)GetValue(BoxValueWidthProperty); }
            set { SetValue(BoxValueWidthProperty, value); }
        }

        public static readonly DependencyProperty BoxValueWidthProperty = DependencyProperty.Register("BoxValueWidth", typeof(double), typeof(TextBoxWithUnit), new PropertyMetadata(default(double)));

        // TEXT TITLE DP

        public string BoxUnit
        {
            get { return (string)GetValue(BoxUnitProperty); }
            set { SetValue(BoxUnitProperty, value); }
        }

        public static readonly DependencyProperty BoxUnitProperty = DependencyProperty.Register("BoxValueProperty", typeof(string), typeof(TextBoxWithUnit), new PropertyMetadata(""));

        // TEXT TITLE DP

        public Visibility ShowUnit
        {
            get { return (Visibility)GetValue(ShowUnitProperty); }
            set { SetValue(ShowUnitProperty, value); }
        }

        public static readonly DependencyProperty ShowUnitProperty = DependencyProperty.Register("ShowUnitProperty", typeof(Visibility), typeof(TextBoxWithUnit), new PropertyMetadata(default(Visibility)));

        #endregion

        public TextBoxWithUnit()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
