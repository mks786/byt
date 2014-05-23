using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Remitentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ventanas.EntradasAlternas;

namespace Trascend.Bolet.Cliente.Presentadores.Remitentes
{
    class PresentadorConsultarRemitente : PresentadorBase
    {

        private IConsultarRemitente _ventana;
        private IPaisServicios _paisServicios;
        private IRemitenteServicios _remitenteServicios;
        private IPoderServicios _poderServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="poder">Poder a mostrar</param>
        public PresentadorConsultarRemitente(IConsultarRemitente ventana, object remitente, object ventanaPadre)
        {
            try
            {
                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventana.Remitente = remitente;
                this._remitenteServicios = (IRemitenteServicios)Activator.GetObject(typeof(IRemitenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RemitenteServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._ventana.SetTipoRemitente = BuscarTipoRemitente(((Remitente)remitente).TipoRemitente);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarRemitente,
                    Recursos.Ids.ConsultarRemitente);

                Remitente remitente = (Remitente)this._ventana.Remitente;

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                this._ventana.Paises = paises;

                this._ventana.Pais = this.BuscarPais(paises, remitente.Pais);

                if (this._ventanaPadre != null)
                {
                    if (this._ventanaPadre.GetType().ToString().Equals("Trascend.Bolet.Cliente.Ventanas.EntradasAlternas.AgregarEntradaAlterna"))
                    {
                        this._ventana.MostrarBotonSeleccionarRemitente();
                    } 
                }

                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos del Remitente
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

                //Modifica los datos del Poder
                else
                {
                    Remitente remitente = (Remitente)this._ventana.Remitente;

                    remitente.Pais = (Pais)this._ventana.Pais;
                    remitente.TipoRemitente = this._ventana.GetTipoRemitente;
                    remitente.Operacion = "MODIFY";

                    bool exitoso = this._remitenteServicios.InsertarOModificar(remitente, UsuarioLogeado.Hash);
                    if (exitoso)
                    {
                        this._ventana.HabilitarCampos = false;
                        this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                        //_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.RemitenteModificado;
                        //this.Navegar(_paginaPrincipal);
                    }
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

        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                Remitente remitente = (Remitente)this._ventana.Remitente;

                remitente.Pais = (Pais)this._ventana.Pais;
                remitente.TipoRemitente = this._ventana.GetTipoRemitente;

                bool exitoso = this._remitenteServicios.Eliminar(remitente, UsuarioLogeado.Hash);
                if (exitoso)
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.RemitenteEliminado;
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
        /// Metodo que permite seleccionar el Remitente en pantalla e incluirlo en el combo de los Remitentes en la ventana
        /// de Entrada Alterna
        /// </summary>
        public void SeleccionarRemitente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventanaPadre != null) && 
                    (this._ventanaPadre.GetType().ToString().Equals("Trascend.Bolet.Cliente.Ventanas.EntradasAlternas.AgregarEntradaAlterna")))
                {
                    Remitente remitente = (Remitente)this._ventana.Remitente;
                    ((AgregarEntradaAlterna)this._ventanaPadre).RefrescarRemitente(remitente);
                    RegresarVentanaPadre();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }
    }
}
