using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarInstruccionDeDescuentoMarca : PresentadorBase
    {
        private IGestionarInstruccionDeDescuentoMarca _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IInstruccionDescuentoServicios _instruccionDescuentoServicios;
        private ICartaServicios _cartaServicios;
        //private IServicioServicios _servicioServicios;
        private IFacServicioServicios _facServicioServicios;

        private bool _nuevaInstruccion = false;
        private Marca _marca;
        private object _ventanaPadreListaInstrucciones;


        public PresentadorGestionarInstruccionDeDescuentoMarca(IGestionarInstruccionDeDescuentoMarca ventana, 
                                                                object instruccion, 
                                                                object marca, 
                                                                object ventanaPadre, 
                                                                object ventanaPadreListaInstrucciones)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._ventanaPadreListaInstrucciones = ventanaPadreListaInstrucciones;
            this._marca = (Marca)marca;

            this._ventana.InstruccionDescuento = (InstruccionDescuento)instruccion;

            if (((InstruccionDescuento)instruccion).Id == 0)
            {
                this._nuevaInstruccion = true;
            }


            this._instruccionDescuentoServicios = (IInstruccionDescuentoServicios)Activator.GetObject(typeof(IInstruccionDescuentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InstruccionDescuentoServicios"]);
            this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
            this._facServicioServicios = (IFacServicioServicios)Activator.GetObject(typeof(IFacServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacServicioServicios"]);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

       
        /// <summary>
        /// Metodo que carga los datos iniciales en la ventana
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarInstruccionNoTipificada,
                    Recursos.Ids.InstruccionNoTipificada);

                IList<FacServicio> servicios = this._facServicioServicios.ConsultarTodos();
                this._ventana.Servicios = servicios;
                if (((InstruccionDescuento)this._ventana.InstruccionDescuento).Servicio != null)
                {
                    this._ventana.Servicio = this.BuscarServicio(servicios, ((InstruccionDescuento)this._ventana.InstruccionDescuento).Servicio);
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que consulta la correspondencia de una Instruccion No Tipificada
        /// </summary>
        public void ConsultarCorrespondencia()
        {

            Carta cartaFiltrar;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.IdCorrespondencia != null) && (!this._ventana.IdCorrespondencia.Equals(String.Empty)))
                {
                    cartaFiltrar = new Carta();
                    cartaFiltrar.Id = int.Parse(this._ventana.IdCorrespondencia);
                    IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(cartaFiltrar);
                    if (cartas.Count > 0)
                    {
                        this.Navegar(new ConsultarCarta(cartas[0], this._ventana));
                    }
                    else
                        this._ventana.Mensaje("El código de Correspondencia no existe", 0);

                }
                else
                {
                    this._ventana.Mensaje("Escriba el código de la Correspondencia a consultar", 0);
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
        /// Metodo para insertar una nueva instruccion no tipificada o para actualizar una existente
        /// </summary>
        /// <returns>True en caso de realizarse con exito; False en caso contrario</returns>
        public bool Aceptar()
        {
            bool exitoso = false;
            int contador = 0, nuevoValor = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                InstruccionDescuento instruccion = CargarInstruccionDeLaPantalla();

                if (instruccion.Servicio != null)
                {
                    if (instruccion.Descuento != 0)
                    {
                        if (instruccion.Correspondencia != null)
                        {
                            if (!this._nuevaInstruccion)
                            {
                                exitoso = this._instruccionDescuentoServicios.InsertarOModificar(instruccion, UsuarioLogeado.Hash);
                            }
                            else
                            {
                                IList<InstruccionDescuento> instruccionesDeDescuento = this._instruccionDescuentoServicios.ConsultarTodos();
                                contador = instruccionesDeDescuento.Count;
                                nuevoValor = contador + 1;
                                instruccion.Id = nuevoValor;

                                exitoso = this._instruccionDescuentoServicios.InsertarOModificar(instruccion, UsuarioLogeado.Hash);
                            }
                        }
                        else this._ventana.Mensaje("La Instrucción de Descuento debe tener una Correspondencia", 0);
                    }
                    else
                        this._ventana.Mensaje("La Instrucción de Descuento debe tener un Descuento", 0);
                }
                else
                    this._ventana.Mensaje("La Instrucción de Descuento debe tener asociado un Servicio", 0);

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
        /// Metodo que carga la instruccion de descuento en un objeto InstruccionDescuento para luego ser actualizado
        /// </summary>
        /// <returns>Instruccion de Descuento de la pantalla con todos sus componentes</returns>
        private InstruccionDescuento CargarInstruccionDeLaPantalla()
        {
            InstruccionDescuento instruccion = null;
            bool existeCarta = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instruccion = new InstruccionDescuento();

                instruccion = (InstruccionDescuento)this._ventana.InstruccionDescuento;

                if ((this._ventana.IdCorrespondencia != null) && (!this._ventana.IdCorrespondencia.Equals("")))
                {
                    Carta carta = new Carta();
                    carta.Id = int.Parse(this._ventana.IdCorrespondencia);
                    existeCarta = this._cartaServicios.VerificarExistencia(carta);
                    if (existeCarta)
                    {
                        IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(carta);
                        instruccion.Correspondencia = cartas[0];
                    }

                }
                else
                    instruccion.Correspondencia = null;

                instruccion.Descuento = !this._ventana.Descuento.Equals("") ? int.Parse(this._ventana.Descuento) : 0;

                instruccion.Servicio = (this._ventana.Servicio != null) ? (FacServicio)this._ventana.Servicio : null;

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

            return instruccion;
        }


        public void IrListaInstruccionesDescuento()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ListaInstruccionesDescuentoMarca(this._marca, this._ventanaPadreListaInstrucciones));

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

    }
}
