using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AplicativoAuxiliarSoftbus.Converters
{
    public class HoraCalrionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new DateTime(1, 1, 1);
            Double mileseconds = (Double.Parse(value.ToString()) - 1) * 10;
            return new DateTime(1, 1, 1).AddMilliseconds(mileseconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hora = value as DateTime?;
            if (hora == null) return "0";
            var centesimos = new DateTime(1, 1, 1, hora.Value.Hour, hora.Value.Minute, hora.Value.Second).Ticks / TimeSpan.TicksPerMillisecond / 10 + 1;
            return centesimos.ToString();

        }
    }
}
