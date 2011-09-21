using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Principales
{
    interface IPaginaPrincipal : IPaginaBase
    {
        string MensajeError { get; set; }

        string MensajeUsuario { get; set; }
    }
}
