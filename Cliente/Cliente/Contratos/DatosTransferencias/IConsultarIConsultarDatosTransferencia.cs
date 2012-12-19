
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.DatosTransferencias
{
    interface IConsultarDatosTransferencia : IPaginaBase
    {
        object DatosTransferencia { get; set; }
        
        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
