using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Trascend.Bolet.Cliente.Transformadores
{
    class FechaABlanco : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string fecha = value.ToString();

            return fecha.Equals("01/01/0001 12:00:00 a.m.") ? "" : ((DateTime)value).ToString("dd/MM/yyyy"); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
