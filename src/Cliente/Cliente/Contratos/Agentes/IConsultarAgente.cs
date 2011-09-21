using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Agentes
{
    interface IConsultarAgente : IPaginaBase
    {
        object Agente { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        char GetEstadoCivil { get; }

        char GetSexo { get; }

        string SetSexo { set; }

        string SetEstadoCivil { set; }

        
    }
}
