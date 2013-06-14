using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ventanas.Interesados;

namespace Trascend.Bolet.Cliente.Presentadores.Interesados
{
    class PresentadorConsultarInteresado : PresentadorBase
    {

        private IConsultarInteresado _ventana;
        private IPaisServicios _paisServicios;
        private IEstadoServicios _estadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IPoderServicios _poderServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="poder">Poder a mostrar</param>
        public PresentadorConsultarInteresado(IConsultarInteresado ventana, object interesado, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventana.Interesado = interesado;
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._estadoServicios = (IEstadoServicios)Activator.GetObject(typeof(IEstadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EstadoServicios"]);
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);

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

                Interesado interesado = (Interesado)this._ventana.Interesado;

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                IList<Estado> estados = this._estadoServicios.ConsultarTodos();
                this._ventana.Paises = paises;
                this._ventana.Nacionalidades = paises;
                this._ventana.Corporaciones = estados;

                this._ventana.Pais = this.BuscarPais(paises, interesado.Pais);
                this._ventana.Nacionalidad = this.BuscarPais(paises, interesado.Nacionalidad);
                this._ventana.Corporacion = this.BuscarEstado(estados, interesado.Corporacion);
                IList<ListaDatosDominio> tiposPersona = this._listaDatosDominioServicios.ConsultarListaDatosDominioPorParametro(new ListaDatosDominio("PERSONA"));
                this._ventana.TipoPersonas = tiposPersona;
                this._ventana.TipoPersona = BuscarTipoPersona(interesado.TipoPersona, (IList<ListaDatosDominio>)this._ventana.TipoPersonas);

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

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarInteresado,
                       Recursos.Ids.ConsultarInteresado);
        }


        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos del usuario
        /// </summary>
        public void Modificar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del Interesado
                else
                {
                    Interesado interesado = (Interesado)this._ventana.Interesado;

                    interesado.Pais = (Pais)this._ventana.Pais;
                    interesado.Nacionalidad = (Pais)this._ventana.Nacionalidad;
                    interesado.Corporacion = (Estado)this._ventana.Corporacion;
                    interesado.Operacion = "MODIFY";
                    interesado.TipoPersona = ((ListaDatosDominio)this._ventana.TipoPersona).Id[0];

                    //if ((interesado.Estado != null) && (!interesado.Estado.Equals("")))
                    //{
                        if ((interesado.Pais != null) && (interesado.Pais.Id != 0))
                        {
                            //if ((interesado.Nacionalidad != null) && (!interesado.Nacionalidad.Equals("")))
                            //{
                                bool exitoso = this._interesadoServicios.InsertarOModificar(interesado, UsuarioLogeado.Hash);
                                if (exitoso)
                                {
                                    //_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.InteresadoModificado;
                                    if (_ventanaPadre != null)
                                        this.RegresarVentanaPadre();
                                    else
                                    {
                                        _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.InteresadoModificado;
                                        this.Navegar(_paginaPrincipal);
                                    }
                                }
                            //}
                            //else
                            //{
                            //    this._ventana.Mensaje(Recursos.MensajesValidaciones.InteresadoNacionalidad, 1);
                            //}
                        }
                        else
                        {
                            this._ventana.Mensaje(Recursos.MensajesValidaciones.InteresadoPais, 1);

                        }
                    //}
                    //else
                    //{
                    //    this._ventana.Mensaje(Recursos.MensajesValidaciones.InteresadoEstado, 1);
                    //}
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
        /// Método que elimina un Interesado de la base de datos
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                Interesado interesado = (Interesado)this._ventana.Interesado;

                interesado.Pais = (Pais)this._ventana.Pais;
                interesado.Nacionalidad = (Pais)this._ventana.Nacionalidad;
                interesado.Corporacion = (Estado)this._ventana.Corporacion;
                interesado.Operacion = "DELETE";
                //interesado.TipoPersona = this._ventana.GetTipoPersona;

                bool exitoso = this._interesadoServicios.Eliminar(interesado, UsuarioLogeado.Hash);
                if (exitoso)
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PoderEliminado;
                    this.Navegar(_paginaPrincipal);
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
        /// Método que guarda el registro para la auditoría
        /// </summary>
        public void Auditoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((Interesado)this._ventana.Interesado).Id;
                auditoria.Tabla = "MYP_INTERESADOS";

                IList<Auditoria> auditorias = this._interesadoServicios.AuditoriaPorFkyTabla(auditoria);
                _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PoderEliminado;
                this.Navegar(new ListaAuditorias(auditorias));


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
        /// Método que llama a la ventana para ver la lista de poderes del
        /// interesado
        /// </summary>
        public void VerPoderes()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Poder> poderes = this._poderServicios.ConsultarPoderesPorInteresado((Interesado)this._ventana.Interesado);

                _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PoderEliminado;
                this.Navegar(new ListaPoderes(poderes, (Interesado)this._ventana.Interesado, this._ventana));


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
        /// Método que invoca una nueva página "ConsultarInteresados" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarInteresados()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarInteresados());

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

    }
}
