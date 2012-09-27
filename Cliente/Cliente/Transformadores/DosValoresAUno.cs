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
                object retorno = values[0].GetType();
                
                DateTime fechaSalida;
                string nombre = retorno.ToString();

                if (null == values[1])
                    values[1] = "";
                else if (null == values[0])
                    values[0] = "";

                if (!(values[0].Equals("")) && !(values[1].Equals("")))
                {
                    if (nombre.Equals("System.String"))
                    {
                        if (DateTime.TryParse(values[1].ToString(), out fechaSalida))
                            //return values[0].ToString() + " - " + ((DateTime)values[1]).ToString("dd/MM/yyyy");
                            return values[0].ToString() + " - " + fechaSalida.ToString("dd/MM/yyyy");
                        else
                            return values[0].ToString() + " - " + values[1].ToString();
                    }
                    else
                    {
                        int numero = int.Parse(values[0].ToString());

                        if (!numero.Equals(int.MinValue))
                            if (DateTime.TryParse(values[1].ToString(), out fechaSalida))
                                //return values[0].ToString() + " - " + ((DateTime)values[1]).ToString("dd/MM/yyyy");
                                return values[0].ToString() + " - " + fechaSalida.ToString("dd/MM/yyyy");
                            else
                                return values[0].ToString() + " - " + values[1].ToString();
                    }
                }
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
