﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using Diginsoft.Bolet.ObjetosComunes.Entidades;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Plantillas
{
    class PresentadorListaValoresDetalle : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IListaValoresDetalle _ventana;
        private IPlantillaServicios _plantillaServicios;
        private IFiltroPlantillaServicios _filtroPlantillaServicios;
        private Plantilla _plantilla;

        
        
        /// <summary>
        /// Constructor por defecto que recibe una ventana IGestionarMaestroPlantilla
        /// </summary>
        /// <param name="ventana">Ventana actual IGestionarMaestroPlantilla</param>
        /// <param name="plantilla">Plantilla Seleccionada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorListaValoresDetalle(IListaValoresDetalle ventana, object plantilla, object ventanaPadre)
        {

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._plantilla = (Plantilla)plantilla;

                
                this._plantillaServicios = (IPlantillaServicios)Activator.GetObject(typeof(IPlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PlantillaServicios"]);

                this._filtroPlantillaServicios = (IFiltroPlantillaServicios)Activator.GetObject(typeof(IFiltroPlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroPlantillaServicios"]);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }

        }


        public void CargarPagina()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                IList<FiltroPlantilla> listaFiltrosEncabezado = this._filtroPlantillaServicios.ObtenerFiltrosDetallePlantilla(this._plantilla);

                this._ventana.FiltrosEncabezado = listaFiltrosEncabezado;

                if (listaFiltrosEncabezado.Count > 0)
                    this._ventana.TotalHits = listaFiltrosEncabezado.Count.ToString();

                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListadoFiltrosDetallePlantilla,
                Recursos.Ids.ListaFiltrosPlantilla);
        }



        public void IrGestionarFiltroEncabezado(bool nuevo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (!nuevo)
            {
                FiltroPlantilla filtro = (FiltroPlantilla)this._ventana.FiltroEncabezado;
                this.Navegar(new GestionarFiltroDePlantilla(filtro, this._ventana, this._ventanaPadre));
            }
            else
            {

                FiltroPlantilla filtro = new FiltroPlantilla();
                filtro.Plantilla = this._plantilla;
                filtro.TipoDeFiltro = "D";
                this.Navegar(new GestionarFiltroDePlantilla(filtro, this._ventana, this._ventanaPadre));
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }




    }
}
