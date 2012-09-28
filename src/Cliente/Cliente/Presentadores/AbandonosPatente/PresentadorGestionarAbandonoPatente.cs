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
using Trascend.Bolet.Cliente.Contratos.AbandonosPatente;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.AbandonosPatente;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.AbandonosPatente
{
    class PresentadorGestionarAbandonoPatente : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public bool _agregar = true;
        private IGestionarAbandonoPatente _ventana;


        private IPatenteServicios _patenteServicios;
        private IAsociadoServicios _asociadoServicios;
        private IPaisServicios _paisServicios;
        private IInteresadoServicios _interesadoServicios;
        private IServicioServicios _servicioServicios;
        private IOperacionServicios _operacionServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IBoletinServicios _boletinServicios;


        private IList<Asociado> _asociados;
        private IList<Patente> _patentes;
        private IList<Interesado> _interesados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarAbandonoPatente(IGestionarAbandonoPatente ventana, object abandono)
        {
            try
            {

                this._ventana = ventana;

                if (abandono != null)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.Operacion = abandono;
                    _agregar = false;
                }
                else
                {
                    Operacion abandonoAgregar = new Operacion();
                    abandonoAgregar.BAplicada = false;
                    this._ventana.Operacion = abandonoAgregar;

                    ((Operacion)this._ventana.Operacion).Fecha = DateTime.Now;
                    this._ventana.Patente = null;
                    this._ventana.Interesado = null;

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;

                    this._ventana.ActivarControlesAlAgregar();



                }

                #region Servicios

                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);

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
            if (_agregar == true)
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarAbandonoPatente,
                Recursos.Ids.GestionarAbandonoPatente);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarAbandonoPatente,
                Recursos.Ids.GestionarAbandonoPatente);
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

                if (_agregar == false)
                {



                    Operacion operacion = (Operacion)this._ventana.Operacion;

                    if (((Operacion)operacion).Patente != null)
                        this._ventana.Patente = this._patenteServicios.ConsultarPatenteConTodo(((Operacion)operacion).Patente);

                    this._ventana.NombrePatente = ((Patente)this._ventana.Patente).Descripcion;

                    CargarPatente();

                    CargarInteresado();

                    CargarAsociado();

                    CargaBoletines();

                    this._ventana.Boletin = this.BuscarBoletin((IList<Boletin>)this._ventana.Boletines, operacion.Boletin);
                }
                else
                {
                    CargarPatente();

                    CargarInteresado();

                    CargarAsociado();

                    CargaBoletines();
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

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
        /// Método que carga la ventana de Consultar Abandonos
        /// </summary>
        public void IrConsultarAbandonos()
        {
            this.Navegar(new ConsultarAbandonosPatente());
        }


        /// <summary>
        /// Método que carga los datos de Operacion Abandono para agregar en la Base de Datos
        /// </summary>
        public Operacion CargarAbandonoDeLaPantalla()
        {

            Operacion operacion = (Operacion)this._ventana.Operacion;

            operacion.Boletin = (Boletin)this._ventana.Boletin;
            operacion.Aplicada = Char.Parse(this._ventana.Aplicada);

            Servicio servicioAuxiliar = new Servicio("AB");
            operacion.Servicio = servicioAuxiliar;

            if (null != this._ventana.Patente)
            {
                operacion.Patente = ((Patente)this._ventana.Patente).Id != int.MinValue ? (Patente)this._ventana.Patente : null;
                operacion.CodigoAplicada = operacion.Patente.Id;
            }

            if (null != this._ventana.Interesado)
                operacion.Interesado = ((Interesado)this._ventana.Interesado).Id != int.MinValue ? (Interesado)this._ventana.Interesado : null;

            if (null != this._ventana.Asociado)
                operacion.Asociado = ((Asociado)this._ventana.Asociado).Id != int.MinValue ? (Asociado)this._ventana.Asociado : null;

            return operacion;
        }


        /// <summary>
        /// Método que carga los boletines registrados
        /// </summary>
        private void CargaBoletines()
        {
            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;
        }


        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// Agrega los datos de Operacion Abandono
        /// </summary>
        public void Agregar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Operacion operacion = CargarAbandonoDeLaPantalla();

                bool exitoso = this._operacionServicios.InsertarOModificar(operacion, UsuarioLogeado.Hash);


                if (exitoso)
                    this.Navegar(Recursos.MensajesConElUsuario.AbandonoInsertado, false);
                else
                    this.Navegar(Recursos.MensajesConElUsuario.AbandonoInsertado, true);


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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que se encarga de eliminar una Abandono
        /// </summary>
        public void Eliminar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._operacionServicios.Eliminar((Operacion)this._ventana.Operacion, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.AbandonoEliminado;
                    this.Navegar(_paginaPrincipal);
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
            finally
            {
                Mouse.OverrideCursor = null;
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


        #region Patentes


        /// <summary>
        /// Método que carga los datos iniciales de Patente a mostrar en la página
        /// </summary>
        private void CargarPatente()
        {
            this._patentes = new List<Patente>();
            Patente primeraPatente = new Patente(int.MinValue);
            this._patentes.Add(primeraPatente);

            if ((Patente)this._ventana.Patente != null)
            {
                this._patentes.Add((Patente)this._ventana.Patente);
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = (Patente)this._ventana.Patente;
                this._ventana.IdPatente = ((Patente)this._ventana.Patente).Id.ToString();

            }
            else
            {
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = primeraPatente;
            }

        }


        /// <summary>
        /// Método que se encarga de consultar una Patente en Base de datos
        /// </summary>
        public void ConsultarPatentes()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Patente primeraPatente = new Patente(int.MinValue);


                Patente patente = new Patente();
                IList<Patente> patentesFiltradas;
                patente.Descripcion = this._ventana.NombrePatenteFiltrar.ToUpper();
                patente.Id = this._ventana.IdPatenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPatenteFiltrar);

                if ((!patente.Descripcion.Equals("")) || (patente.Id != 0))
                    patentesFiltradas = this._patenteServicios.ObtenerPatentesFiltro(patente);
                else
                    patentesFiltradas = new List<Patente>();

                if (patentesFiltradas.ToList<Patente>().Count != 0)
                {
                    patentesFiltradas.Insert(0, primeraPatente);
                    this._ventana.PatentesFiltradas = patentesFiltradas.ToList<Patente>();
                    this._ventana.PatenteFiltrada = primeraPatente;
                }
                else
                {
                    patentesFiltradas.Insert(0, primeraPatente);
                    this._ventana.PatentesFiltradas = this._patentes;
                    this._ventana.PatenteFiltrada = primeraPatente;
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Método que se encarga en cambiar la Patente seleccionada en la lista filtrada
        /// </summary>
        /// <returns>True si se cambió correctamente, false en caso contrario</returns>
        public bool CambiarPatente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.PatenteFiltrada != null)
                {
                    this._ventana.Patente = this._ventana.PatenteFiltrada;
                    this._ventana.NombrePatente = ((Patente)this._ventana.PatenteFiltrada).Descripcion;
                    this._ventana.IdPatente = ((Patente)this._ventana.PatenteFiltrada).Id.ToString();

                    IList<Asociado> listaAsociadoAux = new List<Asociado>();
                    listaAsociadoAux.Add(new Asociado(int.MinValue));

                    if (this._agregar == true)
                    {
                        IList<Interesado> listaInteresadoAux = new List<Interesado>();
                        listaInteresadoAux.Add(new Interesado(int.MinValue));

                        if (null != ((Patente)this._ventana.Patente).Interesado)
                        {
                            Interesado interesadoAux = new Interesado(((Patente)this._ventana.Patente).Interesado.Id);
                            Interesado interesado = this._interesadoServicios.ConsultarInteresadoConTodo(interesadoAux);
                            this._ventana.Interesado = interesado;
                            this._interesados.Add(interesado);
                            this._ventana.Interesado = interesado;



                            listaInteresadoAux.Add(interesado);

                            this._ventana.InteresadosFiltrados = listaInteresadoAux;
                            this._ventana.InteresadoFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosFiltrados, interesado);
                        }
                        else
                        {
                            this._ventana.Interesado = new Interesado(int.MinValue);
                            this._ventana.InteresadosFiltrados = listaInteresadoAux;
                            this._ventana.InteresadoFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosFiltrados, (Interesado)this._ventana.Interesado);
                        }

                        if (null != ((Patente)this._ventana.Patente).Asociado)
                        {
                            Asociado asociadoAux = new Asociado(((Patente)this._ventana.Patente).Asociado.Id);
                            Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo(asociadoAux);
                            this._ventana.Asociado = asociado;


                            listaAsociadoAux.Add(asociado);

                            this._ventana.AsociadosFiltrados = listaAsociadoAux;
                            this._ventana.AsociadoFiltrado = this.BuscarAsociado((IList<Asociado>)this._ventana.AsociadosFiltrados, asociado);
                        }
                        else
                        {
                            this._ventana.Asociado = new Asociado(int.MinValue);
                            this._ventana.AsociadosFiltrados = listaAsociadoAux;
                            this._ventana.AsociadoFiltrado = this.BuscarAsociado((IList<Asociado>)this._ventana.AsociadosFiltrados, (Asociado)this._ventana.Asociado);
                        }

                    }
                    this._patentes.RemoveAt(0);
                    this._patentes.Add((Patente)this._ventana.PatenteFiltrada);

                    retorno = true;

                    if (null != ((Patente)this._ventana.Patente).Interesado)
                        this._ventana.PintarAsociado(((Asociado)this._ventana.Asociado).TipoCliente.Id);
                    else
                        this._ventana.PintarAsociado("5");

                    this._ventana.ConvertirEnteroMinimoABlanco();
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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }


        #endregion


        #region Interesado


        /// <summary>
        /// Método que carga los datos iniciales de Interesado a mostrar en la página
        /// </summary>
        private void CargarInteresado()
        {
            Interesado primerInteresado = new Interesado(int.MinValue);

            this._interesados = new List<Interesado>();

            this._interesados.Add(primerInteresado);

            if (((Operacion)this._ventana.Operacion).Interesado != null)
            {
                this._ventana.Interesado = this._interesadoServicios.ConsultarInteresadoConTodo(((Operacion)this._ventana.Operacion).Interesado);
                this._ventana.NombreInteresado = ((Interesado)this._ventana.Interesado).Nombre;

                if ((Interesado)this._ventana.Interesado != null)
                {
                    this._interesados.Add((Interesado)this._ventana.Interesado);
                    this._ventana.InteresadosFiltrados = this._interesados;
                    this._ventana.InteresadoFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosFiltrados, (Interesado)this._ventana.Interesado);
                }
            }
            else
            {
                //this._ventana.Interesado = primerInteresado;
                this._ventana.InteresadosFiltrados = this._interesados;
                this._ventana.InteresadoFiltrado = primerInteresado;

            }
        }


        /// <summary>
        /// Método que se encarga de consultar un Intesesado en Base de datos
        /// </summary>
        public void ConsultarInteresados()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);

                Interesado interesado = new Interesado();
                IList<Interesado> interesadosFiltrados;
                interesado.Nombre = this._ventana.NombreInteresadoFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosFiltrados = interesadosFiltrados;
                    this._ventana.InteresadoFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosFiltrados = this._interesados;
                    this._ventana.InteresadoFiltrado = primerInteresado;
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Método que se encarga en cambiar el Interesado seleccionada en la lista filtrada
        /// </summary>
        /// <returns>True si se cambió correctamente, false en caso contrario</returns>
        public bool CambiarInteresado()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoFiltrado != null)
                {
                    this._ventana.Interesado = this._ventana.InteresadoFiltrado;
                    this._ventana.NombreInteresado = ((Interesado)this._ventana.InteresadoFiltrado).Nombre;
                    this._interesados.RemoveAt(0);
                    this._interesados.Add((Interesado)this._ventana.InteresadoFiltrado);

                    this._ventana.ConvertirEnteroMinimoABlanco();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }


        /// <summary>
        /// Método que se encarga de limpiar las lista de Interesados
        /// </summary>
        public void LimpiarListaInteresado()
        {
            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            this._ventana.InteresadosFiltrados = listaInteresados;
            this._ventana.InteresadoFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
            this._ventana.Interesado = this._ventana.InteresadoFiltrado;
        }


        #endregion


        #region Asociado


        /// <summary>
        /// Método que se encarga de consultar un Asociado en Base de datos
        /// </summary>
        public void ConsultarAsociados()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Asociado primeraAsociado = new Asociado(int.MinValue);


                Asociado asociado = new Asociado();
                IList<Asociado> asociadosFiltrados;

                asociado.Nombre = this._ventana.NombreAsociadoFiltrar.ToUpper();
                asociado.Id = this._ventana.IdAsociadoFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdAsociadoFiltrar);

                if ((!asociado.Nombre.Equals("")) || (asociado.Id != 0))
                    asociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(asociado);
                else
                    asociadosFiltrados = new List<Asociado>();

                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                {
                    asociadosFiltrados.Insert(0, primeraAsociado);
                    this._ventana.AsociadosFiltrados = asociadosFiltrados.ToList<Asociado>();
                    this._ventana.AsociadoFiltrado = primeraAsociado;
                }
                else
                {
                    asociadosFiltrados.Insert(0, primeraAsociado);
                    this._ventana.AsociadosFiltrados = this._asociados;
                    this._ventana.AsociadoFiltrado = primeraAsociado;
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Método que carga los datos iniciales de Asociado a mostrar en la página
        /// </summary>
        private void CargarAsociado()
        {
            this._asociados = new List<Asociado>();
            Asociado primerAsociado = new Asociado(int.MinValue);
            this._asociados.Add(primerAsociado);

            if (((Operacion)this._ventana.Operacion).Asociado != null)
            {
                this._asociados.Add(((Operacion)this._ventana.Operacion).Asociado);
                this._ventana.AsociadosFiltrados = this._asociados;
                this._ventana.AsociadoFiltrado = ((Operacion)this._ventana.Operacion).Asociado;
                this._ventana.NombreAsociado = ((Operacion)this._ventana.Operacion).Asociado.Nombre;

                if (null != ((Patente)this._ventana.Patente).Asociado)
                    this._ventana.PintarAsociado(((Patente)this._ventana.Patente).Asociado.TipoCliente.Id);
                else
                    this._ventana.PintarAsociado("5");
            }
            else
            {
                this._ventana.AsociadosFiltrados = this._asociados;
                this._ventana.AsociadoFiltrado = primerAsociado;
            }

        }


        /// <summary>
        /// Método que se encarga en cambiar el Asociado seleccionada en la lista filtrada
        /// </summary>
        /// <returns>True si se cambió correctamente, false en caso contrario</returns>
        public bool CambiarAsociado()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.AsociadoFiltrado != null)
                {
                    this._ventana.Asociado = this._ventana.AsociadoFiltrado;
                    this._ventana.NombreAsociado = ((Asociado)this._ventana.AsociadoFiltrado).Nombre;
                    this._asociados.RemoveAt(0);
                    this._asociados.Add((Asociado)this._ventana.AsociadoFiltrado);

                    retorno = true;

                    if (((Asociado)this._ventana.AsociadoFiltrado).Id != int.MinValue)
                        this._ventana.PintarAsociado(((Asociado)this._ventana.AsociadoFiltrado).TipoCliente.Id);
                    else
                        this._ventana.PintarAsociado("5");

                    this._ventana.ConvertirEnteroMinimoABlanco();
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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }


        #endregion


    }
}
