using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.ContactosCxP;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ventanas.Cartas;

namespace Trascend.Bolet.Cliente.Presentadores.ContactosCxP
{
    class PresentadorAgregarContactoCxP : PresentadorBase 
    {
        private IAgregarContactoCxP _ventana;
        private IContactoServicios _contactoServicios;
        private IAsociadoServicios _asociadoServicios;
        private IContactoCxPServicios _contactoCxPServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private Asociado _asociado;
        private bool _nuevoContacto;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Presentador predeterminado que recibe una entidad ContactoCxP y una ventana padre
        /// <param name="ventana">Ventana actual</param>
        /// <param name="contactoCxP">Contacto CxP</param>
        /// <param name="ventanaPadre">Ventana padre</param>
        /// </summary>
        public PresentadorAgregarContactoCxP(IAgregarContactoCxP ventana, object contactoCxP, bool nuevoContacto, object ventanaPadre)
        {
            try
            {
                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventana.ContactoCxP = (ContactoCxP)contactoCxP;
                this._nuevoContacto = nuevoContacto;

                this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);
                this._contactoCxPServicios = (IContactoCxPServicios)Activator.GetObject(typeof(IContactoCxPServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoCxPServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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
            
            ListaDatosValores formaPago = new ListaDatosValores(Recursos.Etiquetas.cbiFacFormasDePagoCxPInt);
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                ContactoCxP contactoCxP = (ContactoCxP)this._ventana.ContactoCxP;

                IList<ListaDatosValores> formasPago = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiFacFormasDePagoCxPInt));
                ListaDatosValores primeraFormaPago = new ListaDatosValores();
                primeraFormaPago.Id = "NGN";
                formasPago.Insert(0, primeraFormaPago);
                this._ventana.FormasDePago = formasPago;

                if (!this._nuevoContacto)
                {
                    switch (contactoCxP.ModoPago)
                    {
                        case "D":
                            formaPago.Valor = "Deposito";
                            break;
                        case "C":
                            formaPago.Valor = "Cheque";
                            break;
                        case "T":
                            formaPago.Valor = "Transferencia";
                            break;
                    }

                    this._ventana.FormaDePago = this.BuscarListaDeDatosValores(formasPago, formaPago);
                }
                else
                {
                    this._ventana.HabilitarBotonEliminar(false);
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que modifica o inserta un nuevo registro
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                ContactoCxP contactoCxP = new ContactoCxP();
                contactoCxP = (ContactoCxP)this._ventana.ContactoCxP;

                if (!String.IsNullOrEmpty(contactoCxP.FrecuenciaPago))
                {
                    if (this._ventana.FormaDePago != null)
                    {
                        if (!((ListaDatosValores)this._ventana.FormaDePago).Id.Equals("NGN"))
                        {
                            String tipoFormaPago = ((ListaDatosValores)this._ventana.FormaDePago).Descripcion;

                            switch (tipoFormaPago)
                            {
                                case "Deposito":
                                    contactoCxP.ModoPago = "D";
                                    break;
                                case "Transferencia":
                                    contactoCxP.ModoPago = "T";
                                    break;
                                case "Cheque":
                                    contactoCxP.ModoPago = "C";
                                    break;
                            }

                            bool exitoso = this._contactoCxPServicios.InsertarOModificar(contactoCxP, UsuarioLogeado.Hash);

                            if (exitoso)
                            {
                                this._ventana.Mensaje("Registro de Contacto CxP exitoso", 2);

                                if (this._nuevoContacto)
                                    this._ventana.HabilitarBotonEliminar(true);
                            }

                        }
                        else
                            this._ventana.Mensaje("Debe seleccionar una Forma de Pago para el Contacto", 0);


                    }
                    else
                        this._ventana.Mensaje("Seleccione una Forma de Pago para el Contacto", 0);
                }
                else
                    this._ventana.Mensaje("Indique la Frecuencia de Pago para el Contacto a registrar", 0);




               

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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo para eliminar un ContactoCxP
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                bool exitoso = false;
                ContactoCxP contactoCxP = new ContactoCxP();
                contactoCxP = (ContactoCxP)this._ventana.ContactoCxP;

                exitoso = this._contactoCxPServicios.Eliminar(contactoCxP, UsuarioLogeado.Hash);

                if (exitoso)
                {
                    this._ventana.Mensaje("Contacto CxP borrado", 2);
                    this.RegresarVentanaPadre();
                }
                else
                {
                    this._ventana.Mensaje("Se originó un error al eliminar el Contacto CxP", 0);
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
