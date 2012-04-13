using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Inventores;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Patentes;

namespace Trascend.Bolet.Cliente.Presentadores.Inventores
{
    class PresentadorConsultarInventor : PresentadorBase
    {
        private IConsultarInventor _ventana;
        private IPatenteServicios _marcaServicios;
        private ICartaServicios _cartaServicios;
        private IInventorServicios _contactoServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="contacto">Inventor a mostrar</param>
        public PresentadorConsultarInventor(IConsultarInventor ventana, object contacto)
        {
            try
            {
                this._ventana = ventana;
                //this._ventana.Inventor = contacto;

                //this._ventana.setDepartamento = this.BuscarDepartamentoInventor(((Inventor)this._ventana.Inventor).Departamento);
                //this._ventana.setFuncion = this.BuscarFuncionInventor(((Inventor)this._ventana.Inventor).Funcion);
                //this._ventana.setCorrespondencia = ((Inventor)this._ventana.Inventor).Carta == null ? "" : ((Inventor)this._ventana.Inventor).Carta.Id.ToString();

                this._contactoServicios = (IInventorServicios)Activator.GetObject(typeof(IInventorServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InventorServicios"]);
                this._marcaServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
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

                //this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarInventor,
                //    Recursos.Ids.ConsultarInventor);
                //this._ventana.FocoPredeterminado();

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
            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    //Habilitar campos
            //    if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
            //    {
            //        this._ventana.HabilitarCampos = true;
            //        this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
            //    }
            //    //Modifica los datos del Inventor
            //    else
            //    {
            //        bool exitoso = false;
            //        Inventor contacto = (Inventor)this._ventana.Inventor;
            //        contacto.Departamento = this.transformarDepartamento(this._ventana.getDepartamento);
            //        contacto.Funcion = this.transformarFuncion(this._ventana.getFuncion);

            //        if (!string.IsNullOrEmpty(this._ventana.getCorrespondencia))
            //        {
            //            Carta carta = new Carta();
            //            carta.Id = int.Parse(this._ventana.getCorrespondencia);
            //            if (this._cartaServicios.VerificarExistencia(carta))
            //            {
            //                contacto.Carta = carta;
            //                exitoso = this._contactoServicios.InsertarOModificar(contacto, UsuarioLogeado.Hash);
            //            }
            //            else
            //            {
            //                this._ventana.mensaje(Recursos.MensajesConElUsuario.ErrorCorrespondenciaNoEncontrada);
            //            }
            //        }
            //        else
            //        {
            //            contacto.Carta = null;
            //            exitoso = this._contactoServicios.InsertarOModificar(contacto, UsuarioLogeado.Hash);
            //        }
            //        if (exitoso)
            //            this.Navegar(new ListaInventores(((Inventor)this._ventana.Inventor).Patente));
            //    }
            //    #region trace
            //    if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion
            //}
            //catch (ApplicationException ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(ex.Message, true);
            //}
            //catch (RemotingException ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            //}
            //catch (SocketException ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            //}
        }

        /// <summary>
        /// Método que se activa al presionar el boton de eliminar al contacto. Es el encargado de eliminar el
        /// contacto
        /// </summary>
        public void Eliminar()
        {
            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    if (this._contactoServicios.Eliminar((Inventor)this._ventana.Inventor, UsuarioLogeado.Hash))
            //    {
            //        Patente asociado = ((Inventor)this._ventana.Inventor).Patente;
            //        asociado.Inventores.Remove((Inventor)this._ventana.Inventor);
            //        this.Navegar(new ListaInventores(asociado));
            //    }

            //    #region trace
            //    if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion
            //}
            //catch (ApplicationException ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(ex.Message, true);
            //}
            //catch (RemotingException ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            //}
            //catch (SocketException ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //    this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            //}
        }


    }
}
