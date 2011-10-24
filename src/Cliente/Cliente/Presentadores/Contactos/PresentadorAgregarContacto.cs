using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Contactos;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Contactos
{
    class PresentadorAgregarContacto : PresentadorBase
    {
        private IAgregarContacto _ventana;
        private IContactoServicios _contactoServicios;
        private IAsociadoServicios _asociadoServicios;
        private ICartaServicios _cartaServicios;
        private Asociado _asociado;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarContacto(IAgregarContacto ventana, object asociado)
        {
            try
            {

                this._ventana = ventana;
                this._asociado = (Asociado)asociado;
                this._ventana.Contacto = new Contacto();
                //((Contacto)this._ventana.Contacto).Carta = this._carta;
                ((Contacto)this._ventana.Contacto).Asociado = this._asociado;

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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarContacto,
                    Recursos.Ids.Contacto);
                this._ventana.borrarId();
                this._ventana.FocoPredeterminado();
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
            try
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
                {
                    this._asociado.Contactos.Insert(0,contacto);
                    this.Navegar(new ListaContactos(this._asociado));
                }

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
