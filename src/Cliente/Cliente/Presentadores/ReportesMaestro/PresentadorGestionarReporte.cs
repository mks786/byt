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
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.ReportesMaestro;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.ReportesMaestro
{
    class PresentadorGestionarReporte : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarReporte _ventana;
        private bool _agregar;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IUsuarioServicios _usuarioServicios;
        private IIdiomaServicios _idiomaServicios;
        private ICamposReporteServicios _camposReporteServicios;
        private IReporteServicios _reporteDeMarcaServicios;
        private ICamposReporteRelacionServicios _camposReporteDeMarcaServicios;
        private IFiltroReporteServicios _filtroReporteDeMarcaServicios;
        private IVistaReporteServicios _vistaReporteServicios;
        private Reporte _reporteDeMarca;


        /// <summary>
        /// Constructor por defecto que recibe la ventana actual, un reporte y una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana IGestionarReporte actual </param>
        /// <param name="reporteMarca">Reporte seleccionado</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarReporte(IGestionarReporte ventana, object reporteMarca, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                if (reporteMarca == null)
                {
                    this._agregar = true;
                    this._reporteDeMarca = new Reporte();
                }
                else
                {
                    this._reporteDeMarca = (Reporte)reporteMarca;
                }

                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._camposReporteServicios = (ICamposReporteServicios)Activator.GetObject(typeof(ICamposReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CamposReporteServicios"]);
                this._reporteDeMarcaServicios = (IReporteServicios)Activator.GetObject(typeof(IReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ReporteServicios"]);
                this._camposReporteDeMarcaServicios = (ICamposReporteRelacionServicios)Activator.GetObject(typeof(ICamposReporteRelacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CamposReporteRelacionServicios"]);
                this._filtroReporteDeMarcaServicios = (IFiltroReporteServicios)Activator.GetObject(typeof(IFiltroReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroReporteServicios"]);
                this._vistaReporteServicios = (IVistaReporteServicios)Activator.GetObject(typeof(IVistaReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["VistaReporteServicios"]);


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


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarReporteDeMarca,
                Recursos.Ids.GeneradorReporteMarca);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarReporteDeMarca, "");

                if (_agregar == false) //Cuando el reporte ya existe
                {

                    Reporte reporteFiltro = new Reporte();
                    reporteFiltro.Id = this._reporteDeMarca.Id;

                    this._reporteDeMarca = this._reporteDeMarcaServicios.ConsultarReporteConTodo(reporteFiltro);
                    
                    this._ventana.IdReporte = this._reporteDeMarca.Id.ToString();
                    this._ventana.DescripcionReporte = this._reporteDeMarca.Descripcion;
                    //this._ventana.TituloReporte = !this._reporteDeMarca.TituloEspanol.Equals(String.Empty) ? this._reporteDeMarca.TituloEspanol : null;
                    this._ventana.TituloReporte = this._reporteDeMarca.TituloEspanol != null ? this._reporteDeMarca.TituloEspanol : null;
                    //this._ventana.TituloReporteIngles = !this._reporteDeMarca.TituloIngles.Equals(String.Empty) ? this._reporteDeMarca.TituloIngles : null;
                    this._ventana.TituloReporteIngles = this._reporteDeMarca.TituloIngles != null ? this._reporteDeMarca.TituloIngles : null;
                    
                    CargarTipoDeReporte();
                    CargarCamposReporte(false);
                    CargarCamposReporteSeleccionado(); 
                    CargarUsuario();
                    CargarIdiomas(false);
                    this._ventana.HabilitarComboVistas(false);

                    //Solo se mostraran los botones de Modificar y Modificacion de Filtros si el Usuario Logueado es el mismo que esta consultando
                    if (UsuarioLogeado.NombreCompleto.Equals(this._reporteDeMarca.Usuario))
                    {
                        IList<FiltroReporte> listaDeFiltros = this._filtroReporteDeMarcaServicios.ConsultarFiltrosReporte(this._reporteDeMarca);
                        if (listaDeFiltros.Count != 0)
                            this._ventana.PintarFiltros();
                    }
                    else
                    {
                        this._ventana.MostarBotonesParaModificarReporte(false);
                    }
                    
                }
                else
                {
                    CargarTipoDeReporte();
                    CargarCamposReporte(true);
                    CargarUsuario();
                    CargarIdiomas(true);
                    this._ventana.ActivarBotonFiltros(false);
                    this._ventana.ActivarBotonValoresParaFiltros(false);

                }

                this._ventana.HabilitarCampos = true;

                this._ventana.FocoPredeterminado();

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
        /// Metodo que carga el combo con las vistas disponibles para los reportes
        /// </summary>
        public void CargarTipoDeReporte()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<VistaReporte> vistas = this._vistaReporteServicios.ConsultarTodos();

                this._ventana.TiposDeReporte = vistas;

                if (this._reporteDeMarca != null)
                {
                    VistaReporte vistaReporte = this._reporteDeMarca.VistaReporte;
                    this._ventana.TipoDeReporte = BuscarVistaReporte(vistas, vistaReporte);
                }

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

        private VistaReporte BuscarVistaReporte(IList<VistaReporte> vistas, VistaReporte vistaReporte)
        {
            VistaReporte retorno = null;
            
            if (vistaReporte != null)
                foreach (VistaReporte vista in vistas)
                {
                    if (vista.Id.Equals(vistaReporte.Id))
                    {
                        retorno = vista;
                        break;
                    }
                }

            return retorno;
        }



        /// <summary>
        /// Metodo que carga todos los campos definidos para un tipo de Reporte. Estos campos son los de la Vista
        /// <param name="agregar">Bandera para saber si se agregar un nuevo reporte o se actualiza uno existente</param>
        /// </summary>
        public void CargarCamposReporte(bool agregar)
        {

            CamposReporte campo;
            IList<CamposReporteRelacion> listaDeCamposDelReporte;
            IList<CamposReporte> campos = new List<CamposReporte>();
            IList<CamposReporte> listaCamposSeleccionados = new List<CamposReporte>();
            int posicion = 0;
            string posiciones = String.Empty;
            string[] posicionesArray = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                VistaReporte vista = null;
                
                if (!agregar) 
                {

                    vista = this._reporteDeMarca.VistaReporte;

                    /*if (vista.NombreVista.Equals("MARCAS"))
                        campos = this._camposReporteServicios.ObtenerCamposReporteDeMarca();
                    else if (vista.NombreVista.Equals("PATENTES"))
                        campos = this._camposReporteServicios.ObtenerCamposReportePatente();*/

                    campos = this._camposReporteServicios.ObtenerCamposPorVista(vista.NombreVistaBD);

                    listaDeCamposDelReporte = this._camposReporteDeMarcaServicios.ConsultarCamposDeReporte(this._reporteDeMarca);
                    foreach (CamposReporteRelacion item in listaDeCamposDelReporte)
                    {
                        campo = item.Campo;
                        listaCamposSeleccionados.Add(campo);
                    }
                    //Se elimina el campo en la lista de la izquierda para que solamente esten en un solo lado 
                    foreach (CamposReporte campoSeleccionado in listaCamposSeleccionados)
                    {
                        foreach (CamposReporte item in campos)
                        {
                            if (campoSeleccionado.EncabezadoEspanol == item.EncabezadoEspanol)
                            {
                                posicion = campos.IndexOf(item);
                                campos.RemoveAt(posicion);
                                break;
                            }
                                
                        }
                        posicion = 0;
                    }

                    this._ventana.CamposReporte = campos;
                }

                
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
        /// Metodo que carga los campos que pertenecen a un reporte seleccionado en la consulta
        /// </summary>
        public void CargarCamposReporteSeleccionado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CamposReporte campo;
                IList<CamposReporteRelacion> listaDeCamposDelReporte;
                IList<CamposReporte> listaCamposAMostrar = new List<CamposReporte>();

                listaDeCamposDelReporte = this._camposReporteDeMarcaServicios.ConsultarCamposDeReporte(this._reporteDeMarca);

                foreach (CamposReporteRelacion item in listaDeCamposDelReporte)
                {
                    campo = item.Campo;
                    listaCamposAMostrar.Add(campo);
                }

                this._ventana.CamposSeleccionados = null;
                this._ventana.CamposSeleccionados = listaCamposAMostrar;

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
        /// Metodo que carga la lista de los usuarios definidos en la base de datos y selecciona el usuario logueado
        /// </summary>
        public void CargarUsuario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                this._ventana.Usuarios = usuarios;
                this._ventana.Usuario = this.BuscarUsuarioPorIniciales(usuarios, UsuarioLogeado);

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
        /// Metodo que carga los idiomas definidos para un reporte de marca
        /// </summary>
        public void CargarIdiomas(bool agregar)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
                this._ventana.Idiomas = idiomas;
                if (!agregar)
                {
                    this._ventana.Idioma = this.BuscarIdioma(idiomas, this._reporteDeMarca.Idioma);
                }

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
        /// Metodo que agrega un campo a la lista de campos seleccionados para un reporte nuevo o existente
        /// </summary>
        public void AgregarCampo()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana.ActivarBotonFiltros(false);

                CamposReporte campoAMover = (CamposReporte)this._ventana.CampoReporte;
                bool exitoso = false;

                if (campoAMover != null)
                {
                    IList<CamposReporte> camposDelReporte = new List<CamposReporte>();
                    if (this._ventana.CamposSeleccionados != null)
                    {
                        camposDelReporte = (IList<CamposReporte>)this._ventana.CamposSeleccionados;
                    }

                    camposDelReporte.Add(campoAMover);
                    this._ventana.CamposSeleccionados = null;
                    this._ventana.CamposSeleccionados = camposDelReporte;

                    IList<CamposReporte> campos = (IList<CamposReporte>)this._ventana.CamposReporte;
                    campos.Remove(campoAMover);
                    this._ventana.CamposReporte = null;
                    this._ventana.CamposReporte = campos;

                    if (!this._agregar)
                    {
                        CamposReporteRelacion campo = new CamposReporteRelacion();
                        campo.Reporte = this._reporteDeMarca;
                        campo.Id = this._reporteDeMarca.Id;
                        campo.Campo = campoAMover;
                        campo.StatusCampo = "N";
                        campo.PosicionCampo = 0;
                        exitoso = this._camposReporteDeMarcaServicios.InsertarOModificar(campo, UsuarioLogeado.Hash);
                    }

                }




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
        /// Metodo que quita un campo de la lista de campos seleccionados para un reporte de marca especifico
        /// </summary>
        public void QuitarCampo()
        {

            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana.ActivarBotonFiltros(false);

                CamposReporte campoAMover = (CamposReporte)this._ventana.CampoSeleccionado;

                if (campoAMover != null)
                {
                    IList<CamposReporte> camposDelReporte = new List<CamposReporte>();
                    if (this._ventana.CamposReporte != null)
                    {
                        camposDelReporte = (IList<CamposReporte>)this._ventana.CamposReporte;
                    }

                    if (!this._agregar)
                    {
                        IList<CamposReporteRelacion> camposRelacion = this._camposReporteDeMarcaServicios.ConsultarCamposDeReporte(this._reporteDeMarca);
                        foreach (CamposReporteRelacion item in camposRelacion)
                        {
                            if (campoAMover.Id == item.Campo.Id)
                            {
                                if (item.CampoFiltro == null)
                                {
                                    camposDelReporte.Add(campoAMover);
                                    this._ventana.CamposReporte = null;
                                    this._ventana.CamposReporte = camposDelReporte;

                                    IList<CamposReporte> campos = (IList<CamposReporte>)this._ventana.CamposSeleccionados;
                                    campos.Remove(campoAMover);
                                    this._ventana.CamposSeleccionados = null;
                                    this._ventana.CamposSeleccionados = campos;

                                    exitoso = this._camposReporteDeMarcaServicios.Eliminar(item,UsuarioLogeado.Hash);
                                    
                                    break;

                                    
                                }
                                else if (item.CampoFiltro.Equals("Y"))
                                {
                                    this._ventana.Mensaje("No puede eliminar este campo porque es filtro para el Reporte", 0);
                                    break;
                                }
                            }

                            
                        }
                    }
                    else
                    {
                        camposDelReporte.Add(campoAMover);
                        this._ventana.CamposReporte = null;
                        this._ventana.CamposReporte = camposDelReporte;

                        IList<CamposReporte> campos = (IList<CamposReporte>)this._ventana.CamposSeleccionados;
                        campos.Remove(campoAMover);
                        this._ventana.CamposSeleccionados = null;
                        this._ventana.CamposSeleccionados = campos;
                    }

                }

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
        /// Metodo que mueve un elemento de la lista de campos seleccionados del reporte hacia arriba
        /// </summary>
        public void SubirCampo()
        {
            int indice;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CamposReporte campoAMover = (CamposReporte)this._ventana.CampoSeleccionado;
                if (campoAMover != null)
                {
                    IList<CamposReporte> listaCamposSeleccionados = (IList<CamposReporte>)this._ventana.CamposSeleccionados;
                    indice = listaCamposSeleccionados.IndexOf(campoAMover);
                    if (indice > 0)
                    {
                        listaCamposSeleccionados.Remove(listaCamposSeleccionados[indice]);
                        listaCamposSeleccionados.Insert(indice - 1, campoAMover);
                        this._ventana.CamposSeleccionados = null;
                        this._ventana.CamposSeleccionados = listaCamposSeleccionados;
                        this._ventana.CampoSeleccionado = listaCamposSeleccionados[indice - 1];
                    }
                }

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
        /// Metodo que mueve un elemento de la lista de campos seleccionados del reporte hacia abajo
        /// </summary>
        public void BajarCampo()
        {

            int indice;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CamposReporte campoAMover = (CamposReporte)this._ventana.CampoSeleccionado;
                if (campoAMover != null)
                {
                    IList<CamposReporte> listaCamposSeleccionados = (IList<CamposReporte>)this._ventana.CamposSeleccionados;
                    indice = listaCamposSeleccionados.IndexOf(campoAMover);
                    if (indice + 1 < listaCamposSeleccionados.Count)
                    {
                        listaCamposSeleccionados.Remove(listaCamposSeleccionados[indice]);
                        listaCamposSeleccionados.Insert(indice + 1, campoAMover);
                        this._ventana.CamposSeleccionados = null;
                        this._ventana.CamposSeleccionados = listaCamposSeleccionados;
                        this._ventana.CampoSeleccionado = listaCamposSeleccionados[indice + 1];
                    }
                }


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
        /// Metodo para guardar los cambios a un reporte nuevo o a uno existente
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = false, exitosoCampo = false, exitosoEliminarCampo = false;
                //Contadores para el Id del reporte y para la posicion de los campos en el reporte al momento de insertar
                int contador, contadorProximoValor, posicion = 1;
                IList<CamposReporteRelacion> listaDeCamposReporte = new List<CamposReporteRelacion>();
                IList<CamposReporte> listaCamposSeleccionados;
                CamposReporteRelacion campoReporteMarca;

                Reporte reporte = CargarReporteDeLaPantalla();

                if (_agregar) //en caso de un reporte nuevo
                {
                    IList<Reporte> reportesDeMarcaExistentes = this._reporteDeMarcaServicios.ConsultarTodos();

                    if (reportesDeMarcaExistentes != null)
                    {
                        if ((!reporte.Descripcion.Equals(String.Empty)) && (!this._ventana.DescripcionReporte.Equals(String.Empty)))
                        {
                            if (reporte.VistaReporte != null)
                            {
                                if ((reporte.TituloEspanol != null) || (reporte.TituloIngles != null))
                                {
                                    contador = reportesDeMarcaExistentes.Count;
                                    contadorProximoValor = contador + 1;
                                    reporte.Id = contadorProximoValor;
                                    //Se guarda el reporte de marca
                                    exitoso = this._reporteDeMarcaServicios.InsertarOModificar(reporte, UsuarioLogeado.Hash);
                                    //Guardar los campos del Reporte si fue exitosa la insercion - Se guardan los campos seleccionados por primera vez
                                    if (exitoso)
                                    {
                                        if (this._ventana.CamposSeleccionados != null)
                                        {
                                            listaCamposSeleccionados = (IList<CamposReporte>)this._ventana.CamposSeleccionados;
                                            foreach (CamposReporte campo in listaCamposSeleccionados)
                                            {
                                                campoReporteMarca = new CamposReporteRelacion();
                                                campoReporteMarca.Reporte = reporte;
                                                campoReporteMarca.Id = reporte.Id;
                                                campoReporteMarca.Campo = campo;
                                                campoReporteMarca.PosicionCampo = posicion;
                                                campoReporteMarca.StatusCampo = "N";
                                                exitosoCampo = this._camposReporteDeMarcaServicios.InsertarOModificar(campoReporteMarca, UsuarioLogeado.Hash);
                                                posicion++;
                                                campoReporteMarca = null;

                                            }

                                            //Se obtienen los campos del reporte de marca y se agregan al reporte
                                            IList<CamposReporteRelacion> camposSeleccionados = this._camposReporteDeMarcaServicios.ConsultarCamposDeReporte(reporte);
                                            reporte.CamposDelReporte = camposSeleccionados;
                                            //this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ConfirmacionReporteModificado,reporte.Id), 1);
                                        }
                                        else
                                            this._ventana.Mensaje("El reporte no tiene campos asociados", 1);

                                        this._reporteDeMarca = reporte;
                                        this._ventana.ActivarBotonFiltros(true);
                                        this._ventana.ActivarBotonValoresParaFiltros(true);
                                    }
                                }
                                else
                                    this._ventana.Mensaje("El Reporte no tiene un Titulo", 0);
                            }
                            else
                                this._ventana.Mensaje("No esta seleccionado el tipo de Reporte", 0);
                        }
                        else
                            this._ventana.Mensaje("El reporte no tiene Descripcion", 0);

                    }
                }

                else
                {
                    if ((!reporte.Descripcion.Equals(String.Empty)) && (!this._ventana.DescripcionReporte.Equals(String.Empty)))
                    {
                        if (reporte.VistaReporte != null)
                        {
                            if ((reporte.TituloEspanol != null) || (reporte.TituloIngles != null))
                            {
                                exitoso = this._reporteDeMarcaServicios.InsertarOModificar(reporte, UsuarioLogeado.Hash);
                                
                                //Lista de los Campos Relacion en base de datos
                                IList<CamposReporteRelacion> camposActualesDelReporte = this._camposReporteDeMarcaServicios.ConsultarCamposDeReporte(this._reporteDeMarca);
                                //Lista de los Campos SELECCIONADOS en la Ventana                                
                                listaCamposSeleccionados = (IList<CamposReporte>)this._ventana.CamposSeleccionados;

                                //Recorremos las listas de los Campos Seleccionados y los Campos Relacion y les actualizamos la posicion
                                foreach (CamposReporte campoSeleccionado in listaCamposSeleccionados)
                                {
                                    foreach (CamposReporteRelacion campoRelacion in camposActualesDelReporte)
                                    {
                                        if (campoRelacion.Campo.Id.Equals(campoSeleccionado.Id))
                                        {
                                            campoRelacion.PosicionCampo = posicion;
                                            exitosoCampo = this._camposReporteDeMarcaServicios.InsertarOModificar(campoRelacion, UsuarioLogeado.Hash);
                                        }
                                    }

                                    posicion++;
                                }

                                //this._ventana.Mensaje("Reporte modificado con éxito", 1);

                                this._reporteDeMarca = reporte;
                                this._ventana.ActivarBotonFiltros(true);
                            }
                            else
                                this._ventana.Mensaje("El Reporte no tiene un Titulo", 0);
                        }
                        else
                            this._ventana.Mensaje("No esta seleccionado el tipo de Reporte", 0);
                    }
                    else
                        this._ventana.Mensaje("El reporte no tiene Descripcion", 0);


                }


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message , true);
            }
        }


        /// <summary>
        /// Devuelve la entidad Reporte armada con todos sus atributos
        /// </summary>
        /// <returns>Reporte de Marca de la Pantalla</returns>
        public Reporte CargarReporteDeLaPantalla()
        {
            Reporte reporte = new Reporte();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._agregar)
                    reporte.Id = int.Parse(this._ventana.IdReporte);

                reporte.Descripcion = this._ventana.DescripcionReporte != null ? this._ventana.DescripcionReporte : null;

                reporte.Usuario = this._ventana.Usuario != null ? ((Usuario)this._ventana.Usuario).NombreCompleto : null;

                reporte.Idioma = this._ventana.Idioma != null ? (Idioma)this._ventana.Idioma : null;

                reporte.VistaReporte = this._ventana.TiposDeReporte != null ? (VistaReporte)this._ventana.TipoDeReporte : null;

                reporte.TituloIngles = !this._ventana.TituloReporteIngles.Equals(String.Empty) ? this._ventana.TituloReporteIngles : null;
                
                reporte.TituloEspanol = !this._ventana.TituloReporte.Equals(String.Empty) ? this._ventana.TituloReporte : null;
                

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

            return reporte;
        }


        /// <summary>
        /// Metodo que llama a la ventana para definir los filtros de un reporte de marca o para modificar los existentes
        /// </summary>
        public void GestionarFiltros()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if(this._reporteDeMarca != null)
                    this.Navegar(new GestionarFiltrosReporte(this._reporteDeMarca, this._ventana));


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
        /// Metodo que carga los campos de los reportes segun la vista seleccionada
        /// Esto solamente se hace cuando se va a insertar un nuevo reporte
        /// </summary>
        public void CargarCamposPorVista()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<CamposReporte> campos = new List<CamposReporte>();
                VistaReporte vista = (VistaReporte)this._ventana.TipoDeReporte;

                if (this._agregar)
                {
                    /*if (vista.NombreVista.Equals("MARCAS"))
                        campos = this._camposReporteServicios.ObtenerCamposReporteDeMarca();
                    else if (vista.NombreVista.Equals("PATENTES"))
                        campos = this._camposReporteServicios.ObtenerCamposReportePatente();*/

                    campos = this._camposReporteServicios.ObtenerCamposPorVista(vista.NombreVistaBD);

                    if (campos.Count != 0)
                    {
                        this._ventana.CamposReporte = campos;
                    }

                    else
                    {
                        this._ventana.Mensaje("No hay campos para el tipo de Reporte seleccionado. Consulte con su Administrador", 1);
                    }

                    
                }


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
        /// Metodo que llama a la ventana para ejecutar un Reporte
        /// </summary>
        public void GestionarValoresParaFiltros()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._reporteDeMarca != null)
                    this.Navegar(new GestionarValoresFiltrosDeReporte(this._reporteDeMarca,this._ventana));

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
        /// Metodo que retorna el Id del Reporte para mensaje de confirmacion
        /// </summary>
        /// <param name="reporte">Reporte guardado exitosamente</param>
        /// <returns>Id del Reporte</returns>
        public String ObtenerIdReporte()
        {
            return this._reporteDeMarca.Id.ToString();
        }
    }
}
