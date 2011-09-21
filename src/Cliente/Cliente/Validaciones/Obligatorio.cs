using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Validaciones
{
    public class Obligatorio : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string valor = (string) value;

            if (string.IsNullOrEmpty(valor))
                return new ValidationResult(false, Recursos.MensajesValidaciones.Obligarotio);
            else
                return ValidationResult.ValidResult;
        }
    }
}
