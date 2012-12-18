using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.TiposEmailAsociado;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.TiposEmailAsociado
{
    class PresentadorGestionarTipoEmailAsociado : PresentadorBase
    {

        private IGestionarTipoEmailAsociado _ventana;
        private ITipoEmailAsociadoServicios _tipoEmailAsociadoServicios;
        private IDepartamentoServicios _departamentoServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _agregar;
        private IList<Auditoria> _auditorias;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="tipoEmail">Pais a mostrar</param>
        public PresentadorGestionarTipoEmailAsociado(IGestionarTipoEmailAsociado ventana, object tipoEmail, object ventanaPadre)
        {
            try
            {
                _agregar = tipoEmail != null ? false : true;
                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventana.HabilitarCampos = true;

                if (_agregar)
                {
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }
                else
                {
                    this._ventana.TipoEmailAsociado = tipoEmail;
                }

                this._tipoEmailAsociadoServicios = (ITipoEmailAsociadoServicios)Activator.GetObject(typeof(ITipoEmailAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEmailAsociadoServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
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

                if (_agregar)
                {
                    this._ventana.TipoEmailAsociado = new TipoEmailAsociado();


                    this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarTipoEmail,
                        string.Empty);
                }
                else
                {

                    this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarTipoEmail,
                        string.Empty);
                    Auditoria auditoria = new Auditoria();
                    auditoria.Fk = int.Parse(((TipoEmailAsociado)this._ventana.TipoEmailAsociado).Id);
                    auditoria.Tabla = "FAC_ASO_TIPO_COR";
                    _auditorias = this._tipoEmailAsociadoServicios.AuditoriaPorFkyTabla(auditoria);


                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;

                    if (_auditorias.Count > 0)
                        this._ventana.PintarAuditoria();
                }

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primerDepartamento = new Departamento();
                primerDepartamento.Id = "NGN";
                departamentos.Insert(0, primerDepartamento);
                this._ventana.Departamentos = departamentos;
                if (!_agregar)
                    this._ventana.Departamento = this.BuscarDepartamento(departamentos, ((TipoEmailAsociado)this._ventana.TipoEmailAsociado).Departamento);

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
        /// modifica los datos del País
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
                else
                {
                    bool noExiste = false;

                    TipoEmailAsociado tipoEmail = (TipoEmailAsociado)this._ventana.TipoEmailAsociado;

                    tipoEmail.Departamento = ((Departamento)this._ventana.Departamento);

                    ((TipoEmailAsociado)this._ventana.TipoEmailAsociado).Operacion = _agregar ? "INSERT" : "UPDATE";

                    if (((_agregar) && !this._tipoEmailAsociadoServicios.VerificarExistencia((TipoEmailAsociado)this._ventana.TipoEmailAsociado)) || (!_agregar))
                    {
                        if (this._tipoEmailAsociadoServicios.InsertarOModificar(tipoEmail, UsuarioLogeado.Hash))
                        {
                            string mensaje = _agregar ? Recursos.MensajesConElUsuario.TipoEmailInsertado : Recursos.MensajesConElUsuario.TipoEmailModificado;
                            _paginaPrincipal.MensajeUsuario = mensaje;
                            this.Navegar(_paginaPrincipal);
                        }
                    }
                    else
                    {
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.IdRepetido);
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
        }


        /// <summary>
        /// Método que se encarga de eliminar un País
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((TipoEmailAsociado)this._ventana.TipoEmailAsociado).Operacion = "DELETE";
                if (this._tipoEmailAsociadoServicios.Eliminar((TipoEmailAsociado)this._ventana.TipoEmailAsociado, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.TipoEmailEliminado;
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
        }


        /// <summary>
        /// Método que muestra la ventana de Auditoría de un Asociado
        /// </summary>
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

    }
}
