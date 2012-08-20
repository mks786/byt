using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Anexos
{
    interface IConsultarAnexo : IPaginaBase
    {
        object Anexo { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }        
    }
}
