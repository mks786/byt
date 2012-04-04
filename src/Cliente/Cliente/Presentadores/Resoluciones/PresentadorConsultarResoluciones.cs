using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Resoluciones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Resoluciones;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Resoluciones
{
    class PresentadorConsultarResoluciones: PresentadorBase
    {
        private IConsultarResoluciones _ventana;
        private IBoletinServicios _boletinServicios;
        private IResolucionServicios _resolucionServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private IList<Resolucion> _resoluciones;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarResoluciones(IConsultarResoluciones ventana)
        {
            try
            {
                this._ventana = ventana;
                this._resolucionServicios = (IResolucionServicios)Activator.GetObject(typeof(IResolucionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ResolucionServicios"]);

                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarResoluciones,
                Recursos.Ids.ConsultarResolucion);
        }

        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                this._resoluciones = this._resolucionServicios.ConsultarTodos();
                this._ventana.Resultados = this._resoluciones;
                this._ventana.TotalHits = this._resoluciones.Count.ToString();

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.Boletines = boletines;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
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
        /// Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        /// por pantalla
        /// </summary>
        public void Consultar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IEnumerable<Resolucion> resolucionesFiltrados = this._resoluciones;

                if (!string.IsNullOrEmpty(this._ventana.Id))
                {
                    resolucionesFiltrados = from resolucion in resolucionesFiltrados
                                         where resolucion.Id.ToLower().Contains(this._ventana.Id.ToLower())
                                         select resolucion;
                }

                if (!string.IsNullOrEmpty(this._ventana.FechaResolucion))
                {
                    DateTime fechaResolucionesAux = DateTime.Parse(this._ventana.FechaResolucion);
                    resolucionesFiltrados = from resolucion in resolucionesFiltrados
                                         where resolucion.FechaResolucion.Equals(fechaResolucionesAux)
                                         select resolucion;
                }

                if (this._ventana.Boletin != null && !((Boletin)this._ventana.Boletin).Id.Equals(int.MinValue))
                {
                    Boletin boletin = (Boletin) this._ventana.Boletin;
                    resolucionesFiltrados = from resolucion in resolucionesFiltrados
                                            where resolucion.Boletin.Id.ToString().ToLower().Contains(boletin.Id.ToString().ToLower())
                                            select resolucion;
                }

                if (!string.IsNullOrEmpty(this._ventana.Volumen))
                {
                    resolucionesFiltrados = from resolucion in resolucionesFiltrados
                                            where resolucion.Volumen != null &&
                                            resolucion.Volumen.ToLower().Contains(this._ventana.Volumen.ToLower())
                                            select resolucion;
                }

                if (!string.IsNullOrEmpty(this._ventana.Pagina))
                {
                    resolucionesFiltrados = from resolucion in resolucionesFiltrados
                                            where resolucion.Pagina != null && 
                                            resolucion.Pagina.ToLower().Contains(this._ventana.Pagina.ToLower())
                                            select resolucion;
                }

                this._ventana.Resultados = resolucionesFiltrados;
                this._ventana.TotalHits = resolucionesFiltrados.ToList<Resolucion>().Count.ToString();

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
        }

        /// <summary>
        /// Método que invoca una nueva página "ConsultarResolucion" y la instancia con el resolucion seleccionado
        /// </summary>
        public void IrConsultarResolucion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.ResolucionSeleccionado != null)
                this.Navegar(new ConsultarResolucion(this._ventana.ResolucionSeleccionado));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
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

        /// <summary>
        /// Método que limpia los campos de la ventana
        /// </summary>
        public void LimpiarCampos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.Id = null;
            this._ventana.Pagina = null;
            this._ventana.Volumen = null;
            this._ventana.FechaResolucion = null;

            this._ventana.Boletin = ((IList<Boletin>)this._ventana.Boletines)[0];

            this._ventana.Resultados = this._resoluciones;
            this._ventana.TotalHits = this._resoluciones.Count().ToString();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
