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
using Trascend.Bolet.Cliente.Contratos.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Traspasos.Cesiones
{
    class PresentadorGestionarCesion : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _validar = true;
        private IGestionarCesion _ventana;

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

        private IList<Interesado> _interesadosCedente;
        private IList<Interesado> _interesadosCesionario;
        private IList<Agente> _agentesCesionario;
        private IList<Agente> _agentesCedente;
        private IList<Marca> _marcas;

        private IList<Poder> _poderesCedente;
        private IList<Poder> _poderesCesionario;

        private IList<Poder> _poderesApoderadosCedente;
        private IList<Poder> _poderesApoderadosCesionario;

        private IList<Poder> _poderesInterseccionCedente;
        private IList<Poder> _poderesInterseccionCesionario;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCesion(IGestionarCesion ventana, object cesion)
        {
            try
            {

                this._ventana = ventana;

                this._ventana.Cesion = cesion;

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCesion,
                Recursos.Ids.GestionarCesion);
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

                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");
                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

                Cesion cesion = (Cesion)this._ventana.Cesion;
                
                this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(cesion.Marca);                                
                
                this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;                
                
                this._ventana.ApoderadoCedente = cesion.AgenteCedente;
                this._ventana.ApoderadoCesionario = cesion.AgenteCesionario;
                this._ventana.PoderCedente = cesion.PoderCedente;
                this._ventana.PoderCesionario = cesion.PoderCesionario;
                
                CargarMarca();
                                                             
                CargarInteresado("Cedente");             
                
                CargarApoderado("Cedente");
                            
                CargarPoder("Cedente");            
               
                CargarInteresado("Cesionario");                                        
                
                CargarApoderado("Cesionario");                         
                               
                CargarPoder("Cesionario");                          

                LlenarListasPoderes((Cesion)this._ventana.Cesion);
                
                ValidarCedente();

                ValidarCesionario();

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

        private void CargarInteresado(string tipo)
        {
            Interesado primerInteresado = new Interesado(int.MinValue);

            if (tipo.Equals("Cedente"))
            {
                this._interesadosCedente = new List<Interesado>();

                this._interesadosCedente.Add(primerInteresado);

                if (((Cesion)this._ventana.Cesion).Cedente != null)
                {
                    this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo(((Cesion)this._ventana.Cesion).Cedente);
                    this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;                    

                    if ((Interesado)this._ventana.InteresadoCedente != null)
                    {
                        this._interesadosCedente.Add((Interesado)this._ventana.InteresadoCedente);
                        this._ventana.CedentesFiltrados = this._interesadosCedente;
                        this._ventana.CedenteFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.CedentesFiltrados, (Interesado)this._ventana.InteresadoCedente);                        
                    }
                }
                else
                {
                    this._ventana.InteresadoCedente = primerInteresado;                    
                    this._ventana.CedentesFiltrados = this._interesadosCedente;
                    this._ventana.CedenteFiltrado = primerInteresado;

                }
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._interesadosCesionario = new List<Interesado>();
                this._interesadosCesionario.Add(primerInteresado);

                if (((Cesion)this._ventana.Cesion).Cesionario != null)
                {
                    this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo(((Cesion)this._ventana.Cesion).Cesionario);
                    this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;

                    if ((Interesado)this._ventana.InteresadoCesionario != null)
                    {
                        this._interesadosCesionario.Add((Interesado)this._ventana.InteresadoCesionario);                        
                        this._ventana.CesionariosFiltrados = this._interesadosCesionario;
                        this._ventana.CesionarioFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.CesionariosFiltrados, (Interesado)this._ventana.InteresadoCesionario);
                    }
                }
                else
                {
                    this._ventana.InteresadoCesionario = primerInteresado;                    
                    this._ventana.CesionariosFiltrados = this._interesadosCesionario;
                    this._ventana.CesionarioFiltrado = primerInteresado;
                }   
            }
        }

        private void CargarApoderado(string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Cedente"))
            {
                this._agentesCedente = new List<Agente>();                
                this._agentesCedente.Add(primerAgente);

                if (((Cesion)this._ventana.Cesion).AgenteCedente != null)
                {
                    this._agentesCedente.Add((Agente)this._ventana.ApoderadoCedente);
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                    this._ventana.ApoderadoCedenteFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosCedenteFiltrados, (Agente)this._ventana.ApoderadoCedente);
                }
                else
                {
                    this._ventana.ApoderadoCedente = primerAgente;                    
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                    this._ventana.ApoderadoCedenteFiltrado = primerAgente;
                }
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._agentesCesionario = new List<Agente>();
                this._agentesCesionario.Add(primerAgente);

                if (((Cesion)this._ventana.Cesion).AgenteCesionario != null)
                {
                    this._agentesCesionario.Add((Agente)this._ventana.ApoderadoCesionario);
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                    this._ventana.ApoderadoCesionarioFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosCesionarioFiltrados, (Agente)this._ventana.ApoderadoCesionario);
                }
                else
                {
                    this._ventana.ApoderadoCesionario = primerAgente;                    
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                    this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
                }       
            }
        }

        private void CargarPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);

            if (tipo.Equals("Cedente"))
            {
                this._poderesCedente = new List<Poder>();                
                this._poderesCedente.Add(primerPoder);

                if (((Cesion)this._ventana.Cesion).PoderCedente != null)
                {
                    this._poderesCedente.Add((Poder)this._ventana.PoderCedente);
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                    this._ventana.PoderCedenteFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesCedenteFiltrados, (Poder)this._ventana.PoderCedente);                   
                }
                else
                {
                    this._ventana.PoderCedente = primerPoder;                    
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                    this._ventana.PoderCedenteFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Cedente");
                }
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._poderesCesionario = new List<Poder>();
                this._poderesCesionario.Add(primerPoder);

                if (((Cesion)this._ventana.Cesion).PoderCesionario != null)
                {
                    this._poderesCesionario.Add((Poder)this._ventana.PoderCesionario);
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                    this._ventana.PoderCesionarioFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesCesionarioFiltrados, (Poder)this._ventana.PoderCesionario);
                }
                else
                {
                    this._ventana.PoderCesionario = primerPoder;                    
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                    this._ventana.PoderCesionarioFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");
                }     
            }
        }
      
        public Cesion CargarCesionDeLaPantalla()
        {

            Cesion cesion = (Cesion)this._ventana.Cesion;

            return cesion;
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
                    Cesion cesion = CargarCesionDeLaPantalla();

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

        #region Marca

        public void IrConsultarMarcas()
        {
            this.Navegar(new ConsultarMarcas());
        }

        private void CargarMarca()
        {
            this._marcas = new List<Marca>();
            Marca primeraMarca = new Marca(int.MinValue);
            this._marcas.Add(primeraMarca);

            if ((Marca)this._ventana.Marca != null)
                this._marcas.Add((Marca)this._ventana.Marca);

            this._ventana.MarcasFiltradas = this._marcas;
            this._ventana.MarcaFiltrada = (Marca)this._ventana.Marca;
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

        #endregion

        #region Cedente

        private void ValidarCedente()
        {
            if (((Interesado)this._ventana.CedenteFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);

                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                    }
                }
                else
                {
                    if (((Cesion)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                    
                    else
                    {                     
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);
                       
                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)                                           
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);
                    
                    else
                    {                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);

                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                    {
                        ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente");                        

                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);
                        ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente");   

                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);
                    }
                }
            }
        }

        public void ConsultarCedentes()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado interesado = new Interesado();
                IList<Interesado> interesadosFiltrados;
                interesado.Nombre = this._ventana.NombreCedenteFiltrar.ToUpper();
                interesado.Id = this._ventana.IdCedenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdCedenteFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, new Interesado(int.MinValue));                    
                    this._ventana.CedentesFiltrados = interesadosFiltrados;
                }
                else
                {
                    interesadosFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.CedentesFiltrados = this._interesadosCedente;
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

        public void ConsultarApoderadosCedente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoInteresado = new Agente();
                IList<Agente> agentesInteresadoFiltrados;
                apoderadoInteresado.Nombre = this._ventana.NombreApoderadoCedenteFiltrar.ToUpper();
                apoderadoInteresado.Id = this._ventana.IdApoderadoCedenteFiltrar.ToUpper();

                if ((!apoderadoInteresado.Nombre.Equals("")) || (!apoderadoInteresado.Id.Equals("")))
                    agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoInteresado);
                else
                    agentesInteresadoFiltrados = new List<Agente>();

                if (agentesInteresadoFiltrados.ToList<Agente>().Count != 0)
                {
                    agentesInteresadoFiltrados.Insert(0, new Agente(""));                   
                    this._ventana.ApoderadosCedenteFiltrados = agentesInteresadoFiltrados;
                }
                else
                {
                    agentesInteresadoFiltrados.Insert(0, new Agente(""));
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
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

        public void ConsultarPoderesCedente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderCedente = new Poder();
                IList<Poder> poderesCedenteFiltrados;                
                
                if (!this._ventana.IdPoderCedenteFiltrar.Equals(""))
                    poderCedente.Id = int.Parse(this._ventana.IdPoderCedenteFiltrar);

                if (!this._ventana.FechaPoderCedenteFiltrar.Equals(""))
                    poderCedente.Fecha = DateTime.Parse(this._ventana.FechaPoderCedenteFiltrar);

                if ((!poderCedente.Fecha.Equals("")) || (poderCedente.Id != 0))
                    poderesCedenteFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderCedente);
                else
                    poderesCedenteFiltrados = new List<Poder>();

                if (poderesCedenteFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesCedenteFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;                    
                    this._ventana.PoderesCedenteFiltrados = poderesCedenteFiltrados;                    
                }
                else
                {
                    poderesCedenteFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
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

        public bool CambiarCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CedenteFiltrado);
                            this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CedenteFiltrado));
                            
                            LimpiarListaPoder("Cedente");

                            if ((this.ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente")))
                            {
                                this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                                this._ventana.NombreCedente = ((Interesado)this._ventana.CedenteFiltrado).Nombre;                                
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCedente, _poderesApoderadosCedente, "Cedente"))
                            {
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Cedente"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                            this._validar = true;

                        this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CedenteFiltrado);
                        this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;
                        retorno = true;                                                
                    }

                }
                else
                {
                    this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                    this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");

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

        public bool CambiarApoderadoCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                            this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCedenteFiltrado));

                            LimpiarListaPoder("Cedente");

                            if ((this.ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente")))
                            {
                                this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                                this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente"))
                            {
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Cedente"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                            this._validar = true;

                        this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                        this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                    this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
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

        public bool CambiarPoderCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                {
                    if ((((Interesado)this._ventana.CedenteFiltrado).Id == int.MinValue) || (((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("")))
                    {
                        LimpiarListaInteresado("Cedente");

                        LimpiarListaAgente("Cedente");

                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedenteFiltrado, "Cedente", false);

                        this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                        this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                        retorno = true;
                    }
                    else
                    {
                        this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                        this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {                   
                    this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                    this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");               

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
       
        #endregion

        #region Cesionario

        private void ValidarCesionario()
        {
            if (((Interesado)this._ventana.CesionarioFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);                        
                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                    }
                }
                else
                {
                    if (((Cesion)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)               
                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                  
                    else
                    {
                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);
                      
                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)                                           
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);
                    
                    else
                    {                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);
                        
                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                    {
                     
                        ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario");

                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);
                    }
                    else
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);
                        ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario");
                       
                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);
                    }
                }
            }
        }          

        public void ConsultarCesionarios()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado cesionario = new Interesado();
                IList<Interesado> cesionariosFiltrados;
                cesionario.Nombre = this._ventana.NombreCesionarioFiltrar.ToUpper();
                cesionario.Id = this._ventana.IdCesionarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdCesionarioFiltrar);

                if ((!cesionario.Nombre.Equals("")) || (cesionario.Id != 0))
                    cesionariosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(cesionario);
                else
                    cesionariosFiltrados = new List<Interesado>();

                if (cesionariosFiltrados.ToList<Interesado>().Count != 0)
                {
                    cesionariosFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.CesionariosFiltrados = cesionariosFiltrados.ToList<Interesado>();
                }
                else
                {
                    cesionariosFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.CesionariosFiltrados = this._interesadosCesionario;
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

        public void ConsultarApoderadosCesionario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoCesionario = new Agente();
                IList<Agente> agentesCesionarioFiltrados;
                apoderadoCesionario.Nombre = this._ventana.NombreApoderadoCesionarioFiltrar.ToUpper();
                apoderadoCesionario.Id = this._ventana.IdApoderadoCesionarioFiltrar.ToUpper();

                if ((!apoderadoCesionario.Nombre.Equals("")) || (!apoderadoCesionario.Id.Equals("")))
                    agentesCesionarioFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoCesionario);
                else
                    agentesCesionarioFiltrados = new List<Agente>();

                if (agentesCesionarioFiltrados.Count != 0)
                {
                    agentesCesionarioFiltrados.Insert(0, new Agente(""));
                    this._ventana.ApoderadosCesionarioFiltrados = agentesCesionarioFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesCesionarioFiltrados.Insert(0, new Agente(""));
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
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

        public void ConsultarPoderesCesionario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderCesionario = new Poder();
                IList<Poder> poderesCesionarioFiltrados;

                if (!this._ventana.IdPoderCesionarioFiltrar.Equals(""))
                    poderCesionario.Id = int.Parse(this._ventana.IdPoderCesionarioFiltrar);

                if (!this._ventana.FechaPoderCesionarioFiltrar.Equals(""))
                    poderCesionario.Fecha = DateTime.Parse(this._ventana.FechaPoderCesionarioFiltrar);

                if ((!poderCesionario.Fecha.Equals("")) || (poderCesionario.Id != 0))
                    poderesCesionarioFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderCesionario);
                else
                    poderesCesionarioFiltrados = new List<Poder>();

                if (poderesCesionarioFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesCesionarioFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesCesionarioFiltrados = poderesCesionarioFiltrados.ToList<Poder>();
                }
                else
                {
                    poderesCesionarioFiltrados.Insert(0, new Poder(int.MinValue));
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
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

        public bool CambiarCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CesionarioFiltrado);
                            this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CesionarioFiltrado));

                            LimpiarListaPoder("Cesionario");

                            if ((this.ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario")))
                            {
                                this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
                                this._ventana.NombreCesionario = ((Interesado)this._ventana.CesionarioFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCesionario, _poderesApoderadosCesionario, "Cesionario"))
                            {
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Cesionario"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                            this._validar = true;

                        this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CesionarioFiltrado);
                        this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;
                        retorno = true;
                    }

                }
                else
                {
                    this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
                    this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

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

        public bool CambiarApoderadoCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                            this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosCesionario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCesionarioFiltrado));

                            LimpiarListaPoder("Cesionario");

                            if ((this.ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario")))
                            {
                                this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                                this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                                
                                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario"))
                            {
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Cesionario"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                            this._validar = true;

                        this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                        this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                    this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
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

        public bool CambiarPoderCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                {
                    if ((((Interesado)this._ventana.CesionarioFiltrado).Id == int.MinValue) || (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals("")))
                    {
                        LimpiarListaInteresado("Cesionario");

                        LimpiarListaAgente("Cesionario");

                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionarioFiltrado, "Cesionario", false);

                        this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                        this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                        retorno = true;
                    }
                    else
                    {
                        this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                        this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                    this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

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

        #endregion

        public void LlenarListasPoderes(Cesion cesion)
        {

            if (cesion.Cedente != null)
                this._poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(cesion.Cedente);

            if (cesion.Cesionario != null)
                this._poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(cesion.Cesionario);

            if (cesion.AgenteCedente != null)
                this._poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteCedente);

            if (cesion.AgenteCesionario != null)
                this._poderesApoderadosCesionario = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteCesionario);
        }

        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB, string tipo)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> listaIntereseccionCedente = new List<Poder>();
            IList<Poder> listaIntereseccionCesionario = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);

            Poder poderActual = new Poder();            
            
            listaIntereseccionCedente.Add(primerPoder);
            listaIntereseccionCesionario.Add(primerPoder);

            if ((listaPoderesA.Count != 0) && (listaPoderesB.Count != 0))
            {                
                foreach (Poder poderA in listaPoderesA)
                {
                    foreach (Poder poderB in listaPoderesB)
                    {
                        if ((poderA.Id == poderB.Id) && (tipo.Equals("Cedente")))
                        {
                            listaIntereseccionCedente.Add(poderA);
                            retorno = true;
                        }

                        else if ((poderA.Id == poderB.Id) && (tipo.Equals("Cesionario")))
                        {
                            listaIntereseccionCesionario.Add(poderA);
                            retorno = true;
                        }
                    }

                }

                if ((listaIntereseccionCedente.Count != 0) && (tipo.Equals("Cedente")))
                {
                    poderActual = (Poder)this._ventana.PoderCedenteFiltrado;
                    this._poderesInterseccionCedente = listaIntereseccionCedente;
                    this._ventana.PoderesCedenteFiltrados = listaIntereseccionCedente;
                    this._ventana.PoderCedenteFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesCedenteFiltrados,poderActual);
                }


                else if ((listaIntereseccionCesionario.Count != 0) && (tipo.Equals("Cesionario")))
                {
                    poderActual = (Poder)this._ventana.PoderCesionarioFiltrado;
                    this._poderesInterseccionCesionario = listaIntereseccionCesionario;
                    this._ventana.PoderesCesionarioFiltrados = listaIntereseccionCesionario;
                    this._ventana.PoderCesionarioFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesCesionarioFiltrados, poderActual);
                }

                else
                    retorno = false;
            }
            
            this._validar = !retorno;

            Mouse.OverrideCursor = null;
            
            return retorno;
        }
        
        public void LlenarListaAgenteEInteresado(Poder poder, string tipo, bool cargaInicial)
        {            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                Interesado interesado = new Interesado();                 
                IList<Agente> agentesInteresadoFiltrados;                
                IList<Interesado> interesadosFiltrados = new List<Interesado>();                
                Poder poderFiltrar = new Poder();

                Interesado primerInteresado = new Interesado(int.MinValue);
                Agente primerAgente = new Agente("");
                
                Agente agenteActual = new Agente();                

                agentesInteresadoFiltrados = new List<Agente>();                                               

                if (tipo.Equals("Cedente"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderCedenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderCedenteFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderCedenteFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderCedenteFiltrado);
                    }                    

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.CedentesFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.CedenteFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.CedenteFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);   
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.CedenteFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoCedenteFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);                        
                        this._ventana.ApoderadosCedenteFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoCedenteFiltrado = BuscarAgente(agentesInteresadoFiltrados,agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoCedenteFiltrado = primerAgente;
                    }
                }
                else if (tipo.Equals("Cesionario"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderCesionarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderCesionarioFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderCesionarioFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderCesionarioFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.CesionariosFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.CesionarioFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.CesionarioFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.CesionarioFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoCesionarioFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosCesionarioFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoCesionarioFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
                    }
                }

                this._validar = false;

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
      
        public bool VerificarCambioInteresado(string tipo)
        {
            bool retorno = false;

            if (tipo.Equals("Cedente"))
            {
                if ((((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("")))
                    retorno = true;
            }
            if (tipo.Equals("Cesionario"))
            {
                if ((((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals("")))
                    retorno = true;
            }

            return retorno;
        }

        public bool VerificarCambioAgente(string tipo)
        {
            bool retorno = false;

            if (tipo.Equals("Cedente"))
            {
                if (!(((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("")) || (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue))
                    retorno = true;
            }
            if (tipo.Equals("Cesionario"))
            {
                if (!(((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("")) || (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue))
                    retorno = true;
            }

            return retorno;
        }

        public bool VerificarCambioPoder(string tipo)
        {
            if (tipo.Equals("Cedente"))
            {
                if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                    return true;
            }
            if (tipo.Equals("Cesionario"))
            {
                if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                    return true;
            }

            return false;
        }
      
        public void LimpiarListaInteresado(string tipo)
        {            
            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            if (tipo.Equals("Cedente"))
            {
                this._ventana.CedentesFiltrados = listaInteresados;
                this._ventana.CedenteFiltrado = BuscarInteresado(listaInteresados,primerInteresado);
                this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._ventana.CesionariosFiltrados = listaInteresados;
                this._ventana.CesionarioFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
            }
        }

        public void LimpiarListaAgente(string tipo)
        {
            Agente primerAgente = new Agente("");
            IList<Agente> listaAgentes = new List<Agente>();
            listaAgentes.Add(primerAgente);

            if (tipo.Equals("Cedente"))
            {
                this._ventana.ApoderadosCedenteFiltrados = listaAgentes;
                this._ventana.ApoderadoCedenteFiltrado = BuscarAgente(listaAgentes,primerAgente);
                this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._ventana.ApoderadosCesionarioFiltrados = listaAgentes;
                this._ventana.ApoderadoCesionarioFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
            }
        }

        public void LimpiarListaPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);
            IList<Poder> listaPoderes = new List<Poder>();
            listaPoderes.Add(primerPoder);

            if (tipo.Equals("Cedente"))
            {
                this._ventana.PoderesCedenteFiltrados = listaPoderes;
                this._ventana.PoderCedenteFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._ventana.PoderesCesionarioFiltrados = listaPoderes;
                this._ventana.PoderCesionarioFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
            }
        }                           
    }
}