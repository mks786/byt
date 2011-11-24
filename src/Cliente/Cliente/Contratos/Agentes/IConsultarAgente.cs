using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Agentes
{
    interface IConsultarAgente : IPaginaBase
    {
        object Agente { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        char GetEstadoCivil { get; }

        object Sexo { get; set; }

        object Sexos { get; set; }

        string SetEstadoCivil { set; }

        
    }
}
