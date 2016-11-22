using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Artebit.Restaurante.AtendimentoProducao
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((int)value == 3)
            {
                return "Img/ok.png";
            }
            else
            {
                return "Img/vermelho.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((int)value == 3)
            {
                return "Img/ok.png";
            }
            else
            {
                return "Img/vermelho.png";
            }
        }
    }
}
