using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Trascend.Bolet.Cliente.Transformadores
{
    class EnteroMinimoABlanco : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int numero =  int.Parse(value.ToString());

            return numero.Equals(int.MinValue) ? "" : numero.ToString(); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
