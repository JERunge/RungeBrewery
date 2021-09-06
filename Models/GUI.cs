using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace BrewUI.Models
{
    public static class GUI
    {
        public static void SetColorFromResource(Border border, string resourceName)
        {
            Brush r = Brushes.White;
            try
            {
                r = Application.Current.TryFindResource(resourceName) as Brush;
            }
            catch
            {
            }
            border.Background = r;
        }

        public static void ToggleContainerHeight(Border border)
        {
            if (border.Height == 45)
            {
                border.Height = Double.NaN;
            }
            else
            {
                border.Height = 45;
            }
        }

        public static void ToggleIcon(Border border, PackIcon packIcon)
        {
            if (border.Height >= 45)
            {
                packIcon.Kind = PackIconKind.ArrowBottomCircleOutline;
            }
            else
            {
                packIcon.Kind = PackIconKind.ArrowTopCircleOutline;
            }
        }

    }
}
