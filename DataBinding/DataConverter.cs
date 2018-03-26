using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataBinding
{
    public class DataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double target = 0;
            if(double.TryParse(value.ToString(), out target))
            {
                return (int)target;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int source = 0;
            if(int.TryParse(value.ToString(), out source))
            {
                source = source < 0 ? 0 : source;
                source = source > 100 ? 100 : source;
                return source;
            }
            return 0;
        }
    }
}
