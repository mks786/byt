using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Estatuses
{
    interface IConsultarEstatus : IPaginaBase
    {
        object Estatus { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }        
    }
}
