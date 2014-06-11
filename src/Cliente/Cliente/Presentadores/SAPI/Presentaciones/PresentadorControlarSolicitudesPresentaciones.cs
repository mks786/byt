using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacAsociadoMarcaPatentes;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Presentaciones;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Presentaciones
{
    class PresentadorControlarSolicitudesPresentaciones : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IControlarSolicitudesPresentaciones _ventana;
        private IPresentacionSapiServicios _presentacionSapiServicios;
        private IPresentacionSapiDetalleServicios _presentacionSapiDetalleServicios;
        private IMaterialSapiServicios _materialSapiServicios;
        private IUsuarioServicios _usuariosServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IFacVistaFacturaServicioServicios _facVistaFacturaServicioServicios;

        private int _filtroValido;
        private string _accion = String.Empty;
        private IList<PresentacionSapiDetalle> _documentosPresentaciones;
        private IList<ListaDatosValores> _statusDocumentos;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorControlarSolicitudesPresentaciones(IControlarSolicitudesPresentaciones ventana)
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
                this._facVistaFacturaServicioServicios =
                    (IFacVistaFacturaServicioServicios)Activator.GetObject(typeof(IFacVistaFacturaServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacVistaFacturaServicioServicios"]);

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleControlarPresentacionSAPI,
                Recursos.Ids.ControlPresentaciones);
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
        /// Metodo que llena los combos de los filtros de la venta actual
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
                this._ventana.Dptos = departamentos;

                IList<Usuario> usuarios = this._usuariosServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                usuarios.Insert(0, new Usuario("NGN"));
                this._ventana.Usuarios = usuarios;
                this._ventana.Gestores = usuarios;
                this._ventana.GestoresRegistro = usuarios;

                IList<MaterialSapi> documentos = this._materialSapiServicios.ConsultarTodos();
                documentos = documentos.OrderBy(o => o.Descripcion).ToList();
                documentos.Insert(0, new MaterialSapi("NGN"));
                this._ventana.Documentos = documentos;

                IList<ListaDatosValores> status =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiStatusDocumentoPresentacionSAPI));
                status.Insert(0, new ListaDatosValores("NGN"));
                this._ventana.StatusTodos = status;
                this._statusDocumentos = status;



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

                this.Navegar(new ControlarSolicitudesPresentaciones());

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
        /// Metodo que obtiene las solicitudes de Presentacion SAPI de acuerdo a un Filtro dado
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
        /// Metodo que obtiene el objeto PresentacionSapiDetalle que servira como filtro para la consulta
        /// </summary>
        /// <returns>Objeto PresentacionSapiDetalle usado como filtro</returns>
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


                if (!this._ventana.FechaPresentacionAnteSAPI.Equals(""))
                {
                    presentacion_Detalle.FechaPres_Gestor2 = DateTime.Parse(this._ventana.FechaPresentacionAnteSAPI);
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.FechaPres_Gestor2 = null;


                if ((this._ventana.Dpto != null) && (!((Departamento)this._ventana.Dpto).Id.Equals("NGN")))
                {
                    presentacion_Detalle.Presentacion_Enc.Departamento = (Departamento)this._ventana.Dpto;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.Presentacion_Enc.Departamento = null;


                if ((this._ventana.Usuario != null) && (!((Usuario)this._ventana.Usuario).Id.Equals("NGN")))
                {
                    presentacion_Detalle.Presentacion_Enc.Iniciales = ((Usuario)this._ventana.Usuario).Iniciales;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.Presentacion_Enc.Iniciales = null;


                if ((this._ventana.Gestor != null) && (!((Usuario)this._ventana.Gestor).Id.Equals("NGN")))
                {
                    presentacion_Detalle.ReceptorMatPresent = ((Usuario)this._ventana.Gestor).Iniciales;
                    this._filtroValido = 2;
                }
                else
                    presentacion_Detalle.ReceptorMatPresent = null;


                if ((this._ventana.Documento != null) && (!((MaterialSapi)this._ventana.Documento).Id.Equals("NGN")))
                {
                    presentacion_Detalle.Material.Id = ((MaterialSapi)this._ventana.Documento).Id;
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


                if ((this._ventana.StatusSeleccionado != null) && (!((ListaDatosValores)this._ventana.StatusSeleccionado).Id.Equals("NGN")))
                {
                    presentacion_Detalle.StatusDocumento = ((ListaDatosValores)this._ventana.StatusSeleccionado).Descripcion;
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


        /// <summary>
        /// Metodo que realiza el Registro de los eventos que van ocurriendo con los Documentos de Solicitudes de Presentacion SAPI
        /// </summary>
        /// <param name="nombreBotonPresionado">Nombre del boton presionado</param>
        public void RegistrarControlDePresentaciones(string nombreBotonPresionado)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            IList<PresentacionSapiDetalle> documentosSeleccionados;
            IList<PresentacionSapiDetalle> listaDocumentosPantalla = (IList<PresentacionSapiDetalle>)this._ventana.Resultados;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (listaDocumentosPantalla != null)
                {
                    switch (nombreBotonPresionado)
                    {
                        case "_btnRecepcionPorGestor":
                            this._accion = "1";
                            documentosSeleccionados = ObtenerDocumentos(listaDocumentosPantalla, this._accion);
                            if (documentosSeleccionados.Count > 0)
                            {
                                this._documentosPresentaciones = documentosSeleccionados;
                                this._ventana.MostrarCamposRegistroEvento(this._accion);
                            }
                            else
                                this._ventana.Mensaje("No hay Documentos para realizar la acción", 0);

                            break;
                        case "_btnPresentacionEnSAPI":
                            this._accion = "2";
                            documentosSeleccionados = ObtenerDocumentos(listaDocumentosPantalla, this._accion);
                            if (documentosSeleccionados.Count > 0)
                            {
                                this._documentosPresentaciones = documentosSeleccionados;
                                this._ventana.MostrarCamposRegistroEvento(this._accion);
                            }
                            else
                                this._ventana.Mensaje("No hay Documentos para realizar la acción", 0);
                            break;
                        case "_btnRecepcionDeSAPI":
                            this._accion = "3";
                            documentosSeleccionados = ObtenerDocumentos(listaDocumentosPantalla, this._accion);
                            if (documentosSeleccionados.Count > 0)
                            {
                                this._documentosPresentaciones = documentosSeleccionados;
                                this._ventana.MostrarCamposRegistroEvento(this._accion);
                            }
                            else
                                this._ventana.Mensaje("No hay Documentos para realizar la acción", 0);
                            break;
                        case "_btnRecepcionDpto":
                            this._accion = "4";
                            documentosSeleccionados = ObtenerDocumentos(listaDocumentosPantalla, this._accion);
                            if (documentosSeleccionados.Count > 0)
                            {
                                this._documentosPresentaciones = documentosSeleccionados;
                                this._ventana.MostrarCamposRegistroEvento(this._accion);
                            }
                            else
                                this._ventana.Mensaje("No hay Documentos para realizar la acción seleccionada", 0);
                            break;

                        case "_btnFacturacion":
                            this._accion = "5";
                            documentosSeleccionados = ObtenerDocumentos(listaDocumentosPantalla, this._accion);
                            if (documentosSeleccionados.Count > 0)
                            {
                                if (documentosSeleccionados.Count == 1)
                                {
                                    this._documentosPresentaciones = documentosSeleccionados;
                                    this._ventana.MostrarCamposRegistroEvento(this._accion);
                                }
                                else
                                    this._ventana.Mensaje("Para registrar el Status de Facturación debe seleccionar solo uno a la vez", 0);
                            }
                            else
                                this._ventana.Mensaje("No hay Documentos para realizar la acción", 0);
                            break;
                    }
                }
                else
                    this._ventana.Mensaje("No hay Documentos para ejecutar la acción deseada", 0);

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
        /// Metodo que obtiene un conjunto de Documentos para realizar una de las 4 acciones principales sobre las Solicitudes de 
        /// Presentacion SAPI
        /// </summary>
        /// <param name="listaDocumentosPantalla">Lista de Documentos presentados en la pantalla para filtrar</param>
        /// <param name="accion">Accion a realizar dependiendo del boton presionado</param>
        /// <returns>Lista de Detalles de Solicitud de Presentacion (Documentos) que van a ser procesados segun la opcion seleccionada por el usuario</returns>
        private IList<PresentacionSapiDetalle> ObtenerDocumentos(IList<PresentacionSapiDetalle> listaDocumentosPantalla, string accion)
        {
            IList<PresentacionSapiDetalle> documentosSeleccionados = new List<PresentacionSapiDetalle>();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (PresentacionSapiDetalle item in listaDocumentosPantalla)
                {
                    if (this._accion.Equals("1"))
                    {
                        if (item.FechaRecep_Gestor1 == null && item.BRecibeDocumento)
                        {
                            documentosSeleccionados.Add(item);
                        }
                    }
                    else if (this._accion.Equals("2"))
                    {
                        if ((item.FechaRecep_Gestor1 != null) && (item.FechaPres_Gestor2 == null)
                            && item.BRecibeDocumento && item.BPresentadoASapi)
                        {
                            documentosSeleccionados.Add(item);
                        }
                    }
                    else if (this._accion.Equals("3"))
                    {
                        if ((item.FechaRecep_Gestor1 != null) 
                            && (item.FechaPres_Gestor2 != null) 
                            && (item.FechaRecep_Gestor3 == null)
                            && item.BRecibeDocumento
                            && item.BPresentadoASapi
                            && item.BRecibioDeSapi)
                        {
                            documentosSeleccionados.Add(item);
                        }
                    }
                    else if (this._accion.Equals("4"))
                    {
                        if ((item.FechaRecep_Gestor1 != null)
                            && (item.FechaPres_Gestor2 != null)
                            && (item.FechaRecep_Gestor3 != null) 
                            && (item.FechaRecep_Dpto == null)
                            && item.BRecibeDocumento
                            && item.BPresentadoASapi
                            && item.BRecibioDeSapi
                            && item.BRecibioDpto)
                        {
                            documentosSeleccionados.Add(item);
                        }
                    }
                    else if (this._accion.Equals("5"))
                    {
                        if ((item.FechaRecep_Gestor1 != null)
                            && (item.FechaPres_Gestor2 != null)
                            && (item.FechaRecep_Gestor3 != null)
                            && (item.FechaRecep_Dpto != null)
                            && (item.FechaFacturacion == null)
                            && item.BRecibeDocumento
                            && item.BPresentadoASapi
                            && item.BRecibioDeSapi
                            && item.BRecibioDpto
                            && item.BDocFacturado)
                        {
                            documentosSeleccionados.Add(item);
                        }
                    }

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

            return documentosSeleccionados;
        }


        /// <summary>
        /// Metodo que oculta los campos de Registro de Evento 
        /// </summary>
        public void SuspenderRegistroEvento()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana.OcultarCamposRegistroEvento(this._accion);

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
        /// Metodo que realiza el Registro del Evento ingresado por el Gestor o el Usuario del Dpto con una serie de Documentos
        /// de una Solicitud de Presentacion SAPI
        /// </summary>
        public void RegistrarEventoPresentacionDocumentos()
        {

            bool exitoso = false;
            ListaDatosValores statusDocumento = new ListaDatosValores();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.GestorRegistro != null)
                {
                    if (!string.IsNullOrEmpty(this._ventana.FechaConfirmacion))
                    {
                        foreach (PresentacionSapiDetalle documentoLista in this._documentosPresentaciones)
                        {
                            switch (this._accion)
                            {
                                case "1":
                                    documentoLista.ReceptorMatPresent = ((Usuario)this._ventana.GestorRegistro).Iniciales;
                                    documentoLista.FechaRecep_Gestor1 = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BRecibeDocumento = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "2";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                                case "2":
                                    documentoLista.PresentadorAnteSAPI = ((Usuario)this._ventana.GestorRegistro).Iniciales;
                                    documentoLista.FechaPres_Gestor2 = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BPresentadoASapi = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "3";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                                case "3":
                                    documentoLista.ReceptorAnteSAPI = ((Usuario)this._ventana.GestorRegistro).Iniciales;
                                    documentoLista.FechaRecep_Gestor3 = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BRecibioDeSapi = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "4";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                                case "4":
                                    documentoLista.InicDptoReceptor = ((Usuario)this._ventana.GestorRegistro).Departamento.Id;
                                    documentoLista.FechaRecep_Dpto = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BRecibioDpto = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "5";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                            }
                            if (exitoso)
                                continue;
                            else
                            {
                                this._ventana.Mensaje("El proceso de Registro falló", 0);
                                break;
                            }
                        }

                        this._ventana.OcultarCamposRegistroEvento(this._accion);
                        this._ventana.GestorRegistro = null;
                        this._ventana.FechaConfirmacion = "";
                        Consultar();
                    }
                    else
                        this._ventana.Mensaje("Debe seleccionar una Fecha para generar el Registro de la operación", 0);
                }
                else
                    this._ventana.Mensaje("Seleccione un Gestor para hacer el Registro de la operación", 0);

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
        /// Metodo que registra el evento de Facturacion 
        /// </summary>
        public void RegistrarEventoFacturacion()
        {
            bool exitoso = false;
            ListaDatosValores statusDocumento = new ListaDatosValores();
            PresentacionSapiDetalle detallePresentacion = this._documentosPresentaciones[0];

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.IdFactura.Equals(String.Empty) || this._ventana.Ourref.Equals(String.Empty))
                {
                    detallePresentacion.BDocFacturado = true;
                    detallePresentacion.FechaFacturacion = DateTime.Today;
                    statusDocumento.Id = "NGN";
                    statusDocumento.Valor = "6";
                    detallePresentacion.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(detallePresentacion, UsuarioLogeado.Hash);

                    if (exitoso)
                    {
                        this._ventana.OcultarCamposRegistroEvento("5");
                        this._ventana.IdFactura = String.Empty;
                        this._ventana.Ourref = String.Empty;
                        Consultar();
                    }
                }
                else
                    this._ventana.Mensaje("Para registrar el Status de Facturado debe escribir el Código de la Factura o el Código Alterno o la Referencia", 0);

                #region CODIGO ORIGINAL COMENTADO - NO BORRAR
                /*if (this._ventana.GestorRegistro != null)
                {
                    if (!string.IsNullOrEmpty(this._ventana.FechaConfirmacion))
                    {
                        foreach (PresentacionSapiDetalle documentoLista in this._documentosPresentaciones)
                        {
                            switch (this._accion)
                            {
                                case "1":
                                    documentoLista.ReceptorMatPresent = ((Usuario)this._ventana.GestorRegistro).Iniciales;
                                    documentoLista.FechaRecep_Gestor1 = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BRecibeDocumento = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "2";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                                case "2":
                                    documentoLista.PresentadorAnteSAPI = ((Usuario)this._ventana.GestorRegistro).Iniciales;
                                    documentoLista.FechaPres_Gestor2 = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BPresentadoASapi = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "3";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                                case "3":
                                    documentoLista.ReceptorAnteSAPI = ((Usuario)this._ventana.GestorRegistro).Iniciales;
                                    documentoLista.FechaRecep_Gestor3 = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BRecibioDeSapi = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "4";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                                case "4":
                                    documentoLista.InicDptoReceptor = ((Usuario)this._ventana.GestorRegistro).Departamento.Id;
                                    documentoLista.FechaRecep_Dpto = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BRecibioDpto = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "5";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;
                                case "5":
                                    /*documentoLista.InicDptoReceptor = ((Usuario)this._ventana.GestorRegistro).Departamento.Id;
                                    documentoLista.FechaFacturacion = DateTime.Parse(this._ventana.FechaConfirmacion);
                                    documentoLista.BRecibioDpto = true;
                                    statusDocumento.Id = "NGN";
                                    statusDocumento.Valor = "5";
                                    documentoLista.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusDocumento).Descripcion;
                                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(documentoLista, UsuarioLogeado.Hash);
                                    break;

                            }
                            if (exitoso)
                                continue;
                            else
                            {
                                this._ventana.Mensaje("El proceso de Registro falló", 0);
                                break;
                            }
                        }

                        this._ventana.OcultarCamposRegistroEvento(this._accion);
                        this._ventana.GestorRegistro = null;
                        this._ventana.FechaConfirmacion = "";
                        Consultar();
                    }
                    else
                        this._ventana.Mensaje("Debe seleccionar una Fecha para generar el Registro de la operación", 0);
                }
                else
                    this._ventana.Mensaje("Seleccione un Gestor para hacer el Registro de la operación", 0);*/

                #endregion

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
        /// Metodo que sirve para exportar el Detalle de la Consulta en pantalla a Excel
        /// </summary>
        public void ExportarResultadosExcel()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<PresentacionSapiDetalle> datosResultadosConsulta;
                DataTable datosExportar = CrearDataTableExportacion();

                if (this._ventana.Resultados != null)
                {
                    datosResultadosConsulta = (IList<PresentacionSapiDetalle>)this._ventana.Resultados;
                    datosExportar = LlenarDataTableExportacion(datosResultadosConsulta, datosExportar);
                    this._ventana.ExportarDatosConsolidadosExcel(datosExportar);
                }
                else
                    this._ventana.Mensaje("No hay datos para exportar", 0);


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
                datos.Columns.Add("Cod Material", typeof(string));
                datos.Columns.Add("Material", typeof(string));
                datos.Columns.Add("Tipo Material", typeof(string));
                datos.Columns.Add("Expediente", typeof(string));
                datos.Columns.Add("Departamento", typeof(string));
                datos.Columns.Add("Fecha Solicitud", typeof(DateTime));
                datos.Columns.Add("Fecha RecepDcto", typeof(DateTime));
                datos.Columns.Add("Fecha PresentSAPI", typeof(DateTime));
                datos.Columns.Add("Fecha RecepSAPI", typeof(DateTime));
                
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
        /// Metodo que llena el DataTable que se va a mostar en la hoja de Excel
        /// </summary>
        /// <param name="datosResultadosConsulta">Datos de la consulta en pantalla</param>
        /// <param name="datosExportar">DataTable que se va a exportar</param>
        /// <returns>DAtaTable lleno con los datos para exportar</returns>
        private DataTable LlenarDataTableExportacion(IList<PresentacionSapiDetalle> datosResultadosConsulta, DataTable datosExportar)
        {

            DataTable datos = datosExportar;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (PresentacionSapiDetalle item in datosResultadosConsulta)
                {
                    DataRow filaNueva = datos.NewRow();
                    filaNueva["Solicitud No"] = item.Presentacion_Enc.Id;
                    filaNueva["Cod Material"] = item.Material.Id;
                    filaNueva["Material"] = item.Material.Descripcion;
                    filaNueva["Tipo Material"] = item.Material.TipoDescripcion;
                    filaNueva["Expediente"] = item.CodExpediente;
                    filaNueva["Departamento"] = item.Presentacion_Enc.Departamento.Descripcion;
                    filaNueva["Fecha Solicitud"] = item.Presentacion_Enc.Fecha;

                    if (item.FechaRecep_Gestor1 != null)
                        filaNueva["Fecha RecepDcto"] = item.FechaRecep_Gestor1;

                    if(item.FechaPres_Gestor2 != null)
                        filaNueva["Fecha PresentSAPI"] = item.FechaPres_Gestor2;

                    if (item.FechaRecep_Gestor3 != null)
                        filaNueva["Fecha RecepSAPI"] = item.FechaRecep_Gestor3;

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
        /// Metodo que toma el nombre del usuario logueado para el Reporte de exportacion
        /// </summary>
        /// <returns>Nombre Completo del usuario logueado</returns>
        public string ObtenerNombreUsuario()
        {
            return UsuarioLogeado.NombreCompleto;
        }


        /// <summary>
        /// Metodo que presenta la ventana para ver las Facturas del Documento de la presentacion
        /// Esto va a depender del tipo de facturacion que posea el documento
        /// <param name="nombreCuadroTexto">Nombre del cuadro de Texto seleccionado</param>
        /// </summary>
        public void VerFacturaDocumento(String nombreCuadroTexto)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                PresentacionSapiDetalle detalleSeleccionado = this._documentosPresentaciones[0];
                MaterialSapi documento = detalleSeleccionado.Material;
                String tipoElemento = String.Empty;

                if (nombreCuadroTexto.Equals("_txtFactura"))
                {
                    if (!this._ventana.IdFactura.Equals(String.Empty))
                    {
                        if(documento.TipoFacturacion != null)
                        {
                            switch (documento.TipoFacturacion)
                            {
                                case '1':
                                    Navegar(new ConsultarFacVistaFacturaServicios(this._ventana.IdFactura, null, null, true));
                                    break;
                                case '2':
                                    Navegar(new ConsultarFacVistaFacturaServicios(null, this._ventana.IdFactura, null, true));
                                    break;
                                case '3':
                                    this._ventana.Mensaje("EL tipo de Facturación para este documento no permite filtrar por Codigo de Factura ni por Código Alterno", 0);
                                    break;
                            }
                        }
                        else
                            this._ventana.Mensaje("El Material no tiene asignado un Tipo de Facturación",0);
                    }
                    else
                        this._ventana.Mensaje("Escriba un codigo de Factura o codigo Alterno para consultarlo", 0);

                }
                else if (nombreCuadroTexto.Equals("_txtOurref"))
                {
                    if (!this._ventana.Ourref.Equals(String.Empty))
                    {
                        if (documento.TipoFacturacion != null)
                        {
                            if (documento.TipoFacturacion.Equals('1') || documento.TipoFacturacion.Equals('2'))
                                this._ventana.Mensaje("El Tipo de Facturación para este documento no es por Nuestra Referencia", 0);
                            else if(documento.TipoFacturacion.Equals('3'))
                                Navegar(new ConsultarFacVistaFacturaServicios(null, null, this._ventana.Ourref, true));
                        }
                        else
                            this._ventana.Mensaje("El Material no tiene asignado un Tipo de Facturación", 0);
                    }
                    else
                        this._ventana.Mensaje("Escriba un codigo de Referencia para consultarlo", 0);
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


        
    }
}
