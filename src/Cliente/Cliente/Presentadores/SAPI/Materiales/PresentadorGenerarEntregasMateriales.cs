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
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales
{
    class PresentadorGenerarEntregasMateriales : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGenerarEntregasMateriales _ventana;
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
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGenerarEntregasMateriales(IGenerarEntregasMateriales ventana, object ventanaPadre)
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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGenerarEntregasMaterialSAPI,
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

                this._ventana.MostrarBotonRecepcionMateriales();

                SolicitudSapi solicitudSapi = new SolicitudSapi();

                this._ventana.SolicitudSapiFiltro = solicitudSapi;

                this._ventana.TotalHits = "0";

                if (UsuarioLogeado.BEntregaMaterial)
                    this._ventana.MostarBotonEntregarMateriales();

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

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                departamentos.Insert(0, new Departamento("NGN"));
                this._ventana.Departamentos = departamentos;

                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                usuarios.Insert(0, new Usuario("NGN"));
                this._ventana.Usuarios = usuarios;
                this._ventana.Usuario = this.BuscarUsuarioPorIniciales(usuarios, UsuarioLogeado);


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
        /// MEtodo que se trae todos los movimientos que se van a registrar como Entregas
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

                SolicitudSapi solicitudAux = new SolicitudSapi();
                this._filtroValido = 0;

                solicitudAux = ObtenerSolicitudFiltroPantalla();

                
                if (this._filtroValido >= 2)
                {
                    IList<SolicitudSapi> solicitudesSapi = this._solicitudSapiServicios.ObtenerSolicitudesSapiPendientesFiltro(solicitudAux);
                    if (solicitudesSapi.Count > 0)
                    {
                        IList<SolicitudSapi> solicitudesResultantes = solicitudesSapi.OrderBy(o => o.FechaSolicitud).ThenBy(o => o.Id).ToList();
                        this._ventana.Resultados = solicitudesResultantes;

                        this._ventana.TotalHits = solicitudesResultantes.Count.ToString();
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
        /// Metodo que duelve los filtros de la pantalla para obtener los movimientos a generar su Entrega
        /// </summary>
        /// <returns>Solicitud Sapi usada como filtro para generar la Entrega</returns>
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

                if ((this._ventana.FechaSolicitudSapi != null) && (!this._ventana.FechaSolicitudSapi.Equals("")))
                {
                    solicitud.FechaSolicitud = DateTime.Parse(this._ventana.FechaSolicitudSapi);
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
        /// Metodo que inicializa la ventana actual
        /// </summary>
        public void LimpíarCampos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new GenerarEntregasMateriales());

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
        /// Metodo que registra la Entrega de Materiales de cada uno de los movimientos de Materiales Sapi seleccionados en la ventana
        /// </summary>
        public void GenerarEntregaMateriales()
        {
            IList<SolicitudSapi> solicitudesEntregar = new List<SolicitudSapi>();
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.Resultados != null)
                {
                    IList<SolicitudSapi> movimientos = (IList<SolicitudSapi>)this._ventana.Resultados;
                    foreach (SolicitudSapi movimiento in movimientos)
                    {
                        if ((movimiento.BEntregado) && (movimiento.FechaEntrega == null))
                            solicitudesEntregar.Add(movimiento);
                    }
                    if (solicitudesEntregar.Count > 0)
                    {
                        ProcesarEntregasMaterial(solicitudesEntregar);
                        this._ventana.Mensaje("Generacion de Entregas de Materiales Terminada", 2);
                        Consultar();
                    }
                    else
                        this._ventana.Mensaje("Seleccione al menos una Solicitud para procesar la Entrega de Material", 0);
                }
                else
                    this._ventana.Mensaje("No existen Solicitudes con Materiales a Entregar", 0);

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
        /// Metodo que realiza la Entrega de los Materiales 
        /// </summary>
        /// <param name="solicitudesEntregar"></param>
        /// <returns></returns>
        private void ProcesarEntregasMaterial(IList<SolicitudSapi> solicitudesEntregar)
        {
            String mensajeNoProcesadas = String.Empty;
            int contadorNoProcesadas = 0;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<SolicitudSapi> movimientoModificado = movimientoModificado = new List<SolicitudSapi>();

                /* Procesamiento de la Entrega de Material comprende: 
                 * 0. Se verifica si el movimiento ya fue procesado anteriormente
                 * 1. Consulta del Material a entregar
                 * 2. Actualizacion de la Existencia
                 * 3. Cambio del Status del Movimiento de SOLICITUD a ENTREGA
                 * */
                foreach (SolicitudSapi movimientoSapi in solicitudesEntregar)
                {
                    if (movimientoSapi.FechaEntrega == null)
                    {
                        MaterialSapi material = new MaterialSapi();
                        material.Id = movimientoSapi.Material.Id;
                        IList<MaterialSapi> materialesEncontrados = this._materialSapiServicios.ObtenerMaterialSapiFiltro(material);
                        material = materialesEncontrados[0];

                        if (movimientoSapi.CantMaterialSol < material.Existencia)
                        {
                            material.Existencia -= movimientoSapi.CantMaterialSol;
                            bool exito = this._materialSapiServicios.InsertarOModificar(material, UsuarioLogeado.Hash);
                            movimientoSapi.FechaEntrega = DateTime.Today;
                            movimientoSapi.TipoMovimiento = "ENTREGADO";
                            movimientoModificado.Add(movimientoSapi);
                            continue;
                        }
                        else
                        {
                            movimientoSapi.BEntregado = false;
                            contadorNoProcesadas++;
                            continue;
                        }
                    }
                }

                /* Actualizacion de los movimientos */
                if (movimientoModificado.Count > 0)
                {
                    bool exitoso = this._solicitudSapiServicios.InsertarOModificarSolicitudMaterialSapi(ref movimientoModificado, "MODIFY", UsuarioLogeado.Hash);

                    if (movimientoModificado.Count < solicitudesEntregar.Count)
                        this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.AlertaSolicitudesSapiNoProcesadas, contadorNoProcesadas.ToString()), 1);

                }
                else
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorEntregaSapiNoProcesadas, 0);
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
        /// Metodo para RECIBIR Materiales Sapi 
        /// </summary>
        public void GenerarRecepcionMateriales()
        {

            IList<SolicitudSapi> solicitudesRecibir = new List<SolicitudSapi>();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.Resultados != null)
                {
                    IList<SolicitudSapi> movimientos = (IList<SolicitudSapi>)this._ventana.Resultados;
                    foreach (SolicitudSapi movimiento in movimientos)
                    {
                        if ((movimiento.BRecibido) && (movimiento.FechaRecepcion == null))
                            solicitudesRecibir.Add(movimiento);
                    }
                    if (solicitudesRecibir.Count > 0)
                    {
                        ProcesarRecepcionDeMaterial(solicitudesRecibir);
                        Consultar();
                    }
                    else
                        this._ventana.Mensaje("Seleccione al menos una Solicitud para procesar la Recepción de Material", 0);
                }
                else
                    this._ventana.Mensaje("No existen Solicitudes con Materiales a Recibir", 0);

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
        /// Metodo que realiza la RECEPCION del Material ENTREGADO
        /// </summary>
        /// <param name="solicitudesRecibir">Lista de Movimientos recibidos por el Usuario</param>
        private void ProcesarRecepcionDeMaterial(IList<SolicitudSapi> solicitudesRecibir)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (SolicitudSapi movimientoSapi in solicitudesRecibir)
                {
                    movimientoSapi.FechaRecepcion = DateTime.Today;
                    movimientoSapi.TipoMovimiento = "RECIBIDO";
                }

                bool exitoso = this._solicitudSapiServicios.InsertarOModificarSolicitudMaterialSapi(ref solicitudesRecibir, "MODIFY", UsuarioLogeado.Hash);

                if (exitoso)
                {
                    this._ventana.Mensaje("Recepcion de Materiales realizada con éxito", 2);
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
    }
}
