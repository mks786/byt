using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales
{
    class PresentadorConsultarSolicitudesMateriales : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarSolicitudesMateriales _ventana;
        private IDepartamentoServicios _departamentoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private ISolicitudSapiServicios _solicitudSapiServicios;
        private IMaterialSapiServicios _materialSapiServicios;
        private int _filtroValido;

        /// <summary>
        /// Constructor predeterminado que recibe una Solicitud Sapi y una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana Actual</param>
        /// <param name="solicitudSapi">Solicitud Sapi seleccionada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorConsultarSolicitudesMateriales(IConsultarSolicitudesMateriales ventana)
        {

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                
                this._materialSapiServicios = (IMaterialSapiServicios)Activator.GetObject(typeof(IMaterialSapiServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaterialSapiServicios"]);
                this._solicitudSapiServicios = (ISolicitudSapiServicios)Activator.GetObject(typeof(ISolicitudSapiServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SolicitudSapiServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarSolicitudSAPI,
                Recursos.Ids.GestionarMovimientoMaterialSAPI);
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

                CargarCombos();

                SolicitudSapi solicitudSapi = new SolicitudSapi();

                this._ventana.SolicitudSapiFiltro = solicitudSapi;

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
        /// Metodo que carga los combos de la ventana actual
        /// </summary>
        private void CargarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<MaterialSapi> materiales = this._materialSapiServicios.ConsultarTodos();
                materiales.Insert(0, new MaterialSapi("NGN"));
                this._ventana.MaterialesSapi = materiales;

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                departamentos.Insert(0, new Departamento("NGN"));
                this._ventana.Departamentos = departamentos;

                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                usuarios.Insert(0, new Usuario("NGN"));
                this._ventana.Usuarios = usuarios;

                IList<ListaDatosValores> statusMaterialesEnSolicitud = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiStatusMovimientoMaterialSAPI));
                statusMaterialesEnSolicitud.Insert(0, new ListaDatosValores("NGN"));
                this._ventana.StatusSolicitudesSapi = statusMaterialesEnSolicitud;

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
        /// Metodo que inicializa la ventana actual
        /// </summary>
        public void LimpiarCampos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ConsultarSolicitudesMateriales());

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

        public void Consultar()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                SolicitudSapi solicitudAux = new SolicitudSapi();
                this._filtroValido = 0;

                solicitudAux = ObtenerSolicitudFiltroPantalla();

                if (this._filtroValido >= 2)
                {
                    IList<SolicitudSapi> solicitudesSapi = this._solicitudSapiServicios.ObtenerSolicitudesSapiFiltro(solicitudAux);
                    if (solicitudesSapi.Count > 0)
                    {
                        this._ventana.Resultados = solicitudesSapi;
                        this._ventana.TotalHits = solicitudesSapi.Count.ToString();
                    }
                    else
                    {
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 0);
                        this._ventana.TotalHits = "0";
                    }
                }
                else
                    this._ventana.Mensaje("Debe seleccionar al menos un filtro para realizar la consulta de Solicitudes Sapi", 0);


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
        /// Metodo que obtiene la Solicitud Sapi para hacer la consulta
        /// </summary>
        /// <returns>Solicitud Sapi filtro</returns>
        private SolicitudSapi ObtenerSolicitudFiltroPantalla()
        {

            SolicitudSapi solicitud = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                solicitud = (SolicitudSapi)this._ventana.SolicitudSapiFiltro;

                if((this._ventana.FechaSolicitudSapi != null) && (!this._ventana.FechaSolicitudSapi.Equals("")))
                {
                    solicitud.FechaSolicitud = DateTime.Parse(this._ventana.FechaSolicitudSapi);
                    this._filtroValido = 2;
                }

                if ((this._ventana.MaterialSapi != null) && (!((MaterialSapi)this._ventana.MaterialSapi).Id.Equals("NGN")))
                {
                    solicitud.Material = (MaterialSapi)this._ventana.MaterialSapi;
                    this._filtroValido = 2;
                }

                if ((this._ventana.Usuario != null) && (!((Usuario)this._ventana.Usuario).Id.Equals("NGN")))
                {
                    solicitud.SolicitanteInic = ((Usuario)this._ventana.Usuario).Iniciales;
                    this._filtroValido = 2;
                }

                if ((this._ventana.Departamento != null) && (!((Departamento)this._ventana.Departamento).Id.Equals("NGN")))
                {
                    solicitud.Departamento = (Departamento)this._ventana.Departamento;
                    this._filtroValido = 2;
                }

                if ((this._ventana.StatusSolicitudSapi != null) && (!((ListaDatosValores)this._ventana.StatusSolicitudSapi).Id.Equals("NGN")))
                {
                    if (((ListaDatosValores)this._ventana.StatusSolicitudSapi).Valor.Equals("Solicitado"))
                    {
                        solicitud.MaterialSolicitado = 'T';
                    }
                    else if (((ListaDatosValores)this._ventana.StatusSolicitudSapi).Valor.Equals("Entregado"))
                    {
                        solicitud.MaterialEntregado = 'T';
                    }
                    else if (((ListaDatosValores)this._ventana.StatusSolicitudSapi).Valor.Equals("Recibido"))
                    {
                        solicitud.MaterialRecibido = 'T';
                    }

                    this._filtroValido = 2;

                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return solicitud;
        }


        /// <summary>
        /// Presenta la solicitud Sapi seleccionada con todo y su detalle
        /// </summary>
        public void VerSolicitudSapi()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.SolicitudSapiSeleccionada != null)
                {
                    this.Navegar(new GestionarMovimientoMaterialSapi((SolicitudSapi)this._ventana.SolicitudSapiSeleccionada,this._ventana));
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
        /// Metodo que exportar a Excel el resultado de la Consulta que se muestra en Pantalla
        /// </summary>
        public void ExportarResultadosExcel()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<SolicitudSapi> datosResultadosConsulta;
                DataTable datosExportar = CrearDataTableExportacion();
                datosResultadosConsulta = (IList<SolicitudSapi>)this._ventana.Resultados;
                datosExportar = LlenarDataTableExportacion(datosResultadosConsulta, datosExportar);

                this._ventana.ExportarDatosConsolidadosExcel(datosExportar);
                
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
        /// Metodo que crea el DataTable necesario para exportar los datos resultantes de la consulta
        /// </summary>
        /// <returns></returns>
        private DataTable CrearDataTableExportacion()
        {

            DataTable datos = new DataTable();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                datos.Columns.Add("Solicitud No", typeof(int));
                datos.Columns.Add("Fecha", typeof(DateTime));
                datos.Columns.Add("Cod Material", typeof(string));
                datos.Columns.Add("Material", typeof(string));
                datos.Columns.Add("Cant Solicitada", typeof(int));
                datos.Columns.Add("Solicitado", typeof(string));
                datos.Columns.Add("Entregado", typeof(string));
                datos.Columns.Add("Recibido", typeof(string));
                
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return datos;
        }


        private DataTable LlenarDataTableExportacion(IList<SolicitudSapi> datosResultadosConsulta, DataTable datosExportar)
        {

            DataTable datos = datosExportar;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (SolicitudSapi item in datosResultadosConsulta)
                {
                    DataRow filaNueva = datos.NewRow();
                    filaNueva["Solicitud No"] = item.Id;
                    filaNueva["Fecha"] = item.FechaSolicitud;
                    filaNueva["Cod Material"] = item.Material.Id;
                    filaNueva["Material"] = item.Material.Descripcion;
                    filaNueva["Cant Solicitada"] = item.CantMaterialSol;
                    if (item.BSolicitado)
                        filaNueva["Solicitado"] = "SI";
                    else
                        filaNueva["Solicitado"] = "NO";

                    if(item.BEntregado)
                        filaNueva["Entregado"] = "SI";
                    else
                        filaNueva["Entregado"] = "NO";

                    if (item.BRecibido)
                        filaNueva["Recibido"] = "SI";
                    else
                        filaNueva["Recibido"] = "NO";
                    
                    datos.Rows.Add(filaNueva);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return datos;
        }


        /// <summary>
        /// Metodo que obtiene el nombre del Usuario que genera el reporte
        /// </summary>
        /// <returns>Nombre del usuario logueado</returns>
        public string ObtenerNombreUsuario()
        {
            return UsuarioLogeado.NombreCompleto;
        }
    }
}
