using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Threading;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using System.Collections.Generic;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Documents;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorGestionarInteresadosDePatente : PresentadorBase
    {
        private IGestionarInteresadosDePatente _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IInteresadoServicios _interesadoServicios;
        private IInteresadoPatenteServicios _interesadoPatenteServicios;
        private InteresadoPatente _interesadoPatente;
        private object _ventanaGestionarPatente;
        private object _ventanaConsultarPatentes;

        private IList<Interesado> _listaInteresado1 = new List<Interesado>();
        private IList<Interesado> _listaInteresado2 = new List<Interesado>();
        private IList<Interesado> _listaInteresado3 = new List<Interesado>();
               
        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="patente">Patente consultada</param>
        /// <param name="ventanaConsultarPatentes">Ventana ConsultarPatentes</param>
        /// <param name="ventanaGestionarPatente">Ventana GestionarPatente</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarInteresadosDePatente(IGestionarInteresadosDePatente ventana, 
                                                        object patente, 
                                                        object ventanaPadre, 
                                                        object ventanaGestionarPatente, 
                                                        object ventanaConsultarPatentes)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventanaGestionarPatente = ventanaGestionarPatente;
                this._ventanaConsultarPatentes = ventanaConsultarPatentes;
                this._ventana.Patente = (Patente)patente;
                

                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._interesadoPatenteServicios = (IInteresadoPatenteServicios)Activator.GetObject(typeof(IInteresadoPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoPatenteServicios"]);
                                
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
        }


        /// <summary>
        /// Constructor predeterminado que obtiene un interesado de una patente y una patente seleccionada
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="patente">Patente consultada</param>
        /// <param name="interesadoPatente">Interesado de la patente</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        /// <param name="ventanaGestionarPatente">Ventana GestionarPatente</param>
        /// <param name="ventanaConsultarPatentes">Ventana ConsultarPatentes</param>
        /// 
        public PresentadorGestionarInteresadosDePatente (IGestionarInteresadosDePatente ventana, 
                                                         object patente, 
                                                         object interesadoPatente, 
                                                         object ventanaPadre, 
                                                         object ventanaGestionarPatente, 
                                                         object ventanaConsultarPatentes)
            {

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventanaGestionarPatente = ventanaGestionarPatente;
                this._ventanaConsultarPatentes = ventanaConsultarPatentes;
                this._ventana.Patente = (Patente)patente;
                this._interesadoPatente = (InteresadoPatente)interesadoPatente;


                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._interesadoPatenteServicios = (IInteresadoPatenteServicios)Activator.GetObject(typeof(IInteresadoPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoPatenteServicios"]);

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionalInfoBol,
                    Recursos.Ids.AgregarInfoBol);

                if (this._interesadoPatente != null)
                    CargarInteresadosAdicionales();
                else
                {
                    InteresadoPatente interesadoPatente = new InteresadoPatente();
                    interesadoPatente.Id = ((Patente)this._ventana.Patente).Id;
                    interesadoPatente.Interesado = ((Patente)this._ventana.Patente).Interesado;
                    this._interesadoPatente = interesadoPatente;
                    CargarInteresadosAdicionales();
                }
                   
                
                this._ventana.FocoPredeterminado();

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
        /// Metodo que carga los datos de los interesados adicinales en los campos de texto
        /// </summary>
        private void CargarInteresadosAdicionales()
        {

            Interesado primerInteresado1 = new Interesado();
            primerInteresado1.Id = int.MinValue;
            Interesado primerInteresado2 = new Interesado();
            primerInteresado2.Id = int.MinValue;
            Interesado primerInteresado3 = new Interesado();
            primerInteresado3.Id = int.MinValue;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._interesadoPatente.Interesado1 != null)
                {
                    this._ventana.IdInteresado1 = ((Interesado)this._interesadoPatente.Interesado1).Id.ToString();
                    this._ventana.NombreInteresado1 = ((Interesado)this._interesadoPatente.Interesado1).Nombre;
                    this._listaInteresado1.Add(primerInteresado1);
                    this._listaInteresado1.Add(this._interesadoPatente.Interesado1);
                    this._ventana.Interesados1 = this._listaInteresado1;
                    this._ventana.Interesado1 = this.BuscarInteresado(this._listaInteresado1, this._interesadoPatente.Interesado1);
                }
                else
                {
                    this._listaInteresado1.Add(primerInteresado1);
                    this._ventana.Interesados1 = this._listaInteresado1;
                }

                if (this._interesadoPatente.Interesado2 != null)
                {
                    this._ventana.IdInteresado2 = ((Interesado)this._interesadoPatente.Interesado2).Id.ToString();
                    this._ventana.NombreInteresado2 = ((Interesado)this._interesadoPatente.Interesado2).Nombre;
                    this._listaInteresado2.Add(primerInteresado2);
                    this._listaInteresado2.Add(this._interesadoPatente.Interesado2);
                    this._ventana.Interesados2 = this._listaInteresado2;
                    this._ventana.Interesado2 = this.BuscarInteresado(this._listaInteresado2, this._interesadoPatente.Interesado2);
                }
                else
                {
                    this._listaInteresado2.Add(primerInteresado2);
                    this._ventana.Interesados2 = this._listaInteresado2;
                }

                if (this._interesadoPatente.Interesado3 != null)
                {
                    this._ventana.IdInteresado3 = ((Interesado)this._interesadoPatente.Interesado3).Id.ToString();
                    this._ventana.NombreInteresado3 = ((Interesado)this._interesadoPatente.Interesado3).Nombre;
                    this._listaInteresado3.Add(primerInteresado3);
                    this._listaInteresado3.Add(this._interesadoPatente.Interesado3);
                    this._ventana.Interesados3 = this._listaInteresado3;
                    this._ventana.Interesado3 = this.BuscarInteresado(this._listaInteresado3, this._interesadoPatente.Interesado3);
                }
                else
                {
                    this._listaInteresado3.Add(primerInteresado3);
                    this._ventana.Interesados3 = this._listaInteresado3;
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
        /// Metodo que busca un interesado adicional de una patente especifica segun el boton presionado
        /// </summary>
        /// <param name="nombreBotonConsulta">Boton presionado para la busqueda</param>
        public void BuscarInteresadoAdicional(string nombreBotonConsulta)
        {

            Interesado interesadoABuscar = new Interesado();
            IList<Interesado> interesadosFiltrados = new List<Interesado>();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                
                if (nombreBotonConsulta.Equals("_btnConsultarInteresado1"))
                {
                    interesadoABuscar.Id = this._ventana.IdInteresado1Filtrar.Equals("") ? int.MinValue :
                    int.Parse(this._ventana.IdInteresado1Filtrar);
                    interesadoABuscar.Nombre = this._ventana.NombreInteresado1Filtrar.Equals("") ? "" :
                        this._ventana.NombreInteresado1Filtrar.ToUpper();
                }
                else if (nombreBotonConsulta.Equals("_btnConsultarInteresado2"))
                {
                    interesadoABuscar.Id = this._ventana.IdInteresado2Filtrar.Equals("") ? int.MinValue :
                    int.Parse(this._ventana.IdInteresado2Filtrar);
                    interesadoABuscar.Nombre = this._ventana.NombreInteresado2Filtrar.Equals("") ? "" :
                        this._ventana.NombreInteresado2Filtrar.ToUpper();
                }
                else if (nombreBotonConsulta.Equals("_btnConsultarInteresado3"))
                {
                    interesadoABuscar.Id = this._ventana.IdInteresado3Filtrar.Equals("") ? int.MinValue :
                    int.Parse(this._ventana.IdInteresado3Filtrar);
                    interesadoABuscar.Nombre = this._ventana.NombreInteresado3Filtrar.Equals("") ? "" :
                        this._ventana.NombreInteresado3Filtrar.ToUpper();
                }


                if ((interesadoABuscar.Id != int.MinValue) || (!interesadoABuscar.Nombre.Equals("")))
                {
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                }

                if (interesadosFiltrados.Count > 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                }
                else
                    interesadosFiltrados.Add(primerInteresado);

                if (nombreBotonConsulta.Equals("_btnConsultarInteresado1"))
                {
                    this._ventana.Interesados1 = interesadosFiltrados;
                }
                else if (nombreBotonConsulta.Equals("_btnConsultarInteresado2"))
                {
                    this._ventana.Interesados2 = interesadosFiltrados;
                }
                else if (nombreBotonConsulta.Equals("_btnConsultarInteresado3"))
                {
                    this._ventana.Interesados3 = interesadosFiltrados;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que cambia el Interesado1 de los Interesados Adicionales asociados a una Patente
        /// </summary>
        public void CambiarInteresado(String nombreListView)
        {

            Interesado interesadoAux;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((nombreListView.Equals("_lstInteresados1")) && (this._ventana.Interesado1 != null))
                {
                    interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.Interesado1);
                    if (interesadoAux != null)
                    {
                        this._ventana.IdInteresado1 = interesadoAux.Id.ToString();
                        this._ventana.NombreInteresado1 = interesadoAux.Nombre;
                    }
                    else
                    {
                        this._ventana.IdInteresado1 = String.Empty;
                        this._ventana.NombreInteresado1 = String.Empty;
                    }

                    this._interesadoPatente.Interesado1 = interesadoAux;
                }
                else if ((nombreListView.Equals("_lstInteresados2")) && (this._ventana.Interesado2 != null))
                {
                    interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.Interesado2);
                    if (interesadoAux != null)
                    {
                        this._ventana.IdInteresado2 = interesadoAux.Id.ToString();
                        this._ventana.NombreInteresado2 = interesadoAux.Nombre;
                    }
                    else
                    {
                        this._ventana.IdInteresado2 = String.Empty;
                        this._ventana.NombreInteresado2 = String.Empty;
                    }

                    this._interesadoPatente.Interesado2 = interesadoAux;
                }
                else if ((nombreListView.Equals("_lstInteresados3")) && (this._ventana.Interesado3 != null))
                {
                    interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.Interesado3);
                    if (interesadoAux != null)
                    {
                        this._ventana.IdInteresado3 = interesadoAux.Id.ToString();
                        this._ventana.NombreInteresado3 = interesadoAux.Nombre;
                    }
                    else
                    {
                        this._ventana.IdInteresado3 = String.Empty;
                        this._ventana.NombreInteresado3 = String.Empty;
                    }

                    this._interesadoPatente.Interesado3 = interesadoAux;

                }

                this._ventana.ConvertirEnteroMinimoABlanco();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que actualiza los interesados asociados a una patente
        /// </summary>
        /// <returns></returns>
        public bool Aceptar()
        {

            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                InteresadoPatente interesadoPatenteAux = new InteresadoPatente();

                interesadoPatenteAux = this._interesadoPatente;

                exitoso = this._interesadoPatenteServicios.InsertarOModificar(interesadoPatenteAux, UsuarioLogeado.Hash);

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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }

            return exitoso;
        }

        public void irListaInteresadosPatente()
        {
            this.Navegar(new ListaInteresadosPatente(this._ventana.Patente, this._ventanaGestionarPatente, this._ventanaConsultarPatentes));
        }
    }
}
