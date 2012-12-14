using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.TiposEmailAsociado
{
    interface IGestionarTipoEmailAsociado : IPaginaBase
    {
        object TipoEmailAsociado { get; set; }

        bool HabilitarCampos { set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        string TextoBotonModificar { get; set; }
    }
}
