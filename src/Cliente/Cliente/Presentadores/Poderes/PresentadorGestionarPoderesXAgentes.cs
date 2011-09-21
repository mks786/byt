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
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Poderes
{
    class PresentadorGestionarPoderesXAgentes : PresentadorBase
    {
        private IGestionarPoderesXAgentes _ventana;
        private IAgenteServicios _agenteServicios;
        private IPoderServicios _poderServicios;
        private IList<Poder> _poderes;
        private IList<Poder> _PoderesSinPoderesDelAgente;
        private IList<Agente> _agentes;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarPoderesXAgentes(IGestionarPoderesXAgentes ventana)
        {
            try
            {
                this._ventana = ventana;
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarPoderesXAgentes,
                    Recursos.Ids.ConsultarObjetos);

                this._agentes = this._agenteServicios.ConsultarAgentesYPoderes();
                this._ventana.Agentes = this._agentes;

                this._poderes = this._poderServicios.ConsultarTodos();
                this._ventana.Poderes = this._poderes;
                this._PoderesSinPoderesDelAgente = this._poderes;

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
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IEnumerable<Poder> poderesFiltrados = this._PoderesSinPoderesDelAgente;

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

            this._ventana.Poderes = poderesFiltrados.ToList<Poder>();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que carga en la lista de los Poderes relacionados con los roles, los Poderes del rol
        /// seleccionado
        /// </summary>
        public void CargarPoderesPorAgente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Poder> poderesDelAgenteSeleccionado = ((Agente)this._ventana.AgenteSeleccionado).Poderes;
            IList<Poder> poderesFiltrados = new List<Poder>();

            foreach (Poder poder in this._poderes)
            {
                bool agregar = true;
                foreach (Poder poderDelAgente in poderesDelAgenteSeleccionado)
                {
                    if (poderDelAgente != null)
                    {
                        if (poder.Id.Equals(poderDelAgente.Id))
                        {
                            agregar = false;
                        }
                    }
                }

                if (agregar)
                    poderesFiltrados.Add(poder);
            }

            this._ventana.PoderesXAgentes = poderesDelAgenteSeleccionado;
            this._ventana.Poderes = poderesFiltrados;
            this._PoderesSinPoderesDelAgente = poderesFiltrados;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que agrega a la lista de objetos del rol, objetos seleccionados en la lista de todos los objetos
        /// </summary>
        public void Agregar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<object> poderesAMover = (IList<object>)this._ventana.PoderesSeleccionados;
                Agente agenteSeleccionado = (Agente)this._ventana.AgenteSeleccionado;

                if (poderesAMover != null && agenteSeleccionado != null)
                {
                    IList<Poder> PoderesXAgentes = (IList<Poder>)this._ventana.PoderesXAgentes;
                    foreach (Poder poder in poderesAMover)
                    {
                        PoderesXAgentes.Add(poder);
                    }
                    this._ventana.PoderesXAgentes = null;
                    this._ventana.PoderesXAgentes = PoderesXAgentes;

                    IList<Poder> poderes = (IList<Poder>)this._ventana.Poderes;
                    foreach (Poder poder in poderesAMover)
                    {
                        poderes.Remove(poder);
                    }
                    this._ventana.Poderes = null;
                    this._ventana.Poderes = poderes;

                    Agente ahhh = (Agente)this._ventana.AgenteSeleccionado;
                    this._agenteServicios.InsertarOModificar(ahhh, UsuarioLogeado.Hash);
                }

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
        }

        /// <summary>
        /// Método que remueve a la lista de objetos del rol, objetos seleccionados en esta
        /// </summary>
        public void Quitar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<object> poderesAMover = (IList<object>)this._ventana.PoderesXAgentesSeleccionados;
                Agente rolSeleccionado = (Agente)this._ventana.AgenteSeleccionado;

                if (poderesAMover != null && rolSeleccionado != null)
                {
                    IList<Poder> poderes = (IList<Poder>)this._ventana.Poderes;
                    foreach (Poder poder in poderesAMover)
                    {
                        poderes.Add(poder);
                    }
                    this._ventana.Poderes = null;
                    this._ventana.Poderes = poderes;

                    IList<Poder> poderesXAgentes = (IList<Poder>)this._ventana.PoderesXAgentes;
                    foreach (Poder poder in poderesAMover)
                    {
                        poderesXAgentes.Remove(poder);
                    }
                    this._ventana.PoderesXAgentes = null;
                    this._ventana.PoderesXAgentes = poderesXAgentes;

                    this._agenteServicios.InsertarOModificar((Agente)this._ventana.AgenteSeleccionado, UsuarioLogeado.Hash);
                    Consultar();
                }
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
        }

        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        /// <param name="column">columna a ordenar</param>
        /// <param name="lista">1: lista de roles, 2: lista de objetos, 3 lista de rolesXojetos</param>
        public void OrdenarColumna(GridViewColumnHeader column, int lista)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                switch (lista)
                {
                    case 1 :
                        this._ventana.ListaAgentes.Items.SortDescriptions.Clear();
                        break;

                    case 2:
                        this._ventana.ListaPoderes.Items.SortDescriptions.Clear();
                        break;

                    case 3:
                        this._ventana.ListaAgentesXPoderes.Items.SortDescriptions.Clear();
                        break;
                }
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            switch (lista)
            {
                case 1:
                    this._ventana.ListaAgentes.Items.SortDescriptions.Add(
                        new SortDescription(field, newDir));
                    break;

                case 2:
                    this._ventana.ListaPoderes.Items.SortDescriptions.Add(
                        new SortDescription(field, newDir));
                    break;

                case 3:
                    this._ventana.ListaAgentesXPoderes.Items.SortDescriptions.Add(
                        new SortDescription(field, newDir));
                    break;
            }
            
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
