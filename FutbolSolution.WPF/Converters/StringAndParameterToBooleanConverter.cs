using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FutbolSolution.WPF.Converters
{
    public class StringAndParameterToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string strValue && values[1] is string strParameter)
            {
                return strValue == strParameter;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return new object[] { Binding.DoNothing, parameter as string };
            }
            return new object[] { Binding.DoNothing, null };
        }
    }
}
