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
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorConsultarMarca : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarMarca _ventana;

        private IMarcaServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IAgenteServicios _agenteServicios;
        private IPoderServicios _poderServicios;
        private IBoletinServicios _boletinServicios;
        private IPaisServicios _paisServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IInteresadoServicios _interesadoServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;

        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Auditoria> _auditorias;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarMarca(IConsultarMarca ventana,object marca)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Marca = marca;

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
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcas,
                Recursos.Ids.ConsultarMarcas);
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

                Marca marca = (Marca)this._ventana.Marca;

                IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
                Asociado primerAsociado = new Asociado();
                primerAsociado.Id = int.MinValue;
                asociados.Insert(0, primerAsociado);
                this._ventana.AsociadosSolicitud = asociados;
                this._ventana.AsociadosDatos = asociados;
                this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, marca.Asociado);
                this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, marca.Asociado);
                this._asociados = asociados;

                this._ventana.TipoMarcasDatos = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaMarca));

                this._ventana.TipoMarcasSolicitud = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaMarca));

                IList<Agente> agentes = this._agenteServicios.ConsultarTodos();
                this._ventana.Agentes = agentes;
                this._ventana.Agente = this.BuscarAgente(agentes, marca.Agente);

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                this._ventana.PaisesSolicitud = paises;
                this._ventana.PaisSolicitud = this.BuscarPais(paises, marca.Pais);

                IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
                this._ventana.Servicios = servicios;
                //this._ventana.PaisSolicitud = this.BuscarPais(paises, marca.Pais);

                IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                this._ventana.Detalles = tipoEstados;

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                this._ventana.BoletinesOrdenPublicacion = boletines;
                this._ventana.BoletinesPublicacion = boletines;
                this._ventana.BoletinConcesion = boletines;
                this._ventana.BoletinConcesion = this.BuscarBoletin(boletines, marca.BoletinConcesion);
                this._ventana.BoletinPublicacion = this.BuscarBoletin(boletines, marca.BoletinPublicacion);

                IList<Poder> poderes = this._poderServicios.ConsultarTodos();
                this._ventana.PoderesDatos = poderes;
                this._ventana.PoderesSolicitud = poderes;
                this._ventana.PoderDatos = this.BuscarPoder(poderes, marca.Poder);
                this._ventana.PoderSolicitud = this.BuscarPoder(poderes, marca.Poder);

                IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                interesados.Insert(0, primerInteresado);
                this._ventana.InteresadosDatos = interesados;
                this._ventana.InteresadosSolicitud = interesados;
                this._interesados = interesados;
                
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
                    //Anexo anexo = (Anexo)this._ventana.Anexo;

                    //if (this._anexoServicios.InsertarOModificar(anexo, UsuarioLogeado.Hash))
                    //{
                    //    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.AnexoModificado;
                    //    this.Navegar(_paginaPrincipal);
                    //}
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
        /// Metodo que se encarga de eliminar un Anexo
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
    }
}
