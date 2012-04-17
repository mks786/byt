
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.EscritosPatente
{
    interface IReconsideracionPerimidaVariante : IPaginaBase
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

        #region Patente

        object Patente { get; set; }

        string NombrePatente { set; }

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        object PatentesFiltrados { get; set; }

        object PatenteFiltrado { get; set; }

        #endregion

        #region Patentes Agregadas

        object PatentesAgregadas { get; set; }

        object PatenteAgregada { get; set; }

        #endregion

        #region Boletin

        object Boletines { get; set; }

        object Boletin { get; set; }

        object Boletines2 { get; set; }

        object Boletin2 { get; set; }


        #endregion

        #region Resolucion

        object Resoluciones { get; set; }

        object Resolucion { get; set; }

        object Resoluciones2 { get; set; }

        object Resolucion2 { get; set; }

        void ActualizarResoluciones();

        void ActualizarResoluciones2();

        #endregion

        object Modalidades { get; set; }

        object Modalidad { get; set; }

        string Fecha { get; }

        string FechaDeAviso { get; }

        void MensajeAlerta(string mensaje);

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        string BotonModificar { get; set; }

        bool HabilitarCampos { set; }
    }
}
