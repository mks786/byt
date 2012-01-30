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
using Trascend.Bolet.Cliente.Contratos.Usuarios;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Usuarios;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Usuarios
{
    class PresentadorConsultarUsuarios : PresentadorBase
    {
        private IConsultarUsuarios _ventana;
        private IDepartamentoServicios _departamentoServicios;
        private IRolServicios _rolServicios;
        private IUsuarioServicios _usuarioServicios;
        private IList<Usuario> _usuarios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarUsuarios(IConsultarUsuarios ventana)
        {
            try
            {
                this._ventana = ventana;
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._rolServicios = (IRolServicios)Activator.GetObject(typeof(IRolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RolServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
            }
            catch (Exception ex)
            {
                _paginaPrincipal.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado;
                logger.Error(ex.Message);
                this.Navegar();
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarUsuarios,
                Recursos.Ids.ConsultarUsuarios);
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

                this._ventana.Resultados = this._usuarioServicios.ConsultarTodos();
                this._ventana.Roles = this._rolServicios.ConsultarTodos();
                this._ventana.Departamentos = this._departamentoServicios.ConsultarTodos();
                this._usuarios = this._usuarioServicios.ConsultarTodos();
                this._ventana.Resultados = this._usuarios;
                this._ventana.TotalHits = this._usuarios.Count.ToString();


                IList<Rol> roles = this._rolServicios.ConsultarTodos();
                Rol primeroRol = new Rol();
                primeroRol.Id = "NGN";
                primeroRol.Descripcion = "";
                roles.Insert(0, primeroRol);
                this._ventana.Roles = roles;

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primerDepartamento = new Departamento();
                primerDepartamento.Id = "NGN";
                primerDepartamento.Descripcion = "";
                departamentos.Insert(0, primerDepartamento);
                this._ventana.Departamentos = departamentos;
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

                IEnumerable<Usuario> usuariosFiltrados = this._usuarios;

                if (!string.IsNullOrEmpty(this._ventana.Id))
                {
                    usuariosFiltrados = from usuario in usuariosFiltrados
                                        where usuario.Id.Contains(this._ventana.Id)
                                        select usuario;
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreCompleto))
                {
                    usuariosFiltrados = from usuario in usuariosFiltrados
                                        where usuario.NombreCompleto != null && 
                                        usuario.NombreCompleto.ToLower().Contains(this._ventana.NombreCompleto.ToLower())
                                        select usuario;
                }

                if (!string.IsNullOrEmpty(this._ventana.Iniciales))
                {
                    usuariosFiltrados = from usuario in usuariosFiltrados
                                        where usuario.Iniciales != null && 
                                        usuario.Iniciales.Contains(this._ventana.Iniciales)
                                        select usuario;
                }

                if (!string.IsNullOrEmpty(this._ventana.Email))
                {
                    usuariosFiltrados = from usuario in usuariosFiltrados
                                        where usuario.Email != null && 
                                            usuario.Email.Contains(this._ventana.Email)
                                        select usuario;
                }

                if (this._ventana.Rol != null && !((Rol)this._ventana.Rol).Id.Equals("NGN"))
                {
                    usuariosFiltrados = from usuario in usuariosFiltrados
                                        where usuario.Rol.Id.Contains(((Rol)this._ventana.Rol).Id)
                                        select usuario;

                }

                if (this._ventana.Departamento != null && !((Departamento)this._ventana.Departamento).Id.Equals("NGN"))
                {
                    usuariosFiltrados = from usuario in usuariosFiltrados
                                        where usuario.Departamento.Id.Contains(((Departamento)this._ventana.Departamento).Id)
                                        select usuario;
                }

                this._ventana.Resultados = usuariosFiltrados;
                this._ventana.TotalHits = usuariosFiltrados.ToList<Usuario>().Count.ToString();

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
        /// Método que invoca una nueva página "ConsultarUsuario" y la instancia con el usuario seleccionado
        /// </summary>
        public void IrConsultarUsuario()
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Usuario usuario = (Usuario)this._ventana.UsuarioSeleccionado;

            if (usuario != null)
                this.Navegar(new ConsultarUsuario(usuario));

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
