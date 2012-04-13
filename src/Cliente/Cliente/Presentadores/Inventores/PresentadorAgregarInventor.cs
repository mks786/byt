using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Inventores;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Inventores
{
    class PresentadorAgregarInventor : PresentadorBase
    {
        private IAgregarInventor _ventana;
        private IInventorServicios _contactoServicios;
        private IPatenteServicios _marcaServicios;
        private ICartaServicios _cartaServicios;
        private Patente _marca;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarInventor(IAgregarInventor ventana, object asociado)
        {
            try
            {
                //this._ventana = ventana;
                //this._marca = (Patente)asociado;
                //this._ventana.Inventor = new Inventor();
                ////((Inventor)this._ventana.Inventor).Carta = this._carta;
                //((Inventor)this._ventana.Inventor).Patente = this._marca;

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
            try
            {                            
                #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                //this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarInventor,
                //        Recursos.Ids.Inventor);
                //    this._ventana.borrarId();
                //    this._ventana.FocoPredeterminado();

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
        /// Método que realiza toda la lógica para agregar al contacto dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    bool exitoso = false;
            //    Inventor contacto = (Inventor)this._ventana.Inventor;
            //    contacto.Departamento = this.transformarDepartamento(this._ventana.getDepartamento);
            //    contacto.Funcion = this.transformarFuncion(this._ventana.getFuncion);

            //    if (!string.IsNullOrEmpty(this._ventana.getCorrespondencia))
            //    {
            //        Carta carta = new Carta();
            //        carta.Id = int.Parse(this._ventana.getCorrespondencia);

            //        if (this._cartaServicios.VerificarExistencia(carta))
            //        {
            //            contacto.Carta = carta;
            //            exitoso = this._contactoServicios.InsertarOModificar(contacto, UsuarioLogeado.Hash);
            //        }
            //        else
            //        {
            //            this._ventana.mensaje(Recursos.MensajesConElUsuario.ErrorCorrespondenciaNoEncontrada);
            //        }
            //    }
            //    else
            //    {
            //        contacto.Carta = null;
            //        exitoso = this._contactoServicios.InsertarOModificar(contacto, UsuarioLogeado.Hash);
            //    }

            //    if (exitoso)
            //    {
            //        this._marca.Inventores.Insert(0, contacto);
            //        this.Navegar(new ListaInventores(this._marca));
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
