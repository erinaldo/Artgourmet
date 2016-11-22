using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Artebit.Restaurante.AtendimentoPDV.Classes
{
    [ValueConversion(typeof (bool), typeof (Visibility))]
    public class VisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            if ((bool) value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}