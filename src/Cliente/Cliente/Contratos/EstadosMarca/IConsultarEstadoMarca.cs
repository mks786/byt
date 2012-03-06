using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.EstadosMarca
{
    interface IConsultarEstadoMarca : IPaginaBase
    {
        object EstadoMarca { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }        
    }
}
