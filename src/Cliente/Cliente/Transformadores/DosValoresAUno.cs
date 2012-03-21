using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Trascend.Bolet.Cliente.Transformadores
{
    class DosValresAUno : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int numero = int.Parse(values[0].ToString());

                if (!numero.Equals(int.MinValue))
                    return values[0].ToString() + " - " + ((DateTime)values[1]).ToString("dd/MM/yyyy");
                return "";
            }
            catch (FormatException ex) { return ""; }
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
