
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.EscritosMarca
{
    interface INumeracionDePoderPorInteresado : IPaginaBase
    {
        object Escrito { get; set; }

        #region Agente

        object Agente { get; set; }

        string NombreAgente { set; }

        string IdAgenteFiltrar { get; }

        string NombreAgenteFiltrar { get; }

        object AgentesFiltrados { get; set; }

        object AgenteFiltrado { get; set; }

        #endregion

        #region Interesado

        object Interesado { get; set; }

        string NombreInteresado { set; }

        string IdInteresadoFiltrar { get; }

        string NombreInteresadoFiltrar { get; }

        object InteresadosFiltrados { get; set; }

        object InteresadoFiltrado { get; set; }

        #endregion

        #region Interesados Agregados

        object InteresadosAgregados { get; set; }

        object InteresadoAgregado { get; set; }

        #endregion

        void MensajeAlerta(string mensaje);

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        string BotonModificar { get; set; }

        bool HabilitarCampos { set; }
    }
}
