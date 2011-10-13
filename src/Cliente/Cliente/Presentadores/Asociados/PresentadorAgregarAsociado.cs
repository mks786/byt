using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.Asociados
{
    class PresentadorAgregarAsociado : PresentadorBase
    {
        private IAgregarAsociado _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IDetallePagoServicios _detallePagoServicios;
        private IEtiquetaServicios _etiquetaServicios;
        private IIdiomaServicios _idiomaServicios;
        private IMonedaServicios _monedaServicios;
        private ITarifaServicios _tarifaServicios;
        private ITipoClienteServicios _tipoClienteServicios;
        private IPaisServicios _paisServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarAsociado(IAgregarAsociado ventana)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Asociado = new Asociado();
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._detallePagoServicios = (IDetallePagoServicios)Activator.GetObject(typeof(IDetallePagoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DetallePagoServicios"]);
                this._etiquetaServicios = (IEtiquetaServicios)Activator.GetObject(typeof(IEtiquetaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EtiquetaServicios"]);
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);
                this._monedaServicios = (IMonedaServicios)Activator.GetObject(typeof(IMonedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MonedaServicios"]);
                this._tarifaServicios = (ITarifaServicios)Activator.GetObject(typeof(ITarifaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TarifaServicios"]);
                this._tipoClienteServicios = (ITipoClienteServicios)Activator.GetObject(typeof(ITipoClienteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoClienteServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarAsociado,
                    Recursos.Ids.AgregarAsociado);

                IList<DetallePago> detallesPagos = this._detallePagoServicios.ConsultarTodos();
                DetallePago primerDetallePago = new DetallePago();
                primerDetallePago.Id = "NGN";
                detallesPagos.Insert(0, primerDetallePago);
                this._ventana.DetallesPagos = detallesPagos;

                IList<Etiqueta> etiquetas = this._etiquetaServicios.ConsultarTodos();
                Etiqueta primeraEtiqueta = new Etiqueta();
                primeraEtiqueta.Id = "NGN";
                etiquetas.Insert(0, primeraEtiqueta);
                this._ventana.Etiquetas = etiquetas;

                IList<Tarifa> tarifas = this._tarifaServicios.ConsultarTodos();
                Tarifa primeraTarifa= new Tarifa();
                primeraTarifa.Id = "NGN";
                tarifas.Insert(0, primeraTarifa);
                this._ventana.Tarifas = tarifas;

                this._ventana.Idiomas = this._idiomaServicios.ConsultarTodos();

                this._ventana.Monedas = this._monedaServicios.ConsultarTodos();

                this._ventana.Paises = this._paisServicios.ConsultarTodos();

                this._ventana.TiposClientes = this._tipoClienteServicios.ConsultarTodos();

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
        public void Aceptar()
        {
            try
            {
                Asociado asociado = (Asociado)this._ventana.Asociado;

                asociado.Operacion = "CREATE";
                asociado.TipoPersona = this._ventana.TipoPersona;
                asociado.Pais = (Pais)this._ventana.Pais;
                asociado.Idioma = (Idioma)this._ventana.Idioma;
                asociado.Moneda = (Moneda)this._ventana.Moneda;
                asociado.TipoCliente = (TipoCliente)this._ventana.TipoCliente;
                asociado.Tarifa = !((Tarifa)this._ventana.Tarifa).Id.Equals("NGN") ? (Tarifa)this._ventana.Tarifa : null;
                asociado.Etiqueta = !((Etiqueta)this._ventana.Etiqueta).Id.Equals("NGN") ? (Etiqueta)this._ventana.Etiqueta : null;

               bool exitoso = this._asociadoServicios.InsertarOModificar(asociado, UsuarioLogeado.Hash);
                
               if (exitoso)
                    this.Navegar(Recursos.MensajesConElUsuario.AsociadoInsertado, false);
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
