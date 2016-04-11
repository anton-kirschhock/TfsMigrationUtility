using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TfsMigrationUtility.UI.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a bool to visibility enum (Visible = True, Hidden = else)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;            
        }
        /// <summary>
        /// Converts Visibility enum to a Boolean (true = Visible, Else = false)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible) return true;
            return false;
        }
    }
}
