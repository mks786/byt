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
using Trascend.Bolet.Cliente.Contratos.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Traspasos.Licencias
{
    class PresentadorGestionarLicencia : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _validar = true;
        private IGestionarLicencia _ventana;

        private IMarcaServicios _marcaServicios;
        private IAnaquaServicios _anaquaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IAgenteServicios _agenteServicios;
        private IPoderServicios _poderServicios;
        private IBoletinServicios _boletinServicios;
        private IPaisServicios _paisServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IInteresadoServicios _interesadoServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;
        private ICorresponsalServicios _corresponsalServicios;
        private ICondicionServicios _condicionServicios;
        private IInfoAdicionalServicios _infoAdicionalServicios;
        private IInfoBolServicios _infoBolServicios;
        private IOperacionServicios _operacionServicios;
        private IBusquedaServicios _busquedaServicios;
        private IStatusWebServicios _statusWebServicios;

        private IList<Interesado> _interesadosLicenciante;
        private IList<Interesado> _interesadosLicenciatario;
        private IList<Agente> _agentesLicenciatario;
        private IList<Agente> _agentesLicenciante;
        private IList<Marca> _marcas;

        private IList<Poder> _poderesLicenciante;
        private IList<Poder> _poderesLicenciatario;

        private IList<Poder> _poderesApoderadosLicenciante;
        private IList<Poder> _poderesApoderadosLicenciatario;

        private IList<Poder> _poderesInterseccionLicenciante;
        private IList<Poder> _poderesInterseccionLicenciatario;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarLicencia(IGestionarLicencia ventana, object licencia)
        {
            try
            {

                this._ventana = ventana;

                this._ventana.Licencia = licencia;

                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                this._tipoEstadoServicios = (ITipoEstadoServicios)Activator.GetObject(typeof(ITipoEstadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEstadoServicios"]);
                this._corresponsalServicios = (ICorresponsalServicios)Activator.GetObject(typeof(ICorresponsalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CorresponsalServicios"]);
                this._condicionServicios = (ICondicionServicios)Activator.GetObject(typeof(ICondicionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CondicionServicios"]);
                this._anaquaServicios = (IAnaquaServicios)Activator.GetObject(typeof(IAnaquaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnaquaServicios"]);
                this._infoAdicionalServicios = (IInfoAdicionalServicios)Activator.GetObject(typeof(IInfoAdicionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoAdicionalServicios"]);
                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._busquedaServicios = (IBusquedaServicios)Activator.GetObject(typeof(IBusquedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BusquedaServicios"]);
                this._statusWebServicios = (IStatusWebServicios)Activator.GetObject(typeof(IStatusWebServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["StatusWebServicios"]);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarLicencia,
                Recursos.Ids.GestionarLicencias);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarca, "");

                Licencia licencia = (Licencia)this._ventana.Licencia;


                this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(licencia.Marca);                                
                
                this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;

                this._ventana.ApoderadoLicenciante = licencia.AgenteLicenciatario;
                this._ventana.ApoderadoLicenciatario = licencia.AgenteLicenciatario;
                this._ventana.PoderLicenciante = licencia.PoderLicenciante;
                this._ventana.PoderLicenciatario = licencia.PoderLicenciatario;

                this._marcas = new List<Marca>();
                Marca primeraMarca = new Marca(int.MinValue);
                this._marcas.Add(primeraMarca);

                if ((Marca)this._ventana.Marca != null)
                    this._marcas.Add((Marca)this._ventana.Marca);

                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.MarcaFiltrada = (Marca)this._ventana.Marca;

                
                
                this._interesadosLicenciante = new List<Interesado>();
                Interesado primerInteresado = new Interesado(int.MinValue);
                this._interesadosLicenciante.Add(primerInteresado);

                if (licencia.InteresadoLicenciante != null)
                {
                    this._ventana.InteresadoLicenciante = this._interesadoServicios.ConsultarInteresadoConTodo(licencia.InteresadoLicenciante);
                    this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;

                    if ((Interesado)this._ventana.InteresadoLicenciante != null)
                    {
                        this._interesadosLicenciante.Add((Interesado)this._ventana.InteresadoLicenciante);
                        this._ventana.LicencianteFiltrado = (Interesado)this._ventana.InteresadoLicenciante;
                        this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;
                    }
                }
                else
                {
                    this._ventana.InteresadoLicenciante = primerInteresado;
                    this._ventana.LicencianteFiltrado = primerInteresado;
                    this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;

                }                                

                this._interesadosLicenciatario = new List<Interesado>();
                this._interesadosLicenciatario.Add(primerInteresado);

                if (licencia.InteresadoLicenciatario != null)
                {
                    this._ventana.InteresadoLicenciatario = this._interesadoServicios.ConsultarInteresadoConTodo(licencia.InteresadoLicenciatario);
                    this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;

                    if ((Interesado)this._ventana.InteresadoLicenciatario != null)
                    {
                        this._interesadosLicenciatario.Add((Interesado)this._ventana.InteresadoLicenciatario);
                        this._ventana.LicenciatarioFiltrado = (Interesado)this._ventana.InteresadoLicenciatario;
                        this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                    }
                }
                else
                {
                    this._ventana.InteresadoLicenciatario = primerInteresado;
                    this._ventana.LicenciatarioFiltrado = primerInteresado;
                    this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                }                

                this._agentesLicenciante = new List<Agente>();
                Agente primerAgente = new Agente("");
                this._agentesLicenciante.Add(primerAgente);

                if (licencia.AgenteLicenciante != null)
                {
                    this._agentesLicenciante.Add((Agente)this._ventana.ApoderadoLicenciante);
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.ApoderadoLicencianteFiltrado = (Agente)this._ventana.ApoderadoLicenciante;
                }
                else
                {
                    this._ventana.ApoderadoLicenciante = primerAgente;
                    this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                }                

                this._agentesLicenciatario = new List<Agente>();
                this._agentesLicenciatario.Add(primerAgente);

                if (licencia.AgenteLicenciatario != null)
                {
                    this._agentesLicenciatario.Add((Agente)this._ventana.ApoderadoLicenciatario);
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                    this._ventana.ApoderadoLicenciatarioFiltrado = (Agente)this._ventana.ApoderadoLicenciatario;
                }
                else
                {
                    this._ventana.ApoderadoLicenciatario = primerAgente;
                    this._ventana.ApoderadoLicenciatarioFiltrado = primerAgente;
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                }                                

                this._poderesLicenciante = new List<Poder>();
                Poder primerPoder = new Poder(int.MinValue);
                this._poderesLicenciante.Add(primerPoder);

                if (licencia.PoderLicenciante != null)
                {
                    this._poderesLicenciante.Add((Poder)this._ventana.PoderLicenciante);
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.PoderLicencianteFiltrado = (Poder)this._ventana.PoderLicenciante;
                }
                else
                {
                    this._ventana.PoderLicenciante = primerPoder;
                    this._ventana.PoderLicencianteFiltrado = primerPoder;
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                }                

                this._poderesLicenciatario = new List<Poder>();
                this._poderesLicenciatario.Add(primerPoder);

                if (licencia.PoderLicenciatario != null)
                {
                    this._poderesLicenciatario.Add((Poder)this._ventana.PoderLicenciatario);
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = (Poder)this._ventana.PoderLicenciatario;
                }
                else
                {
                    this._ventana.PoderLicenciatario = primerPoder;
                    this._ventana.PoderLicenciatarioFiltrado = primerPoder;
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                }                

                LlenarListasPoderes((Licencia)this._ventana.Licencia);

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

        public void IrConsultarMarcas()
        {
            this.Navegar(new ConsultarMarcas());
        }

        public Licencia CargarlicenciaDeLaPantalla()
        {

            Licencia licencia = (Licencia)this._ventana.Licencia;

            return licencia;
        }

        public void CambiarAModificar()
        {
            this._ventana.HabilitarCampos = true;
            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
        }

        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos del usuario
        /// </summary>
        public void Modificar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del Pais
                else
                {
                    Licencia licencia = CargarlicenciaDeLaPantalla();

                    //bool exitoso = this._marcaServicios.InsertarOModificar(fusion, UsuarioLogeado.Hash);

                    //if (exitoso)
                    //    this.Navegar(Recursos.MensajesConElUsuario.MarcaModificada, false);
                }

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
        }

        /// <summary>
        /// Metodo que se encarga de eliminar una Marca
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //if (this._anexoServicios.Eliminar((Anexo)this._ventana.Anexo, UsuarioLogeado.Hash))
                //{
                //    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PaisEliminado;
                //    this.Navegar(_paginaPrincipal);
                //}

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
        }

        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column, ListView ListaResultados)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        public void ConsultarMarcas()
        {

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Marca marca = new Marca();
                IList<Marca> marcasFiltradas;
                marca.Descripcion = this._ventana.NombreMarcaFiltrar.ToUpper();
                marca.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);

                if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                    marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marca);
                else
                    marcasFiltradas = new List<Marca>();

                if (marcasFiltradas.ToList<Marca>().Count != 0)
                {
                    marcasFiltradas.Insert(0, new Marca(int.MinValue));
                    this._ventana.MarcasFiltradas = marcasFiltradas.ToList<Marca>();
                }
                else
                {
                    marcasFiltradas.Insert(0, new Marca(int.MinValue));
                    this._ventana.MarcasFiltradas = this._marcas;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        }

        public void ConsultarLicenciantes()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado Licenciante = new Interesado();
                IList<Interesado> LicenciantesFiltrados;
                Licenciante.Nombre = this._ventana.NombreLicencianteFiltrar.ToUpper();
                Licenciante.Id = this._ventana.IdLicencianteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdLicencianteFiltrar);

                if ((!Licenciante.Nombre.Equals("")) || (Licenciante.Id != 0))
                    LicenciantesFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(Licenciante);
                else
                    LicenciantesFiltrados = new List<Interesado>();

                if (LicenciantesFiltrados.ToList<Interesado>().Count != 0)
                {
                    LicenciantesFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.LicenciantesFiltrados = LicenciantesFiltrados.ToList<Interesado>();
                }
                else
                {
                    LicenciantesFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        }

        public void ConsultarApoderadosLicenciante()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoLicenciante = new Agente();
                IList<Agente> agentesLicencianteFiltrados;
                apoderadoLicenciante.Nombre = this._ventana.NombreApoderadoLicencianteFiltrar.ToUpper();
                apoderadoLicenciante.Id = this._ventana.IdApoderadoLicencianteFiltrar.ToUpper();

                if ((!apoderadoLicenciante.Nombre.Equals("")) || (!apoderadoLicenciante.Id.Equals("")))
                    agentesLicencianteFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoLicenciante);
                else
                    agentesLicencianteFiltrados = new List<Agente>();

                if (agentesLicencianteFiltrados.ToList<Agente>().Count != 0)
                {
                    agentesLicencianteFiltrados.Insert(0, new Agente(""));                   
                    this._ventana.ApoderadosLicencianteFiltrados = agentesLicencianteFiltrados;
                }
                else
                {
                    agentesLicencianteFiltrados.Insert(0, new Agente(""));
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        }

        public void ConsultarPoderesLicenciante()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderLicenciante = new Poder();
                IList<Poder> poderesLicencianteFiltrados;                
                
                if (!this._ventana.IdPoderLicencianteFiltrar.Equals(""))
                    poderLicenciante.Id = int.Parse(this._ventana.IdPoderLicencianteFiltrar);

                if (!this._ventana.FechaPoderLicencianteFiltrar.Equals(""))
                    poderLicenciante.Fecha = DateTime.Parse(this._ventana.FechaPoderLicencianteFiltrar);

                if ((!poderLicenciante.Fecha.Equals("")) || (poderLicenciante.Id != 0))
                    poderesLicencianteFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderLicenciante);
                else
                    poderesLicencianteFiltrados = new List<Poder>();

                if (poderesLicencianteFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesLicencianteFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;                    
                    this._ventana.PoderesLicencianteFiltrados = poderesLicencianteFiltrados;                    
                }
                else
                {
                    poderesLicencianteFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
                

                Mouse.OverrideCursor = null;

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
        }

        public void ConsultarLicenciatarios()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado Licenciatario = new Interesado();
                IList<Interesado> LicenciatariosFiltrados;
                Licenciatario.Nombre = this._ventana.NombreLicenciatarioFiltrar.ToUpper();
                Licenciatario.Id = this._ventana.IdLicenciatarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdLicenciatarioFiltrar);

                if ((!Licenciatario.Nombre.Equals("")) || (Licenciatario.Id != 0))
                    LicenciatariosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(Licenciatario);
                else
                    LicenciatariosFiltrados = new List<Interesado>();

                if (LicenciatariosFiltrados.ToList<Interesado>().Count != 0)
                {
                    LicenciatariosFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.LicenciatariosFiltrados = LicenciatariosFiltrados.ToList<Interesado>();
                }
                else
                {
                    LicenciatariosFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        }

        public void ConsultarApoderadosLicenciatario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoLicenciatario = new Agente();
                IList<Agente> agentesLicenciatarioFiltrados;
                apoderadoLicenciatario.Nombre = this._ventana.NombreApoderadoLicenciatarioFiltrar.ToUpper();
                apoderadoLicenciatario.Id = this._ventana.IdApoderadoLicenciatarioFiltrar.ToUpper();

                if ((!apoderadoLicenciatario.Nombre.Equals("")) || (!apoderadoLicenciatario.Id.Equals("")))
                    agentesLicenciatarioFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoLicenciatario);
                else
                    agentesLicenciatarioFiltrados = new List<Agente>();

                if (agentesLicenciatarioFiltrados.ToList<Agente>().Count != 0)
                {
                    agentesLicenciatarioFiltrados.Insert(0, new Agente(""));
                    this._ventana.ApoderadosLicenciatarioFiltrados = agentesLicenciatarioFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesLicenciatarioFiltrados.Insert(0, new Agente(""));
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        }

        public void ConsultarPoderesLicenciatario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderLicenciatario = new Poder();
                IList<Poder> poderesLicenciatarioFiltrados;

                if (!this._ventana.IdPoderLicenciatarioFiltrar.Equals(""))
                    poderLicenciatario.Id = int.Parse(this._ventana.IdPoderLicenciatarioFiltrar);

                if (!this._ventana.FechaPoderLicenciatarioFiltrar.Equals(""))
                    poderLicenciatario.Fecha = DateTime.Parse(this._ventana.FechaPoderLicenciatarioFiltrar);

                if ((!poderLicenciatario.Fecha.Equals("")) || (poderLicenciatario.Id != 0))
                    poderesLicenciatarioFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderLicenciatario);
                else
                    poderesLicenciatarioFiltrados = new List<Poder>();

                if (poderesLicenciatarioFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesLicenciatarioFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesLicenciatarioFiltrados = poderesLicenciatarioFiltrados.ToList<Poder>();
                }
                else
                {
                    poderesLicenciatarioFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        }

        public bool CambiarMarca()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.MarcaFiltrada != null)
                {
                    this._ventana.Marca = this._ventana.MarcaFiltrada;
                    this._ventana.NombreMarca = ((Marca)this._ventana.MarcaFiltrada).Descripcion;
                    retorno = true;
                }

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

            return retorno;
        }

        public bool CambiarLicenciante()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.LicencianteFiltrado != null)
                {
                    if ((this._ventana.ApoderadoLicencianteFiltrado != null) && (((Interesado)this._ventana.LicencianteFiltrado).Nombre != null) &&
                        (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre != null))
                    {
                        _poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicencianteFiltrado));

                        if ((this._ventana.LicencianteFiltrado != null) && (this.ValidarListaDePoderes(_poderesLicenciante, _poderesApoderadosLicenciante, "Licenciante")))
                        {
                            this._ventana.InteresadoLicenciante = this._ventana.LicencianteFiltrado;
                            this._ventana.NombreLicenciante = ((Interesado)this._ventana.LicencianteFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesLicenciante, _poderesApoderadosLicenciante, "Licenciante"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Licenciante"), 0);
                        }
                    }
                    else if (this._ventana.LicencianteFiltrado != null)
                    {
                        this._ventana.InteresadoLicenciante = this._ventana.LicencianteFiltrado;
                        this._ventana.NombreLicenciante = ((Interesado)this._ventana.LicencianteFiltrado).Nombre;
                        retorno = true;
                    }
                }

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

            return retorno;
        }

        public bool CambiarApoderadoLicenciante()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ApoderadoLicenciatarioFiltrado != null)
                {
                    if ((this._ventana.LicencianteFiltrado != null) && (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre != null) &&
                        (((Interesado)this._ventana.LicencianteFiltrado).Nombre != null))
                    {
                        _poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicencianteFiltrado));

                        if ((this._ventana.ApoderadoLicencianteFiltrado != null) && (this.ValidarListaDePoderes(_poderesLicenciante, _poderesApoderadosLicenciante, "Licenciante")))
                        {
                            this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                            this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesLicenciante, _poderesApoderadosLicenciante, "Licenciante"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Licenciante"), 0);
                        }
                    }
                    else if (this._ventana.ApoderadoLicencianteFiltrado != null)
                    {
                        this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                        this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                        retorno = true;
                    }
                }

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

            return retorno;
        }

        public bool CambiarPoderLicenciante()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                
                if (this._ventana.PoderLicencianteFiltrado != null)
                {
                    if (this._validar)
                    {
                        if ((((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue))
                            if ((((Interesado)this._ventana.LicencianteFiltrado).Id == int.MinValue))
                                if (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                                {
                                    LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicencianteFiltrado);

                                    this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                                    this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                                    retorno = true;

                                }
                        if (((Poder)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)                            
                        {
                            this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                            this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (((Interesado)this._ventana.LicencianteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoLicenciante = this._ventana.LicencianteFiltrado;
                            this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (!((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                        {
                            this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                            this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                }

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

            return retorno;
        }

        public bool CambiarLicenciatario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.LicenciatarioFiltrado != null)
                {
                    if ((this._ventana.ApoderadoLicenciatarioFiltrado != null) && (((Interesado)this._ventana.LicenciatarioFiltrado).Nombre != null) &&
                        ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre != null)
                    {
                        _poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicenciatarioFiltrado));

                        if ((this._ventana.LicenciatarioFiltrado != null) && (this.ValidarListaDePoderes(_poderesLicenciatario, _poderesApoderadosLicenciante, "Licenciatario")))
                        {

                            this._ventana.InteresadoLicenciatario = this._ventana.LicenciatarioFiltrado;
                            this._ventana.NombreLicenciatario = ((Interesado)this._ventana.LicenciatarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesLicenciatario, _poderesApoderadosLicenciatario, "Licenciatario"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Licenciatario"), 0);
                        }
                    }
                    if (this._ventana.LicenciatarioFiltrado != null)
                    {

                        this._ventana.InteresadoLicenciatario = this._ventana.LicenciatarioFiltrado;
                        this._ventana.NombreLicenciatario = ((Interesado)this._ventana.LicenciatarioFiltrado).Nombre;
                        retorno = true;
                    }
                }

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

            return retorno;
        }

        public bool CambiarPoderLicenciatario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.PoderLicenciatarioFiltrado != null)
                {
                    if (this._validar)
                    {
                        if ((((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue))
                            if ((((Interesado)this._ventana.LicenciatarioFiltrado).Id == int.MinValue))
                                if (((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                                {
                                    LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatarioFiltrado);

                                    this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                                    this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                                    retorno = true;

                                }
                        if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                        {
                            this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                            this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (((Interesado)this._ventana.LicenciatarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                            this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (!((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                        {
                            this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                            this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                }

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

            return retorno;
        }

        public bool CambiarApoderadoLicenciatario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ApoderadoLicenciatarioFiltrado != null)
                {
                    if ((this._ventana.LicenciatarioFiltrado != null) && (((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre != null) &&
                        ((Interesado)this._ventana.LicenciatarioFiltrado).Nombre != null)
                    {
                        _poderesApoderadosLicenciatario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicenciatarioFiltrado));

                        if ((this._ventana.ApoderadoLicenciatarioFiltrado != null) && (this.ValidarListaDePoderes(_poderesLicenciatario, _poderesApoderadosLicenciatario, "Licenciatario")))
                        {
                            this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                            this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesLicenciatario, _poderesApoderadosLicenciatario, "Licenciatario"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Licenciatario"), 0);
                        }
                    }
                    else if ((this._ventana.ApoderadoLicenciatarioFiltrado != null))
                    {
                        this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                        this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                        retorno = true;
                    }
                }

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

            return retorno;
        }

        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB, string tipo)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> listaIntereseccionLicenciante = new List<Poder>();
            IList<Poder> listaIntereseccionLicenciatario = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);
            
            listaIntereseccionLicenciante.Add(primerPoder);
            listaIntereseccionLicenciatario.Add(primerPoder);

            if ((listaPoderesA.Count != 0) && (listaPoderesB.Count != 0))
            {
                foreach (Poder poderA in listaPoderesA)
                {
                    foreach (Poder poderB in listaPoderesB)
                    {
                        if ((poderA.Id == poderB.Id) && (tipo.Equals("Licenciante")))
                        {
                            listaIntereseccionLicenciante.Add(poderA);
                            retorno = true;
                        }

                        else if ((poderA.Id == poderB.Id) && (tipo.Equals("Licenciatario")))
                        {
                            listaIntereseccionLicenciatario.Add(poderA);
                            retorno = true;
                        }
                    }

                }

                if ((listaIntereseccionLicenciante.Count != 0) && (tipo.Equals("Licenciante")))
                {
                    _poderesInterseccionLicenciante = listaIntereseccionLicenciante;
                    this._ventana.PoderesLicencianteFiltrados = listaIntereseccionLicenciante;                    
                }


                else if ((listaIntereseccionLicenciatario.Count != 0) && (tipo.Equals("Licenciatario")))
                {
                    _poderesInterseccionLicenciatario = listaIntereseccionLicenciatario;
                    this._ventana.PoderesLicenciatarioFiltrados = listaIntereseccionLicenciante;                    
                }

                else
                    retorno = false;
            }
            
            this._validar = !retorno;

            Mouse.OverrideCursor = null;
            
            return retorno;
        }

        public void LlenarListasPoderes(Licencia licencia)
        {

            if (licencia.InteresadoLicenciante != null)
                _poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(licencia.InteresadoLicenciante);

            if (licencia.InteresadoLicenciatario != null)
                _poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(licencia.InteresadoLicenciatario);

            if (licencia.AgenteLicenciante != null)
                _poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(licencia.AgenteLicenciante);

            if (licencia.AgenteLicenciatario != null)
                _poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(licencia.AgenteLicenciatario);
        }

        public void LlenarListaAgenteEInteresado(Poder poder)
        {            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                Interesado Licenciante = new Interesado();                
                Agente apoderadoLicenciante = new Agente();
                IList<Agente> agentesLicencianteFiltrados;                
                IList<Interesado> LicenciantesFiltrados = new List<Interesado>();                
                Poder poderFiltrar = new Poder();
                
                Interesado primerInteresado = new Interesado(int.MinValue);
                Agente primerAgente = new Agente("");

                agentesLicencianteFiltrados = new List<Agente>();
                                
                poderFiltrar.Id = this._ventana.IdPoderLicencianteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderLicencianteFiltrar);

                if (poderFiltrar.Id != 0)
                {                    
                    Licenciante = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderLicencianteFiltrado);
                    agentesLicencianteFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderLicencianteFiltrado);                                         
                }                                    

                if (Licenciante != null)
                {
                    LicenciantesFiltrados.Insert(0, primerInteresado);
                    LicenciantesFiltrados.Add(Licenciante);
                    this._ventana.LicenciantesFiltrados = LicenciantesFiltrados;
                    this._ventana.LicencianteFiltrado = primerInteresado;
                }
                else
                {
                    LicenciantesFiltrados.Insert(0, primerInteresado);                                        
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                    this._ventana.LicencianteFiltrado = primerInteresado;
                }                
                
                if (agentesLicencianteFiltrados.Count != 0)
                {
                    agentesLicencianteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosLicencianteFiltrados = agentesLicencianteFiltrados;
                    this._ventana.ApoderadoLicencianteFiltrado = primerAgente;

                }
                else
                {
                    agentesLicencianteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                    this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
                }
                
                
              
                Mouse.OverrideCursor = null;

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
            
        }

        public bool VerificarCambioPoder()
        {
            if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                return true;

            return false;
        }
    }
}
