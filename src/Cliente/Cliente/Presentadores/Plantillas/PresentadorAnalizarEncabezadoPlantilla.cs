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
    class PresentadorAnalizarEncabezadoPlantilla : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IAnalizarEncabezadoPlantilla _ventana;
        private IFiltroPlantillaServicios _filtroPlantillaServicios;
        private EncabezadoPlantilla _encabezado;
        private string _comandoSQL;
        private string _comandoBat;



        /// <summary>
        /// Constructor por defecto que recibe como parametro la ventana actual 
        /// </summary>
        /// <param name="ventana">Ventana actual IAnalizarEncabezadoPlantilla</param>
        public PresentadorAnalizarEncabezadoPlantilla(IAnalizarEncabezadoPlantilla ventana, object encabezado, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                this._encabezado = (EncabezadoPlantilla)encabezado;
                this._ventana.EncabezadoPlantilla = this._encabezado;
                
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
        /// Metodo que se encarga de cargar los valores en la pagina y mostrarlos al usuario
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

                EncabezadoPlantilla encabezado = (EncabezadoPlantilla)this._ventana.EncabezadoPlantilla;

                 

                IList<FiltroPlantilla> listaDeFiltros = this._filtroPlantillaServicios.ObtenerFiltrosEncabezadoPlantilla(((EncabezadoPlantilla)this._ventana.EncabezadoPlantilla).MaestroDePlantilla);

                if (listaDeFiltros.Count > 0)
                {
                    ((EncabezadoPlantilla)this._ventana.EncabezadoPlantilla).VariablesEncabezado = listaDeFiltros;
                    this._ventana.Variables = ((EncabezadoPlantilla)this._ventana.EncabezadoPlantilla).VariablesEncabezado;
                }

                else
                {
                    this._ventana.DesactivarListaVariables();
                }
                               

                EscribirComando(encabezado);

                if (encabezado.BatPlantilla != null)
                    EscribirComandoBat(encabezado);
                else
                    this._ventana.GestionarVisibilidadBat();

                if (!UsuarioLogeado.Rol.Id.Equals("ADMINISTRADOR"))
                {
                    this._ventana.AplicarVisibilidadControles();
                }

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
        /// Metodo que escribe la cabecera del comando en el campo de texto para que el usuario ejecute el mismo. 
        /// </summary>
        /// <param name="encabezado">Encabezado de la plantilla</param>
        public void EscribirComando(EncabezadoPlantilla encabezado)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String comandoSQL = String.Empty;
                StringBuilder sb = new StringBuilder();
                String comandoSQLPlus = String.Empty;

                String in_database = ConfigurationManager.AppSettings["NombreBaseDatosMaestroPlantillas"];
                String comandoConsola = ConfigurationManager.AppSettings["ComandoTestMaestroPlantilla"];
                String usuario_password = ConfigurationManager.AppSettings["UserPasswordMaestroPlantilla"];

                sb.Append(comandoConsola + " ");
                sb.Append(usuario_password);
                sb.Append("@" + in_database + " @");
                sb.Append(encabezado.RutaEncabezado + " ");

                if (encabezado.VariablesEncabezado != null)
                {
                    IList<FiltroPlantilla> listaFiltrosEncabezado = encabezado.VariablesEncabezado;
                    foreach (FiltroPlantilla variable in listaFiltrosEncabezado)
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
        /// Metodo para escribir el comando predeterminado para ejecutar el comando Bat del Encabezado
        /// </summary>
        /// <param name="encabezado">Encabezado de la plantilla</param>
        public void EscribirComandoBat(EncabezadoPlantilla encabezado)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                StringBuilder sb = new StringBuilder();
                String comandoBatConsola = String.Empty;
                BatPlantilla batEncabezdo = encabezado.BatPlantilla;

                sb.Append(batEncabezdo.RutaBat + " ");

                if (encabezado.VariablesEncabezado != null)
                {
                    IList<FiltroPlantilla> listaFiltrosEncabezado = encabezado.VariablesEncabezado;
                    foreach (FiltroPlantilla variable in listaFiltrosEncabezado)
                    {
                        if(!variable.AplicaBAT.Equals("NO"))
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
        /// Metodo que ejecuta el comando Sqlplus que se encuentra en la ventana 
        /// </summary>
        public void EjecutarEncabezadoPlantilla()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<String> valoresVariables = new List<String>();
                StringBuilder sb = new StringBuilder();
                String in_database = ConfigurationManager.AppSettings["NombreBaseDatosMaestroPlantillas"];
                String comandoConsola = ConfigurationManager.AppSettings["ComandoTestMaestroPlantilla"];
                String usuario_password = ConfigurationManager.AppSettings["UserPasswordMaestroPlantilla"];
                String comandoSQLPlus = String.Empty;
                String comandoCompleto = String.Empty, comandoConsola1 = String.Empty, argumentos = String.Empty;
                String valorVariable = null;
                EncabezadoPlantilla encabezado = (EncabezadoPlantilla)this._ventana.EncabezadoPlantilla;
                IList<FiltroPlantilla> filtrosEncabezado = (IList<FiltroPlantilla>)this._ventana.Variables;

                GuardarValoresFiltro(filtrosEncabezado);
                
                sb.Append(comandoConsola + " ");
                sb.Append(usuario_password);
                sb.Append("@" + in_database + " @");
                sb.Append(encabezado.RutaEncabezado + " ");
                

                
                foreach (FiltroPlantilla filtro in filtrosEncabezado)
                {
                    valorVariable = filtro.ValorFiltro;
                    sb.Append(valorVariable + " ");

                }

                

                //comandoCompleto = this._ventana.ComandoSQLPlus;
                comandoCompleto = sb.ToString();
                comandoConsola1 = comandoCompleto.Substring(0, 7);
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
        /// Metodo que permite guardar los valores dados al filtro antes de ejecutar el SQL y/o el BAT
        /// </summary>
        /// <param name="filtrosEncabezado"></param>
        private void GuardarValoresFiltro(IList<FiltroPlantilla> filtrosEncabezado)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool retorno = false;

                foreach (FiltroPlantilla filtro in filtrosEncabezado)
                {
                    retorno = this._filtroPlantillaServicios.InsertarOModificar(filtro, UsuarioLogeado.Hash);                   
                }

                if (retorno)
                    this._ventana.MensajeAlerta("Filtros actualizados satisfactoriamente", 2);

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
        /// Metodo que presenta el contenido del archivo .sql en una ventana de notepad 
        /// </summary>
        public void VerScript()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                EncabezadoPlantilla encabezado = (EncabezadoPlantilla)this._ventana.EncabezadoPlantilla;

                String rutaEncabezadoSQL = encabezado.RutaEncabezado;

                ProcessStartInfo psi = new ProcessStartInfo("notepad", rutaEncabezadoSQL);
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


        /// <summary>
        /// Metodo para ejecutar el BAT asociado con el Encabezado. Se usan las mismas variables que el SQL Encabezado
        /// </summary>
        public void EjecutarBatEncabezadoPlantilla()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                StringBuilder sb = new StringBuilder();
                String comandoBatConsola = String.Empty;
                String comando = String.Empty, parametros = String.Empty;
                String[] _comandoBatPorPartes = null;
                int contador = 1;


                EncabezadoPlantilla encabezado = (EncabezadoPlantilla)this._ventana.EncabezadoPlantilla;
                IList<FiltroPlantilla> filtrosEncabezado = (IList<FiltroPlantilla>)this._ventana.Variables;
                encabezado.VariablesEncabezado = filtrosEncabezado;

                GuardarValoresFiltro(filtrosEncabezado);

                BatPlantilla batEncabezdo = encabezado.BatPlantilla;

                sb.Append(batEncabezdo.RutaBat + " ");

                if (encabezado.VariablesEncabezado != null)
                {
                    IList<FiltroPlantilla> listaFiltrosEncabezado = encabezado.VariablesEncabezado;
                    foreach (FiltroPlantilla variable in listaFiltrosEncabezado)
                    {
                        if(!variable.AplicaBAT.Equals("NO"))
                            sb.Append(variable.ValorFiltro + " ");
                    }

                }

                
                //comando = this._ventana.ComandoBat;
                comando = sb.ToString();
                _comandoBatPorPartes = comando.Split(' ');

                //EncabezadoPlantilla encabezado = (EncabezadoPlantilla)this._ventana.EncabezadoPlantilla;
                BatPlantilla bat = ((EncabezadoPlantilla)this._ventana.EncabezadoPlantilla).BatPlantilla;

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
