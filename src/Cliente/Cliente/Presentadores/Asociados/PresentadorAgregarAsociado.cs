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
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using System.Text.RegularExpressions;
using Trascend.Bolet.ControlesByT.Ventanas;

using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacAsociadoMarcaPatentes;

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
        private IListaDatosDominioServicios _listaDatosDominioServicios;
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
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarAsociado,
                    Recursos.Ids.AgregarAsociado);

                IList<DetallePago> detallesPagos = this._detallePagoServicios.ConsultarTodos();
                DetallePago primerDetallePago = new DetallePago();
                primerDetallePago.Id = "NGN";
                detallesPagos.Insert(0, primerDetallePago);
                this._ventana.DetallesPagos = detallesPagos;

                IList<Etiqueta> etiquetas = this._etiquetaServicios.ConsultarTodos();
                Etiqueta primeraEtiqueta = new Etiqueta();
                primeraEtiqueta.Id = string.Empty;
                etiquetas.Insert(0, primeraEtiqueta);
                this._ventana.Etiquetas = etiquetas;

                IList<Tarifa> tarifas = this._tarifaServicios.ConsultarTodos();
                Tarifa primeraTarifa = new Tarifa();
                primeraTarifa.Id = "NGN";
                tarifas.Insert(0, primeraTarifa);
                this._ventana.Tarifas = tarifas;

                IList<ListaDatosDominio> tiposPersona = this._listaDatosDominioServicios.ConsultarListaDatosDominioPorParametro(new ListaDatosDominio("PERSONA"));
                this._ventana.TipoPersonas = tiposPersona;

                IList<Moneda> monedas = this._monedaServicios.ConsultarTodos();
                Moneda primerMoneda = new Moneda();
                primerMoneda.Id = "NGN";
                monedas.Insert(0, primerMoneda);
                this._ventana.Monedas = monedas;

                IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
                Idioma primerIdioma = new Idioma();
                primerIdioma.Id = "NGN";
                idiomas.Insert(0, primerIdioma);
                this._ventana.Idiomas = idiomas;


                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                Pais primerPais = new Pais();
                primerPais.Id = int.MinValue;
                paises.Insert(0, primerPais);
                this._ventana.Paises = paises;

                this._ventana.TiposClientes = this._tipoClienteServicios.ConsultarTodos();

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
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Asociado asociado = (Asociado)this._ventana.Asociado;

                if (ValidarCamposObligatorios())
                {
                    asociado.Operacion = "CREATE";
                    asociado.TipoPersona = ((ListaDatosDominio)this._ventana.TipoPersona).Id[0];
                    asociado.Pais = (Pais)this._ventana.Pais;
                    asociado.Idioma = (Idioma)this._ventana.Idioma;
                    asociado.Moneda = (Moneda)this._ventana.Moneda;
                    asociado.TipoCliente = (TipoCliente)this._ventana.TipoCliente;
                    asociado.Tarifa = !((Tarifa)this._ventana.Tarifa).Id.Equals("NGN") ? (Tarifa)this._ventana.Tarifa : null;
                    asociado.Etiqueta = !((Etiqueta)this._ventana.Etiqueta).Id.Equals("NGN") ? (Etiqueta)this._ventana.Etiqueta : null;
                    asociado.DetallePago = !((DetallePago)this._ventana.DetallePago).Id.Equals("NGN") ? (DetallePago)this._ventana.DetallePago : null;

                    int? exitoso = this._asociadoServicios.InsertarOModificarAsociado(asociado, UsuarioLogeado.Hash);

                    if (exitoso != null)
                    {
                        asociado.Id = exitoso.Value;
                        this.Navegar(new ConsultarAsociado(asociado, null, false));
                    }
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.AlertaDebeSeleccionarPaisMonedaIdioma);
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

        private bool ValidarCamposObligatorios()
        {
            bool retorno = true;

            if (((Pais)this._ventana.Pais).Id == int.MinValue)
                retorno = false;
            if (((Moneda)this._ventana.Moneda).Id == "NGN")
                retorno = false;
            if (((Idioma)this._ventana.Idioma).Id == "NGN")
                retorno = false;

            return retorno;
        }


        public void IrWebAsociado()
        {
            if (!string.IsNullOrEmpty(((Asociado)this._ventana.Asociado).Web))
            {
                Match match = Regex.Match(((Asociado)this._ventana.Asociado).Web, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$",
        RegexOptions.IgnoreCase);
                if (match.Success)
                    IrURL(((Asociado)this._ventana.Asociado).Web);
                else
                    this._ventana.Mensaje("Disculpe, URL errónea");
            }
        }


        public void VerEtiqueta()
        {
            if (((Etiqueta)this._ventana.Etiqueta).Id != string.Empty)
            {
                string stringAMostrar = "Español:" +
                                        Environment.NewLine +
                                        ((Etiqueta)this._ventana.Etiqueta).Descripcion1 +
                                        Environment.NewLine + "Inglés: " +
                                        Environment.NewLine +
                                        ((Etiqueta)this._ventana.Etiqueta).Descripcion2;

                ChildWindow detalle = new ChildWindow(stringAMostrar);
                detalle.ShowDialog();
            }

        }

        public void IrVentanaImprimirEdoCuenta()
        {
            if ((Asociado)this._ventana.Asociado != null)
            {
                Asociado Asociado = ((Asociado)this._ventana.Asociado).Id != int.MinValue ? (Asociado)this._ventana.Asociado : null;
                Navegar(new EstadoCuentas("2", Asociado));

            }
        }

        public void IrVentanaCXPINTDatos()
        {
            if ((Asociado)this._ventana.Asociado != null)
            {
                Asociado Asociado = ((Asociado)this._ventana.Asociado).Id != int.MinValue ? (Asociado)this._ventana.Asociado : null;
                Navegar(new ConsultarFacVistaFacturacionCxpInternas(Asociado));

            }
        }

        public void CalcularSaldos()
        {
            if ((Asociado)this._ventana.Asociado != null)
            {
                Asociado Asociado = ((Asociado)this._ventana.Asociado).Id != int.MinValue ? (Asociado)this._ventana.Asociado : null;

                double? w_1, w_2, w_3, w_4, w_5, w_6, msaldope;
                w_1 = 0;
                w_2 = 0;
                w_3 = 0;
                w_4 = 0;
                w_5 = 0;
                w_6 = 0;
                msaldope = 0;
                string moneda = "";
                int casociado = Asociado.Id;
                int? dias = 30;
                CalcularSaldosAsociado(casociado, dias, ref w_1, ref w_2, ref w_3, ref w_4, ref w_5, ref w_6, ref msaldope, ref  moneda);

                if (moneda == "US")
                {

                    this._ventana.SaldoVencidoSolicitud = System.Convert.ToString(w_2);
                    this._ventana.SaldoPorVencerSolicitud = System.Convert.ToString(w_4);
                    this._ventana.TotalSolicitud = System.Convert.ToString(w_2 + w_4);

                }
                else
                {
                    this._ventana.SaldoVencidoSolicitud = System.Convert.ToString(w_1);
                    this._ventana.SaldoPorVencerSolicitud = System.Convert.ToString(w_3);
                    this._ventana.TotalSolicitud = System.Convert.ToString(w_1 + w_3);
                }
            }

        }
    }
}
