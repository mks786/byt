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
using Trascend.Bolet.Cliente.Contratos.Anualidades;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Anualidades
{
    class PresentadorGestionarAnualidades : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _agregar = true;
        int _idAnualidad = 0;
        private IGestionarAnualidades _ventana;

        private IPatenteServicios _marcaServicios;
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
        private IPatenteServicios _patentesServicios;
        private IPlanillaServicios _planillaServicios;
        private IAnualidadServicios _anualidadServicios;

        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Patente> _patentes;



        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarAnualidades(IGestionarAnualidades ventana, object patente)
        {
            try
            {
                this._ventana = ventana;

                if (patente != null)
                {
                    this._ventana.Patente = patente;
                    _agregar = false;
                }
                else
                {
                    Patente patenteAgregar = new Patente();
                    this._ventana.Patente = patenteAgregar;


                    CambiarAModificar();

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;

                   
                }

                #region Servicios

                this._marcaServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
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
                this._patentesServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._planillaServicios = (IPlanillaServicios)Activator.GetObject(typeof(IPlanillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PlanillaServicios"]);
                this._anualidadServicios = (IAnualidadServicios)Activator.GetObject(typeof(IAnualidadServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnualidadServicios"]);


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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarAnualidad,
                Recursos.Ids.GestionarAnualidad);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarAnualidad,
                Recursos.Ids.GestionarAnualidad);
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

                    Patente patente = (Patente)this._ventana.Patente;

                    this._ventana.NombrePatente = ((Patente)this._ventana.Patente).Descripcion;
                    this._ventana.NombreAsociadoSolicitud = patente.Asociado.Nombre;
                    this._ventana.NombreInteresadoSolicitud = patente.Interesado.Nombre;
                    this._ventana.Anualidades = patente.Anualidades;
                    this._ventana.Referencia = patente.PrimeraReferencia;
                    this._ventana.RegistroCodigo = patente.CodigoRegistro;
                 //   this._ventana.RegistroFecha = patente.FechaRegistro;


                    CargarPatente();
                    CargaComboBox();


                    this._ventana.FocoPredeterminado();

                }
                else
                {
                    CargarPatente();
                    CargaComboBox();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        public void IrConsultarPatentes()
        {
       //OJO     this.Navegar(new ConsultarPatentes());
        }

        /// <summary>
        /// Método que carga la Patente registrada
        /// </summary>
        private void CargarPatente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._patentes = new List<Patente>();
            Patente primeraPatente = new Patente(int.MinValue);
            this._patentes.Add(primeraPatente);

            if ((Patente)this._ventana.Patente != null)
            {
                this._patentes.Add((Patente)this._ventana.Patente);
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = (Patente)this._ventana.Patente;

                if (null != ((Patente)this._ventana.Patente).Asociado)
                    this._ventana.PintarAsociado(((Patente)this._ventana.Patente).Asociado.TipoCliente.Id);
                else
                    this._ventana.PintarAsociado("5");

            }
            else
            {
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = primeraPatente;
            }
  

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Método que dependiendo del estado de la pagina carga una patente seleccionada
        /// o una nueva
        /// </summary>
        public Patente CargarPatenteDeLaPantalla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Patente patente = (Patente)this._ventana.Patente;

            if (null != this._ventana.BoletinConcesion)
                patente.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue ? (Boletin)this._ventana.BoletinConcesion : null;

            if (null != this._ventana.BoletinPublicacion)
                patente.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue ? (Boletin)this._ventana.BoletinPublicacion : null;

            #region Comentado
            //marca.Operacion = "MODIFY";
            //if (null != this._ventana.Agente)
            //    marca.Agente = !((Agente)this._ventana.Agente).Id.Equals("NGN") ? (Agente)this._ventana.Agente : null;

            //if (null != this._ventana.AsociadoSolicitud)
            //    marca.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

            //if (null != this._ventana.InteresadoSolicitud)
            //    marca.Interesado = !((Interesado)this._ventana.InteresadoSolicitud).Id.Equals("NGN") ? ((Interesado)this._ventana.InteresadoSolicitud) : null;

            //if (null != this._ventana.Servicio)
            //    marca.Servicio = !((Servicio)this._ventana.Servicio).Id.Equals("NGN") ? ((Servicio)this._ventana.Servicio) : null;

            //if (null != this._ventana.PoderSolicitud)
            //    marca.Poder = !((Poder)this._ventana.PoderSolicitud).Id.Equals("NGN") ? ((Poder)this._ventana.PoderSolicitud) : null;

            //if (null != this._ventana.PaisSolicitud)
            //    marca.Pais = ((Pais)this._ventana.PaisSolicitud).Id != int.MinValue ? ((Pais)this._ventana.PaisSolicitud) : null;

            //if (null != this._ventana.StatusWeb)
            //    marca.StatusWeb = ((StatusWeb)this._ventana.StatusWeb).Id.Equals("NGN") ? ((StatusWeb)this._ventana.StatusWeb) : null;

            //if (null != this._ventana.CorresponsalSolicitud)
            //    marca.Corresponsal = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id != int.MinValue ? ((Corresponsal)this._ventana.CorresponsalSolicitud) : null;

            //if (null != this._ventana.Sector)
            //    marca.Sector = !((ListaDatosDominio)this._ventana.Sector).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.Sector).Id : null;

            //if (null != this._ventana.TipoReproduccion)
            //    marca.TipoRps = ((ListaDatosDominio)this._ventana.TipoReproduccion).Id[0];

            //if (null != this._ventana.TipoPatenteDatos)
            //    marca.Tipo = !((ListaDatosDominio)this._ventana.TipoPatenteDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoPatenteDatos).Id : null;

            //if(string.IsNullOrEmpty(this._ventana.IdInternacional))
            //    marca.Internacional = null;

            //if(string.IsNullOrEmpty(this._ventana.IdNacional))
            //    marca.Nacional = null;
            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return patente;
        }

        /// <summary>
        /// Método que habilita los campos
        /// </summary>
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
            Mouse.OverrideCursor = Cursors.Wait;
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

                //Modifica los datos de la patente
                else if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnAceptar)
                {
                    Patente patente = CargarPatenteDeLaPantalla();


                    bool exitoso = this._anualidadServicios.InsertarOModificarAnualidad(patente, UsuarioLogeado.Hash);

                    if ((exitoso) && (this._agregar == false))
                        this.Navegar(Recursos.MensajesConElUsuario.AnualidadModificada, false);
                    else if ((exitoso) && (this._agregar == true))
                        this.Navegar(Recursos.MensajesConElUsuario.AnualidadInsertada, false);
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
        /// Método Muestra las marcas consultadas
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

                Patente marca = new Patente();
                IList<Patente> marcasFiltradas;
                marca.Descripcion = this._ventana.NombrePatenteFiltrar.ToUpper();
                marca.Id = this._ventana.IdPatenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPatenteFiltrar);

                if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                    marcasFiltradas = this._marcaServicios.ObtenerPatentesFiltro(marca);
                else
                    marcasFiltradas = new List<Patente>();

                if (marcasFiltradas.ToList<Patente>().Count != 0)
                {
                    marcasFiltradas.Insert(0, new Patente(int.MinValue));
                    this._ventana.PatentesFiltradas = marcasFiltradas.ToList<Patente>();
                }
                else
                {
                    marcasFiltradas.Insert(0, new Patente(int.MinValue));
                    this._ventana.PatentesFiltradas = this._patentes;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
        /// Método que cambia la marca
        /// </summary>
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
                    this._patentes.RemoveAt(0);
                    this._patentes.Add((Patente)this._ventana.PatenteFiltrada);
                    retorno = true;

                    if (null != ((Patente)this._ventana.Patente).Asociado)
                        this._ventana.PintarAsociado(((Patente)this._ventana.Patente).Asociado.TipoCliente.Id);
                    else
                        this._ventana.PintarAsociado("5");
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


        #region Asociados

        public void CambiarAsociadoSolicitud()
        {
            try
            {
                if ((Asociado)this._ventana.AsociadoSolicitud != null)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoSolicitud);
                    this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                    //this._ventana.AsociadoDatos = (Asociado)this._ventana.AsociadoSolicitud;
                    //this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreAsociadoSolicitud = "";
                //this._ventana.NombreAsociadoDatos = "";
            }
        }

        public void BuscarAsociado(int filtrarEn)
        {
            IEnumerable<Asociado> asociadosFiltrados = this._asociados;

            if (filtrarEn == 0)
            {

                if (!string.IsNullOrEmpty(this._ventana.IdAsociadoSolicitudFiltrar))
                {
                    asociadosFiltrados = from p in asociadosFiltrados
                                         where p.Id == int.Parse(this._ventana.IdAsociadoSolicitudFiltrar)
                                         select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoSolicitudFiltrar))
                {
                    asociadosFiltrados = from p in asociadosFiltrados
                                         where p.Nombre != null &&
                                         p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoSolicitudFiltrar.ToLower())
                                         select p;
                }
            }
            //else
            //{

            //    if (!string.IsNullOrEmpty(this._ventana.IdAsociadoDatosFiltrar))
            //    {
            //        asociadosFiltrados = from p in asociadosFiltrados
            //                             where p.Id == int.Parse(this._ventana.IdAsociadoDatosFiltrar)
            //                             select p;
            //    }

            //    if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoDatosFiltrar))
            //    {
            //        asociadosFiltrados = from p in asociadosFiltrados
            //                             where p.Nombre != null &&
            //                             p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoDatosFiltrar.ToLower())
            //                             select p;
            //    }
            //}

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                    this._ventana.AsociadosSolicitud = asociadosFiltrados.ToList<Asociado>();
                else
                    this._ventana.AsociadosSolicitud = this._asociados;
            }
            //else
            //{
            //    if (asociadosFiltrados.ToList<Asociado>().Count != 0)
            //        this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
            //    else
            //        this._ventana.AsociadosDatos = this._asociados;
            //}
        }

        public void CargarAsociados()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            Patente patente = (Patente)this._ventana.Patente;
            IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
            Asociado primerAsociado = new Asociado();
            primerAsociado.Id = int.MinValue;
            asociados.Insert(0, primerAsociado);
            this._ventana.AsociadosSolicitud = asociados;
            //this._ventana.AsociadosDatos = asociados;
            this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, patente.Asociado);
            //this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, marcaTercero.Asociado);
            //this._ventana.NombreAsociadoDatos = ((MarcaTercero)this._ventana.MarcaTercero).Asociado.Nombre;
            if (((Patente)this._ventana.Patente).Asociado !=null)
                this._ventana.NombreAsociadoSolicitud = ((Patente)this._ventana.Patente).Asociado.Nombre;
            this._asociados = asociados;
            this._ventana.AsociadosEstanCargados = true;

            Mouse.OverrideCursor = null;
        }


        #endregion

        #region Interesados

        public void CambiarInteresadoSolicitud()
        {
            try
            {
                if ((Interesado)this._ventana.InteresadoSolicitud != null)
                {
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSolicitud);
                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    //this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoSolicitud;
                    //this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;

                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreInteresadoSolicitud = "";
                //this._ventana.NombreInteresadoDatos = "";
            }
        }

        public void BuscarInteresado(int filtrarEn)
        {
            IEnumerable<Interesado> interesadosFiltrados = this._interesados;

            if (filtrarEn == 0)
            {
                if (!string.IsNullOrEmpty(this._ventana.IdInteresadoSolicitudFiltrar))
                {
                    interesadosFiltrados = from p in interesadosFiltrados
                                           where p.Id == int.Parse(this._ventana.IdInteresadoSolicitudFiltrar)
                                           select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoSolicitudFiltrar))
                {
                    interesadosFiltrados = from p in interesadosFiltrados
                                           where p.Nombre != null &&
                                           p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoSolicitudFiltrar.ToLower())
                                           select p;
                }
            }
            //else
            //{
            //    if (!string.IsNullOrEmpty(this._ventana.IdInteresadoDatosFiltrar))
            //    {
            //        interesadosFiltrados = from p in interesadosFiltrados
            //                               where p.Id == int.Parse(this._ventana.IdInteresadoDatosFiltrar)
            //                               select p;
            //    }

            //    if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoDatosFiltrar))
            //    {
            //        interesadosFiltrados = from p in interesadosFiltrados
            //                               where p.Nombre != null &&
            //                               p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoDatosFiltrar.ToLower())
            //                               select p;
            //    }
            //}

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
                else
                    this._ventana.InteresadosSolicitud = this._interesados;
            }
            //else
            //{
            //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
            //        this._ventana.InteresadosDatos = interesadosFiltrados.ToList<Interesado>();
            //    else
            //        this._ventana.InteresadosDatos = this._interesados;
            //}
        }

        public void CargarInteresados()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Patente patente = (Patente)this._ventana.Patente;

            IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
            Interesado primerInteresado = new Interesado();
            primerInteresado.Id = int.MinValue;
            interesados.Insert(0, primerInteresado);
            //this._ventana.InteresadosDatos = interesados;
            this._ventana.InteresadosSolicitud = interesados;
            ((Patente)this._ventana.Patente).Interesado = this.BuscarInteresado(interesados, patente.Interesado);
            Interesado interesado = this.BuscarInteresado(interesados, patente.Interesado);
            this._ventana.InteresadoSolicitud = interesado;
            //this._ventana.InteresadoDatos = interesado;
            //interesado = this._interesadoServicios.ConsultarInteresadoConTodo(interesado);
            //this._ventana.NombreInteresadoDatos = ((MarcaTercero)this._ventana.MarcaTercero).Interesado.Nombre;
            if (((Patente)this._ventana.Patente).Interesado!=null)
                 this._ventana.NombreInteresadoSolicitud = ((Patente)this._ventana.Patente).Interesado.Nombre;
            this._interesados = interesados;

            this._ventana.InteresadosEstanCargados = true;

            Mouse.OverrideCursor = null;
        }



        #endregion

        #region Boletines y situacion

        /// <summary>
        /// Método que carga los combobox con la data
        /// </summary>
        /// <returns></returns>
        public void CargaComboBox()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Patente patente = (Patente)this._ventana.Patente;
            EstadoMarca estadoMarca = new EstadoMarca();
            TipoBase tipoBase = new TipoBase();

            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            Boletin primerBoletin = new Boletin();
            primerBoletin.Id = int.MinValue;
            boletines.Insert(0, primerBoletin);
            this._ventana.BoletinesPublicacion = boletines;
            this._ventana.BoletinesConcesion = boletines;
            if (!_agregar)
            {
                this._ventana.BoletinConcesion = this.BuscarBoletin(boletines, patente.BoletinConcesion);
                this._ventana.BoletinPublicacion = this.BuscarBoletin(boletines, patente.BoletinPublicacion);
            }


            IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
            Servicio primerServicio = new Servicio();
            primerServicio.Id = "NGN";
            servicios.Insert(0, primerServicio);
            this._ventana.Situaciones = servicios;
           // this._ventana.ISituaciones = servicios;
            if (!_agregar)
                this._ventana.Situacion = this.BuscarServicio(servicios, patente.Servicio);

            ListaDatosDominio Dominio = new ListaDatosDominio();
            Dominio.Filtro = "SITUACION_ANUALIDAD";
            IList<ListaDatosDominio> dominios = this._listaDatosDominioServicios.ConsultarListaDatosDominioPorParametro(Dominio);
            Dominio = new ListaDatosDominio();
            Dominio.Id = "NGN";
            dominios.Insert(0, Dominio);
            this._ventana.ISituaciones = dominios;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        #endregion

        #region Anualidad

        /// <summary>
        /// Método que carga lista de Anualidad
        /// </summary>
        /// <returns>true si se realizó correctamente</returns>
        public bool CargarAnualidad()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;
            Patente patente = (Patente)this._ventana.Patente;

            if ((null != patente.Anualidades) && (patente.Anualidades.Count != 0))
            {
                this._ventana.Anualidades = null;
                this._ventana.Anualidades = patente.Anualidades;
                retorno = true;
                
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// Método que carga lista de Anualidad
        /// </summary>
        /// <returns>true si se realizó correctamente</returns>
        public bool AgregarAnualidad()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anualidad> anulidades;
            int conta = 0;

            bool retorno = false;

                if (null == ((Patente)this._ventana.Patente).Anualidades)
                    anulidades = new List<Anualidad>();
                else
                    anulidades = ((Patente)this._ventana.Patente).Anualidades;

                    this._idAnualidad = conta;
                    Anualidad aux = new Anualidad();
                    aux.Asociado = ((Patente)this._ventana.Patente).Asociado;
                    aux.Id = conta;
                    aux.Recibo = this._ventana.Recibo;
                    if (this._ventana.FechaAnualidad != "")
                        aux.FechaAnualidad = DateTime.Parse(this._ventana.FechaAnualidad);
                    aux.Voucher = this._ventana.Voucher;
                    if (this._ventana.FechaVoucher != "")
                        aux.FechaVoucher = DateTime.Parse(this._ventana.FechaVoucher);
                    if (this._ventana.ISituacion != null)
                        aux.Situacion = ((ListaDatosDominio)this._ventana.ISituacion).Id;
                    if (this._ventana.Factura != "")
                        aux.Factura = int.Parse(this._ventana.Factura);
                    aux.IFactura = "T";
                    aux.Patente = ((Patente)this._ventana.Patente);
                    anulidades.Add(aux);
                
                
                ((Patente)this._ventana.Patente).Anualidades = anulidades;
                this._ventana.Anualidades = anulidades.ToList<Anualidad>();
                // this._patentesBaseTercero.Remove((MarcaBaseTercero)this._ventana.MarcaTercero);
                // ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero = this._patentesBaseTercero.ToList<MarcaBaseTercero>();
                retorno = true;
            

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// Método que Modifica lista de Anualidad
        /// </summary>
        /// <returns>true si se realizó correctamente</returns>
        public bool ModificarAnualidad()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion


            IList<Anualidad> anulidades;
            Anualidad anuSelect = ((Anualidad)this._ventana.Anualidad);

            if (anuSelect != null)
            {
                bool retorno = false;

                if (null == ((Patente) this._ventana.Patente).Anualidades)
                    anulidades = new List<Anualidad>();
                else
                    anulidades = ((Patente) this._ventana.Patente).Anualidades;

                int contador = 0;

                foreach (Anualidad aux1 in anulidades)
                {
                    if ((aux1.Id == anuSelect.Id))
                    {

                        anulidades[contador].Id = anuSelect.Id;
                        if (this._ventana.Recibo != "")
                            anulidades[contador].Recibo = this._ventana.Recibo;
                        if (this._ventana.FechaAnualidad != "")
                            anulidades[contador].FechaAnualidad = DateTime.Parse(this._ventana.FechaAnualidad);
                        if (this._ventana.FechaVoucher != "")
                            anulidades[contador].FechaVoucher = DateTime.Parse(this._ventana.FechaVoucher);
                        if (this._ventana.Voucher != "")
                            anulidades[contador].Voucher = this._ventana.Voucher;
                        if (this._ventana.Factura != "")
                            anulidades[contador].Factura = int.Parse(this._ventana.Factura);
                        if (this._ventana.ISituaciones != "")
                            anulidades[contador].Situacion = ((ListaDatosDominio)this._ventana.ISituacion).Id;



                    }
                    contador++;
                }



                ((Patente) this._ventana.Patente).Anualidades = anulidades;
                this._ventana.Anualidades = anulidades.ToList<Anualidad>();

                #region trace

                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);

                #endregion

            }
            return true;
        }

        /// <summary>
        /// Metodo que deshabilita los Anulidadaes
        /// </summary>
        /// <returns>retorno true si se deshabilitó</returns>
        public bool DeshabilitarAnualidad()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anualidad> anualidades;
            bool respuesta = false;

            if ((null != ((Anualidad)this._ventana.Anualidad)))
            {
                if (null == ((Patente)this._ventana.Patente).Anualidades)
                    anualidades = new List<Anualidad>();
                else
                    anualidades = ((Patente)this._ventana.Patente).Anualidades;

                
                anualidades.Remove((Anualidad)this._ventana.Anualidad);
                ((Patente)this._ventana.Patente).Anualidades = anualidades;
                this._ventana.Anualidades = anualidades.ToList<Anualidad>();

                if (anualidades.Count == 0)
                    respuesta = true;

            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return respuesta;
        }

        /// <summary>
        /// Método que carga la anualdiad seleccionada
        /// <returns>true si se realizó correctamente</returns>
        public void CargarAnualidadSeleccionada(bool nuevo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
            //IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
            //_ventana.ISituaciones = servicios;
            //Servicio serv = new Servicio();
            //Servicio servi2;

            ListaDatosDominio Dominio = new ListaDatosDominio();
            ListaDatosDominio domi = new ListaDatosDominio();
            Dominio.Filtro = "SITUACION_ANUALIDAD";
            IList<ListaDatosDominio> dominios = this._listaDatosDominioServicios.ConsultarListaDatosDominioPorParametro(Dominio);
            Dominio = new ListaDatosDominio();
            Dominio.Id = "NGN";
            dominios.Insert(0, Dominio);
            this._ventana.ISituaciones = dominios;


            if (!nuevo)
            {
                Anualidad anuSelect = ((Anualidad) this._ventana.Anualidad);
                domi.Id = anuSelect.Situacion;
                this._ventana.Recibo = anuSelect.Recibo;
                this._ventana.FechaAnualidad = anuSelect.FechaAnualidad.ToString();
                this._ventana.FechaVoucher = anuSelect.FechaVoucher.ToString();
                this._ventana.ISituacion = BuscarListaDeDominio(dominios, domi);
                this._ventana.Voucher = anuSelect.Voucher;
                this._ventana.Factura = anuSelect.Factura.ToString();
            }
            else
            {
                domi.Id = "MGM";
                this._ventana.Recibo = "";
                this._ventana.FechaAnualidad = "";
                this._ventana.FechaVoucher = "";
                this._ventana.ISituacion = this.BuscarListaDeDominio(dominios, domi);
                this._ventana.Voucher = "";
                this._ventana.Factura = "";
            }



            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

           
        }



        #endregion
        public void IrImprimir(string nombreBoton)
        {
            try
            {
                switch (nombreBoton)
                {
                    case "_btnPlanilla":
                        ImprimirPlanilla();
                        break;
                    case "_btnAnexo":
                        ImprimirAnexo();
                        break;
                    //case "_btnCarpeta":
                    //    ImprimirCarpeta();
                    //    break;
                    default:
                        break;
                }
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ExcepcionPaquetes, true);
            }
        }

        //private void ImprimirCarpeta()
        //{
        //    if (ValidarPatenteAntesDeImprimirCarpeta())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_PFUSIONES";
        //        string procedimiento = "P4";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento(((Patente)this._ventana.Patente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        //Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
        //        //if (planilla != null)
        //        //{
        //        this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnCarpeta);
        //        //}
        //    }
        //}

        private bool ValidarPatenteAntesDeImprimirCarpeta()
        {
            return true;
        }

        private void ImprimirAnexo()
        {
            if (ValidarPatenteAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_PFUSIONES";
                string procedimiento = "P2";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Patente)this._ventana.Patente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                //Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                //if (planilla != null)
                //{
                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnAnexo);
                //}
            }
        }

        private bool ValidarPatenteAntesDeImprimirAnexo()
        {
            return true;
        }

        private void ImprimirPlanilla()
        {
            if (ValidarPatenteAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_PFUSIONES";
                string procedimiento = "P1";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Patente)this._ventana.Patente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                //Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                //if (planilla != null)
                //{
                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanilla);
                //}
            }
        }

        private bool ValidarPatenteAntesDeImprimirPlanilla()
        {
            return true;
        }
    }
}
