using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using Diginsoft.Bolet.ObjetosComunes.Entidades;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Plantillas
{
    class PresentadorAnalizarDetallePlantilla : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IAnalizarDetallePlantilla _ventana;
        private IFiltroPlantillaServicios _filtroPlantillaServicios;
        private DetallePlantilla _detalle;



        /// <summary>
        /// Constructor predeterminado 
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="detalle">Detalle del plantilla seleccionada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorAnalizarDetallePlantilla(IAnalizarDetallePlantilla ventana, object detalle, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                this._detalle = (DetallePlantilla)detalle;
                this._ventana.DetallePlantilla = this._detalle;
                
                this._filtroPlantillaServicios = (IFiltroPlantillaServicios)Activator.GetObject(typeof(IFiltroPlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroPlantillaServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Metodo que carga los datos de la pantalla
        /// </summary>
        public void CargarPagina()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                DetallePlantilla detalle = (DetallePlantilla)this._ventana.DetallePlantilla;


                IList<FiltroPlantilla> listaDeFiltros = this._filtroPlantillaServicios.ObtenerFiltrosDetallePlantilla(((DetallePlantilla)this._ventana.DetallePlantilla).Plantilla);

                if (listaDeFiltros.Count > 0)
                {
                    ((DetallePlantilla)this._ventana.DetallePlantilla).VariablesDetalle = listaDeFiltros;
                    this._ventana.Variables = ((DetallePlantilla)this._ventana.DetallePlantilla).VariablesDetalle;
                }

                else
                {
                    this._ventana.DesactivarListaVariables();
                }


                EscribirComando(detalle);

                if (detalle.BatPlantilla != null)
                    EscribirComandoBat(detalle);
                else
                    this._ventana.GestionarVisibilidadBat();



                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListadoFiltrosEncabezadoPlantilla,
                Recursos.Ids.ListaFiltrosPlantilla);
        }


        /// <summary>
        /// Metodo que escribe en el recuadro de texto el comando a ejecutar con el comando de consola sqlplus
        /// </summary>
        /// <param name="detalle">Detalle de la plantilla seleccionada</param>
        public void EscribirComando(DetallePlantilla detalle)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                StringBuilder sb = new StringBuilder();
                String comandoSQLPlus = String.Empty;

                String in_database = ConfigurationManager.AppSettings["NombreBaseDatosMaestroPlantillas"];
                String comandoConsola = ConfigurationManager.AppSettings["ComandoTestMaestroPlantilla"];
                String usuario_password = ConfigurationManager.AppSettings["UserPasswordMaestroPlantilla"];

                sb.Append(comandoConsola + " ");
                sb.Append(usuario_password);
                sb.Append("@" + in_database + " @");
                sb.Append(detalle.RutaDetalle + " ");

                if (detalle.VariablesDetalle != null)
                {
                    IList<FiltroPlantilla> listaFiltrosDetalle = detalle.VariablesDetalle;
                    foreach (FiltroPlantilla variable in listaFiltrosDetalle)
                    {
                        sb.Append(variable.NombreVariableFiltro + " ");
                    }

                }

                comandoSQLPlus = sb.ToString();

                this._ventana.ComandoSQLPlus = comandoSQLPlus;


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Metodo que escribe en el recuadro de texto el comando a ejecutar el BAT del detalle
        /// </summary>
        /// <param name="detalle">Detalle de la plantilla</param>
        public void EscribirComandoBat(DetallePlantilla detalle)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                StringBuilder sb = new StringBuilder();
                String comandoBatConsola = String.Empty;
                BatPlantilla batEncabezdo = detalle.BatPlantilla;

                sb.Append(batEncabezdo.RutaBat + " ");

                if (detalle.VariablesDetalle != null)
                {
                    IList<FiltroPlantilla> listaFiltrosDetalle = detalle.VariablesDetalle;
                    foreach (FiltroPlantilla variable in listaFiltrosDetalle)
                    {
                        sb.Append(variable.NombreVariableFiltro + " ");
                    }

                }

                this._ventana.ComandoBat = sb.ToString();


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Metodo que ejecuta el comando sqlplus del recuadro de texto del Detalle de la plantilla seleccionada
        /// </summary>
        public void EjecutarDetallePlantilla()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String comandoCompleto = String.Empty, comandoConsola = String.Empty, argumentos = String.Empty;

                comandoCompleto = this._ventana.ComandoSQLPlus;
                comandoConsola = comandoCompleto.Substring(0, 7);
                argumentos = comandoCompleto.Substring(8);


                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = comandoConsola;
                //p.StartInfo.Arguments = string.Format(usuario_password + "@{0} @{1}", in_database, s);
                p.StartInfo.Arguments = argumentos;
                bool started = p.Start();
                string output = p.StandardOutput.ReadToEnd();

                p.WaitForExit();

                if (started)
                    this._ventana.MensajeAlerta("Proceso ejecutado", 2);
                else
                    this._ventana.MensajeAlerta("Ocurrio un error al ejecutar el proceso", 0);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar("Error al momento de ejecutar el SQL del Encabezado: " + ex.Message, true);
            }

        }


        /// <summary>
        /// Metodo que muestra a traves del notepad el codigo del SQL del Detalle
        /// </summary>
        public void VerScript()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                DetallePlantilla detalle = (DetallePlantilla)this._ventana.DetallePlantilla;

                String rutaDetalleSQL = detalle.RutaDetalle;

                ProcessStartInfo psi = new ProcessStartInfo("notepad", rutaDetalleSQL);
                Process p = Process.Start(psi);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
        }



        public void EjecutarBatDetallePlantilla()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String comando = String.Empty, parametros = String.Empty;
                String[] _comandoBatPorPartes = null;
                int contador = 1;

                comando = this._ventana.ComandoBat;
                _comandoBatPorPartes = comando.Split(' ');

                 DetallePlantilla detalle = (DetallePlantilla)this._ventana.DetallePlantilla;
                BatPlantilla bat = ((DetallePlantilla)this._ventana.DetallePlantilla).BatPlantilla;

                String _urlBat = _comandoBatPorPartes[0];

                for (int i = 1; i < _comandoBatPorPartes.Length; i++)
                {
                    parametros += _comandoBatPorPartes[i] + " ";
                }

                this.EjecutarArchivoBAT(_comandoBatPorPartes[0], parametros);

                this._ventana.MensajeAlerta("Archivo BAT de Encabezado ejecutado", 2);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
        }


    }
}
