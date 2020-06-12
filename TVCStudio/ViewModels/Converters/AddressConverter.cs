using System;
using System.Globalization;
using System.Windows.Data;
using Z80.Kernel.Z80Assembler;

namespace TVCStudio.ViewModels.Converters
{
    public class AddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ((ushort)value).UshortToHexa();
            }

            return "$0000";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string sValue = value.ToString();
                try
                {
                    return sValue.ResolveUshortConstant();
                }
                catch (Z80AssemblerException)
                {
                    return 0;
                }
            }

            return 0;
        }
    }
}
