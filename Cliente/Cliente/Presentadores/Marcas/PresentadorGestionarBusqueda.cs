using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Threading;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarBusqueda : PresentadorBase
    {

        private IGestionarBusqueda _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IBusquedaServicios _busquedaServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private bool _nuevaBusqueda = false;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorGestionarBusqueda(IGestionarBusqueda ventana, object busqueda)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventana.Busqueda = null != (Busqueda)busqueda ? (Busqueda)busqueda : new Busqueda();

                if (((Busqueda)busqueda).Id == int.MinValue)
                    this._nuevaBusqueda = true;

                this._busquedaServicios = (IBusquedaServicios)Activator.GetObject(typeof(IBusquedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BusquedaServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);

                //IList<TipoInfobol> infoboles = this._tipoInfobolServicios.ConsultarTodos();
                //this._ventana.Tipos = null;
                //this._ventana.Tipos = infoboles;
                //this._ventana.Tipo = this.BuscarTipoInfobol(infoboles, ((InfoBol)this._ventana.InfoBol).TipoInfobol);

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

                if (this._nuevaBusqueda)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                    this._ventana.OcultarControlesAlAgregar();
                    this._ventana.BorrarValorMinimo();
                }

                IList<ListaDatosDominio> tiposBusqueda = this._listaDatosDominioServicios.
                ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoBusqueda));
                ListaDatosDominio primerTipoBusqueda = new ListaDatosDominio();
                primerTipoBusqueda.Id = "NGN";
                tiposBusqueda.Insert(0, primerTipoBusqueda);
                this._ventana.TiposBusqueda = tiposBusqueda;

                if (!this._nuevaBusqueda)
                    this._ventana.TipoBusqueda = this.BuscarTipoBusqueda(((Busqueda)this._ventana.Busqueda).TipoBusqueda, tiposBusqueda);

                //IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                //Boletin primerBoletin = new Boletin();
                //primerBoletin.Id = int.MinValue;
                //boletines.Insert(0, primerBoletin);
                //this._ventana.Boletines = null;
                //this._ventana.Boletines = boletines;

                //if(!this._nuevaInfoBol)
                //    this._ventana.Boletin = this.BuscarBoletin(boletines, ((InfoBol)this._ventana.InfoBol).Boletin);


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
        /// Método que realiza toda la lógica para agregar la Busqueda dentro de la base de datos
        /// </summary>
        public bool Aceptar()
        {
            bool exitoso = false;
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

                //Agregar o modificar datos
                else
                {
                    Busqueda busqueda = (Busqueda)this._ventana.Busqueda;

                    busqueda.TipoBusqueda = ((ListaDatosDominio)this._ventana.TipoBusqueda).Id != "NGN" ? ((ListaDatosDominio)this._ventana.TipoBusqueda).Id[0] : (char?)null;

                    exitoso = this._busquedaServicios.InsertarOModificar(busqueda, UsuarioLogeado.Hash);
                    if (this._nuevaBusqueda)
                        ((Busqueda)this._ventana.Busqueda).Marca.Busquedas.Add(busqueda);

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

            return exitoso;
        }


        /// <summary>
        /// Método que se encarga de eliminar una Busqueda
        /// </summary>
        /// <returns></returns>
        public bool Eliminar()
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((Busqueda)this._ventana.Busqueda).Marca.Busquedas.Remove((Busqueda)this._ventana.Busqueda);

                if (this._busquedaServicios.Eliminar((Busqueda)this._ventana.Busqueda, UsuarioLogeado.Hash))
                {
                    exitoso = true;
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

            return exitoso;
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de lista de Busqueda
        /// </summary>
        public void irListaBusqueda()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaBusquedas(((Busqueda)this._ventana.Busqueda).Marca));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

    }
}
