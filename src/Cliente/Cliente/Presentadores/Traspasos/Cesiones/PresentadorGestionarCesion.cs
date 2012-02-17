﻿using System;
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarca, "");

                Cesion cesion = (Cesion)this._ventana.Cesion;


                this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(cesion.Marca);                                
                
                this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;                
                
                this._ventana.ApoderadoCedente = cesion.AgenteCedente;
                this._ventana.ApoderadoCesionario = cesion.AgenteCesionario;
                this._ventana.PoderCedente = cesion.PoderCedente;
                this._ventana.PoderCesionario = cesion.PoderCesionario;

                this._marcas = new List<Marca>();
                Marca primeraMarca = new Marca(int.MinValue);
                this._marcas.Add(primeraMarca);

                if ((Marca)this._ventana.Marca != null)
                    this._marcas.Add((Marca)this._ventana.Marca);

                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.MarcaFiltrada = (Marca)this._ventana.Marca;

                
                
                this._interesadosCedente = new List<Interesado>();
                Interesado primerInteresado = new Interesado(int.MinValue);
                this._interesadosCedente.Add(primerInteresado);

                if (cesion.Cedente != null)
                {
                    this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo(cesion.Cedente);
                    this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;

                    if ((Interesado)this._ventana.InteresadoCedente != null)
                    {
                        this._interesadosCedente.Add((Interesado)this._ventana.InteresadoCedente);
                        this._ventana.CedenteFiltrado = (Interesado)this._ventana.InteresadoCedente;
                        this._ventana.CedentesFiltrados = this._interesadosCedente;
                    }
                }
                else
                {
                    this._ventana.InteresadoCedente = primerInteresado;
                    this._ventana.CedenteFiltrado = primerInteresado;
                    this._ventana.CedentesFiltrados = this._interesadosCedente;

                }                                

                this._interesadosCesionario = new List<Interesado>();
                this._interesadosCesionario.Add(primerInteresado);

                if (cesion.Cesionario != null)
                {
                    this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo(cesion.Cesionario);
                    this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;

                    if ((Interesado)this._ventana.InteresadoCesionario != null)
                    {
                        this._interesadosCesionario.Add((Interesado)this._ventana.InteresadoCesionario);
                        this._ventana.CesionarioFiltrado = (Interesado)this._ventana.InteresadoCesionario;
                        this._ventana.CesionariosFiltrados = this._interesadosCesionario;
                    }
                }
                else
                {
                    this._ventana.InteresadoCesionario = primerInteresado;
                    this._ventana.CesionarioFiltrado = primerInteresado;
                    this._ventana.CesionariosFiltrados = this._interesadosCesionario;
                }                

                this._agentesCedente = new List<Agente>();
                Agente primerAgente = new Agente("");
                this._agentesCedente.Add(primerAgente);

                if (cesion.AgenteCedente != null)
                {
                    this._agentesCedente.Add((Agente)this._ventana.ApoderadoCedente);
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                    this._ventana.ApoderadoCedenteFiltrado = (Agente)this._ventana.ApoderadoCedente;
                }
                else
                {
                    this._ventana.ApoderadoCedente = primerAgente;
                    this._ventana.ApoderadoCedenteFiltrado = primerAgente;
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                }                

                this._agentesCesionario = new List<Agente>();
                this._agentesCesionario.Add(primerAgente);

                if (cesion.AgenteCesionario != null)
                {
                    this._agentesCesionario.Add((Agente)this._ventana.ApoderadoCesionario);
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                    this._ventana.ApoderadoCesionarioFiltrado = (Agente)this._ventana.ApoderadoCesionario;
                }
                else
                {
                    this._ventana.ApoderadoCesionario = primerAgente;
                    this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                }                                

                this._poderesCedente = new List<Poder>();
                Poder primerPoder = new Poder(int.MinValue);
                this._poderesCedente.Add(primerPoder);

                if (cesion.PoderCedente != null)
                {
                    this._poderesCedente.Add((Poder)this._ventana.PoderCedente);
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                    this._ventana.PoderCedenteFiltrado = (Poder)this._ventana.PoderCedente;
                }
                else
                {
                    this._ventana.PoderCedente = primerPoder;
                    this._ventana.PoderCedenteFiltrado = primerPoder;
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                }                

                this._poderesCesionario = new List<Poder>();
                this._poderesCesionario.Add(primerPoder);

                if (cesion.PoderCesionario != null)
                {
                    this._poderesCesionario.Add((Poder)this._ventana.PoderCesionario);
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                    this._ventana.PoderCesionarioFiltrado = (Poder)this._ventana.PoderCesionario;
                }
                else
                {
                    this._ventana.PoderCesionario = primerPoder;
                    this._ventana.PoderCesionarioFiltrado = primerPoder;
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                }                

                LlenarListasPoderes((Cesion)this._ventana.Cesion);

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

        public void ConsultarCedentes()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado cedente = new Interesado();
                IList<Interesado> cedentesFiltrados;
                cedente.Nombre = this._ventana.NombreCedenteFiltrar.ToUpper();
                cedente.Id = this._ventana.IdCedenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdCedenteFiltrar);

                if ((!cedente.Nombre.Equals("")) || (cedente.Id != 0))
                    cedentesFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(cedente);
                else
                    cedentesFiltrados = new List<Interesado>();

                if (cedentesFiltrados.ToList<Interesado>().Count != 0)
                {
                    cedentesFiltrados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.CedentesFiltrados = cedentesFiltrados.ToList<Interesado>();
                }
                else
                {
                    cedentesFiltrados.Insert(0, new Interesado(int.MinValue));
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
                Agente apoderadoCedente = new Agente();
                IList<Agente> agentesCedenteFiltrados;
                apoderadoCedente.Nombre = this._ventana.NombreApoderadoCedenteFiltrar.ToUpper();
                apoderadoCedente.Id = this._ventana.IdApoderadoCedenteFiltrar.ToUpper();

                if ((!apoderadoCedente.Nombre.Equals("")) || (!apoderadoCedente.Id.Equals("")))
                    agentesCedenteFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoCedente);
                else
                    agentesCedenteFiltrados = new List<Agente>();

                if (agentesCedenteFiltrados.ToList<Agente>().Count != 0)
                {
                    agentesCedenteFiltrados.Insert(0, new Agente(""));                   
                    this._ventana.ApoderadosCedenteFiltrados = agentesCedenteFiltrados;
                }
                else
                {
                    agentesCedenteFiltrados.Insert(0, new Agente(""));
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

                if (agentesCesionarioFiltrados.ToList<Agente>().Count != 0)
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

        public bool CambiarCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.CedenteFiltrado != null)
                {
                    if ((this._ventana.ApoderadoCedenteFiltrado != null) && (((Interesado)this._ventana.CedenteFiltrado).Nombre != null) &&
                        (((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre != null))
                    {
                        _poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CedenteFiltrado));

                        if ((this._ventana.CedenteFiltrado != null) && (this.ValidarListaDePoderes(_poderesCedente, _poderesApoderadosCedente, "Cedente")))
                        {
                            this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                            this._ventana.NombreCedente = ((Interesado)this._ventana.CedenteFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesCedente, _poderesApoderadosCedente, "Cedente"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Cedente"), 0);
                        }
                    }
                    else if (this._ventana.CedenteFiltrado != null)
                    {
                        this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                        this._ventana.NombreCedente = ((Interesado)this._ventana.CedenteFiltrado).Nombre;
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

        public bool CambiarApoderadoCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ApoderadoCesionarioFiltrado != null)
                {
                    if ((this._ventana.CedenteFiltrado != null) && (((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre != null) &&
                        (((Interesado)this._ventana.CedenteFiltrado).Nombre != null))
                    {
                        _poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCedenteFiltrado));

                        if ((this._ventana.ApoderadoCedenteFiltrado != null) && (this.ValidarListaDePoderes(_poderesCedente, _poderesApoderadosCedente, "Cedente")))
                        {
                            this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                            this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesCedente, _poderesApoderadosCedente, "Cedente"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Cedente"), 0);
                        }
                    }
                    else if (this._ventana.ApoderadoCedenteFiltrado != null)
                    {
                        this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                        this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
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

        public bool CambiarPoderCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                
                if (this._ventana.PoderCedenteFiltrado != null)
                {
                    if (this._validar)
                    {
                        if ((((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue))
                            if ((((Interesado)this._ventana.CedenteFiltrado).Id == int.MinValue))
                                if (((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                                {
                                    LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedenteFiltrado);

                                    this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                                    this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                                    retorno = true;

                                }
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)                            
                        {
                            this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                            this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                            this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (!((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                        {
                            this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                            this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
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

        public bool CambiarCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.CesionarioFiltrado != null)
                {
                    if ((this._ventana.ApoderadoCesionarioFiltrado != null) && (((Interesado)this._ventana.CesionarioFiltrado).Nombre != null) &&
                        ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre != null)
                    {
                        _poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CesionarioFiltrado));

                        if ((this._ventana.CesionarioFiltrado != null) && (this.ValidarListaDePoderes(_poderesCesionario, _poderesApoderadosCedente, "Cesionario")))
                        {

                            this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
                            this._ventana.NombreCesionario = ((Interesado)this._ventana.CesionarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesCesionario, _poderesApoderadosCesionario, "Cesionario"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Cesionario"), 0);
                        }
                    }
                    if (this._ventana.CesionarioFiltrado != null)
                    {

                        this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
                        this._ventana.NombreCesionario = ((Interesado)this._ventana.CesionarioFiltrado).Nombre;
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

        public bool CambiarPoderCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.PoderCesionarioFiltrado != null)
                {
                    if (this._validar)
                    {
                        if ((((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue))
                            if ((((Interesado)this._ventana.CesionarioFiltrado).Id == int.MinValue))
                                if (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                                {
                                    LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionarioFiltrado);

                                    this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                                    this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                                    retorno = true;

                                }
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                        {
                            this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                            this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                            this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                            retorno = true;
                        }

                        if (!((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                        {
                            this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                            this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
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

        public bool CambiarApoderadoCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ApoderadoCesionarioFiltrado != null)
                {
                    if ((this._ventana.CesionarioFiltrado != null) && (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre != null) &&
                        ((Interesado)this._ventana.CesionarioFiltrado).Nombre != null)
                    {
                        _poderesApoderadosCesionario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCesionarioFiltrado));

                        if ((this._ventana.ApoderadoCesionarioFiltrado != null) && (this.ValidarListaDePoderes(_poderesCesionario, _poderesApoderadosCesionario, "Cesionario")))
                        {
                            this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                            this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else if (!this.ValidarListaDePoderes(_poderesCesionario, _poderesApoderadosCesionario, "Cesionario"))
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Cesionario"), 0);
                        }
                    }
                    else if ((this._ventana.ApoderadoCesionarioFiltrado != null))
                    {
                        this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                        this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
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
            IList<Poder> listaIntereseccionCedente = new List<Poder>();
            IList<Poder> listaIntereseccionCesionario = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);
            
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
                    _poderesInterseccionCedente = listaIntereseccionCedente;
                    this._ventana.PoderesCedenteFiltrados = listaIntereseccionCedente;                    
                }


                else if ((listaIntereseccionCesionario.Count != 0) && (tipo.Equals("Cesionario")))
                {
                    _poderesInterseccionCesionario = listaIntereseccionCesionario;
                    this._ventana.PoderesCesionarioFiltrados = listaIntereseccionCedente;                    
                }

                else
                    retorno = false;
            }
            
            this._validar = !retorno;

            Mouse.OverrideCursor = null;
            
            return retorno;
        }

        public void LlenarListasPoderes(Cesion cesion)
        {

            if (cesion.Cedente != null)
                _poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(cesion.Cedente);

            if (cesion.Cesionario != null)
                _poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(cesion.Cesionario);

            if (cesion.AgenteCedente != null)
                _poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteCedente);

            if (cesion.AgenteCesionario != null)
                _poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteCesionario);
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

                Interesado cedente = new Interesado();                
                Agente apoderadoCedente = new Agente();
                IList<Agente> agentesCedenteFiltrados;                
                IList<Interesado> cedentesFiltrados = new List<Interesado>();                
                Poder poderFiltrar = new Poder();
                
                Interesado primerInteresado = new Interesado(int.MinValue);
                Agente primerAgente = new Agente("");

                agentesCedenteFiltrados = new List<Agente>();
                                
                poderFiltrar.Id = this._ventana.IdPoderCedenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderCedenteFiltrar);

                if (poderFiltrar.Id != 0)
                {                    
                    cedente = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderCedenteFiltrado);
                    agentesCedenteFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderCedenteFiltrado);                                         
                }                                    

                if (cedente != null)
                {
                    cedentesFiltrados.Insert(0, primerInteresado);
                    cedentesFiltrados.Add(cedente);
                    this._ventana.CedentesFiltrados = cedentesFiltrados;
                    this._ventana.CedenteFiltrado = primerInteresado;
                }
                else
                {
                    cedentesFiltrados.Insert(0, primerInteresado);                                        
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                    this._ventana.CedenteFiltrado = primerInteresado;
                }                
                
                if (agentesCedenteFiltrados.Count != 0)
                {
                    agentesCedenteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosCedenteFiltrados = agentesCedenteFiltrados;
                    this._ventana.ApoderadoCedenteFiltrado = primerAgente;

                }
                else
                {
                    agentesCedenteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                    this._ventana.ApoderadoCedenteFiltrado = primerAgente;
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
            if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                return true;

            return false;
        }
    }
}