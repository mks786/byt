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
using Trascend.Bolet.Cliente.Contratos.Traspasos.CambiosDeNombre;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Traspasos.CambiosDeNombre
{
    class PresentadorGestionarCambioDeNombre : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IGestionarCambioDeNombre _ventana;

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

        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Corresponsal> _corresponsales;
        private IList<Auditoria> _auditorias;
        private IList<Marca> _marcas;
        private IList<Interesado> _interesadosEntre;
        private IList<Interesado> _interesadosSobreviviente;
        private IList<Agente> _agentesApoderados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCambioDeNombre(IGestionarCambioDeNombre ventana, object CambioDeNombre)
        {
            try
            {

                this._ventana = ventana;
                this._ventana.CambioDeNombre = CambioDeNombre;

                #region Servicios

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCambiosDeNombre,
                Recursos.Ids.GestionarCambioDeNombre);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCambiosDeNombre,
                                                      Recursos.Ids.GestionarCambioDeNombre);

                CambioNombre CambioDeNombre = (CambioNombre)this._ventana.CambioDeNombre;
                Poder poder = new Poder(1);
                IList<Agente> prueba = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
                int x = prueba.Count;

                Interesado prueba1 = this._interesadoServicios.ObtenerInteresadosDeUnPoder(poder);
                string a = prueba1.Nombre;

                this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(((CambioNombre)CambioDeNombre).Marca);
                this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioNombre)CambioDeNombre).InteresadoAnterior);
                this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioNombre)CambioDeNombre).InteresadoActual);
                this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;
                this._ventana.AgenteApoderado = ((CambioNombre)CambioDeNombre).Agente;

                this._marcas = new List<Marca>();
                this._marcas.Add((Marca)this._ventana.Marca);
                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.MarcaFiltrada = (Marca)this._ventana.Marca;

                this._interesadosEntre = new List<Interesado>();
                this._interesadosEntre.Add(((CambioNombre)CambioDeNombre).InteresadoAnterior);
                this._ventana.InteresadosAnteriorFiltrados = this._interesadosEntre;
                this._ventana.InteresadoAnteriorFiltrado = (Interesado)this._ventana.InteresadoAnterior;

                this._interesadosSobreviviente = new List<Interesado>();
                this._interesadosSobreviviente.Add(((CambioNombre)CambioDeNombre).InteresadoActual);
                this._ventana.InteresadosActualFiltrados = this._interesadosSobreviviente;
                this._ventana.InteresadoActualFiltrado = (Interesado)this._ventana.InteresadoActual;

                this._agentesApoderados = new List<Agente>();
                this._agentesApoderados.Add(((CambioNombre)CambioDeNombre).Agente);
                this._ventana.AgenteApoderadoFiltrados = this._agentesApoderados;
                this._ventana.AgenteApoderadoFiltrado = (Agente)this._ventana.AgenteApoderado;

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

        public CambioNombre CargarCambioDeNombreDeLaPantalla()
        {

            CambioNombre CambioDeNombre = (CambioNombre)this._ventana.CambioDeNombre;

            //marca.Operacion = "MODIFY";

            //if (null != this._ventana.Agente)
            //    marca.Agente = !((Agente)this._ventana.Agente).Id.Equals("NGN") ? (Agente)this._ventana.Agente : null;

            //if (null != this._ventana.AsociadoSolicitud)
            //    marca.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

            //if (null != this._ventana.BoletinConcesion)
            //    marca.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue ? (Boletin)this._ventana.BoletinConcesion : null;

            //if (null != this._ventana.BoletinPublicacion)
            //    marca.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue ? (Boletin)this._ventana.BoletinPublicacion : null;

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

            //if (null != this._ventana.TipoMarcaDatos)
            //    marca.Tipo = !((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id : null;

            //if(string.IsNullOrEmpty(this._ventana.IdInternacional))
            //    marca.Internacional = null;

            //if(string.IsNullOrEmpty(this._ventana.IdNacional))
            //    marca.Nacional = null;

            return CambioDeNombre;
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
                    CambioNombre CambioDeNombre = CargarCambioDeNombreDeLaPantalla();

                    //bool exitoso = this._marcaServicios.InsertarOModificar(CambioDeNombre, UsuarioLogeado.Hash);

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

        public void Auditoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                this.Navegar(new ListaAuditorias(_auditorias));


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

        #region Marcas

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
                IEnumerable<Marca> marcasFiltradas;
                marca.Descripcion = this._ventana.NombreMarcaFiltrar.ToUpper();
                marca.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);
                if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                    marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marca);
                else
                    marcasFiltradas = new List<Marca>();

                if (marcasFiltradas.ToList<Marca>().Count != 0)
                    this._ventana.MarcasFiltradas = marcasFiltradas.ToList<Marca>();
                else
                    this._ventana.MarcasFiltradas = this._marcas;

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
                    this._marcas.RemoveAt(0);
                    this._marcas.Add((Marca)this._ventana.MarcaFiltrada);
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

        #region InteresadoAnterior

        public void ConsultarInteresadosAnterior()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado interesado = new Interesado();
                IEnumerable<Interesado> interesadosFiltradas;
                interesado.Nombre = this._ventana.NombreInteresadoAnteriorFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoAnteriorFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoAnteriorFiltrar);
                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltradas = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltradas = new List<Interesado>();

                if (interesadosFiltradas.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosAnteriorFiltrados = interesadosFiltradas.ToList<Interesado>();
                else
                    this._ventana.InteresadosAnteriorFiltrados = this._interesadosEntre;

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

        public bool CambiarInteresadoAnterior()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoAnteriorFiltrado != null)
                {
                    this._ventana.InteresadoAnterior =
                        this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoAnteriorFiltrado);
                    this._ventana.NombreInteresadoAnterior = ((Interesado)this._ventana.InteresadoAnteriorFiltrado).Nombre;
                    this._interesadosEntre.RemoveAt(0);
                    this._interesadosEntre.Add((Interesado)this._ventana.InteresadoAnteriorFiltrado);
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

        #region InteresadoActual

        public void ConsultarInteresadosActual()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado interesado = new Interesado();
                IEnumerable<Interesado> interesadosFiltradas;
                interesado.Nombre = this._ventana.NombreInteresadoActualFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoActualFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoActualFiltrar);
                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltradas = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltradas = new List<Interesado>();

                if (interesadosFiltradas.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosActualFiltrados = interesadosFiltradas.ToList<Interesado>();
                else
                    this._ventana.InteresadosActualFiltrados = this._interesadosSobreviviente;

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

        public bool CambiarInteresadoActual()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoActualFiltrado != null)
                {
                    this._ventana.InteresadoActual =
                        this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoActualFiltrado);
                    this._ventana.NombreInteresadoActual = ((Interesado)this._ventana.InteresadoActualFiltrado).Nombre;
                    this._interesadosSobreviviente.RemoveAt(0);
                    this._interesadosSobreviviente.Add((Interesado)this._ventana.InteresadoActualFiltrado);
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

        #region Agente Apoderado

        public void ConsultarApoderados()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoCedente = new Agente();
                IEnumerable<Agente> agentesCedenteFiltrados;
                apoderadoCedente.Nombre = this._ventana.NombreAgenteFiltrar.ToUpper();
                apoderadoCedente.Id = this._ventana.IdAgenteFiltrar.ToUpper();

                if ((!apoderadoCedente.Nombre.Equals("")) || (!apoderadoCedente.Id.Equals("")))
                    agentesCedenteFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoCedente);
                else
                    agentesCedenteFiltrados = new List<Agente>();

                if (agentesCedenteFiltrados.ToList<Agente>().Count != 0)
                    this._ventana.AgenteApoderadoFiltrados = agentesCedenteFiltrados.ToList<Agente>();
                else
                    this._ventana.AgenteApoderadoFiltrados = this._agentesApoderados;

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

        public bool CambiarApoderado()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.AgenteApoderadoFiltrado != null)
                {
                    this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;
                    this._ventana.NombreAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Nombre;
                    this._agentesApoderados.RemoveAt(0);
                    this._agentesApoderados.Add((Agente)this._ventana.AgenteApoderadoFiltrado);
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
    }
}
