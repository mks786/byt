using System.Windows.Controls;
using System.Windows;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInfoBol : IPaginaBase
    {
        object InfoBol { get; set; }

        object Boletin { get; set; }

        object Boletines { get; set; }

        object Tomo { get; set; }

        object Tomos { get; set; }

        object Tipo { get; set; }

        object Tipos { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        void OculatarControlesAlAgregar();

    }
}
