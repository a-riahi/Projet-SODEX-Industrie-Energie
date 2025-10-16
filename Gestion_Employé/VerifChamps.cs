using System;
using System.Globalization;
using System.Windows.Data;

namespace EmployE
{
    public class VerifChamps : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null; // actif si un employé est sélectionné
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
