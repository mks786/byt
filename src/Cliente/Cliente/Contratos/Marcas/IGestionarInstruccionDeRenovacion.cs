using System.Windows.Controls;
using System.Windows;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInstruccionDeRenovacion : IPaginaBase
    {

        object InstruccionDeRenovacion { get; set; }

        string IdCorrespondencia { get; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        void OcultarControlesAlAgregar();

        void BorrarValorMinimo();

        void Alerta();

    }
}
