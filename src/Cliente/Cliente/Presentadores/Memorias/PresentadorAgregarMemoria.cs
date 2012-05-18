using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Memorias;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Memorias;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.Memorias
{
    class PresentadorAgregarMemoria : PresentadorBase
    {
        private IAgregarMemoria _ventana;
        private Patente _patente;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IMemoriaServicios _memoriaServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarMemoria(IAgregarMemoria ventana, object patente)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._patente = (Patente)patente;
                this._memoriaServicios = (IMemoriaServicios)Activator.GetObject(typeof(IMemoriaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MemoriaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

                this._ventana.Memoria = new Memoria();

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarMemoria, "");

                IList<ListaDatosValores> formatosDocs = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaFormatoDoc));
                ListaDatosValores primerFormato = new ListaDatosValores();
                primerFormato.Id = "NGN";
                formatosDocs.Insert(0, primerFormato);
                this._ventana.FormatosDocumentos = formatosDocs;
                ListaDatosValores formatoDocumento = new ListaDatosValores();
                formatoDocumento.Valor = ((Memoria)this._ventana.Memoria).TipoDocumento.ToString();

                IList<ListaDatosValores> tiposMensaje = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoMensaje));
                this._ventana.TiposMensajes = tiposMensaje;

                this._ventana.SetPatente = this._patente.Descripcion;

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
        /// Método que agrega un Memoria
        /// </summary>
        public void AgregarMemoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((Memoria)this._ventana.Memoria).TipoDocumento = !((ListaDatosValores)this._ventana.FormatoDocumento).Id.Equals("NGN") ?
                    ((ListaDatosValores)this._ventana.FormatoDocumento).Valor[0] : (char?)null;
                ((Memoria)this._ventana.Memoria).TipoMensaje = !((ListaDatosValores)this._ventana.TipoMensaje).Id.Equals("NGN") ?
                    int.Parse(((ListaDatosValores)this._ventana.TipoMensaje).Valor) : (int?)null;

                ((Memoria)this._ventana.Memoria).Patente = this._patente;


                if (!this._memoriaServicios.VerificarExistenciaMemoria(this._patente, (Memoria)this._ventana.Memoria))
                {

                    if (this._memoriaServicios.InsertarOModificar((Memoria)this._ventana.Memoria, UsuarioLogeado.Hash))
                    {
                        this.Navegar(new ListaMemorias(this._patente));
                    }
                }
                else
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorMemoriaRepetida);
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
    }
}
