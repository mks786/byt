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
using Trascend.Bolet.Cliente.Ventanas.EntradasAlternas;
using Trascend.Bolet.Cliente.Ventanas.Remitentes;

namespace Trascend.Bolet.Cliente.Presentadores.Remitentes
{
    class PresentadorAgregarRemitente : PresentadorBase
    {
        private IAgregarRemitente _ventana;
        private IPaisServicios _paisServicios;
        private IRemitenteServicios _remitenteServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarRemitente(IAgregarRemitente ventana, object ventanaPadre)
        {
            try
            {
                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventana.Remitente = new Remitente();
                this._remitenteServicios = (IRemitenteServicios)Activator.GetObject(typeof(IRemitenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RemitenteServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarRemitente,
                    Recursos.Ids.AgregarRemitente);
                this._ventana.FocoPredeterminado();

                Remitente remitente = (Remitente)this._ventana.Remitente;

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                this._ventana.Paises = paises;
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
        /// Método que realiza toda la lógica para agregar al remintente dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            String exitoso = null;

            try
            {
                Remitente remitente = (Remitente)this._ventana.Remitente;

                remitente.Pais = (Pais)this._ventana.Pais;
                remitente.TipoRemitente = this._ventana.TipoRemitente;
                remitente.Operacion = "CREATE";
                //bool exitoso = this._remitenteServicios.InsertarOModificar(remitente, UsuarioLogeado.Hash);
                exitoso = this._remitenteServicios.InsertarOModificarRemitente(remitente, UsuarioLogeado.Hash);

                if (!string.IsNullOrEmpty(exitoso))
                {
                    remitente.Id = exitoso;
                    
                    if (this._ventanaPadre != null)
                    {
                        this.Navegar(new ConsultarRemitente(remitente, this._ventanaPadre));
                    }
                    else
                    {
                        this.Navegar(new ConsultarRemitente(remitente, null));
                    }
                }

                #region CODIGO ORIGINAL COMENTADO -  NO BORRAR
                /*if (exitoso)
                    if (_ventanaPadre == null)
                        this.Navegar(Recursos.MensajesConElUsuario.RemitenteInsertado, false);
                    else
                    {

                        ((AgregarEntradaAlterna)this._ventanaPadre).RefrescarRemitente(remitente);
                        RegresarVentanaPadre();
                    }*/
                
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
