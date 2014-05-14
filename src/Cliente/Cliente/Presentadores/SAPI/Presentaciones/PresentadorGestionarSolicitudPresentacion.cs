using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Presentaciones;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Presentaciones
{
    class PresentadorGestionarSolicitudPresentacion : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarSolicitudPresentacion _ventana;
        private IMaterialSapiServicios _materialSapiServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IPresentacionSapiServicios _presentacionSapiServicios;
        private IPresentacionSapiDetalleServicios _presentacionSapiDetalleServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;

        private bool _agregar;
        private IList<ListaDatosValores> _statusDocumentos;
       

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="presentacionSapi">Encabezado de la Solicitud de Presentacion SAPI</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarSolicitudPresentacion(IGestionarSolicitudPresentacion ventana, object presentacionSapi, object ventanaPadre)
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

                if (presentacionSapi != null)
                {
                    this._ventana.EncabezadoPresentacion = (PresentacionSapi)presentacionSapi;
                    this._agregar = false;
                }
                else
                {
                    this._agregar = true;
                    PresentacionSapi presentacionSapiEnc = new PresentacionSapi();
                    this._ventana.EncabezadoPresentacion = presentacionSapiEnc;
                }
                

                this._materialSapiServicios = (IMaterialSapiServicios)Activator.GetObject(typeof(IMaterialSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaterialSapiServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._presentacionSapiServicios = (IPresentacionSapiServicios)Activator.GetObject(typeof(IPresentacionSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PresentacionSapiServicios"]);
                this._presentacionSapiDetalleServicios = (IPresentacionSapiDetalleServicios)Activator.GetObject(typeof(IPresentacionSapiDetalleServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PresentacionSapiDetalleServicios"]);
                
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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarSolicitudPresentacionSAPI,
                Recursos.Ids.GestionarSolicitudPresentacionSapi);
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

                CargarCombosVentana();

                CargarEstatusDeDocumentos();

                if (!this._agregar)
                {
                    PresentacionSapiDetalle detalle = new PresentacionSapiDetalle();
                    detalle.Presentacion_Enc = (PresentacionSapi)this._ventana.EncabezadoPresentacion;
                    IList<PresentacionSapiDetalle> detallePresentacion = this._presentacionSapiDetalleServicios.ObtenerSolicitudesPresentacionSapiFiltro(detalle);
                    if (detallePresentacion.Count > 0)
                    {
                        ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion = detallePresentacion;
                        this._ventana.DetallePresentacion = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion;
                        this._ventana.CantidadDocumentos = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion.Count.ToString();
                    }

                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                    this._ventana.HabilitarCampos = false;
                }
                else
                {
                    this._ventana.IdPresentacionSapi = String.Empty;
                    this._ventana.CantidadDocumentos = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).CantDocumentos.ToString();
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

        
        /// <summary>
        /// Metodo que carga los combos de la ventana 
        /// </summary>
        private void CargarCombosVentana()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                /* Carga del combo de usuarios */
                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                usuarios.Insert(0, new Usuario("NGN"));
                this._ventana.Usuarios = usuarios;
                if (this._agregar)
                    this._ventana.Usuario = this.BuscarUsuarioPorIniciales(usuarios, UsuarioLogeado);
                else
                {
                    Usuario usuarioPresentacion = new Usuario();
                    usuarioPresentacion.Iniciales = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).Iniciales;
                    this._ventana.Usuario = this.BuscarUsuarioPorIniciales(usuarios, usuarioPresentacion);
                }

                /*Carga del combo de Departamentos */
                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                departamentos.Insert(0, new Departamento("NGN"));
                this._ventana.Departamentos = departamentos;
                if (!this._agregar)
                {
                    this._ventana.Departamento = this.BuscarDepartamento(departamentos, ((PresentacionSapi)this._ventana.EncabezadoPresentacion).Departamento);
                }
                else
                {
                    this._ventana.Departamento = this.BuscarDepartamento(departamentos, UsuarioLogeado.Departamento);
                }

                /*Carga de Documentos (Materiales Sapi) */
                IList<MaterialSapi> documentos = this._materialSapiServicios.ConsultarTodos();
                documentos.Insert(0, new MaterialSapi("NGN"));
                this._ventana.Documentos = documentos;

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
        /// Metodo que carga los status posibles de un documento que forma parte de la Solicitud de Presentacion en SAPI
        /// </summary>
        private void CargarEstatusDeDocumentos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._statusDocumentos = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiStatusDocumentoPresentacionSAPI));

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
        /// Metodo que Agregar un Documento a la lista de Detalle de la Solicitud de Presentacion SAPI
        /// </summary>
        public void AgregarDocumentoASolicitud()
        {
            ListaDatosValores statusPendiente = new ListaDatosValores();
            statusPendiente.Id = Recursos.Etiquetas.cbiStatusDocumentoPresentacionSAPI;
            statusPendiente.Valor = "1";

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<PresentacionSapiDetalle> renglonesDetalle;
                PresentacionSapiDetalle renglonDetalle;

                if ((this._ventana.Documento != null) && (!((MaterialSapi)this._ventana.Documento).Id.Equals("NGN")))
                {
                    if (!this._ventana.CodigoExpedientePresentacion.Equals(String.Empty) && !this._ventana.CodigoExpedientePresentacion.Equals(""))
                    {
                        if (null == ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion)
                            renglonesDetalle = new List<PresentacionSapiDetalle>();
                        else
                            renglonesDetalle = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion;

                        renglonDetalle = new PresentacionSapiDetalle();
                        renglonDetalle.Material = (MaterialSapi)this._ventana.Documento;
                        renglonDetalle.Presentacion_Enc = (PresentacionSapi)this._ventana.EncabezadoPresentacion;
                        renglonDetalle.CodExpediente = this._ventana.CodigoExpedientePresentacion;
                        renglonDetalle.StatusDocumento = this.BuscarListaDeDatosValores(this._statusDocumentos, statusPendiente).Descripcion;
                                                
                        renglonesDetalle.Add(renglonDetalle);
                        ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion = renglonesDetalle;
                        this._ventana.DetallePresentacion = null;
                        this._ventana.DetallePresentacion = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion;
                        this._ventana.CantidadDocumentos = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion.Count.ToString();
                        ((PresentacionSapi)this._ventana.EncabezadoPresentacion).CantDocumentos = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion.Count;
                        this._ventana.SeleccionarPrimerItem();
                    }
                    else
                        this._ventana.Mensaje("Introduzca el Código del Expediente al que pertenece el Documento a presentar", 0);
                }
                else
                    this._ventana.Mensaje("Seleccione un Documento válido para la Solicitud de Presentación", 0);

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
        /// Metodo que Quita de la lista de Detalle un Documento agregado a la Solicitud de Presentacion SAPI
        /// </summary>
        public void QuitarDocumentoDeSolicitud()
        {

            IList<PresentacionSapiDetalle> renglonesDetalle;
            PresentacionSapiDetalle renglonDetalle;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.DetallePresentacionSeleccionado != null)
                {
                    if (null == ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion)
                        renglonesDetalle = new List<PresentacionSapiDetalle>();
                    else
                        renglonesDetalle = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion;

                    renglonDetalle = (PresentacionSapiDetalle)this._ventana.DetallePresentacionSeleccionado;

                    if (!this._agregar)
                    {
                        bool exitoso = this._presentacionSapiDetalleServicios.Eliminar(renglonDetalle,UsuarioLogeado.Hash);
                    }

                    renglonesDetalle.Remove((PresentacionSapiDetalle)this._ventana.DetallePresentacionSeleccionado);
                    ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion = renglonesDetalle;
                    this._ventana.DetallePresentacion = null;
                    this._ventana.DetallePresentacion = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion;
                    this._ventana.CantidadDocumentos = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion.Count.ToString();
                    ((PresentacionSapi)this._ventana.EncabezadoPresentacion).CantDocumentos = ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion.Count;
                    this._ventana.DetallePresentacionSeleccionado = null;
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
        /// Metodo que permite guardar los cambios de una Solicitud de Presentacion SAPI
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
                    PresentacionSapi presentacionSapiEnc = ObtenerEncabcezadoPresentacionPantalla();

                    if (presentacionSapiEnc.CantDocumentos != 0)
                    {
                        if (presentacionSapiEnc.Fecha != null)
                        {
                            if (presentacionSapiEnc.DetalleSolicitudPresentacion != null)
                            {
                                exitoso = this._presentacionSapiServicios.InsertarOModificarPresentacionSapi(ref presentacionSapiEnc, UsuarioLogeado.Hash);
                                if (exitoso)
                                {
                                    GuardarDetallePresentacionSapi(presentacionSapiEnc);

                                    if (this._agregar)
                                    {
                                        this._agregar = false;
                                        this._ventana.Mensaje("La Solicitud de Presentacion fue registrada con éxito", 2);
                                    }
                                    else
                                        this._ventana.Mensaje(string.Format("La Solicitud de Presentacion {0} fue modificada", presentacionSapiEnc.Id.ToString()), 2);

                                    this._ventana.HabilitarCampos = false;
                                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                                }
                            }
                            else
                                this._ventana.Mensaje("Incluya al menos un Documento, no se puede guardar la Solicitud de Presentación", 0);
                        }
                        else
                            this._ventana.Mensaje("La Solicitud de Presentacion no tiene fecha", 0);
                    }
                    else
                        this._ventana.Mensaje("La Solicitud de Presentación no tiene Documentos a presentar", 0);
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
        /// Metodo que guarda el Detalle de la Presentacion SAPI
        /// </summary>
        /// <param name="presentacionSapiEnc"></param>
        private void GuardarDetallePresentacionSapi(PresentacionSapi presentacionSapiEnc)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<PresentacionSapiDetalle> detallePresentacion = 
                    ((PresentacionSapi)this._ventana.EncabezadoPresentacion).DetalleSolicitudPresentacion;

                foreach (PresentacionSapiDetalle item in detallePresentacion)
                {
                    item.Presentacion_Enc = presentacionSapiEnc;
                    exitoso = this._presentacionSapiDetalleServicios.InsertarOModificar(item, UsuarioLogeado.Hash);
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
        /// Metodo que obtiene los campos del Encabezado de la Solicitud de Presentacion de la Pantalla
        /// </summary>
        /// <returns></returns>
        private PresentacionSapi ObtenerEncabcezadoPresentacionPantalla()
        {

            PresentacionSapi presentacion = (PresentacionSapi)this._ventana.EncabezadoPresentacion;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.Departamento != null) && !((Departamento)this._ventana.Departamento).Id.Equals("NGN"))
                    presentacion.Departamento = (Departamento)this._ventana.Departamento;
                else
                    presentacion.Departamento = null;

                if ((this._ventana.Usuario != null) && !((Usuario)this._ventana.Usuario).Id.Equals("NGN"))
                    presentacion.Iniciales = ((Usuario)this._ventana.Usuario).Iniciales;
                else
                    presentacion.Iniciales = null;

                if ((this._ventana.FechaPresentacion != null) && !this._ventana.FechaPresentacion.Equals(""))
                {
                    presentacion.Fecha = DateTime.Parse(this._ventana.FechaPresentacion);
                }
                else
                    presentacion.Fecha = null;

                if ((this._ventana.CantidadDocumentos != null) && !this._ventana.CantidadDocumentos.Equals(""))
                    presentacion.CantDocumentos = int.Parse(this._ventana.CantidadDocumentos);
                else
                    presentacion.CantDocumentos = 0;

                if (this._agregar)
                    presentacion.Operacion = "CREATE";
                else
                    presentacion.Operacion = "MODIFY";


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return presentacion;
        }
    }
}
