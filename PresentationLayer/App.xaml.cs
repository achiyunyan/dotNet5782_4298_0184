using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public class NullToCollapsedConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullToVisibleConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BatteryPercentToColor : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((double)value)
            {
                case double n when (n < 12.5):
                    return Brushes.DarkRed;
                case double n when (n < 25):
                    return Brushes.Red;
                case double n when (n < 37.5):
                    return Brushes.DarkOrange;
                case double n when (n < 50):
                    return Brushes.Orange;
                case double n when (n < 62.5):
                    return Brushes.Gold;
                case double n when (n < 75):
                    return Brushes.Yellow;
                case double n when (n < 87.5):
                    return Brushes.LawnGreen;
                default:
                    return Brushes.Lime;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class App : Application
    {
    }
}
