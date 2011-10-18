using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Asociados
{
    class PresentadorConsultarAsociados : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarAsociados _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IDetallePagoServicios _detallePagoServicios;
        private IEtiquetaServicios _etiquetaServicios;
        private IIdiomaServicios _idiomaServicios;
        private IMonedaServicios _monedaServicios;
        private ITarifaServicios _tarifaServicios;
        private ITipoClienteServicios _tipoClienteServicios;
        private IPaisServicios _paisServicios;
        private IList<Asociado> _asociados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarAsociados(IConsultarAsociados ventana)
        {
            try
            {
                this._ventana = ventana;
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

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAsociados,
            Recursos.Ids.ConsultarAsociados);
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

                this._asociados = this._asociadoServicios.ConsultarTodos();
                this._ventana.Resultados = this._asociados;
                this._ventana.AsociadoFiltrar = new Asociado();

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
                Tarifa primeraTarifa = new Tarifa();
                primeraTarifa.Id = "NGN";
                tarifas.Insert(0, primeraTarifa);
                this._ventana.Tarifas = tarifas;

                IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
                Idioma primerIdioma = new Idioma();
                primerIdioma.Id = "NGN";
                idiomas.Insert(0, primerIdioma);
                this._ventana.Idiomas = idiomas;

                IList<Moneda> monedas = this._monedaServicios.ConsultarTodos();
                Moneda primeraMoneda = new Moneda();
                primeraMoneda.Id = "NGN";
                monedas.Insert(0, primeraMoneda);
                this._ventana.Monedas = monedas;

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                Pais primerPais = new Pais();
                primerPais.Id = int.MinValue;
                paises.Insert(0, primerPais);
                this._ventana.Paises = paises;

                IList<TipoCliente> tiposCLientes = this._tipoClienteServicios.ConsultarTodos();
                TipoCliente primerTipoCliente = new TipoCliente();
                primerTipoCliente.Id = "NGN";
                tiposCLientes.Insert(0, primerTipoCliente);
                this._ventana.TiposClientes = tiposCLientes;

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
        /// Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        /// por pantalla
        /// </summary>
        public void Consultar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Asociado asociado = (Asociado) this._ventana.AsociadoFiltrar;

                IEnumerable<Asociado> asociadosFiltrados = this._asociados;

                if (!string.IsNullOrEmpty(this._ventana.Id))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.Id == int.Parse(this._ventana.Id)
                                         select a;
                }

                if (!string.IsNullOrEmpty(asociado.Nombre))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                       where a.Nombre != null &&
                                       a.Nombre.ToLower().Contains(asociado.Nombre.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(asociado.Domicilio))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                       where a.Domicilio != null &&
                                       a.Domicilio.ToLower().Contains(asociado.Domicilio.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(this._ventana.TipoPersona.ToString()) && !this._ventana.TipoPersona.Equals(' '))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.TipoPersona == this._ventana.TipoPersona
                                         select a;
                }

                if (this._ventana.Pais != null && ((Pais)this._ventana.Pais).Id != int.MinValue)
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.Pais != null &&  
                                         a.Pais.Id == ((Pais)this._ventana.Pais).Id
                                        select a;

                }

                if (this._ventana.Idioma != null && !((Idioma)this._ventana.Idioma).Id.Equals("NGN"))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.Idioma != null && 
                                         a.Idioma.Id.Contains(((Idioma)this._ventana.Idioma).Id)
                                         select a;

                }

                if (this._ventana.Moneda != null && !((Moneda)this._ventana.Moneda).Id.Equals("NGN"))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.Moneda != null && 
                                         a.Moneda.Id.Contains(((Moneda)this._ventana.Moneda).Id)
                                         select a;

                }

                if (this._ventana.Tarifa != null && !((Tarifa)this._ventana.Tarifa).Id.Equals("NGN"))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.Tarifa != null &&  
                                         a.Tarifa.Id.Contains(((Tarifa)this._ventana.Tarifa).Id)
                                         select a;

                }

                if (this._ventana.TipoCliente != null && !((TipoCliente)this._ventana.TipoCliente).Id.Equals("NGN"))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.TipoCliente != null &&
                                         a.TipoCliente.Id.Contains(((TipoCliente)this._ventana.TipoCliente).Id)
                                         select a;

                }

                if (this._ventana.Etiqueta != null && !((Etiqueta)this._ventana.Etiqueta).Id.Equals("NGN"))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.Etiqueta != null && 
                                         a.Etiqueta.Id.Contains(((Etiqueta)this._ventana.Etiqueta).Id)
                                         select a;

                }

                if (this._ventana.DetallePago != null && !((DetallePago)this._ventana.DetallePago).Id.Equals("NGN"))
                {
                    asociadosFiltrados = from a in asociadosFiltrados
                                         where a.DetallePago != null && 
                                         a.DetallePago.Id.Contains(((DetallePago)this._ventana.DetallePago).Id)
                                         select a;

                }


                this._ventana.Resultados = asociadosFiltrados.ToList<Asociado>();

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
        /// Método que invoca una nueva página "ConsultarAsociado" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if(this._ventana.AsociadoSeleccionado != null)
                this.Navegar(new ConsultarAsociado(this._ventana.AsociadoSeleccionado));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                this._ventana.ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            this._ventana.ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
