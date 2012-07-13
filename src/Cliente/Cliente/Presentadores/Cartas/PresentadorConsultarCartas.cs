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
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Cartas
{
    class PresentadorConsultarCartas : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _precargada = false;
        private Asociado _asociadoAFiltrar; //Asociado que es pasado a esta ventana para filtrar directamente
        private object _ventanaAVolver; //Asociado que es pasado a esta ventana para filtrar directamente

        int _posicion;
        private IConsultarCartas _ventana;
        private ICartaServicios _cartaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IList<Carta> _cartas;
        private IList<Asociado> _asociados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarCartas(IConsultarCartas ventana, object asociado, object ventanaAVolver)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                if (null != asociado)
                {
                    _precargada = true;
                    _asociadoAFiltrar = (Asociado)asociado;
                    _ventanaAVolver = ventanaAVolver;
                }

                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);

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
        /// Método que se encarga de actualizar el título de la ventana Consultar Carta
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarCartas,
                Recursos.Ids.ConsultarCartas);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
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

                if (_precargada)
                {
                    Carta carta = new Carta();
                    carta.Asociado = _asociadoAFiltrar;
                    IList<Asociado> _asociados = new List<Asociado>();
                    this._ventana.Asociados = _asociados;
                    _asociados.Add(carta.Asociado);
                    this._ventana.Asociado = carta.Asociado;
                    this._ventana.Resultados = this._cartaServicios.ObtenerCartasFiltro(carta);
                    this._ventana.TotalHits = ((IList<Carta>)this._ventana.Resultados).Count.ToString();
                }
                else
                {
                    this._ventana.TotalHits = "0";
                }
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

                Mouse.OverrideCursor = Cursors.Wait;

                bool consultaResumen = false;
                int filtroValido = 0;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                //dos filtros sean utilizados

                Carta cartaAuxiliar = new Carta();

                if (!this._ventana.Id.Equals(""))
                {
                    filtroValido = 2;
                    cartaAuxiliar.Id = int.Parse(this._ventana.Id);
                }

                if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                {
                    cartaAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                    filtroValido++;
                }


                if (!this._ventana.ResumenFiltrar.Equals(""))
                {
                    filtroValido++;
                    consultaResumen = true;
                    Resumen resumenAux = new Resumen();
                    resumenAux.Descripcion = this._ventana.ResumenFiltrar;
                    cartaAuxiliar.Resumen = resumenAux;
                }


                if (!this._ventana.ReferenciaFiltrar.Equals(""))
                {
                    filtroValido++;
                    consultaResumen = true;
                    cartaAuxiliar.Referencia = this._ventana.ReferenciaFiltrar;
                }

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaCarta = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    cartaAuxiliar.Fecha = fechaCarta;
                }

                if (filtroValido != 0)
                {
                    this._cartas = new List<Carta>();
                    this._cartas = this._cartaServicios.ObtenerCartasFiltro(cartaAuxiliar);
                    this._ventana.Resultados = this._cartas;
                    this._ventana.TotalHits = this._cartas.Count.ToString();

                    if (this._cartas.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
                Mouse.OverrideCursor = null;
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
        /// Método que invoca una nueva página "ConsultarPoder" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarCarta()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (((List<Carta>)this._ventana.Resultados).Count == 1)
            {
                if (this._ventana.CartaSeleccionado != null)
                    if (_precargada)
                        this.Navegar(new ConsultarCarta(this._ventana.CartaSeleccionado, this._ventana));
                    else
                        this.Navegar(new ConsultarCarta(this._ventana.CartaSeleccionado));
            }
            else
            {
                _posicion = ((List<Carta>) this._ventana.Resultados).IndexOf((Carta) this._ventana.CartaSeleccionado);
                this.Navegar(new ConsultarCarta(this._ventana.CartaSeleccionado, this._ventana.Resultados, _posicion));
            }

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

        /// <summary>
        /// Método que se encarga de buscar un asociado con filtros
        /// </summary>
        public void BuscarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //IEnumerable<Asociado> asociadosFiltrados = (IList<Asociado>)this._asociados;

            //if (!string.IsNullOrEmpty(this._ventana.IdAsociadoFiltrar))
            //{
            //    asociadosFiltrados = from p in asociadosFiltrados
            //                         where p.Id == int.Parse(this._ventana.IdAsociadoFiltrar)
            //                         select p;
            //}

            //if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoFiltrar))
            //{
            //    asociadosFiltrados = from p in asociadosFiltrados
            //                         where p.Nombre != null &&
            //                         p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoFiltrar.ToLower())
            //                         select p;
            //}
            Asociado asociadoAFiltrar = new Asociado();
            asociadoAFiltrar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ? int.Parse(this._ventana.IdAsociadoFiltrar) : 0;
            asociadoAFiltrar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ? this._ventana.NombreAsociadoFiltrar.ToUpper() : string.Empty;


            IList<Asociado> asociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoAFiltrar);

            if (asociadosFiltrados.Count != 0)
                this._ventana.Asociados = asociadosFiltrados.ToList<Asociado>();
            else
                this._ventana.Asociados = this._asociados;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que limpia los campos de búsqueda
        /// </summary>
        public void LimpiarCampos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IEnumerable<Carta> cartasFiltradas = this._cartas;
            IEnumerable<Asociado> asociadosFiltrados = (IList<Asociado>)this._asociados;

            this._ventana.Resultados = null;

            this._ventana.Id = null;
            this._ventana.ResumenFiltrar = null;
            this._ventana.Fecha = null;

            this._ventana.IdAsociadoFiltrar = null;
            this._ventana.NombreAsociadoFiltrar = null;

            this._ventana.Asociados = asociadosFiltrados.ToList<Asociado>();
            this._ventana.Asociado = null;

            if (this._cartas != null)
                this._ventana.TotalHits = cartasFiltradas.ToList<Carta>().Count.ToString();
            else
                this._ventana.TotalHits = "0";

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que vuelve a una ventana cualquiera
        /// </summary>
        public void Volver()
        {
            this.Navegar((Page)_ventanaAVolver);
        }
    }
}
