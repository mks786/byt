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
using Trascend.Bolet.Cliente.Contratos.Objetos;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Objetos
{
    class PresentadorGestionarObjetosXRoles : PresentadorBase
    {
        private IGestionarObjetosXRoles _ventana;
        private IRolServicios _rolServicios;
        private IObjetoServicios _objetoServicios;
        private IList<Objeto> _objetos;
        private IList<Objeto> _objetosSinObjetosDelRol;
        private IList<Rol> _roles;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarObjetosXRoles(IGestionarObjetosXRoles ventana)
        {
            try
            {
                this._ventana = ventana;
                this._rolServicios = (IRolServicios)Activator.GetObject(typeof(IRolServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RolServicios"]);
                this._objetoServicios = (IObjetoServicios)Activator.GetObject(typeof(IObjetoServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ObjetoServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarObjetosXRoles,
                    Recursos.Ids.ConsultarObjetos);

                this._roles = this._rolServicios.ConsultarRolesYObjetos();
                this._ventana.Roles = this._roles;

                this._objetos = this._objetoServicios.ConsultarTodos();
                this._ventana.Objetos = this._objetos;
                this._objetosSinObjetosDelRol = this._objetos;

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

            IEnumerable<Objeto> objetosFiltrados = this._objetosSinObjetosDelRol;

            if (!string.IsNullOrEmpty(this._ventana.Id))
            {
                objetosFiltrados = from objeto in objetosFiltrados
                                   where objeto.Id.Contains(this._ventana.Id)
                                   select objeto;
            }

            if (!string.IsNullOrEmpty(this._ventana.Descripcion))
            {
                objetosFiltrados = from objeto in objetosFiltrados
                                   where objeto.Descripcion != null && 
                                   objeto.Descripcion.Contains(this._ventana.Descripcion)
                                   select objeto;
            }

            this._ventana.Objetos = objetosFiltrados.ToList<Objeto>();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que carga en la lista de los objetos relacionados con los roles, los objetos del rol
        /// seleccionado
        /// </summary>
        public void CargarObjetosPorRol()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Objeto> objetosDelRolSeleccionado = ((Rol)this._ventana.RolSeleccionado).Objetos;
            IList<Objeto> objetosFiltrados = new List<Objeto>();

            foreach (Objeto objeto in this._objetos)
            {
                bool agregar = true;
                foreach (Objeto objetodelrol in objetosDelRolSeleccionado)
                {
                    if (objetodelrol != null)
                    {
                        if (objeto.Id.Equals(objetodelrol.Id))
                        {
                            agregar = false;
                        }
                    }
                }

                if (agregar)
                    objetosFiltrados.Add(objeto);
            }

            this._ventana.ObjetosXRoles = objetosDelRolSeleccionado;
            this._ventana.Objetos = objetosFiltrados;
            this._objetosSinObjetosDelRol = objetosFiltrados;

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

                IList<object> objetosAMover = (IList<object>)this._ventana.ObjetosSeleccionados;
                Rol rolSeleccionado = (Rol)this._ventana.RolSeleccionado;

                if (objetosAMover != null && rolSeleccionado != null)
                {
                    IList<Objeto> objetosXRoles = (IList<Objeto>)this._ventana.ObjetosXRoles;
                    foreach (Objeto objeto in objetosAMover)
                    {
                        objetosXRoles.Add(objeto);
                    }
                    this._ventana.ObjetosXRoles = null;
                    this._ventana.ObjetosXRoles = objetosXRoles;

                    IList<Objeto> objetos = (IList<Objeto>)this._ventana.Objetos;
                    foreach (Objeto objeto in objetosAMover)
                    {
                        objetos.Remove(objeto);
                    }
                    this._ventana.Objetos = null;
                    this._ventana.Objetos = objetos;

                    this._rolServicios.InsertarOModificar((Rol)this._ventana.RolSeleccionado, UsuarioLogeado.Hash);
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

                IList<object> objetosAMover = (IList<object>)this._ventana.ObjetosXRolesSeleccionados;
                Rol rolSeleccionado = (Rol)this._ventana.RolSeleccionado;

                if (objetosAMover != null && rolSeleccionado != null)
                {
                    IList<Objeto> objetos = (IList<Objeto>)this._ventana.Objetos;
                    foreach (Objeto objeto in objetosAMover)
                    {
                        objetos.Add(objeto);
                    }
                    this._ventana.Objetos = null;
                    this._ventana.Objetos = objetos;

                    IList<Objeto> objetosXRoles = (IList<Objeto>)this._ventana.ObjetosXRoles;
                    foreach (Objeto objeto in objetosAMover)
                    {
                        objetosXRoles.Remove(objeto);
                    }
                    this._ventana.ObjetosXRoles = null;
                    this._ventana.ObjetosXRoles = objetosXRoles;

                    this._rolServicios.InsertarOModificar((Rol)this._ventana.RolSeleccionado, UsuarioLogeado.Hash);
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
                        this._ventana.ListaRoles.Items.SortDescriptions.Clear();
                        break;

                    case 2:
                        this._ventana.ListaObjetos.Items.SortDescriptions.Clear();
                        break;

                    case 3:
                        this._ventana.ListaRolesXObjetos.Items.SortDescriptions.Clear();
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
                    this._ventana.ListaRoles.Items.SortDescriptions.Add(
                        new SortDescription(field, newDir));
                    break;

                case 2:
                    this._ventana.ListaObjetos.Items.SortDescriptions.Add(
                        new SortDescription(field, newDir));
                    break;

                case 3:
                    this._ventana.ListaRolesXObjetos.Items.SortDescriptions.Add(
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
