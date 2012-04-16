﻿
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.EscritosPatente
{
    interface IContestacionAOposicion : IPaginaBase
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


        #endregion

        #region Resolucion

        object Resoluciones { get; set; }

        object Resolucion { get; set; }

        void ActualizarResoluciones();

        #endregion

        string Fecha { get; }

        string Oponente { set; }

        string Domiciliado { set; }

        void MensajeAlerta(string mensaje);

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        string BotonModificar { get; set; }

        bool HabilitarCampos { set; }
    }
}