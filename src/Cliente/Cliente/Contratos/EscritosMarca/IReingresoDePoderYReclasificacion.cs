
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.EscritosMarca
{
    interface IReingresoDePoderYReclasificacion : IPaginaBase
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

        #region Marcas Agregadas

        object MarcasAgregadas { get; set; }

        object MarcaAgregada { get; set; }

        #endregion

        #region Boletin y Numerales

        object Boletines { get; set; }

        object Boletin { get; set; }

        object CantidadNumeralSelec { get; set; }

        object CantidadNumerales { get; set; }

        #endregion

        #region Poder Etiqueta y Reclasificacion

        object TipoDePoderes { get; set; }

        object TipoDePoder { get; set; }

        object TipoDeEtiquetas { get; set; }

        object TipoDeEtiqueta { get; set; }

        object Reclasificaciones { get; set; }

        object Reclasificacion { get; set; }

        #endregion

        void MensajeAlerta(string mensaje);

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        string BotonModificar { get; set; }

        string Numerales { get; set; }

        bool HabilitarCampos { set; }
    }
}
