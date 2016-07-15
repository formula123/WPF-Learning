using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Globalization;

namespace ContactManager.Presenters
{
    public class PhoneConverter : IValueConverter
    {
        public object ConvertBack(object value, Type TargetType, object parmeter, CultureInfo culture)
        {
            return filterNonmeric(value as string);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = value as string;
            
            if (!string.IsNullOrEmpty(result))
            {
                string  filterResult = filterNonmeric(result);


                long theNumber = System.Convert.ToInt64(filterResult);

                switch (filterResult.Length)
                {
                    case 11:
                        result = string.Format("{0:+# (###) ###-####}", theNumber);
                        break;
                    case 10:
                        result = string.Format("{0:(###) ###-####}", theNumber);
                        break;
                    case 7:
                        result = string.Format("{0:###-####}", theNumber);
                        break;
                }
            }
            return result;
        }

        private string filterNonmeric(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }
            string filterResult = string.Empty;

            foreach (char c in result)
            {
                if (Char.IsDigit(c))
                {
                    filterResult += c;
                }
            }
            return filterResult;
        }
    }
}
