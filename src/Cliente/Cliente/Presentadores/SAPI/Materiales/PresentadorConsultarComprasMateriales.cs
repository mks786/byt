using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales
{
    class PresentadorConsultarComprasMateriales : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarComprasMateriales _ventana;
        private ICompraSapiServicios _compraSapiServicios;
        private ICompraSapiDetalleServicios _compraSapiDetalleServicios;
        private IMaterialSapiServicios _materialSapiServicios;
        private int _filtroValido;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorConsultarComprasMateriales(IConsultarComprasMateriales ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                
                this._compraSapiServicios = (ICompraSapiServicios)Activator.GetObject(typeof(ICompraSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CompraSapiServicios"]);
                this._compraSapiDetalleServicios = (ICompraSapiDetalleServicios)Activator.GetObject(typeof(ICompraSapiDetalleServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CompraSapiDetalleServicios"]);
                this._materialSapiServicios = (IMaterialSapiServicios)Activator.GetObject(typeof(IMaterialSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaterialSapiServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarComprasSapi,
                Recursos.Ids.ConsultarComprasSAPI);
        }


        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {


            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                LlenarComboMateriales();

                this._ventana.TotalHits = "0";

                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }

        }

        private void LlenarComboMateriales()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<MaterialSapi> materiales = this._materialSapiServicios.ConsultarTodos();
                IList<MaterialSapi> materialesOrdenados = materiales.OrderBy(o => o.Descripcion).ToList();
                //materiales.Insert(0, new MaterialSapi("NGN"));
                materialesOrdenados.Insert(0, new MaterialSapi("NGN"));
                //this._ventana.MaterialesSapi = materiales;
                this._ventana.MaterialesSapi = materialesOrdenados;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }

        public void Consultar()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._filtroValido = 0;

                CompraSapiDetalle compraSapiDetalleAux = new CompraSapiDetalle();
                compraSapiDetalleAux = ObtenerCompraFiltroPantalla();

                if (this._filtroValido >= 2)
                {
                    IList<CompraSapiDetalle> compraDetalles = this._compraSapiDetalleServicios.ObtenerCompraSapiDetalleFiltro(compraSapiDetalleAux);
                    if (compraDetalles.Count > 0)
                    {
                        this._ventana.Resultados = compraDetalles;
                        this._ventana.TotalHits = compraDetalles.Count.ToString();
                    }
                    else
                    {
                        this._ventana.TotalHits = "0";
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 0);
                    }
                }
                else
                    this._ventana.Mensaje("Seleccione al menos un filtro para relizar la consulta", 0);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        private CompraSapiDetalle ObtenerCompraFiltroPantalla()
        {
            CompraSapiDetalle compraDetalle = new CompraSapiDetalle();
            CompraSapi compra = new CompraSapi();
            compraDetalle.Compra = compra;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._ventana.IdCompraSapi.Equals(""))
                {
                    compraDetalle.Compra.Id = int.Parse(this._ventana.IdCompraSapi);
                    this._filtroValido = 2;
                }
                else
                    compraDetalle.Compra.Id = int.MinValue;

                if (!this._ventana.FechaCompraSapi.Equals(""))
                {
                    DateTime fechaCompra = DateTime.Parse(this._ventana.FechaCompraSapi);
                    this._filtroValido = 2;
                    compraDetalle.Compra.FechaCompra = fechaCompra;
                }
                else
                    compraDetalle.Compra.FechaCompra = null;

                if ((this._ventana.MaterialSapi != null) && (!((MaterialSapi)this._ventana.MaterialSapi).Id.Equals("NGN")))
                {
                    MaterialSapi material = new MaterialSapi();
                    material.Id = ((MaterialSapi)this._ventana.MaterialSapi).Id;
                    compraDetalle.Material = material;
                    this._filtroValido = 2;
                }
                else
                    compraDetalle.Material = null;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return compraDetalle;
        }

        public void LimpiarCampos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ConsultarComprasMateriales());

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que visualiza una compra sapi
        /// </summary>
        public void VerCompraSAPI()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.CompraSapiSeleccionada != null)
                {
                    CompraSapi compra = ((CompraSapiDetalle)this._ventana.CompraSapiSeleccionada).Compra;
                    this.Navegar(new GestionarCompraMaterialesSapi(compra, this._ventana));
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String field = column.Tag as String;

                if (this._ventana.CurSortCol != null)
                {
                    AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                    this._ventana.ListaResultados.Items.SortDescriptions.Clear();
                }

                ListSortDirection newDir = ListSortDirection.Ascending;
                if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                    newDir = ListSortDirection.Descending;

                this._ventana.CurSortCol = column;
                this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
                this._ventana.ListaResultados.Items.SortDescriptions.Add(
                    new SortDescription(field, newDir));

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }

    }
}
