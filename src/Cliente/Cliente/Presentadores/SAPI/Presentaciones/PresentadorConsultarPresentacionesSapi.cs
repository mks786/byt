using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Presentaciones;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Presentaciones
{
    class PresentadorConsultarPresentacionesSapi : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarPresentacionesSapi _ventana;
        private IPresentacionSapiServicios _presentacionSapiServicios;
        private IPresentacionSapiDetalleServicios _presentacionSapiDetalleServicios;
        private IMaterialSapiServicios _materialSapiServicios;
        private IUsuarioServicios _usuariosServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IDepartamentoServicios _departamentoServicios;

        private int _filtroValido;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorConsultarPresentacionesSapi(IConsultarPresentacionesSapi ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;

                this._presentacionSapiServicios = (IPresentacionSapiServicios)Activator.GetObject(typeof(IPresentacionSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PresentacionSapiServicios"]);
                this._presentacionSapiDetalleServicios = (IPresentacionSapiDetalleServicios)Activator.GetObject(typeof(IPresentacionSapiDetalleServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PresentacionSapiDetalleServicios"]);
                this._materialSapiServicios = (IMaterialSapiServicios)Activator.GetObject(typeof(IMaterialSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaterialSapiServicios"]);
                this._usuariosServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarPresentacionesSapi,
                Recursos.Ids.ConsultarPresentacionesSAPI);
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

                ActualizarTitulo();

                LlenarCombos();

                this._ventana.TotalHits = "0";

                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }

        }


        /// <summary>
        /// Metodo que llena los combos de los filtros de la ventana
        /// </summary>
        private void LlenarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                departamentos.Insert(0, new Departamento("NGN"));
                this._ventana.DptosSolicitudPresentacion = departamentos;

                IList<Usuario> usuarios = this._usuariosServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                usuarios.Insert(0, new Usuario("NGN"));
                this._ventana.UsuariosPresentacion = usuarios;

                IList<MaterialSapi> documentos = this._materialSapiServicios.ConsultarTodos();
                documentos = documentos.OrderBy(o => o.Descripcion).ToList();
                documentos.Insert(0, new MaterialSapi("NGN"));
                this._ventana.DctosPresentacion = documentos;

                IList<ListaDatosValores> status = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiStatusDocumentoPresentacionSAPI));
                status.Insert(0, new ListaDatosValores("NGN"));
                this._ventana.StatusPresentacion = status;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Metodo que inicializar la ventana de consulta de presentaciones 
        /// </summary>
        public void LimpiarCampos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ConsultarPresentacionesSapi());

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que ordena una columna del Listview de la consulta resultante
        /// </summary>
        /// <param name="column">Columna a ordenar</param>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que muestra una Solicitud de Presentacion SAPI
        /// </summary>
        public void VerSolicitudPresentacionSAPI()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.SolicitudPresentacionSeleccionada != null)
                {
                    PresentacionSapi EncabezadoPresentacion = 
                        ((PresentacionSapiDetalle)this._ventana.SolicitudPresentacionSeleccionada).Presentacion_Enc;
                    this.Navegar(new GestionarSolicitudPresentacion(EncabezadoPresentacion, this._ventana));
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que obtiene los documentos que se encuentran para presentacion ante SAPI
        /// </summary>
        public void Consultar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._filtroValido = 0;

                PresentacionSapiDetalle filtroAuxiliar = new PresentacionSapiDetalle();
                filtroAuxiliar = ObtenerFiltroPantalla();

                if (this._filtroValido >= 2)
                {
                    IList<PresentacionSapiDetalle> solicitudesPresentacionResultantes = 
                        this._presentacionSapiDetalleServicios.ObtenerSolicitudesPresentacionSapiFiltro(filtroAuxiliar);
                    if (solicitudesPresentacionResultantes.Count > 0)
                    {
                        this._ventana.Resultados = solicitudesPresentacionResultantes;
                        this._ventana.TotalHits = solicitudesPresentacionResultantes.Count.ToString();
                    }
                    else
                    {
                        this._ventana.TotalHits = "0";
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 0);
                    }
                }
                else
                    this._ventana.Mensaje("Seleccione al menos un filtro para relizar la consulta", 0);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que obtiene el filtro a aplicar en el servicio desde la pantalla
        /// </summary>
        /// <returns>PresentacionSapiDetalle que sirve de filtro para la consulta</returns>
        private PresentacionSapiDetalle ObtenerFiltroPantalla()
        {

            PresentacionSapiDetalle presentacion_Detalle = new PresentacionSapiDetalle();
            PresentacionSapi presentacion_Enc = new PresentacionSapi();
            MaterialSapi documento = new MaterialSapi();
            presentacion_Detalle.Presentacion_Enc = presentacion_Enc;
            presentacion_Detalle.Material = documento;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._ventana.FechaSolicitudPresentacion.Equals(""))
                {
                    presentacion_Detalle.Presentacion_Enc.Fecha = DateTime.Parse(this._ventana.FechaSolicitudPresentacion);
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.Presentacion_Enc.Fecha = null;

                if ((this._ventana.DptoSolicitudPresentacion != null) && (!((Departamento)this._ventana.DptoSolicitudPresentacion).Id.Equals("NGN")))
                {
                    presentacion_Detalle.Presentacion_Enc.Departamento = (Departamento)this._ventana.DptoSolicitudPresentacion;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.Presentacion_Enc.Departamento = null;

                if ((this._ventana.UsuarioPresentacion != null) && (!((Usuario)this._ventana.UsuarioPresentacion).Id.Equals("NGN")))
                {
                    presentacion_Detalle.Presentacion_Enc.Iniciales = ((Usuario)this._ventana.UsuarioPresentacion).Iniciales;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.Presentacion_Enc.Iniciales = null;

                if ((this._ventana.DctoPresentacion != null) && (!((MaterialSapi)this._ventana.DctoPresentacion).Id.Equals("NGN")))
                {
                    presentacion_Detalle.Material.Id = ((MaterialSapi)this._ventana.DctoPresentacion).Id;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.Material = null;

                if (!this._ventana.CodigoExpPresentacion.Equals(""))
                {
                    presentacion_Detalle.CodExpediente = this._ventana.CodigoExpPresentacion;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.CodExpediente = null;

                if ((this._ventana.StatusPresentacionSeleccionado != null) && (!((ListaDatosValores)this._ventana.StatusPresentacionSeleccionado).Id.Equals("NGN")))
                {
                    presentacion_Detalle.StatusDocumento = ((ListaDatosValores)this._ventana.StatusPresentacionSeleccionado).Descripcion;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.StatusDocumento = null;


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return presentacion_Detalle;
        }
    }
}
