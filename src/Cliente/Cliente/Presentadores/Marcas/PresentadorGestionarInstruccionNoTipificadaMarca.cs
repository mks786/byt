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

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarInstruccionNoTipificadaMarca : PresentadorBase
    {
        
        private IGestionarInstruccionNoTipificadaMarca _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IInstruccionOtrosServicios _instruccionOtrosServicios;
        private ICartaServicios _cartaServicios;
        private bool _nuevaInstruccion = false;
        private Marca _marca;
        private object _ventanaPadreListaInstrucciones;


        public PresentadorGestionarInstruccionNoTipificadaMarca(IGestionarInstruccionNoTipificadaMarca ventana, 
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

            this._ventana.InstruccionNoTipificada = (InstruccionOtros)instruccion;

            if (((InstruccionOtros)instruccion).Id == 0)
            {
                this._nuevaInstruccion = true;
            }


            this._instruccionOtrosServicios = (IInstruccionOtrosServicios)Activator.GetObject(typeof(IInstruccionOtrosServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InstruccionOtrosServicios"]);

            this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);


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

                InstruccionOtros instruccionNoTipificada = CargarInstruccionDeLaPantalla();

                if (!instruccionNoTipificada.Descripcion.Equals(""))
                {
                    if (!this._nuevaInstruccion)
                    {
                        exitoso = this._instruccionOtrosServicios.InsertarOModificar(instruccionNoTipificada, UsuarioLogeado.Hash);
                    }
                    else
                    {
                        IList<InstruccionOtros> instruccionesNoTipificadas = this._instruccionOtrosServicios.ConsultarTodos();
                        contador = instruccionesNoTipificadas.Count;
                        nuevoValor = contador + 1;
                        instruccionNoTipificada.Id = nuevoValor;

                        exitoso = this._instruccionOtrosServicios.InsertarOModificar(instruccionNoTipificada, UsuarioLogeado.Hash);
                    }
                }
                else
                    this._ventana.Mensaje("La Instrucción debe poseer una Descripción", 0);

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
        /// Metodo que carga la instruccion no tipificada de la pantalla en una nuevo objeto InstruccionOtros
        /// </summary>
        /// <returns>Instruccion no tipificada para insertar o actualizar</returns>
        private InstruccionOtros CargarInstruccionDeLaPantalla()
        {
            InstruccionOtros instruccion = null;
            bool existeCarta = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instruccion = new InstruccionOtros();

                instruccion = (InstruccionOtros)this._ventana.InstruccionNoTipificada;

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


        public void IrListaInstruccionesNoTipificadas()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ListaInstruccionesNoTipificadasDeMarca(this._marca,this._ventanaPadreListaInstrucciones));

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
