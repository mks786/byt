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
        private IGestionarInfoBol _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IInfoBolServicios _infoBolServicios;
        private IBoletinServicios _boletinServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private ITipoInfobolServicios _tipoInfobolServicios;
        private bool _nuevaInfoBol = false;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorGestionarBusqueda(IGestionarInfoBol ventana, object infoBol)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.InfoBol = null != (InfoBol)infoBol ? (InfoBol)infoBol : new InfoBol();

                if (((InfoBol)infoBol).Id == int.MinValue)
                    this._nuevaInfoBol = true;

                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._tipoInfobolServicios = (ITipoInfobolServicios)Activator.GetObject(typeof(ITipoInfobolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoInfobolServicios"]);

                IList<TipoInfobol> infoboles = this._tipoInfobolServicios.ConsultarTodos();
                this._ventana.Tipos = null;
                this._ventana.Tipos = infoboles;
                this._ventana.Tipo = this.BuscarTipoInfobol(infoboles, ((InfoBol)this._ventana.InfoBol).TipoInfobol);

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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionalInfoBol,
                    Recursos.Ids.AgregarInfoBol);

                if (this._nuevaInfoBol)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                    this._ventana.OculatarControlesAlAgregar();
                }

                IList<ListaDatosDominio> tomos = this._listaDatosDominioServicios.
                ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTomo));
                ListaDatosDominio primerTomo = new ListaDatosDominio();
                primerTomo.Id = "NGN";
                tomos.Insert(0, primerTomo);
                this._ventana.Tomos = null;
                this._ventana.Tomos = tomos;

                if (!this._nuevaInfoBol)
                    this._ventana.Tomo = this.BuscarTomos(tomos, ((InfoBol)this._ventana.InfoBol).Tomo);

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.Boletines = null;
                this._ventana.Boletines = boletines;

                if(!this._nuevaInfoBol)
                    this._ventana.Boletin = this.BuscarBoletin(boletines, ((InfoBol)this._ventana.InfoBol).Boletin);


                this._ventana.FocoPredeterminado();
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
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public bool Aceptar()
        {
            bool exitoso = false;
            try
            {

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Agregar o modificar datos
                else
                {
                    InfoBol infoBol = (InfoBol)this._ventana.InfoBol;

                    infoBol.Tomo = ((ListaDatosDominio)this._ventana.Tomo).Id;
                    infoBol.Boletin = (Boletin)this._ventana.Boletin;
                    infoBol.TimeStamp = System.DateTime.Now;
                    infoBol.Usuario = UsuarioLogeado;

                    if (this._nuevaInfoBol)
                    {
                        infoBol.TipoInfobol = (TipoInfobol)this._ventana.Tipo;
                        ((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Add(infoBol);
                    }

                    exitoso = this._infoBolServicios.InsertarOModificar(infoBol, UsuarioLogeado.Hash);

                }
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

        public bool Eliminar()
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Remove((InfoBol)this._ventana.InfoBol);

                if (this._infoBolServicios.Eliminar((InfoBol)this._ventana.InfoBol, UsuarioLogeado.Hash))
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

        public void irListaInfoBol()
        {
            this.Navegar(new ListaInfoBoles(((InfoBol)this._ventana.InfoBol).Marca));
        }
    }
}
