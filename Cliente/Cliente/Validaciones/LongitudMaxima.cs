using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Validaciones
{
    public class LongitudMaxima : ValidationRule
    {
        private int _longitud = 0;

        public int Longitud
        {
            get { return _longitud; }
            set { this._longitud = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string valor = (string) value;

            if(!string.IsNullOrEmpty(valor) && valor.Length > this.Longitud)
                return new ValidationResult(false, string.Format(Recursos.MensajesValidaciones.LongitudMaxima,this.Longitud));
            else
                return ValidationResult.ValidResult;
        }
    }
}
