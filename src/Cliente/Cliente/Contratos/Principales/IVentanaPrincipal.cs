using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Principales
{
    interface IVentanaPrincipal
    {
        Frame Contenedor { get; }

        Menu Menu { get; }
    }
}
