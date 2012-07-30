using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Contactos;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Asociados;

namespace Trascend.Bolet.Cliente.Presentadores.Contactos
{
    class PresentadorConsultarContacto : PresentadorBase
    {
        private IConsultarContacto _ventana;
        private IAsociadoServicios _asociadoServicios;
        private ICartaServicios _cartaServicios;
        private IContactoServicios _contactoServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="contacto">Contacto a mostrar</param>
        public PresentadorConsultarContacto(IConsultarContacto ventana, object contacto)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Contacto = contacto;

                this._ventana.setDepartamento = this.BuscarDepartamentoContacto(((Contacto)this._ventana.Contacto).Departamento);
                this._ventana.setFuncion = this.BuscarFuncionContacto(((Contacto)this._ventana.Contacto).Funcion);
                this._ventana.setCorrespondencia = ((Contacto)this._ventana.Contacto).Carta == null ? "" : ((Contacto)this._ventana.Contacto).Carta.Id.ToString();

                this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarContacto,
                    Recursos.Ids.ConsultarContacto);
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
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos del contacto
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
                //Modifica los datos del Contacto
                else
                {
                    bool exitoso = false;
                    Contacto contacto = (Contacto)this._ventana.Contacto;
                    contacto.Departamento = this.transformarDepartamento(this._ventana.getDepartamento);
                    contacto.Funcion = this.transformarFuncion(this._ventana.getFuncion);

                    if (!string.IsNullOrEmpty(this._ventana.getCorrespondencia))
                    {
                        Carta carta = new Carta();
                        carta.Id = int.Parse(this._ventana.getCorrespondencia);
                        if (this._cartaServicios.VerificarExistencia(carta))
                        {
                            contacto.Carta = carta;
                            exitoso = this._contactoServicios.InsertarOModificar(contacto, UsuarioLogeado.Hash);
                        }
                        else
                        {
                            this._ventana.mensaje(Recursos.MensajesConElUsuario.ErrorCorrespondenciaNoEncontrada);
                        }
                    }
                    else
                    {
                        contacto.Carta = null;
                        exitoso = this._contactoServicios.InsertarOModificar(contacto, UsuarioLogeado.Hash);
                    }
                    if (exitoso)
                        this.Navegar(new ListaContactos(((Contacto)this._ventana.Contacto).Asociado,null));
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
        /// Método que se activa al presionar el boton de eliminar al contacto. Es el encargado de eliminar el
        /// contacto
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._contactoServicios.Eliminar((Contacto)this._ventana.Contacto, UsuarioLogeado.Hash))
                {
                    Asociado asociado = ((Contacto)this._ventana.Contacto).Asociado;
                    asociado.Contactos.Remove((Contacto)this._ventana.Contacto);
                    this.Navegar(new ListaContactos(asociado,null));
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


    }
}
