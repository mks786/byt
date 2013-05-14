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
using Trascend.Bolet.Cliente.Ventanas.Cartas;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarIntruccionRenovacion : PresentadorBase
    {

        private IGestionarInstruccionDeRenovacion _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IInstruccionDeRenovacionServicios _instruccionDeRenovacionServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private ICartaServicios _cartaServicios;


        private bool _nuevaBusqueda = false;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorGestionarIntruccionRenovacion(IGestionarInstruccionDeRenovacion ventana, object instruccion)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                //this._ventana.InstruccionDeRenovacion = null != (InstruccionDeRenovacion)instruccion ? (InstruccionDeRenovacion)instruccion : new InstruccionDeRenovacion();
                //if (null != (InstruccionDeRenovacion)instruccion)
                if (((InstruccionDeRenovacion)instruccion).Carta != null)
                {
                    this._ventana.InstruccionDeRenovacion = (InstruccionDeRenovacion)instruccion;
                }
                else
                {
                    InstruccionDeRenovacion instruccionAux = new InstruccionDeRenovacion();
                    instruccionAux.Fecha = System.DateTime.Now;
                    instruccionAux.Marca = ((InstruccionDeRenovacion)instruccion).Marca;
                    this._ventana.InstruccionDeRenovacion = instruccionAux;
                }
                if (((InstruccionDeRenovacion)instruccion).Carta == null)
                    this._nuevaBusqueda = true;

                this._instruccionDeRenovacionServicios = (IInstruccionDeRenovacionServicios)Activator.GetObject(typeof(IInstruccionDeRenovacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InstruccionDeRenovacionServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);

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
                    ((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion).Fecha = System.DateTime.Now;
                }

                //IList<ListaDatosDominio> tiposBusqueda = this._listaDatosDominioServicios.
                //ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoBusqueda));
                //ListaDatosDominio primerTipoBusqueda = new ListaDatosDominio();
                //primerTipoBusqueda.Id = "NGN";
                //tiposBusqueda.Insert(0, primerTipoBusqueda);
                //this._ventana.TiposBusqueda = tiposBusqueda;

                //if (!this._nuevaBusqueda)
                //    this._ventana.TipoBusqueda = this.BuscarTipoBusqueda(((Busqueda)this._ventana.Busqueda).TipoBusqueda, tiposBusqueda);

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
            Carta carta = null;
            bool flag = false; // Bandera para indicar si la carta existe
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
                    if((null != this._ventana.IdCorrespondencia) && (!this._ventana.IdCorrespondencia.Equals("")))
                        carta = new Carta(int.Parse(this._ventana.IdCorrespondencia));

                    if (null != carta)
                        flag = this._cartaServicios.VerificarExistencia(carta);

                    if(flag)
                    //if (this._cartaServicios.VerificarExistencia(carta))
                    {
                        InstruccionDeRenovacion instruccion = (InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion;

                        //busqueda.TipoBusqueda = ((ListaDatosDominio)this._ventana.TipoBusqueda).Id != "NGN" ? ((ListaDatosDominio)this._ventana.TipoBusqueda).Id[0] : (char?)null;

                        instruccion.Carta = this._cartaServicios.ObtenerCartasFiltro(carta)[0];
                        exitoso = this._instruccionDeRenovacionServicios.InsertarOModificar(instruccion, UsuarioLogeado.Hash);
                        if (this._nuevaBusqueda)
                            ((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion).Marca.InstruccionesDeRenovacion.Add(instruccion);
                    }
                    else
                    {
                        InstruccionDeRenovacion instruccion = (InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion;
                        exitoso = this._instruccionDeRenovacionServicios.InsertarOModificar(instruccion, UsuarioLogeado.Hash);
                        if (this._nuevaBusqueda)
                            ((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion).Marca.InstruccionesDeRenovacion.Add(instruccion);
                        //this._ventana.Alerta();
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

                ((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion).Marca.InstruccionesDeRenovacion.Remove((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion);

                if (this._instruccionDeRenovacionServicios.Eliminar((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion, UsuarioLogeado.Hash))
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
        public void IrListaInstruccionesDeRenovacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaInstruccionesRenovacion(((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion).Marca));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        public void ConsultarCarta()
        {
            Carta carta = new Carta();
            carta.Id = ((InstruccionDeRenovacion)this._ventana.InstruccionDeRenovacion).Carta.Id;
            IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(carta);
            Navegar(new ConsultarCarta(cartas[0], this._ventana));
        }

    }
}
