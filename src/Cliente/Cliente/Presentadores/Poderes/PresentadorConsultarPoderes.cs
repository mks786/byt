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
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Ventanas.Poderes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Poderes
{
    class PresentadorConsultarPoderes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarPoderes _ventana;
        private IBoletinServicios _boletinServicios;
        private IPoderServicios _poderServicios;
        private IInteresadoServicios _interesadoServicios;
        private IList<Poder> _poderes;
        private IList<Interesado> _interesados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarPoderes(IConsultarPoderes ventana)
        {
            try
            {
                this._ventana = ventana;
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarPoderes,
                Recursos.Ids.ConsultarPoderes);
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
                
                this._poderes = this._poderServicios.ConsultarTodos();
                this._ventana.Resultados = this._poderes;

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.Boletines = boletines;

                IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                interesados.Insert(0, primerInteresado);
                this._ventana.Interesados = interesados;
                this._interesados = interesados;

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
                
                IEnumerable<Poder> poderesFiltrados = this._poderes;

                if (!string.IsNullOrEmpty(this._ventana.Id))
                {
                    poderesFiltrados = from p in poderesFiltrados
                                       where p.Id == Int32.Parse(this._ventana.Id)
                                       select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.NumPoder))
                {
                    poderesFiltrados = from p in poderesFiltrados
                                       where p.NumPoder != null &&
                                       p.NumPoder.ToLower().Contains(this._ventana.NumPoder.ToLower())
                                       select p;
                }

                if (this._ventana.Boletin != null && !((Boletin)this._ventana.Boletin).Id.Equals(int.MinValue))
                {
                    Boletin boletin = (Boletin)this._ventana.Boletin;
                    poderesFiltrados = from p in poderesFiltrados
                                            where p.Boletin != null &&
                                            p.Boletin.Id.ToString().ToLower().Contains(boletin.Id.ToString().ToLower())
                                            select p;
                }

                if (this._ventana.Interesado != null && !((Interesado)this._ventana.Interesado).Id.Equals(int.MinValue))
                {
                    Interesado interesado = (Interesado)this._ventana.Interesado;
                    poderesFiltrados = from p in poderesFiltrados
                                       where p.Interesado != null &&
                                       p.Interesado.Id.ToString().ToLower().Contains(interesado.Id.ToString().ToLower())
                                       select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.Facultad))
                {
                    poderesFiltrados = from p in poderesFiltrados
                                       where p.Facultad != null &&
                                       p.Facultad.ToLower().Contains(this._ventana.Facultad.ToLower())
                                       select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.Anexo))
                {
                    poderesFiltrados = from p in poderesFiltrados
                                       where p.Anexo != null &&
                                       p.Anexo.ToLower().Contains(this._ventana.Anexo.ToLower())
                                       select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.Observaciones))
                {
                    poderesFiltrados = from p in poderesFiltrados
                                       where p.Observaciones != null &&
                                       p.Observaciones.ToLower().Contains(this._ventana.Observaciones.ToLower())
                                       select p;
                }

                this._ventana.Resultados = poderesFiltrados.ToList<Poder>();

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
        /// Método que invoca una nueva página "ConsultarPoder" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarPoder()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarPoder(this._ventana.PoderSeleccionado, this._ventana.Boletines, this._ventana.Interesados));

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

        public void BuscarInteresado()
        {
            IEnumerable<Interesado> interesadosFiltrados = (IList<Interesado>)this._interesados;

            if (!string.IsNullOrEmpty(this._ventana.IdInteresadoFiltrar))
            {
                interesadosFiltrados = from p in interesadosFiltrados
                                       where p.Id == int.Parse(this._ventana.IdInteresadoFiltrar)
                                       select p;
            }

            if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoFiltrar))
            {
                interesadosFiltrados = from p in interesadosFiltrados
                                       where p.Nombre != null &&
                                       p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoFiltrar.ToLower())
                                       select p;
            }

            if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                this._ventana.Interesados = interesadosFiltrados.ToList<Interesado>();
            else
                this._ventana.Interesados = this._interesados;
        }
    }
}
