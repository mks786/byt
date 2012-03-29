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
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorConsultarMarcas : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarMarcas _ventana;

        private IMarcaServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private ICorresponsalServicios _corresponsalServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;
        private IPaisServicios _paisServicios;

        private IList<Marca> _marcas;
        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Corresponsal> _corresponsales;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarMarcas(IConsultarMarcas ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._corresponsalServicios = (ICorresponsalServicios)Activator.GetObject(typeof(ICorresponsalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CorresponsalServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                this._tipoEstadoServicios = (ITipoEstadoServicios)Activator.GetObject(typeof(ITipoEstadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEstadoServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);

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
        /// Método que se encarga de actualizar el títutlo de la ventana
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcas,
                Recursos.Ids.ConsultarMarcas);

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

                IList<Asociado> asociados = new List<Asociado>();
                Asociado primerAsociado = new Asociado();
                primerAsociado.Id = int.MinValue;
                asociados.Insert(0, primerAsociado);
                this._ventana.Asociados = asociados;
                this._asociados = asociados;

                IList<Interesado> interesados = new List<Interesado>();
                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                interesados.Insert(0, primerInteresado);
                this._ventana.Interesados = interesados;
                this._interesados = interesados;

                IList<Corresponsal> corresponsales = this._corresponsalServicios.ConsultarTodos();
                Corresponsal primerCorresponsal = new Corresponsal();
                primerCorresponsal.Id = int.MinValue;
                corresponsales.Insert(0, primerCorresponsal);
                this._ventana.Corresponsales = corresponsales;
                this._corresponsales = corresponsales;

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                Pais primerPais = new Pais();
                primerPais.Id = int.MinValue;
                paises.Insert(0, primerPais);
                this._ventana.Paises = paises;

                IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                TipoEstado primerDetalle = new TipoEstado();
                primerDetalle.Id = "NGN";
                tipoEstados.Insert(0, primerDetalle);
                this._ventana.Detalles = tipoEstados;

                IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
                Servicio primerServicio = new Servicio();
                primerServicio.Id = "NGN";
                servicios.Insert(0, primerServicio);
                this._ventana.Servicios = servicios;
                //this._ventana.Servicio = this.BuscarServicio(servicios, marca.Servicio);

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

                Marca MarcaAuxiliar = new Marca();

                if (!this._ventana.Id.Equals(""))
                {
                    filtroValido = 2;
                    MarcaAuxiliar.Id = int.Parse(this._ventana.Id);
                }

                if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                {
                    MarcaAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                    filtroValido = 2;
                }

                if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                {
                    MarcaAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                    filtroValido = 2;
                }


                //if (!this._ventana.FichasFiltrar.Equals(""))
                //{
                //    MarcaAuxiliar.Fichas = this._ventana.FichasFiltrar.ToUpper();
                //    filtroValido++;
                //}


                if (!this._ventana.DescripcionFiltrar.Equals(""))
                {
                    filtroValido = 2;
                    MarcaAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                }

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    MarcaAuxiliar.FechaPublicacion = fechaPublicacion;
                }

                if (filtroValido >= 2)
                {
                    this._marcas = this._marcaServicios.ObtenerMarcasFiltro(MarcaAuxiliar);

                    IList<Marca> marcasDesinfladas = new List<Marca>();

                    foreach (var marca in this._marcas)
                    {
                        MarcaAuxiliar = new Marca(marca.Id);
                        Asociado asociadoAuxiliar = new Asociado();
                        Interesado interesadoAuxiliar = new Interesado();

                        MarcaAuxiliar.Descripcion = marca.Descripcion != null ? marca.Descripcion : "";

                        if ((marca.Asociado != null) && (!string.IsNullOrEmpty(marca.Asociado.Nombre)))
                        {
                            asociadoAuxiliar.Nombre = marca.Asociado.Nombre;
                            MarcaAuxiliar.Asociado = asociadoAuxiliar;
                        }

                        if ((marca.Interesado != null) && (!string.IsNullOrEmpty(marca.Interesado.Nombre)))
                        {
                            interesadoAuxiliar.Nombre = marca.Interesado.Nombre;
                            MarcaAuxiliar.Interesado = interesadoAuxiliar;
                        }

                        MarcaAuxiliar.FechaPublicacion = marca.FechaPublicacion != null ? marca.FechaPublicacion : null;

                        marcasDesinfladas.Add(MarcaAuxiliar);

                    }

                    this._ventana.Resultados = marcasDesinfladas;
                    this._ventana.TotalHits = marcasDesinfladas.Count.ToString();
                    if (marcasDesinfladas.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);

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
        /// Método que invoca una nueva página "ConsultarPoder" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.MarcaSeleccionada != null)
            {
                Marca marcaParaNavegar = null;
                bool encontrada = false;
                int cont = 0;
                while (!encontrada)
                {
                    Marca marca = this._marcas[cont];
                    if (marca.Id == ((Marca)this._ventana.MarcaSeleccionada).Id)
                    {
                        marcaParaNavegar = marca;
                        encontrada = true;
                    }
                    cont++;
                }


                this.Navegar(new ConsultarMarca(marcaParaNavegar));
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

        //public void BuscarAsociado()
        //{
        //    IEnumerable<Asociado> asociadosFiltrados = (IList<Asociado>)this._asociados;

        //    if (!string.IsNullOrEmpty(this._ventana.IdAsociadoFiltrar))
        //    {
        //        asociadosFiltrados = from p in asociadosFiltrados
        //                             where p.Id == int.Parse(this._ventana.IdAsociadoFiltrar)
        //                             select p;
        //    }

        //    if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoFiltrar))
        //    {
        //        asociadosFiltrados = from p in asociadosFiltrados
        //                             where p.Nombre != null &&
        //                             p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoFiltrar.ToLower())
        //                             select p;
        //    }

        //    if (asociadosFiltrados.ToList<Asociado>().Count != 0)
        //        this._ventana.Asociados = asociadosFiltrados.ToList<Asociado>();
        //    else
        //        this._ventana.Asociados = this._asociados;
        //}

        //public void BuscarInteresado()
        //{
        //    IEnumerable<Interesado> interesadosFiltrados = (IList<Interesado>)this._interesados;

        //    if (!string.IsNullOrEmpty(this._ventana.IdInteresadoFiltrar))
        //    {
        //        interesadosFiltrados = from p in interesadosFiltrados
        //                             where p.Id == int.Parse(this._ventana.IdInteresadoFiltrar)
        //                             select p;
        //    }

        //    if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoFiltrar))
        //    {
        //        interesadosFiltrados = from p in interesadosFiltrados
        //                             where p.Nombre != null &&
        //                             p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoFiltrar.ToLower())
        //                             select p;
        //    }

        //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
        //        this._ventana.Interesados = interesadosFiltrados.ToList<Interesado>();
        //    else
        //        this._ventana.Interesados = this._interesados;
        //}

        #region Interesado

        /// <summary>
        /// Método que se encarga de buscar el interesado definido en el filtro
        /// </summary>
        public void BuscarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Interesado interesadoABuscar = new Interesado();

            interesadoABuscar.Id = !this._ventana.IdInteresadoFiltrar.Equals("") ?
                                   int.Parse(this._ventana.IdInteresadoFiltrar) : 0;

            interesadoABuscar.Nombre = !this._ventana.NombreInteresadoFiltrar.Equals("") ?
                                       this._ventana.NombreInteresadoFiltrar.ToUpper() : "";

            if ((interesadoABuscar.Id != 0) || !(interesadoABuscar.Nombre.Equals("")))
            {
                IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                interesados.Insert(0,new Interesado(int.MinValue));
                this._ventana.Interesados = interesados;
            }
            else
            {
                this._ventana.Interesados = this._interesados;
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Metodo que cambia el texto del interesado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el interesado haya sido valido, false en caso contrario</returns>
        public bool CambiarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Interesado != null)
            {
                this._ventana.InteresadoFiltro = ((Interesado)this._ventana.Interesado).Nombre;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;

        }

        #endregion


        #region Asociado

        /// <summary>
        /// Método que se encarga de buscar el asociado definido en el filtro
        /// </summary>
        public void BuscarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Asociado asociadoABuscar = new Asociado();

            asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                                 int.Parse(this._ventana.IdAsociadoFiltrar) : 0;

            asociadoABuscar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ?
                                     this._ventana.NombreAsociadoFiltrar.ToUpper() : "";

            if ((asociadoABuscar.Id != 0) || !(asociadoABuscar.Nombre.Equals("")))
            {
                IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);
                asociados.Insert(0, new Asociado(int.MinValue));
                this._ventana.Asociados = asociados;

            }
            else
            {
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                this._ventana.Asociados = this._asociados;
            }

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Metodo que cambia el texto del Asociado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el Asociado haya sido valido, false en caso contrario</returns>
        public bool CambiarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Asociado != null)
            {
                this._ventana.AsociadoFiltro = ((Asociado)this._ventana.Asociado).Nombre;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        #endregion


        #region Corresponsal

        /// <summary>
        /// Método que se encarga de buscar el asociado definido en el filtro
        /// </summary>
        public void BuscarCorresponsal()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Corresponsal corresponsalABuscar = new Corresponsal();

            corresponsalABuscar.Id = !this._ventana.IdCorresponsalFiltrar.Equals("") ?
                                 int.Parse(this._ventana.IdCorresponsalFiltrar) : 0;

            corresponsalABuscar.Descripcion = !this._ventana.NombreCorresponsalFiltrar.Equals("") ?
                                     this._ventana.NombreCorresponsalFiltrar.ToUpper() : "";

            if ((corresponsalABuscar.Id != 0) || !(corresponsalABuscar.Descripcion.Equals("")))
            {
                IList<Corresponsal> corresponsales = this._corresponsalServicios.ConsultarTodos();
                corresponsales.Insert(0, new Corresponsal(int.MinValue));
                this._ventana.Corresponsales = corresponsales;

            }
            else
            {
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                this._ventana.Corresponsales = this._asociados;
            }

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Metodo que cambia el texto del Corresponsal en la interfaz
        /// </summary>
        /// <returns>true en caso de que el Corresponsal haya sido valido, false en caso contrario</returns>
        public bool CambiarCorresponsal()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Corresponsal != null)
            {
                this._ventana.CorresponsalFiltro = ((Corresponsal)this._ventana.Corresponsal).Descripcion;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        #endregion
    }
}
