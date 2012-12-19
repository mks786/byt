using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Medios
{
    interface IConsultarMedio : IPaginaBase
    {
        object Medio { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }        
    }
}
