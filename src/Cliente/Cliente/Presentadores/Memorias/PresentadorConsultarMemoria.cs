using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Memorias;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.IO;

namespace Trascend.Bolet.Cliente.Presentadores.Memorias
{
    class PresentadorConsultarMemoria : PresentadorBase
    {

        private IConsultarMemoria _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IMemoriaServicios _memoriaServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;

        private Patente _patente;
        private IList<ListaDatosValores> _tiposMensaje;
        private ListaDatosValores _formatoDocumento;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="memoria">Memoria a mostrar</param>
        public PresentadorConsultarMemoria(IConsultarMemoria ventana, object memoria, object patente)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventana.Memoria = memoria;
                this._patente = (Patente)patente;

                this._memoriaServicios = (IMemoriaServicios)Activator.GetObject(typeof(IMemoriaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MemoriaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMemoria, "");

                IList<ListaDatosValores> formatosDocs = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaFormatoDoc));
                ListaDatosValores primerFormato = new ListaDatosValores();
                primerFormato.Id = "NGN";
                formatosDocs.Insert(0, primerFormato);
                this._ventana.FormatosDocumentos = formatosDocs;
                _formatoDocumento = new ListaDatosValores();
                _formatoDocumento.Valor = ((Memoria)this._ventana.Memoria).TipoDocumento.ToString();
                this._ventana.FormatoDocumento = this.BuscarFormatoDoc(formatosDocs, _formatoDocumento);

                _tiposMensaje = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoMensaje));
                this._ventana.TiposMensajes = _tiposMensaje;
                ListaDatosValores tipoMensaje = new ListaDatosValores();
                tipoMensaje.Valor = ((Memoria)this._ventana.Memoria).TipoMensaje.ToString();
                this._ventana.TipoMensaje = this.BuscarFormatoDoc(_tiposMensaje, tipoMensaje);

                this._ventana.SetPatente = this._patente.Descripcion;


                //this._ventana.TiposMensajes = this._listaDatosValoresServicios.
                //    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoMensaje));

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
                    Memoria memoria = (Memoria)this._ventana.Memoria;

                    ((Memoria)this._ventana.Memoria).TipoDocumento = !((ListaDatosValores)this._ventana.FormatoDocumento).Id.Equals("NGN") ?
                        ((ListaDatosValores)this._ventana.FormatoDocumento).Valor[0] : (char?)null;
                    ((Memoria)this._ventana.Memoria).TipoMensaje = !((ListaDatosValores)this._ventana.TipoMensaje).Id.Equals("NGN") ?
                        int.Parse(((ListaDatosValores)this._ventana.TipoMensaje).Valor) : (int?)null;

                    if (this._memoriaServicios.InsertarOModificar(memoria, UsuarioLogeado.Hash))
                    {
                        this.Navegar(new ListaMemorias(this._patente));
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
        /// Metodo que se encarga de eliminar un Memoria
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._memoriaServicios.Eliminar((Memoria)this._ventana.Memoria, UsuarioLogeado.Hash))
                {
                    this.Navegar(new ListaMemorias(this._patente));
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

        public void VerMemoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                string rutaArchivo = ConfigurationManager.AppSettings["RutaMemoriasDePatentes" + ((Memoria)this._ventana.Memoria).Id]
                                                + this._patente.Id + "." +
                                                ((ListaDatosValores)this._ventana.FormatoDocumento).Descripcion.ToLower();

                if (File.Exists(rutaArchivo))
                {
                    this.AbrirArchivoPorConsola(rutaArchivo, "Abriendo Archivo de Memoria de la Patente: " + this._patente.Id);
                }
                else { this._ventana.ArchivoNoEncontrado(); }

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
            catch (FileNotFoundException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(String.Format(Recursos.MensajesConElUsuario.ErrorArchivoMemoriaNoEncontrado,"Memoria"), true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }
    }
}
