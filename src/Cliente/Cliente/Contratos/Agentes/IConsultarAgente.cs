using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Agentes
{
    interface IConsultarAgente : IPaginaBase
    {
        object Agente { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object EstadosCivil { get; set; }

        object EstadoCivil { get; set; }

        object Sexo { get; set; }

        object Sexos { get; set; }

        
    }
}
