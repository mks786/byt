using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Trascend.Bolet.Cliente.Validaciones
{
    public class CorreoElectronico : ValidationRule
    {
        private const string _expresionRegular = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string valor = (string) value;

            Regex regular = new Regex(_expresionRegular);

            if (!regular.IsMatch(valor) && !string.IsNullOrEmpty(valor))
            {
               return new ValidationResult(false, Recursos.MensajesValidaciones.CorreoElectronico);
            }
            else
                return ValidationResult.ValidResult;
        }
    }
}
