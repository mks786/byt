
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.Justificaciones
{
    interface IConsultarJustificacion : IPaginaBase
    {
        object Justificacion { get; set; }

        object Concepto { get; set; }

        object Conceptos { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        bool EstaCargada { get; set; }
    }
}
