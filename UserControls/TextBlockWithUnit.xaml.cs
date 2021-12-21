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
    /// Interaction logic for TextBlockWithUnit.xaml
    /// </summary>
    public partial class TextBlockWithUnit : UserControl
    {
        #region Dependency properties

        // TEXT TITLE DP

        public string BlockLabel
        {
            get { return (string)GetValue(BlockLabelProperty); }
            set { SetValue(BlockLabelProperty, value); }
        }

        public static readonly DependencyProperty BlockLabelProperty = DependencyProperty.Register("BlockLabel", typeof(string), typeof(TextBlockWithUnit), new PropertyMetadata(""));

        // TEXT TITLE DP

        public double BlockValue
        {
            get { return (double)GetValue(BlockValueProperty); }
            set { SetValue(BlockValueProperty, value); }
        }

        public static readonly DependencyProperty BlockValueProperty = DependencyProperty.Register("BlockValue", typeof(double), typeof(TextBlockWithUnit), new PropertyMetadata(default(double)));

        // TEXT TITLE DP

        public string BlockUnit
        {
            get { return (string)GetValue(BlockUnitProperty); }
            set { SetValue(BlockUnitProperty, value); }
        }

        public static readonly DependencyProperty BlockUnitProperty = DependencyProperty.Register("BlockUnit", typeof(string), typeof(TextBlockWithUnit), new PropertyMetadata(""));

        #endregion

        public TextBlockWithUnit()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
