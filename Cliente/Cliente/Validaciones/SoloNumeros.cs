using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Trascend.Bolet.Cliente.Validaciones
{
    public class SoloNumeros : ValidationRule
    {
        private const string _expresionRegular = "\\d";

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string valor = (string) value;

            Regex regular = new Regex(_expresionRegular);

            if (!regular.IsMatch(valor) && !string.IsNullOrEmpty(valor))
            {
               return new ValidationResult(false, Recursos.MensajesValidaciones.SoloNumeros);
            }
            else
                return ValidationResult.ValidResult;
        }
    }
}
