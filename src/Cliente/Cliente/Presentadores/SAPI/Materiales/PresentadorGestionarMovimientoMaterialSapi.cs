using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales
{
    class PresentadorGestionarMovimientoMaterialSapi : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarMovimientoMaterialSapi _ventana;
        private IMaterialSapiServicios _materialSapiServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private ISolicitudSapiServicios _solicitudSapiServicios;

        private IList<ListaDatosValores> _tiposMovimientoMaterial;
        private bool _agregar;
        private SolicitudSapi _solicitudEncabezado;
        private int _ultimoIdSolicitud;

        /// <summary>
        /// Constructor predeterminado que recibe un movimiento de material Sapi (Solicitud) y una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana Actual</param>
        /// <param name="solicitud">Movimiento de Material Sapi (Solicitud)</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarMovimientoMaterialSapi(IGestionarMovimientoMaterialSapi ventana, object solicitud, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                if (ventanaPadre != null)
                    this._ventanaPadre = ventanaPadre;

                if (solicitud != null)
                {
                    this._agregar = false;
                    _solicitudEncabezado = (SolicitudSapi)solicitud;
                }
                else
                {
                    this._agregar = true;
                }
                
                this._materialSapiServicios = (IMaterialSapiServicios)Activator.GetObject(typeof(IMaterialSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaterialSapiServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._solicitudSapiServicios = (ISolicitudSapiServicios)Activator.GetObject(typeof(ISolicitudSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SolicitudSapiServicios"]);

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarMovimientoMaterialSAPI,
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

                if (!this._agregar)
                {
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                    this._ventana.IdSolicitudMaterial = this._solicitudEncabezado.Id.ToString();
                    this._ventana.FechaSolicitudMaterial = this._solicitudEncabezado.FechaSolicitud.ToString();

                    if (this._solicitudEncabezado.Departamento != null)
                    {
                        this._ventana.Departamento = this.BuscarDepartamento((IList<Departamento>)this._ventana.Departamentos, this._solicitudEncabezado.Departamento);
                        Usuario usuarioBuscar = new Usuario();
                        usuarioBuscar.Iniciales = this._solicitudEncabezado.SolicitanteInic;
                        this._ventana.UsuarioSolicitante = this.BuscarUsuarioPorIniciales((IList<Usuario>)this._ventana.UsuariosSolicitantes, usuarioBuscar);
                        ListaDatosValores tipoMovimiento = new ListaDatosValores("NGN");
                        tipoMovimiento.Valor = this._solicitudEncabezado.TipoMovimiento;
                        this._ventana.TipoMovimientoMaterial = this.BuscarListaDeDatosValores(this._tiposMovimientoMaterial, tipoMovimiento);
                        CargarDetalleSolicitud();
                    }

                    this._ventana.MostrarBotonEliminar(true);
                    this._ventana.HabilitarCampos = false;
                    
                }
                else
                {
                    this._ventana.IdSolicitudMaterial = String.Empty;
                }

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

        private void CargarDetalleSolicitud()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                SolicitudSapi solicitudBuscar = new SolicitudSapi();
                solicitudBuscar.Id = this._solicitudEncabezado.Id;

                IList<SolicitudSapi> detalleSolicitud = this._solicitudSapiServicios.ObtenerSolicitudesSapiFiltro(solicitudBuscar);
                if (detalleSolicitud.Count > 0)
                {
                    this._ventana.DetallesSolicitudMaterial = detalleSolicitud;
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
        }

        /// <summary>
        /// Metodo que carga los combos de la Ventana actual
        /// </summary>
        private void CargarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._tiposMovimientoMaterial = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTiposMovimientoMaterialSAPI));
                this._ventana.TiposMovimientosMaterial = this._tiposMovimientoMaterial;
                if (this._agregar)
                {
                    ListaDatosValores tipoMovimientoBuscar = new ListaDatosValores();
                    tipoMovimientoBuscar.Id = "NGN";
                    tipoMovimientoBuscar.Valor = "SOLICITUD";
                    this._ventana.TipoMovimientoMaterial = this.BuscarListaDeDatosValores(this._tiposMovimientoMaterial, tipoMovimientoBuscar);
                }

                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                usuarios.Insert(0, new Usuario("NGN"));
                this._ventana.UsuariosSolicitantes = usuarios;
                if(this._agregar)
                    this._ventana.UsuarioSolicitante = this.BuscarUsuarioPorIniciales(usuarios, UsuarioLogeado);

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                departamentos.Insert(0, new Departamento("NGN"));
                this._ventana.Departamentos = departamentos;
                if(this._agregar)
                    this._ventana.Departamento = this.BuscarDepartamento(departamentos, UsuarioLogeado.Departamento);

                IList<MaterialSapi> materiales = this._materialSapiServicios.ConsultarTodos();
                materiales.Insert(0, new MaterialSapi("NGN"));
                this._ventana.MaterialesSAPI = materiales;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para Agregar un Material en la Solicitud de Material SAPI
        /// </summary>
        public void AgregarMaterial()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<SolicitudSapi> renglonesDetalle = new List<SolicitudSapi>();
                
                if ((null != (MaterialSapi)this._ventana.MaterialSAPI) && (!((MaterialSapi)this._ventana.MaterialSAPI).Id.Equals("NGN")))
                {
                    
                    if ((this._ventana.UsuarioSolicitante != null) && (!((Usuario)this._ventana.UsuarioSolicitante).Id.Equals("NGN")))
                    {
                        if ((this._ventana.Departamento != null) && (!((Departamento)this._ventana.Departamento).Id.Equals("NGN")))
                        {
                            if (!this._ventana.FechaSolicitudMaterial.Equals(""))
                            {
                                if (null == (IList<SolicitudSapi>)this._ventana.DetallesSolicitudMaterial)
                                    renglonesDetalle = new List<SolicitudSapi>();
                                else
                                    renglonesDetalle = (IList<SolicitudSapi>)this._ventana.DetallesSolicitudMaterial;

                                SolicitudSapi solicitudMaterial = new SolicitudSapi();
                                solicitudMaterial.FechaSolicitud = DateTime.Parse(this._ventana.FechaSolicitudMaterial);
                                solicitudMaterial.Material = (MaterialSapi)this._ventana.MaterialSAPI;
                                if (solicitudMaterial.Material.Existencia == 0)
                                    this._ventana.Mensaje("Esta solicitando un Material que no tiene existencia en este momento", 1);
                                solicitudMaterial.FechaSolicitud = DateTime.Parse(this._ventana.FechaSolicitudMaterial);
                                solicitudMaterial.BSolicitado = true;
                                solicitudMaterial.Departamento = (Departamento)this._ventana.Departamento;
                                solicitudMaterial.SolicitanteInic = UsuarioLogeado.Iniciales;

                                if (!this._agregar)
                                {
                                    solicitudMaterial.Id = int.Parse(this._ventana.IdSolicitudMaterial);
                                }

                                renglonesDetalle.Add(solicitudMaterial);
                                this._ventana.DetallesSolicitudMaterial = null;
                                this._ventana.DetallesSolicitudMaterial = renglonesDetalle;
                                this._ventana.SeleccionarPrimerItem();
                            }
                            else
                                this._ventana.Mensaje("Debe asignar una fecha a la Solicitud", 0);
                        }
                        else
                            this._ventana.Mensaje("Debe seleccionar el Departamento donde se está generando la Solicitud", 0);
                    }
                    else
                        this._ventana.Mensaje("Debe seleccionar un Usuario válido para la Solicitud", 0);
                }
                else
                {
                    this._ventana.Mensaje("Debe seleccionar un Material en la Solicitud", 0);
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
        /// Metodo que quita un material de la solicitud
        /// </summary>
        public void QuitarMaterial()
        {

            IList<SolicitudSapi> renglonesDetalle = new List<SolicitudSapi>();
            SolicitudSapi detalleQuitar;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.DetalleSolicitudMaterial != null)
                {
                    if (null == (IList<SolicitudSapi>)this._ventana.DetallesSolicitudMaterial)
                        renglonesDetalle = new List<SolicitudSapi>();
                    else
                        renglonesDetalle = (IList<SolicitudSapi>)this._ventana.DetallesSolicitudMaterial;

                    //Se actualiza la tabla de detalle y las existencias de los Materiales
                    detalleQuitar = (SolicitudSapi)this._ventana.DetalleSolicitudMaterial;

                    if (!this._agregar)
                    {
                        bool exitoEliminarMaterial = this._solicitudSapiServicios.Eliminar(detalleQuitar, UsuarioLogeado.Hash);
                        if (exitoEliminarMaterial)
                            this._ventana.Mensaje("Este detalle fue eliminado", 2);
                    }

                    renglonesDetalle.Remove((SolicitudSapi)this._ventana.DetalleSolicitudMaterial);
                    this._ventana.DetallesSolicitudMaterial = null;
                    this._ventana.DetallesSolicitudMaterial = renglonesDetalle;
                }
                else
                    this._ventana.Mensaje("Debe seleccionar al menos un Material para eliminar", 0);

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
        /// Metodo que insertar una nueva solicitud de material o modifica una ya existente
        /// </summary>
        public void Aceptar()
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }
                else
                {
                    if (this._ventana.DetallesSolicitudMaterial != null)
                    {
                        IList<SolicitudSapi> solicitudesMaterial = (IList<SolicitudSapi>)this._ventana.DetallesSolicitudMaterial;
                                                                     
                        if (this._ventana.TipoMovimientoMaterial != null)
                        {

                            if (this._agregar)
                            {
                                foreach (SolicitudSapi item in solicitudesMaterial)
                                {
                                    item.TipoMovimiento = ((ListaDatosValores)this._ventana.TipoMovimientoMaterial).Valor;
                                }
                                exitoso = this._solicitudSapiServicios.InsertarOModificarSolicitudMaterialSapi(ref solicitudesMaterial, "CREATE", UsuarioLogeado.Hash);
                                this._ultimoIdSolicitud = solicitudesMaterial[0].Id;
                            }
                            else
                            {
                                //En el caso de querer modificar la misma solicitud que se acaba de insertar
                                if (!this._agregar && (this._ultimoIdSolicitud != 0))
                                {
                                    foreach (SolicitudSapi item in solicitudesMaterial)
                                    {
                                        item.Id = this._ultimoIdSolicitud;
                                    }
                                }

                                exitoso = this._solicitudSapiServicios.InsertarOModificarSolicitudMaterialSapi(ref solicitudesMaterial, "MODIFY", UsuarioLogeado.Hash);
                            }

                            if (exitoso)
                            {
                                this._ventana.Mensaje("Solicitud de Material Sapi guardada con exito", 2);
                                this._ventana.HabilitarCampos = false;
                                this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;

                                if (this._agregar)
                                {
                                    this._agregar = false;
                                    this._ventana.MostrarBotonEliminar(true);
                                }
                            }
                        }
                        else
                            this._ventana.Mensaje("Indique el tipo de Movimiento de Material SAPI", 0);
                    }
                    else
                        this._ventana.Mensaje("La Solicitud SAPI no tiene materiales a solicitar", 0); 
                    
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
        /// Metodo que elimina una Solicitud existente
        /// </summary>
        public void Eliminar()
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.DetallesSolicitudMaterial != null)
                {
                    foreach (SolicitudSapi detalle in (IList<SolicitudSapi>)this._ventana.DetallesSolicitudMaterial)
                    {
                        exitoso = this._solicitudSapiServicios.Eliminar(detalle, UsuarioLogeado.Hash);
                    }

                    if (exitoso)
                    {
                        this._ventana.Mensaje("La Solicitud de Material fue eliminada satisfactoriamente", 2);
                        RegresarVentanaPadre();
                    }
                }
                else
                    this._ventana.Mensaje("No es posible eliminar la Solicitud de Material", 0);

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
