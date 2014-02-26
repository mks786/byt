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
using Microsoft.Office.Interop.Outlook;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Administracion.Gestiones_Automaticas;
using Trascend.Bolet.Cliente.Ventanas.Administracion.Gestiones_Automaticas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;



namespace Trascend.Bolet.Cliente.Presentadores.Administracion.Gestiones_Automaticas
{
    class PresentadorConsultarCorreosParaGestionAutomatica: PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarCorreosParaGestionAutomatica _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IMediosGestionServicios _medioGestionServicios;
        private IConceptoGestionServicios _conceptosGestionServicios;
        private IList<MediosGestion> _listaMediosGestion;
        private IList<ConceptoGestion> _listaConceptosGestion;
        private ICarpetaGestionAutomaticaServicios _carpetaGestionAutomaticaServicios;
        private IFacGestionServicios _facGestionServicios;
        private IList<CarpetaGestionAutomatica> _listaCarpetasOutlookUsuario;
        private IList<Asociado> _asociados; 
        
        

        /// <summary>
        /// Constructor por defecto que recibe una ventana ConsultarCorreosParaGestionAutomatica
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorConsultarCorreosParaGestionAutomatica(IConsultarCorreosParaGestionAutomatica ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._medioGestionServicios = (IMediosGestionServicios)Activator.GetObject(typeof(IMediosGestionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MediosGestionServicios"]);
                this._conceptosGestionServicios = (IConceptoGestionServicios)Activator.GetObject(typeof(IConceptoGestionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ConceptoGestionServicios"]);
                this._carpetaGestionAutomaticaServicios = (ICarpetaGestionAutomaticaServicios)Activator.GetObject(typeof(ICarpetaGestionAutomaticaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CarpetaGestionAutomaticaServicios"]);
                this._facGestionServicios = (IFacGestionServicios)Activator.GetObject(typeof(IFacGestionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacGestionServicios"]);

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

        public void CargarPagina()
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            ActualizarTitulo();

            CargarMediosGestion();
            CargarConceptosGestion();
            CargarUsuarioLogueado();
            CargarCarpetasOutlookDeUsuario();
            CargarComboUsuarios();

            this._ventana.FocoPredeterminado();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion


        }

        private void CargarComboUsuarios()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Usuario> _usuarios = this._usuarioServicios.ConsultarTodos();
                IList<Usuario> _usuariosDepurados = this.FiltrarUsuariosRepetidos(_usuarios);
                this._ventana.Usuarios = _usuariosDepurados;
                this._ventana.Usuario = this.BuscarUsuarioPorIniciales(_usuariosDepurados, UsuarioLogeado);

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGenerarGestionAutomatica,
                Recursos.Ids.fac_GestionAutomatica);
        }

        /// <summary>
        /// Metodo que carga los medios de gestion de cobranza en la pantalla. Por defecto se coloca el medio Email
        /// </summary>
        public void CargarMediosGestion()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                _listaMediosGestion = this._medioGestionServicios.ConsultarTodos();
                this._ventana.Medios = _listaMediosGestion;
                this._ventana.Medio = this.BuscarMediosGestion(_listaMediosGestion, _listaMediosGestion[1]);

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
        /// Metodo que carga los conceptos de gestion de cobranza
        /// </summary>
        public void CargarConceptosGestion()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ConceptoGestion primerConcepto = new ConceptoGestion();
                primerConcepto.Id = "NGN";
                _listaConceptosGestion = this._conceptosGestionServicios.ConsultarTodos();
                _listaConceptosGestion.Insert(0, primerConcepto);
                this._ventana.Conceptos = _listaConceptosGestion;
                this._ventana.Concepto = this.BuscarConceptoGestion(_listaConceptosGestion, _listaConceptosGestion[0]);

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
        /// Metodo que muestra el nombre y las iniciales del usuario logueado en la ventana
        /// </summary>
        public void CargarUsuarioLogueado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana.IdentificacionDeUsuario = UsuarioLogeado.Iniciales.Trim() + " - " + UsuarioLogeado.NombreCompleto.Trim();
                
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
        /// Metodo que carga las carpetas de correo de outlook de un usuario logueado especifico
        /// </summary>
        public void CargarCarpetasOutlookDeUsuario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                _listaCarpetasOutlookUsuario = this._carpetaGestionAutomaticaServicios.ObtenerCarpetasPorIniciales(UsuarioLogeado);
                //CarpetaGestionAutomatica primeraCarpeta = new CarpetaGestionAutomatica();
                //primeraCarpeta.Id = "NGN";
                //_listaCarpetasOutlookUsuario.Insert(0,primeraCarpeta);
                this._ventana.Carpetas = _listaCarpetasOutlookUsuario;
                                
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
        /// Metodo que verifica si Outlook esta activo antes de descargar los correos
        /// </summary>
        /// <returns></returns>
        public bool VerificarOutlookActivo()
        {
            bool retorno = false;

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                System.Diagnostics.Process[] procesos = System.Diagnostics.Process.GetProcesses();

                foreach (System.Diagnostics.Process proceso in procesos)
                {
                    if (proceso.ProcessName == "OUTLOOK")
                    {
                        retorno = true;
                        break;
                    }

                }

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

            return retorno;
        }


        
        /// <summary>
        /// Metodo que carga los correos de una carpeta de Outlook determinada. 
        /// NOTA: El usuario debe tener permisologia para poder ver la carpeta seleccionada desde Outlook
        /// </summary>
        public void CargarCorreosOutlook()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            String carpetaUsuarioLogueado = String.Empty;
            bool outlookActivo = false;
            int contador = 1;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                outlookActivo = VerificarOutlookActivo();




                if (outlookActivo)
                {
                    Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
                    NameSpace outlookNS = null;
                    MAPIFolder mails = null;
                    outlookNS = outlookApp.GetNamespace("MAPI");
                    mails = outlookNS.GetDefaultFolder(OlDefaultFolders.olPublicFoldersAllPublicFolders);

                    if ((this._ventana.Carpeta != null) && (!((CarpetaGestionAutomatica)this._ventana.Carpeta).Id.Equals("NGN")))
                    {
                        carpetaUsuarioLogueado = ((CarpetaGestionAutomatica)this._ventana.Carpeta).Carpeta;
                        outlookApp.ActiveExplorer().CurrentFolder = mails.Folders[carpetaUsuarioLogueado];

                        Items Correos = outlookApp.ActiveExplorer().CurrentFolder.Items.Restrict("[MessageClass]='IPM.Note'");

                        int numCorreos = Correos.Count;

                        IList<CorreoOutlook> listaCorreosOutlook = new List<CorreoOutlook>();



                        #region Codigo comentado
                        //DataTable mitabla = new DataTable();
                        //mitabla.Columns.Add("Fecha");
                        //mitabla.Columns.Add("Subject");
                        //mitabla.Columns.Add("Remite"); 
                        #endregion

                        //foreach (object obj in outlookApp.ActiveExplorer().CurrentFolder.Items)
                        foreach (object obj in Correos)
                        {

                            MailItem item = obj as MailItem;
                            if (item != null)
                            {
                                #region Codigo comentado
                                //DataRow fila = mitabla.NewRow();
                                //fila["Fecha"] = item.LastModificationTime.ToString();
                                //fila["Subject"] = item.Subject;
                                //fila["Remite"] = item.SenderName;
                                //mitabla.Rows.Add(fila); 
                                #endregion

                                CorreoOutlook correo = new CorreoOutlook();
                                //correo.Fecha = item.LastModificationTime;
                                correo.Fecha = item.ReceivedTime;
                                correo.Subject = item.Subject;
                                correo.Remite = item.SenderName;
                                correo.Destino = item.To;
                                correo.ConCopiaA = item.CC;
                                item.BodyFormat = OlBodyFormat.olFormatRichText;
                                correo.Body = item.Body;
                                listaCorreosOutlook.Add(correo);
                                #region Codigo comentado
                                //EscribirLogGeneracionGestion("El correo No. " + contador.ToString() + " fue reconocido", @"C:\bolyter\correos.txt");
                                //contador++;

                                //if (contador == 248)
                                //{
                                //    String prueba = String.Empty;
                                //    prueba = "Llego a 248";
                                //} 
                                #endregion

                            }
                            #region Codigo comentado 
                            //else
                            //{
                            //    EscribirLogGeneracionGestion("El correo No. " + contador.ToString() + " no fue reconocido", @"C:\bolyter\correos.txt");
                            //    contador++;
                            //} 
                            #endregion

                        }

                        this._ventana.Resultados = listaCorreosOutlook;
                        this._ventana.TotalHits = listaCorreosOutlook.Count.ToString();
                    }
                    else
                    {
                        this._ventana.Mensaje("Seleccion de Carpeta Usuario invalida", 1);
                    }
                }
                else
                {
                    this._ventana.Mensaje("Inicie Outlook para poder descargar los correos", 1);
                }

                


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorCargaDeCorreosOutlook, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que recorre cada uno de los correos seleccionado en la ventana para generar las gg
        /// </summary>
        public void GenerarGestiones()
        {

            Mouse.OverrideCursor = Cursors.Wait;
            StringBuilder cadenaLog = new StringBuilder();
            String str_log = String.Empty;
            String archivoLog = ConfigurationManager.AppSettings["rutaGestionesAutomaticas"];
            Asociado asociado = null;
            IList<FacGestion> listaGestiones = null;
            int codigoRespGestion, idAsociado, contadorGestionesRepetidas = 0, contadorGestionSinAsociado = 0;
            int idGestion = 0;
            bool gestionRepetida = false;
            

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((IList<object>)this._ventana.Resultado).Count != 0)
                {
                    IList<CorreoOutlook> listaCorreo = new List<CorreoOutlook>();

                    IList<object> resultadoAux = (IList<object>)this._ventana.Resultado;
                    //IList<object> resultadoAux = (IList<object>)this._ventana.Resultados;

                    str_log = "Generando Gestiones de Asociado " + "Fecha: " + 
                               DateTime.Today.ToShortDateString() + " " + "Hora: " + DateTime.Now.ToShortTimeString();
                    EscribirLogGeneracionGestion(str_log, archivoLog);

 

                    foreach (CorreoOutlook correo in resultadoAux)
                    {
                        FacGestion gestionNueva = new FacGestion();
                        codigoRespGestion = ObtenerCodigoResp(correo);
                        gestionNueva.CodigoResp = codigoRespGestion;

                        //Verifico si existe una gestion con el codigo del CRESP
                        listaGestiones = this._facGestionServicios.ObtenerFacGestionesFiltro(gestionNueva);

                        if (listaGestiones.Count != 0)
                        {
                            gestionRepetida = true;
                            
                            str_log = "Gestion No " + codigoRespGestion.ToString() + " ya se encuentra registrada";
                            cadenaLog.AppendLine(str_log);
                            str_log = String.Empty;
                            str_log = cadenaLog.ToString();

                            EscribirLogGeneracionGestion(str_log, archivoLog);

                            contadorGestionesRepetidas++;

                            continue;
                        }

                        //Valido si la gestion no es repetida por el numero de CRESP
                        if (!gestionRepetida)
                        {
                            gestionNueva.Medio = (this._ventana.Medio != null) ? ((MediosGestion)this._ventana.Medio).Id : null;
                            gestionNueva.ConceptoGestion = (this._ventana.Concepto != null) ? ((ConceptoGestion)this._ventana.Concepto).Id : null;
                            gestionNueva.FechaGestion = DateTime.Today;
                            gestionNueva.FechaIngreso = DateTime.Today;
                            //gestionNueva.Inicial = UsuarioLogeado.Iniciales;
                            gestionNueva.Inicial = ((Usuario)this._ventana.Usuario).Iniciales;
                            gestionNueva.Observacion = this._ventana.DetalleGestion;
                            gestionNueva.Operacion = "CREATE";

                            idAsociado = ObtenerCodigoAsociado(correo);

                            //if ((idAsociado != int.MinValue) && (this._ventana.IdAsociado.Equals("")))
                            if ((idAsociado != int.MinValue) && (this._ventana.Asociado == null))
                            {
                                asociado = new Asociado();
                                asociado.Id = idAsociado;
                                asociado = this._asociadoServicios.ConsultarAsociadoConTodo(asociado);
                                gestionNueva.Asociado = asociado;
                                gestionNueva.TipoAsociado = asociado.TipoCliente;
                                idGestion = ObtenerUltimaGestionAsociado(asociado);
                            }
                            //else if (((idAsociado == int.MinValue) && (!this._ventana.IdAsociado.Equals("")))
                            //|| ((idAsociado != int.MinValue) && (!this._ventana.IdAsociado.Equals(""))))
                            else if (((idAsociado == int.MinValue) && (this._ventana.Asociado != null))
                            || ((idAsociado != int.MinValue) && (this._ventana.Asociado != null)))
                            {
                                asociado = new Asociado();
                                //asociado.Id = int.Parse(this._ventana.IdAsociado);
                                asociado.Id = ((Asociado)this._ventana.Asociado).Id;
                                asociado = this._asociadoServicios.ConsultarAsociadoConTodo(asociado);
                                gestionNueva.Asociado = asociado;
                                gestionNueva.TipoAsociado = asociado.TipoCliente;
                                idGestion = ObtenerUltimaGestionAsociado(asociado);
                            }
                            //else if ((idAsociado == int.MinValue) && (this._ventana.IdAsociado.Equals("")))
                            else if ((idAsociado == int.MinValue) && (this._ventana.Asociado == null))
                            {
                                str_log = "Gestion No " + codigoRespGestion.ToString() + " sin Asociado. No pudo ser registrada.";
                                EscribirLogGeneracionGestion(str_log, archivoLog);
                                contadorGestionSinAsociado++;
                                continue;
                            }

                            gestionNueva.Id = idGestion;

                            bool exito = this._facGestionServicios.InsertarOModificar(gestionNueva, UsuarioLogeado.Hash);
                            if (exito)
                            {
                                str_log = "Gestion No " + codigoRespGestion.ToString() + " con Asociado# " + gestionNueva.Asociado.Id.ToString() + " fue registrada";
                                EscribirLogGeneracionGestion(str_log, archivoLog);
                            }
                            
                        }
                                      

                        gestionRepetida = false;
                    }

                    if ((contadorGestionesRepetidas > 0) || (contadorGestionSinAsociado > 0))
                    {
                        str_log = String.Empty;
                        if (contadorGestionesRepetidas > 0)
                        {
                            str_log = "Hay " + contadorGestionesRepetidas.ToString() + " gestiones repetidas. Revise el archivo de Incidencias";
                            this._ventana.Mensaje(str_log, 1);
                        }

                        if (contadorGestionSinAsociado > 0)
                        {
                            str_log = "Hay " + contadorGestionSinAsociado.ToString() + " gestiones sin Codigo de Asociado. Revise el archivo de Incidencias";
                            this._ventana.Mensaje(str_log, 1);
                        }

                    }

                    this._ventana.MensajeFinalProceso("El proceso de generacion de Gestiones ha terminado");

                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.AlertaCorreosOutlookSinSeleccion, 1);

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
            finally
            {
                Mouse.OverrideCursor = null;
            }


        }


        /// <summary>
        /// Metodo que escribe en un archivo de texto las incidencias en el momento de la generacion de las gestiones automaticas
        /// </summary>
        /// <param name="mensaje">Mensaje a escribir en el archivo</param>
        public void EscribirLogGeneracionGestion(String mensaje, String rutaArchivoLog)
        {
            //StreamWriter sw = new StreamWriter(@"C:\bolyter\Test1.txt", true, Encoding.ASCII);
            StreamWriter sw = new StreamWriter(rutaArchivoLog, true, Encoding.Unicode);
            sw.WriteLine(mensaje);
            sw.Flush();
            sw.Close();
        }



        /// <summary>
        /// Metodo que se utiliza para recorrer el subject del correo de outlook y recuperar el id del Asociado
        /// </summary>
        /// <param name="correo">Correo de outlook que se usa para inspeccionar</param>
        /// <returns>String con el id del Asociado</returns>
        public int ObtenerCodigoAsociado(CorreoOutlook correo)
        {
            String numeroAsociado = String.Empty;
            String cadena = correo.Subject.Trim();
            String aux = String.Empty;
            Char[] arrayChar = null;
            int index, startIndex, indice;
            index = cadena.IndexOf("Asociado#");
            if ((index != -1) && (index == 35))
            {
                startIndex = index + 9;
                bool esDigito = false;

                arrayChar = cadena.ToCharArray();
                char x = arrayChar[startIndex];
                indice = startIndex;


                int j = 0;
                do
                {
                    char caracter = arrayChar[indice];
                    if (Char.IsDigit(caracter))
                    {
                        esDigito = true;
                        aux += caracter.ToString();
                        indice++;
                    }
                    else
                        break;


                } while (esDigito);

                numeroAsociado = aux;

                return int.Parse(numeroAsociado);
            }
            else
            {
                return int.MinValue;
            }

        }


        public int ObtenerCodigoResp(CorreoOutlook correo)
        {
            String numeroAsociado = String.Empty;
            String cadena = correo.Subject.Trim();
            String aux = String.Empty;
            Char[] arrayChar = null;
            int indexInicial, indexFinal, startIndex, valor;
            indexInicial = cadena.IndexOf("Correspondencia No ");
            indexFinal = cadena.IndexOf("-");
            

            indexInicial += 19;

            aux = cadena.Substring(indexInicial, indexFinal - indexInicial);

            valor = int.Parse(aux);

            return valor;
        }


        public int ObtenerUltimaGestionAsociado(Asociado asociado)
        {
            IList<FacGestion> listaGestiones = new List<FacGestion>();
            FacGestion gestion = new FacGestion();
            gestion.Asociado = asociado;
            int idGestion = 0, contador;

            listaGestiones = this._facGestionServicios.ObtenerFacGestionesFiltro(gestion);

            if (listaGestiones.Count == 0)
            {
                idGestion = 1;
            }
            else
            {
                contador = listaGestiones.Count;
                contador++;
                idGestion = contador;
            }

            return idGestion;
        }


        /// <summary>
        /// Método que se encarga de buscar el asociado definido en el filtro
        /// </summary>
        public void BuscarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Asociado asociadoABuscar = new Asociado();

                //asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                //                     int.Parse(this._ventana.IdAsociadoFiltrar) : 0;

                asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                                    int.Parse(this._ventana.IdAsociadoFiltrar) : int.MinValue;

                asociadoABuscar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ?
                                         this._ventana.NombreAsociadoFiltrar.ToUpper() : "";

                //if ((asociadoABuscar.Id != 0) || !(asociadoABuscar.Nombre.Equals("")))
                if ((asociadoABuscar.Id != int.MinValue) || !(asociadoABuscar.Nombre.Equals("")))
                {
                    IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);

                    if (asociados.Count > 0)
                    {
                        asociados.Insert(0, new Asociado(int.MinValue));
                        this._ventana.Asociados = asociados;
                    }
                    else
                    {
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.Asociados = this._asociados;
                    }
                }

                else
                    this._ventana.Mensaje("Ingrese criterios validos para la busqueda del Asociado", 1);
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que cambia el texto del Asociado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el Asociado haya sido valido, false en caso contrario</returns>
        public bool CambiarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Asociado != null)
            {
                this._ventana.IdAsociado = ((Asociado)this._ventana.Asociado).Nombre;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }



        /// <summary>
        /// Metodo para desplegar la ventana para ver el detalle de un correo outlook seleccionado
        /// </summary>
        public void VerDetalleCorreoOutlook()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.CorreoSeleccionado != null)
            {
                CorreoOutlook correo = (CorreoOutlook)this._ventana.CorreoSeleccionado;
                this.Navegar(new GestionarDetallesCorreoOutlook(correo, this._ventana));
            }


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo para ver el contenido del archivo log que guarda las incidencias del proceso de generacion automatica de gestiones
        /// </summary>
        public void VerArchivoLog()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String rutaArchivoLog = String.Empty;

            rutaArchivoLog = ConfigurationManager.AppSettings["rutaGestionesAutomaticas"].ToString();

            if (File.Exists(rutaArchivoLog))
            {
                Process.Start(rutaArchivoLog);
            }
            else
            {
                this._ventana.Mensaje("El archivo Log de generacion de Gestiones no existe. Revise", 0);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
