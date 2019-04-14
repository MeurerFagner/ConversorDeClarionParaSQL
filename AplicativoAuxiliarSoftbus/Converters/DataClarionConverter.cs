using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AplicativoAuxiliarSoftbus.Converters
{
    public class DataClarionConverter : IValueConverter
    {
        private DateTime _baseData = new DateTime(1800, 12, 28);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new DateTime(1, 1, 1);
            var dias = Double.Parse(value.ToString());
            return _baseData.AddDays(dias);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as DateTime?;
            if (data == null || data.Value <= _baseData) return "0";
            return (data.Value.Subtract(_baseData).Ticks / TimeSpan.TicksPerDay);
        }
    }
}
