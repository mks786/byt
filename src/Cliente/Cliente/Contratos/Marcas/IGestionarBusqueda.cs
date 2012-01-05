using System.Windows.Controls;
using System.Windows;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarBusqueda : IPaginaBase
    {
        object InfoBol { get; set; }

        object Boletin { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        void OculatarControlesAlAgregar();

    }
}
