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
using Trascend.Bolet.Cliente.Contratos.Recordatorios;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Recordatorios;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Recordatorios
{
    class PresentadorConsultarRecordatorios : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IList<ListaDatosValores> _listaRecordatorios;
        private IConsultarRecordatorios _ventana;
        private IMarcaServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IList<Marca> _marcas;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarRecordatorios(IConsultarRecordatorios ventana)
        {
            try
            {
                this._ventana = ventana;
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcas,
                Recursos.Ids.ConsultarRecordatorios);
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

                CargarTipoRecordatorio();
                
                //Marca MarcaAuxiliar = new Marca();
                //ListaDatosValores listaAuxiliar = this.BuscarRecordatorio(_listaRecordatorios, (ListaDatosValores)this._ventana.Recordatorio);                
                //MarcaAuxiliar.Recordatorio = int.Parse(listaAuxiliar.Valor);

                //this._marcas = this._marcaServicios.ObtenerMarcasFiltro(MarcaAuxiliar);

                this._ventana.TotalHits = "0";
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
        /// Método que carga los datos iniciales de TipoRecordatorio a mostrar en la página
        /// </summary>
        private void CargarTipoRecordatorio()
        {


            ListaDatosValores filtro = new ListaDatosValores(Recursos.Etiquetas.cbiRecordatorio);
            _listaRecordatorios =
                this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(filtro);
            this._ventana.Recordatorios = _listaRecordatorios;
            this._ventana.Recordatorio = this.BuscarRecordatorio(_listaRecordatorios, _listaRecordatorios.ElementAt(0));
        }

        public void ActualizarMarcasRecordatorio()
        {
            this._ventana.LimpiarFiltros();

            Marca MarcaAuxiliar = new Marca();
            ListaDatosValores listaAuxiliar = this.BuscarRecordatorio(_listaRecordatorios, (ListaDatosValores)this._ventana.Recordatorio);
            MarcaAuxiliar.Recordatorio = int.Parse(listaAuxiliar.Valor);

            this._marcas = this._marcaServicios.ObtenerMarcasFiltro(MarcaAuxiliar);

            this._ventana.GestionarEnableChecksFiltro(false);

            this._ventana.Resultados = _marcas;
            this._ventana.TotalHits = _marcas.Count.ToString();
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
               
                int filtroValido = 0;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                //dos filtros sean utilizados

                

                ListaDatosValores filtro = new ListaDatosValores(Recursos.Etiquetas.cbiRecordatorio);

                IEnumerable<Marca> marcasDesinfladas = new List<Marca>();

                if (this._ventana.TodosFiltro.Value)
                {
                    marcasDesinfladas = from m in _marcas
                                        select m;
                }
                else
                {
                    if (this._ventana.EmailFiltro.Value)
                    {
                        marcasDesinfladas = from m in _marcas
                                            where m.Asociado.Email != null
                                            select m;
                        filtroValido = 1;
                    }

                    if (this._ventana.FaxFiltro.Value)
                    {
                        marcasDesinfladas = from m in _marcas
                                            where m.Asociado.Fax1 != null
                                            select m;
                        filtroValido = 1;
                    }

                    if ((this._ventana.FaxFiltro.Value) && ((this._ventana.EmailFiltro.Value)))
                    {
                        marcasDesinfladas = from m in _marcas
                                            where m.Asociado.Fax1 != null && m.Asociado.Email != null
                                            select m;
                        filtroValido = 1;
                    }

                    if (!(this._ventana.FaxFiltro.Value) && (!(this._ventana.EmailFiltro.Value)))
                    {
                        marcasDesinfladas = from m in _marcas
                                            where m.Asociado.Fax1 == null && m.Asociado.Email == null
                                            select m;
                        filtroValido = 1;
                    }

                }
               
                if (!this._ventana.AnoFiltro.Equals(""))
                {                 
                    marcasDesinfladas = from m in _marcas
                                        where m.FechaRenovacion.Value.Year == int.Parse(this._ventana.AnoFiltro)
                                        select m;
                    filtroValido = 1;

                }
                
                if (!this._ventana.MesFiltro.Equals(""))
                {                 
                    marcasDesinfladas = from m in _marcas
                                        where m.FechaRenovacion.Value.Month == int.Parse(this._ventana.MesFiltro)
                                        select m;
                    filtroValido = 1;
                }

                if ((!this._ventana.AnoFiltro.Equals("")) && (!this._ventana.MesFiltro.Equals("")))
                {
                    marcasDesinfladas = from m in _marcas
                                        where m.FechaRenovacion.Value.Month == int.Parse(this._ventana.MesFiltro) &&
                                        m.FechaRenovacion.Value.Year == int.Parse(this._ventana.AnoFiltro)
                                        select m;
                }

                if ((!this._ventana.FechaDesdeFiltro.Equals("")) && (!this._ventana.FechaDesdeFiltro.Equals("")))
                {
                    marcasDesinfladas = from m in _marcas
                                        where m.FechaRenovacion >= DateTime.Parse(this._ventana.FechaDesdeFiltro) &&
                                        m.FechaRenovacion <= DateTime.Parse(this._ventana.FechaHastaFiltro)
                                        select m;
                    filtroValido = 1;
                }               

                
                this._ventana.Resultados = marcasDesinfladas;
                this._ventana.TotalHits = marcasDesinfladas.ToList<Marca>().Count.ToString();


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
