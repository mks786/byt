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
using Trascend.Bolet.Cliente.Contratos.Traspasos.CambiosDePeticionario;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Traspasos.CambiosDePeticionario
{
    class PresentadorGestionarCambioDePeticionario : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _validar = true;
        private IGestionarCambioDePeticionario _ventana;

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

        private IList<Interesado> _interesadosAnterior;
        private IList<Interesado> _interesadosActual;
        private IList<Agente> _agentesActual;
        private IList<Agente> _agentesAnterior;
        private IList<Marca> _marcas;

        private IList<Poder> _poderesAnterior;
        private IList<Poder> _poderesActual;

        private IList<Poder> _poderesApoderadosAnterior;
        private IList<Poder> _poderesApoderadosActual;

        private IList<Poder> _poderesInterseccionAnterior;
        private IList<Poder> _poderesInterseccionActual;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCambioDePeticionario(IGestionarCambioDePeticionario ventana, object cambioPeticionario)
        {
            try
            {

                this._ventana = ventana;

                this._ventana.CambioPeticionario = cambioPeticionario;

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCambioPeticionario,
                Recursos.Ids.GestionarCambioPeticionario);
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

                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");
                this._ventana.ConvertirEnteroMinimoABlanco("Actual");

                CambioPeticionario cesion = (CambioPeticionario)this._ventana.CambioPeticionario;
                
                this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(cesion.Marca);                                
                
                this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;                
                
                this._ventana.ApoderadoAnterior = cesion.AgenteAnterior;
                this._ventana.ApoderadoActual = cesion.AgenteActual;
                this._ventana.PoderAnterior = cesion.PoderAnterior;
                this._ventana.PoderActual = cesion.PoderActual;

                
                CargarMarca();
                                                             
                CargarInteresado("Anterior");             
                
                CargarApoderado("Anterior");
                            
                CargarPoder("Anterior");            
               
                CargarInteresado("Actual");                                        
                
                CargarApoderado("Actual");                         
                               
                CargarPoder("Actual");                          

                LlenarListasPoderes((CambioPeticionario)this._ventana.CambioPeticionario);
                
                ValidarAnterior();

                ValidarActual();

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

            if (tipo.Equals("Anterior"))
            {
                this._interesadosAnterior = new List<Interesado>();

                this._interesadosAnterior.Add(primerInteresado);

                if (((CambioPeticionario)this._ventana.CambioPeticionario).InteresadoAnterior != null)
                {
                    this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioPeticionario)this._ventana.CambioPeticionario).InteresadoAnterior);
                    this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;                    

                    if ((Interesado)this._ventana.InteresadoAnterior != null)
                    {
                        this._interesadosAnterior.Add((Interesado)this._ventana.InteresadoAnterior);
                        this._ventana.AnteriorsFiltrados = this._interesadosAnterior;
                        this._ventana.AnteriorFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.AnteriorsFiltrados, (Interesado)this._ventana.InteresadoAnterior);                        
                    }
                }
                else
                {
                    this._ventana.InteresadoAnterior = primerInteresado;                    
                    this._ventana.AnteriorsFiltrados = this._interesadosAnterior;
                    this._ventana.AnteriorFiltrado = primerInteresado;

                }
            }
            else if (tipo.Equals("Actual"))
            {
                this._interesadosActual = new List<Interesado>();
                this._interesadosActual.Add(primerInteresado);

                if (((CambioPeticionario)this._ventana.CambioPeticionario).InteresadoActual != null)
                {
                    this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioPeticionario)this._ventana.CambioPeticionario).InteresadoActual);
                    this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;

                    if ((Interesado)this._ventana.InteresadoActual != null)
                    {
                        this._interesadosActual.Add((Interesado)this._ventana.InteresadoActual);                        
                        this._ventana.ActualsFiltrados = this._interesadosActual;
                        this._ventana.ActualFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.ActualsFiltrados, (Interesado)this._ventana.InteresadoActual);
                    }
                }
                else
                {
                    this._ventana.InteresadoActual = primerInteresado;                    
                    this._ventana.ActualsFiltrados = this._interesadosActual;
                    this._ventana.ActualFiltrado = primerInteresado;
                }   
            }
        }

        private void CargarApoderado(string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Anterior"))
            {
                this._agentesAnterior = new List<Agente>();                
                this._agentesAnterior.Add(primerAgente);

                if (((CambioPeticionario)this._ventana.CambioPeticionario).AgenteAnterior != null)
                {
                    this._agentesAnterior.Add((Agente)this._ventana.ApoderadoAnterior);
                    this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                    this._ventana.ApoderadoAnteriorFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosAnteriorFiltrados, (Agente)this._ventana.ApoderadoAnterior);
                }
                else
                {
                    this._ventana.ApoderadoAnterior = primerAgente;                    
                    this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                    this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
                }
            }
            else if (tipo.Equals("Actual"))
            {
                this._agentesActual = new List<Agente>();
                this._agentesActual.Add(primerAgente);

                if (((CambioPeticionario)this._ventana.CambioPeticionario).AgenteActual != null)
                {
                    this._agentesActual.Add((Agente)this._ventana.ApoderadoActual);
                    this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                    this._ventana.ApoderadoActualFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosActualFiltrados, (Agente)this._ventana.ApoderadoActual);
                }
                else
                {
                    this._ventana.ApoderadoActual = primerAgente;                    
                    this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                    this._ventana.ApoderadoActualFiltrado = primerAgente;
                }       
            }
        }

        private void CargarPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);

            if (tipo.Equals("Anterior"))
            {
                this._poderesAnterior = new List<Poder>();                
                this._poderesAnterior.Add(primerPoder);

                if (((CambioPeticionario)this._ventana.CambioPeticionario).PoderAnterior != null)
                {
                    this._poderesAnterior.Add((Poder)this._ventana.PoderAnterior);
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesAnteriorFiltrados, (Poder)this._ventana.PoderAnterior);
                }
                else
                {
                    this._ventana.PoderAnterior = primerPoder;                    
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Anterior");
                }
            }
            else if (tipo.Equals("Actual"))
            {
                this._poderesActual = new List<Poder>();
                this._poderesActual.Add(primerPoder);

                if (((CambioPeticionario)this._ventana.CambioPeticionario).PoderActual != null)
                {
                    this._poderesActual.Add((Poder)this._ventana.PoderActual);
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesActualFiltrados, (Poder)this._ventana.PoderActual);
                }
                else
                {
                    this._ventana.PoderActual = primerPoder;                    
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Actual");
                }     
            }
        }
      
        public CambioPeticionario CargarCambioPeticionarioDeLaPantalla()
        {

            CambioPeticionario cesion = (CambioPeticionario)this._ventana.CambioPeticionario;

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
                    CambioPeticionario cesion = CargarCambioPeticionarioDeLaPantalla();

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

                Marca primeraMarca = new Marca(int.MinValue);

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
                    marcasFiltradas.Insert(0, primeraMarca);
                    this._ventana.MarcasFiltradas = marcasFiltradas.ToList<Marca>();
                    this._ventana.MarcaFiltrada = primeraMarca;
                }
                else
                {
                    marcasFiltradas.Insert(0, primeraMarca);
                    this._ventana.MarcasFiltradas = this._marcas;
                    this._ventana.MarcaFiltrada = primeraMarca;
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

        #region Anterior

        private void ValidarAnterior()
        {
            if (((Interesado)this._ventana.AnteriorFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);

                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                    }
                }
                else
                {
                    if (((CambioPeticionario)this._ventana.PoderAnteriorFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                    
                    else
                    {                     
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);
                       
                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderAnteriorFiltrado).Id == int.MinValue)                                           
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    
                    else
                    {                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);

                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderAnteriorFiltrado).Id == int.MinValue)
                    {
                        ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior");                        

                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);
                        ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior");   

                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    }
                }
            }
        }

        public void ConsultarAnteriors()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado interesado = new Interesado();
                IList<Interesado> interesadosFiltrados;
                interesado.Nombre = this._ventana.NombreAnteriorFiltrar.ToUpper();
                interesado.Id = this._ventana.IdAnteriorFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdAnteriorFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.AnteriorsFiltrados = interesadosFiltrados;
                    this._ventana.AnteriorFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.AnteriorsFiltrados = this._interesadosAnterior;
                    this._ventana.AnteriorFiltrado = primerInteresado;
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

        public void ConsultarApoderadosAnterior()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoAnterior = new Agente();
                IList<Agente> agentesAnteriorFiltrados;
                apoderadoAnterior.Nombre = this._ventana.NombreApoderadoAnteriorFiltrar.ToUpper();
                apoderadoAnterior.Id = this._ventana.IdApoderadoAnteriorFiltrar.ToUpper();

                if ((!apoderadoAnterior.Nombre.Equals("")) || (!apoderadoAnterior.Id.Equals("")))
                    agentesAnteriorFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoAnterior);
                else
                    agentesAnteriorFiltrados = new List<Agente>();

                if (agentesAnteriorFiltrados.Count != 0)
                {
                    agentesAnteriorFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
                    this._ventana.ApoderadosAnteriorFiltrados = agentesAnteriorFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesAnteriorFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                    this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
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

        public void ConsultarPoderesAnterior()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderAnterior = new Poder();
                IList<Poder> poderesAnteriorFiltrados;

                if (!this._ventana.IdPoderAnteriorFiltrar.Equals(""))
                    poderAnterior.Id = int.Parse(this._ventana.IdPoderAnteriorFiltrar);

                if (!this._ventana.FechaPoderAnteriorFiltrar.Equals(""))
                    poderAnterior.Fecha = DateTime.Parse(this._ventana.FechaPoderAnteriorFiltrar);

                if ((!poderAnterior.Fecha.Equals("")) || (poderAnterior.Id != 0))
                    poderesAnteriorFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderAnterior);
                else
                    poderesAnteriorFiltrados = new List<Poder>();

                if (poderesAnteriorFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesAnteriorFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = primerPoder;
                    this._ventana.PoderesAnteriorFiltrados = poderesAnteriorFiltrados;
                }
                else
                {
                    poderesAnteriorFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = primerPoder;
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

        public bool CambiarAnterior()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.AnteriorFiltrado);
                            this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.AnteriorFiltrado));
                            
                            LimpiarListaPoder("Anterior");

                            if ((this.ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior")))
                            {
                                this._ventana.InteresadoAnterior = this._ventana.AnteriorFiltrado;
                                this._ventana.NombreAnterior = ((Interesado)this._ventana.AnteriorFiltrado).Nombre;                                
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesAnterior, _poderesApoderadosAnterior, "Anterior"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Anterior"), 0);
                            }
                        }
                    }
                    else
                    {
                        this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.AnteriorFiltrado));
                        this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.AnteriorFiltrado);
                        this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                        retorno = true;                                                
                    }

                }
                else
                {
                    this._ventana.InteresadoAnterior = this._ventana.AnteriorFiltrado;
                    this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");

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

        public bool CambiarApoderadoAnterior()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                            this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosAnterior = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoAnteriorFiltrado));

                            LimpiarListaPoder("Anterior");

                            if ((this.ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior")))
                            {
                                this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                                this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Anterior"), 0);
                            }
                        }
                    }
                    else
                    {
                        this._poderesApoderadosAnterior = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoAnteriorFiltrado));
                        this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                        this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                    this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");

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

        public bool CambiarPoderAnterior()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                {
                    if ((((Interesado)this._ventana.AnteriorFiltrado).Id == int.MinValue) || (((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals("")))
                    {
                        LimpiarListaInteresado("Anterior");

                        LimpiarListaAgente("Anterior");

                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnteriorFiltrado, "Anterior", false);

                        this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
                        this._ventana.IdPoderAnterior = ((Poder)this._ventana.PoderAnteriorFiltrado).Id.ToString();
                        retorno = true;
                    }
                    else
                    {
                        this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
                        this._ventana.IdPoderAnterior = ((Poder)this._ventana.PoderAnteriorFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {                   
                    this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
                    this._ventana.IdPoderAnterior = ((Poder)this._ventana.PoderAnteriorFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");               

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

        #region Actual

        private void ValidarActual()
        {
            if (((Interesado)this._ventana.ActualFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);                        
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                    }
                }
                else
                {
                    if (((CambioPeticionario)this._ventana.PoderActualFiltrado).Id == int.MinValue)               
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                  
                    else
                    {
                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);
                      
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderActualFiltrado).Id == int.MinValue)                                           
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    
                    else
                    {                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);
                        
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderActualFiltrado).Id == int.MinValue)
                    {
                     
                        ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual");

                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    }
                    else
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);
                        ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual");
                       
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    }
                }
            }
        }

        public void ConsultarActuals()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado interesadoActual = new Interesado();
                IList<Interesado> interesadoActualsFiltrados;
                interesadoActual.Nombre = this._ventana.NombreActualFiltrar.ToUpper();
                interesadoActual.Id = this._ventana.IdActualFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdActualFiltrar);

                if ((!interesadoActual.Nombre.Equals("")) || (interesadoActual.Id != 0))
                    interesadoActualsFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoActual);
                else
                    interesadoActualsFiltrados = new List<Interesado>();

                if (interesadoActualsFiltrados.ToList<Interesado>().Count != 0)
                {
                    interesadoActualsFiltrados.Insert(0, primerInteresado);
                    this._ventana.ActualsFiltrados = interesadoActualsFiltrados.ToList<Interesado>();
                    this._ventana.ActualFiltrado = primerInteresado;
                }
                else
                {
                    interesadoActualsFiltrados.Insert(0, primerInteresado);
                    this._ventana.ActualsFiltrados = this._interesadosActual;
                    this._ventana.ActualFiltrado = primerInteresado;
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

        public void ConsultarApoderadosActual()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoActual = new Agente();
                IList<Agente> agentesActualFiltrados;
                apoderadoActual.Nombre = this._ventana.NombreApoderadoActualFiltrar.ToUpper();
                apoderadoActual.Id = this._ventana.IdApoderadoActualFiltrar.ToUpper();

                if ((!apoderadoActual.Nombre.Equals("")) || (!apoderadoActual.Id.Equals("")))
                    agentesActualFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoActual);
                else
                    agentesActualFiltrados = new List<Agente>();

                if (agentesActualFiltrados.Count != 0)
                {
                    agentesActualFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoActualFiltrado = primerAgente;
                    this._ventana.ApoderadosActualFiltrados = agentesActualFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesActualFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                    this._ventana.ApoderadoActualFiltrado = primerAgente;
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

        public void ConsultarPoderesActual()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderActual = new Poder();
                IList<Poder> poderesActualFiltrados;

                if (!this._ventana.IdPoderActualFiltrar.Equals(""))
                    poderActual.Id = int.Parse(this._ventana.IdPoderActualFiltrar);

                if (!this._ventana.FechaPoderActualFiltrar.Equals(""))
                    poderActual.Fecha = DateTime.Parse(this._ventana.FechaPoderActualFiltrar);

                if ((!poderActual.Fecha.Equals("")) || (poderActual.Id != 0))
                    poderesActualFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderActual);
                else
                    poderesActualFiltrados = new List<Poder>();

                if (poderesActualFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesActualFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = primerPoder;
                    this._ventana.PoderesActualFiltrados = poderesActualFiltrados;
                }
                else
                {
                    poderesActualFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = primerPoder;
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

        public bool CambiarActual()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.ActualFiltrado);
                            this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.ActualFiltrado));

                            LimpiarListaPoder("Actual");

                            if ((this.ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual")))
                            {
                                this._ventana.InteresadoActual = this._ventana.ActualFiltrado;
                                this._ventana.NombreActual = ((Interesado)this._ventana.ActualFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesActual, _poderesApoderadosActual, "Actual"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Actual");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Actual"), 0);
                            }
                        }
                    }
                    else
                    {
                        this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.ActualFiltrado));
                        this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.ActualFiltrado);
                        this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                        retorno = true;
                    }

                }
                else
                {
                    this._ventana.InteresadoActual = this._ventana.ActualFiltrado;
                    this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Actual");

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

        public bool CambiarApoderadoActual()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                            this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosActual = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoActualFiltrado));                            

                            LimpiarListaPoder("Actual");

                            if ((this.ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual")))
                            {
                                this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                                this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                                
                                this._ventana.ConvertirEnteroMinimoABlanco("Actual");

                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Actual");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Actual"), 0);
                            }
                        }
                    }
                    else
                    {
                        this._poderesApoderadosActual = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoActualFiltrado));                            
                        this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                        this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                    this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Actual");

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

        public bool CambiarPoderActual()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                {
                    if ((((Interesado)this._ventana.ActualFiltrado).Id == int.MinValue) || (((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals("")))
                    {
                        LimpiarListaInteresado("Actual");

                        LimpiarListaAgente("Actual");

                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActualFiltrado, "Actual", false);

                        this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
                        this._ventana.IdPoderActual = ((Poder)this._ventana.PoderActualFiltrado).Id.ToString();
                        retorno = true;
                    }
                    else
                    {
                        this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
                        this._ventana.IdPoderActual = ((Poder)this._ventana.PoderActualFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
                    this._ventana.IdPoderActual = ((Poder)this._ventana.PoderActualFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Actual");

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

        public void LlenarListasPoderes(CambioPeticionario cesion)
        {

            if (cesion.InteresadoAnterior != null)
                this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(cesion.InteresadoAnterior);

            if (cesion.InteresadoActual != null)
                this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(cesion.InteresadoActual);

            if (cesion.AgenteAnterior != null)
                this._poderesApoderadosAnterior = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteAnterior);

            if (cesion.AgenteActual != null)
                this._poderesApoderadosActual = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteActual);
        }

        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB, string tipo)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> listaIntereseccionAnterior = new List<Poder>();
            IList<Poder> listaIntereseccionActual = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);

            Poder poderActual = new Poder();            
            
            listaIntereseccionAnterior.Add(primerPoder);
            listaIntereseccionActual.Add(primerPoder);

            if ((listaPoderesA.Count != 0) && (listaPoderesB.Count != 0))
            {                
                foreach (Poder poderA in listaPoderesA)
                {
                    foreach (Poder poderB in listaPoderesB)
                    {
                        if ((poderA.Id == poderB.Id) && (tipo.Equals("Anterior")))
                        {
                            listaIntereseccionAnterior.Add(poderA);
                            retorno = true;
                        }

                        else if ((poderA.Id == poderB.Id) && (tipo.Equals("Actual")))
                        {
                            listaIntereseccionActual.Add(poderA);
                            retorno = true;
                        }
                    }

                }

                if ((listaIntereseccionAnterior.Count != 0) && (tipo.Equals("Anterior")))
                {
                    poderActual = (Poder)this._ventana.PoderAnteriorFiltrado;
                    this._poderesInterseccionAnterior = listaIntereseccionAnterior;
                    this._ventana.PoderesAnteriorFiltrados = listaIntereseccionAnterior;
                    this._ventana.PoderAnteriorFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesAnteriorFiltrados,poderActual);
                }


                else if ((listaIntereseccionActual.Count != 0) && (tipo.Equals("Actual")))
                {
                    poderActual = (Poder)this._ventana.PoderActualFiltrado;
                    this._poderesInterseccionActual = listaIntereseccionActual;
                    this._ventana.PoderesActualFiltrados = listaIntereseccionActual;
                    this._ventana.PoderActualFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesActualFiltrados, poderActual);
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

                if (tipo.Equals("Anterior"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderAnteriorFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderAnteriorFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderAnteriorFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderAnteriorFiltrado);
                    }                    

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.AnteriorsFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.AnteriorFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.AnteriorFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);   
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.AnteriorFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoAnteriorFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);                        
                        this._ventana.ApoderadosAnteriorFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoAnteriorFiltrado = BuscarAgente(agentesInteresadoFiltrados,agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
                    }
                }
                else if (tipo.Equals("Actual"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderActualFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderActualFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderActualFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderActualFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.ActualsFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.ActualFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.ActualFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ActualFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoActualFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosActualFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoActualFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoActualFiltrado = primerAgente;
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

            if (tipo.Equals("Anterior"))
            {
                if ((((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals("")))
                    retorno = true;
            }
            if (tipo.Equals("Actual"))
            {
                if ((((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals("")))
                    retorno = true;
            }

            return retorno;
        }

        public bool VerificarCambioAgente(string tipo)
        {
            bool retorno = false;

            if (tipo.Equals("Anterior"))
            {
                if (!(((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals("")) || (((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue))
                    retorno = true;
            }
            if (tipo.Equals("Actual"))
            {
                if (!(((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals("")) || (((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue))
                    retorno = true;
            }

            return retorno;
        }

        public bool VerificarCambioPoder(string tipo)
        {
            if (tipo.Equals("Anterior"))
            {
                if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                    return true;
            }
            if (tipo.Equals("Actual"))
            {
                if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                    return true;
            }

            return false;
        }
      
        public void LimpiarListaInteresado(string tipo)
        {            
            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            if (tipo.Equals("Anterior"))
            {
                this._ventana.AnteriorsFiltrados = listaInteresados;
                this._ventana.AnteriorFiltrado = BuscarInteresado(listaInteresados,primerInteresado);
                this._ventana.InteresadoAnterior = this._ventana.AnteriorFiltrado;
            }
            else if (tipo.Equals("Actual"))
            {
                this._ventana.ActualsFiltrados = listaInteresados;
                this._ventana.ActualFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoActual = this._ventana.ActualFiltrado;
            }
        }

        public void LimpiarListaAgente(string tipo)
        {
            Agente primerAgente = new Agente("");
            IList<Agente> listaAgentes = new List<Agente>();
            listaAgentes.Add(primerAgente);

            if (tipo.Equals("Anterior"))
            {
                this._ventana.ApoderadosAnteriorFiltrados = listaAgentes;
                this._ventana.ApoderadoAnteriorFiltrado = BuscarAgente(listaAgentes,primerAgente);
                this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
            }
            else if (tipo.Equals("Actual"))
            {
                this._ventana.ApoderadosActualFiltrados = listaAgentes;
                this._ventana.ApoderadoActualFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
            }
        }

        public void LimpiarListaPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);
            IList<Poder> listaPoderes = new List<Poder>();
            listaPoderes.Add(primerPoder);

            if (tipo.Equals("Anterior"))
            {
                this._ventana.PoderesAnteriorFiltrados = listaPoderes;
                this._ventana.PoderAnteriorFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
            }
            else if (tipo.Equals("Actual"))
            {
                this._ventana.PoderesActualFiltrados = listaPoderes;
                this._ventana.PoderActualFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
            }
        }

        public void VerificarCasos(string tipo)
        {
            
            if (LlenarPoderes(tipo) == 1)
            {
                //llenamos Poderes y quitamos consultar de poder
                ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior");


            }
            else if (LlenarPoderes(tipo) == 0)
            {
                //llenamos agentes e interesado, quitamos consultar de los mismos
                LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnteriorFiltrado, tipo, false);           
            }            
        }

        private int LlenarPoderes(string tipo)
        {
            int retorno = -1;

            if (tipo.Equals("Anterior"))
            {
                if (((Interesado)this._ventana.InteresadoAnterior).Id != int.MinValue)
                    if (!((Agente)this._ventana.ApoderadoAnterior).Id.Equals(""))
                        if (((Poder)this._ventana.PoderAnterior).Id == int.MinValue)
                        {
                            retorno = 1;
                        }
                if (((Interesado)this._ventana.InteresadoAnterior).Id == int.MinValue)
                    if (((Agente)this._ventana.ApoderadoAnterior).Id.Equals(""))
                        if (((Poder)this._ventana.PoderAnterior).Id != int.MinValue)
                        {
                            retorno = 0;
                        }
                
            }
            else if (tipo.Equals("Anterior"))
            {
                if (((Interesado)this._ventana.InteresadoActual).Id != int.MinValue)
                    if (!((Agente)this._ventana.ApoderadoActual).Id.Equals(""))
                        if (((Poder)this._ventana.PoderActual).Id == int.MinValue)
                        {
                            retorno = 1;
                        }
                if (((Interesado)this._ventana.InteresadoActual).Id == int.MinValue)
                    if (((Agente)this._ventana.ApoderadoActual).Id.Equals(""))
                        if (((Poder)this._ventana.PoderActual).Id != int.MinValue)
                        {
                            retorno = 0;
                        }
            }

            return retorno;
        }
               
    }
}