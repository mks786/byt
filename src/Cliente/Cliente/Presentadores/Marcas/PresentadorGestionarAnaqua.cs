using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarAnaqua : PresentadorBase
    {
        private IGestionarAnaqua _ventana;
        private Marca _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IAnaquaServicios _anaquaServicios;
        private bool _nuevaAnaqua = false;

        static BackgroundWorker _bw = new BackgroundWorker();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorGestionarAnaqua(IGestionarAnaqua ventana, object marca)
        {
            try
            {
                this._ventana = ventana;
                this._marca = (Marca)marca;
                this._ventana.Anaqua = null != ((Marca)marca).Anaqua ? ((Marca)marca).Anaqua : new Anaqua(this._marca.Id);

                this._ventana.DatosMarca(((Marca)marca).CodigoRegistro, ((Marca)marca).CodigoInscripcion);
                

                if (null == ((Marca)marca).Anaqua)
                {
                    this._nuevaAnaqua = true;
                }

                this._anaquaServicios = (IAnaquaServicios)Activator.GetObject(typeof(IAnaquaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnaquaServicios"]);
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
                //cambiar titulo
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarAgente,
                    Recursos.Ids.AgregarAgente);

                if (this._nuevaAnaqua)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }
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
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public bool Aceptar()
        {
            bool retorno = false;
            try
            {

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del boletin
                else
                {
                    this._marca.Anaqua = (Anaqua)this._ventana.Anaqua;

                    this._marca.Anaqua.InsertarOModificar = true;
                    this._marca.Anaqua.Operacion = this._nuevaAnaqua ? "CREATE" : "MODIFY";
                    if (this._marca.Anaqua.Colores == null)
                        this._marca.Anaqua.Colores = "F";
                    this._marca.Anaqua.Usuario = UsuarioLogeado;

                    bool exitoso = this._anaquaServicios.InsertarOModificar((Anaqua)this._marca.Anaqua, UsuarioLogeado.Hash);

                    if (exitoso)
                    {
                        retorno = true;
                    }
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
            return retorno;
        }

        private void imprimirMensajeExito(object sender, DoWorkEventArgs e)
        {
            this._ventana.TextoBotonModificar = (string)e.Argument;
            Thread.Sleep(2000);
        }
    }
}
