
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.EscritosMarca
{
    interface IConsignacionDeBusqueda : IPaginaBase
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

        #region Marca

        object Marca { get; set; }

        string NombreMarca { set; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltrados { get; set; }

        object MarcaFiltrado { get; set; }

        #endregion

        #region Marcas Busqueda

        object MarcasBusqueda { get; set; }

        object MarcaBusquedaSeleccionada { get; set; }

        #endregion

        void MensajeAlerta(string mensaje);

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        string BotonModificar { get; set; }

        CheckBox ChkMixto { get; set; }

        CheckBox ChkPalabra { get; set; }

        CheckBox ChkDiseño { get; set; }

        void AsignarCheckbox();

        bool HabilitarCampos { set; }
    }
}
