using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorListaFechasDeMarca : PresentadorBase
    {
        private IListaFechasDeMarca _ventana;
        private Marca _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ITipoFechaServicios _tipoFechaServicios;
        private IFechaMarcaServicios _fechaMarcaServicios;
        private FechaMarca _fechaMarcaSeleccionada = null;

        /// <summary>
        /// Constructor Predeterminado que recibe una ventana padre por parametro
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        /// <param name="marca">marca la cual se visualizan sus fechas</param>
        /// <param name="ventanaPadre">ventana que precede a esta ventana</param>
        public PresentadorListaFechasDeMarca(IListaFechasDeMarca ventana, object marca, object ventanaPadre)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._marca = (Marca)marca;
            
            this._tipoFechaServicios = (ITipoFechaServicios)Activator.GetObject(typeof(ITipoFechaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoFechaServicios"]);

            this._fechaMarcaServicios = (IFechaMarcaServicios)Activator.GetObject(typeof(IFechaMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FechaMarcaServicios"]);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Constructor Predeterminado que solo recibe la marca
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        /// <param name="marca">marca la cual se visualizan sus fechas</param>
        /// <param name="ventanaPadre">ventana que precede a esta ventana</param>
        public PresentadorListaFechasDeMarca(IListaFechasDeMarca ventana, object marca)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            //this._ventanaPadre = ventanaPadre;
            this._marca = (Marca)marca;

            this._tipoFechaServicios = (ITipoFechaServicios)Activator.GetObject(typeof(ITipoFechaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoFechaServicios"]);

            this._fechaMarcaServicios = (IFechaMarcaServicios)Activator.GetObject(typeof(IFechaMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FechaMarcaServicios"]);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }



        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            TipoInfobol aux = null;


            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaFechasMarca,
                    Recursos.Ids.FechaMarca);

                Marca marca = this._marca;

                IList<FechaMarca> listaFechas = this._fechaMarcaServicios.ConsultarFechasPorMarca(marca);

                this._ventana.Fechas = listaFechas;
                if (listaFechas != null)
                    this._ventana.TotalHits = listaFechas.Count.ToString();
                else
                    this._ventana.TotalHits = "0";

                #region Codigo Comentado
                //IList<TipoInfobol> tipoInfobol = this._tipoInfobolServicios.ConsultarTodos();

                //IList<InfoBol> listaInfobolesMarca = ((Marca)this._marca).InfoBoles;

                //foreach (InfoBol item in listaInfobolesMarca)
                //{

                //    TipoInfobol tipoInfobolMarca = item.TipoInfobol;
                //    aux = this.BuscarTipoInfobol(tipoInfobol, tipoInfobolMarca);
                //    if (aux != null)
                //    {
                //        //tipoInfobolMarca = null;
                //        //tipoInfobolMarca = aux;
                //        item.TipoInfobol = aux;
                //    }

                //}

                //((Marca)this._marca).InfoBoles = listaInfobolesMarca;


                //this._ventana.InfoBoles = ((Marca)this._marca).InfoBoles; 
                #endregion
                
                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Método que invoca una nueva página "GestionarFechaDeMarca" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrGestionarFechaDeMarca(bool nuevo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (!nuevo)
            {
                
                
                _fechaMarcaSeleccionada = (FechaMarca)this._ventana.FechaSeleccionada;

                //this.Navegar(new GestionarFechaDeMarca(this._ventana.FechaSeleccionada, this._ventana));
                this.Navegar(new GestionarFechaDeMarca(this._ventana.FechaSeleccionada, this._ventana, this._ventanaPadre));
                 
            }
            else
            {
                FechaMarca fechaMarca = new FechaMarca();
                fechaMarca.Marca = this._marca;
                fechaMarca.FechaRegistro = DateTime.Today;
                this.Navegar(new GestionarFechaDeMarca(fechaMarca,this._ventana));
                
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

    }
}
