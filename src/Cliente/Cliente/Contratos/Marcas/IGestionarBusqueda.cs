using System.Windows.Controls;
using System.Windows;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarBusqueda : IPaginaBase
    {
        object Busqueda { get; set; }

        object TiposBusqueda { get; set; }

        object TipoBusqueda { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        void OcultarControlesAlAgregar();

    }
}
