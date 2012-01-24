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
using Trascend.Bolet.Cliente.Contratos.Estatuses;
using Trascend.Bolet.Cliente.Ventanas.Estatuses;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Estatuses
{
    class PresentadorConsultarEstatuses : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarEstatuses _ventana;
        private IEstatusServicios _estatusServicios;
        private IList<Estatus> _estatuses;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarEstatuses(IConsultarEstatuses ventana)
        {
            try
            {
                this._ventana = ventana;
                this._estatusServicios = (IEstatusServicios)Activator.GetObject(typeof(IEstatusServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EstatusServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarEstatuses,
            Recursos.Ids.ConsultarEstatuses);
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

                this._estatuses = this._estatusServicios.ConsultarTodos();
                this._ventana.Resultados = this._estatuses;
                this._ventana.TotalHits = this._estatuses.Count.ToString();
                this._ventana.EstatusFiltrar = new Estatus();
                this._ventana.FocoPredeterminado();

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

                Estatus estatus = (Estatus)this._ventana.EstatusFiltrar;

                IEnumerable<Estatus> estatusesFiltrados = this._estatuses;

                if (!string.IsNullOrEmpty(estatus.Id))
                {
                    estatusesFiltrados = from p in estatusesFiltrados
                                       where p.Id != null && 
                                       p.Id.ToLower().Contains(estatus.Id.ToLower())
                                       select p;
                }

                if (!string.IsNullOrEmpty(estatus.Descripcion))
                {
                    estatusesFiltrados = from p in estatusesFiltrados
                                         where p.Descripcion != null &&
                                         p.Descripcion.ToLower().Contains(estatus.Descripcion.ToLower())
                                         select p;
                }

                if (!string.IsNullOrEmpty(estatus.DescripcionIngles))
                {
                    estatusesFiltrados = from p in estatusesFiltrados
                                         where p.DescripcionIngles != null &&
                                         p.DescripcionIngles.ToLower().Contains(estatus.DescripcionIngles.ToLower())
                                         select p;
                }

                if (!string.IsNullOrEmpty(estatus.StatusProximoPaso))
                {
                    estatusesFiltrados = from p in estatusesFiltrados
                                         where p.StatusProximoPaso != null &&
                                         p.StatusProximoPaso.ToLower().Contains(estatus.StatusProximoPaso.ToLower())
                                         select p;
                }

                if (!string.IsNullOrEmpty(estatus.StatusProximoPasoIngles))
                {
                    estatusesFiltrados = from p in estatusesFiltrados
                                         where p.StatusProximoPasoIngles != null &&
                                         p.StatusProximoPasoIngles.ToLower().Contains(estatus.StatusProximoPasoIngles.ToLower())
                                         select p;
                }

                this._ventana.Resultados = estatusesFiltrados.ToList<Estatus>();
                this._ventana.TotalHits = estatusesFiltrados.ToList<Estatus>().Count.ToString();

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
        /// Método que invoca una nueva página "ConsultarEstatus" y la instancia con el estatus seleccionado
        /// </summary>
        public void IrConsultarEstatus()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarEstatus(this._ventana.EstatusSeleccionado));

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
    }
}
