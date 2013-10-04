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
    class PresentadorGestionarMaestroPlantilla: PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IGestionarMaestroPlantilla _ventana;
        private bool _nuevo = false;
        private IIdiomaServicios _idiomaServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IPlantillaServicios _plantillaServicios;
        private IMaestroDePlantillaServicios _maestroDePlantillaServicios;
        private IFiltroPlantillaServicios _filtroPlantillaServicios;


        /// <summary>
        /// Constructor por defecto que recibe una ventana IGestionarMaestroPlantilla
        /// </summary>
        /// <param name="ventana">Ventana actual a llamar</param>
        /// <param name="maestroPlantilla">Datos maestros para una plantilla seleccionados</param>
        public PresentadorGestionarMaestroPlantilla(IGestionarMaestroPlantilla ventana, object maestroPlantilla, object ventanaPadre)
        {

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                if (maestroPlantilla == null)
                {
                    this._nuevo = true;
                    MaestroDePlantilla _maestroPlantilla = new MaestroDePlantilla();
                    this._ventana.DatosMaestrosPlantilla = _maestroPlantilla;
                }
                else
                {
                    this._ventana.DatosMaestrosPlantilla = (MaestroDePlantilla)maestroPlantilla;
                }
                 
                 
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._plantillaServicios = (IPlantillaServicios)Activator.GetObject(typeof(IPlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PlantillaServicios"]);
                this._maestroDePlantillaServicios = (IMaestroDePlantillaServicios)Activator.GetObject(typeof(IMaestroDePlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaestroDePlantillaServicios"]);
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



        public void CargarPagina()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                CargarComboPlantillas(this._nuevo);
                CargarComboIdiomas(this._nuevo);
                CargarReferencias(this._nuevo);
                CargarCriterios(this._nuevo);
                CargarComboDepartamentos();
                CargarComboUsuarios();
                CargarArchivosEncabezado(this._nuevo);
                CargarArchivosDetalle(this._nuevo);
                CargarArchivosBAT("encabezado",this._nuevo);
                CargarArchivosBAT("detalle",this._nuevo);

                if (!this._nuevo)
                {
                    IList<FiltroPlantilla> variablesEncabezado = 
                        this._filtroPlantillaServicios.ObtenerFiltrosEncabezadoPlantilla((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla);
                    if (variablesEncabezado.Count > 0)
                    {
                        this._ventana.PintarBotonVariablesEncabezado();
                    }
                    IList<FiltroPlantilla> variablesDetallle = 
                        this._filtroPlantillaServicios.ObtenerFiltrosDetallePlantilla((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla);
                    if (variablesDetallle.Count > 0)
                    {
                        this._ventana.PintarBotonVariablesDetalle();
                    }
                }
                else
                {
                    this._ventana.ActivarBotonVariables(false);
                }


                IList<ListaDatosValores> localidades = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiLocalidadMarca));

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

        
        /// <summary>
        /// Metodo que llena el combo con todas las plantillas definidas en base de datos
        /// <param name="nuevoMaestro">Bandera que indica que si es un nuevo maestro de plantilla o no</param>
        /// </summary>
        private void CargarComboPlantillas(bool nuevoMaestro)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Plantilla> listaPlantillas = this._plantillaServicios.ConsultarTodos();
                this._ventana.Plantillas = listaPlantillas;
                if (!nuevoMaestro)
                {
                    this._ventana.Plantilla = this.BuscarPlantilla(listaPlantillas, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).Plantilla);
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
        }

        
        /// <summary>
        /// Metodo que carga en el combobox de los Encabezados, todos los archivos existentes en la carpeta respectiva para 
        /// analizarlos
        /// <param name="nuevoMaestro">Bandera que indica que si es un nuevo maestro de plantilla o no</param>
        /// </summary>
        private void CargarArchivosEncabezado(bool nuevoMaestro)
        {


            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String rutaEncabezados = String.Empty, rutaCarpetaEncabezado = String.Empty, 
                    nombreArchivo = String.Empty, rutaArchivo = String.Empty;
                IList<EncabezadoPlantilla> archivos = new List<EncabezadoPlantilla>();
                
 
                rutaEncabezados = ConfigurationManager.AppSettings["RutaMaestroPlantillas"];
                rutaCarpetaEncabezado = rutaEncabezados + "encabezado" + @"\";
                
                DirectoryInfo dir = new System.IO.DirectoryInfo(rutaCarpetaEncabezado);

                foreach (FileInfo archivo in dir.GetFiles("*.*"))
                {
                    EncabezadoPlantilla encabezado = new EncabezadoPlantilla();
                    nombreArchivo = archivo.Name;
                    rutaArchivo = archivo.FullName;
                    encabezado.NombreEncabezado = nombreArchivo;
                    encabezado.RutaEncabezado = rutaArchivo;
                    archivos.Add(encabezado);
                }

                this._ventana.ArchivosEncabezado = archivos;

                if (!nuevoMaestro)
                {
                    this._ventana.ArchivoEncabezado = this.BuscarEncabezadoPlantilla(archivos, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).SQL_Encabezado);
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


        }


        /// <summary>
        /// Metodo que llena el combo con todos los archivos BAT definidos en la carpeta 
        /// <param name="tipoDeBat">Tipo de Archivo BAT, Encabezado o Detalle</param>
        /// <param name="nuevoMaestro">Bandera que indica si es un nuevo maestro de plantilla o no</param>
        /// </summary>
        private void CargarArchivosBAT(String tipoDeBat, bool nuevoMaestro)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String rutaBat = String.Empty, rutaCarpetaBat = String.Empty, nombreArchivo = String.Empty, rutaArchivo = String.Empty;
                IList<BatPlantilla> archivos = new List<BatPlantilla>();

                if (tipoDeBat.Equals("encabezado"))
                {
                    rutaBat = ConfigurationManager.AppSettings["RutaMaestroPlantillas"];
                    rutaCarpetaBat = rutaBat + "bat" + @"\";
                }
                else if (tipoDeBat.Equals("detalle"))
                {
                    rutaBat = ConfigurationManager.AppSettings["RutaMaestroPlantillas"];
                    rutaCarpetaBat = rutaBat + "BatDetalle" + @"\";
                }

                DirectoryInfo dir = new System.IO.DirectoryInfo(rutaCarpetaBat);

                foreach (FileInfo archivo in dir.GetFiles("*.*"))
                {
                    BatPlantilla bat = new BatPlantilla();
                    nombreArchivo = archivo.Name;
                    rutaArchivo = archivo.FullName;
                    bat.NombreBat = nombreArchivo;
                    bat.RutaBat = rutaArchivo;
                    archivos.Add(bat);
                }

                if (tipoDeBat.Equals("encabezado"))
                {
                    this._ventana.ArchivosBat = archivos;
                    if(!nuevoMaestro)
                        this._ventana.ArchivoBat = this.BuscarArchivoBatMaestroPlantilla(archivos, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).BAT_Encabezado);
                }

                else if (tipoDeBat.Equals("detalle"))
                {
                    this._ventana.ArchivosBatDetalle = archivos;
                    if (!nuevoMaestro)
                        this._ventana.ArchivoBatDetalle = this.BuscarArchivoBatMaestroPlantilla(archivos, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).BAT_Detalle);
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
        }



        /// <summary>
        /// Metodo que sirve para cargar el contenido de la carpeta donde se encuentran los archivos con el SQL del detalle
        /// <param name="nuevoMaestro">Bandera que indica si es un nuevo Maestro de Plantilla o no</param>
        /// </summary>
        private void CargarArchivosDetalle(bool nuevoMaestro)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String rutaDetalle = String.Empty, rutaCarpetaDetalle = String.Empty, nombreArchivo = String.Empty, rutaArchivo = String.Empty;
                IList<DetallePlantilla> archivos = new List<DetallePlantilla>();


                rutaDetalle = ConfigurationManager.AppSettings["RutaMaestroPlantillas"];
                rutaCarpetaDetalle = rutaDetalle + "Detalle" + @"\";

                DirectoryInfo dir = new System.IO.DirectoryInfo(rutaCarpetaDetalle);

                foreach (FileInfo archivo in dir.GetFiles("*.*"))
                {
                    DetallePlantilla detalle = new DetallePlantilla();
                    nombreArchivo = archivo.Name;
                    rutaArchivo = archivo.FullName;
                    detalle.NombreDetalle = nombreArchivo;
                    detalle.RutaDetalle = rutaArchivo;
                    archivos.Add(detalle);
                }

                this._ventana.ArchivosDetalle = archivos;

                if (!nuevoMaestro)
                {
                    this._ventana.ArchivoDetalle = this.BuscarDetallePlantilla(archivos, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).SQL_Detalle);
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
        }


        /// <summary>
        /// Metodo que llena el combo de los Criterios para una Plantilla
        /// <param name="nuevoMaestro">Bandera que indica que es un nuevo Maestro de Plantilla o no</param>
        /// </summary>
        private void CargarCriterios(bool nuevoMaestro)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<ListaDatosValores> criterios = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCriteriosPlantilla));
                this._ventana.Criterios = criterios;

                if (!nuevoMaestro)
                {
                    this._ventana.Criterio = 
                        this.BuscarTipoReferenciaYCriterio(criterios, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).Criterio);
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
        }

        
        
        /// <summary>
        /// Metodo que carga las Referencias de una plantilla
        /// <param name="nuevoMaestro">Bandera que indica si es un nuevo Maestro de Plantilla o no</param>
        /// </summary>
        private void CargarReferencias(bool nuevoMaestro)
        {
           try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

               IList<ListaDatosValores> referencias = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiReferenciaPlantilla));
               this._ventana.Referencias = referencias;

               if (!nuevoMaestro)
               {
                   this._ventana.Referencia = 
                       this.BuscarTipoReferenciaYCriterio(referencias, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).Referido);
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
        }

        
        
        
        /// <summary>
        /// Metodo que carga el Usuario Logueado y los demas usuarios
        /// </summary>
        private void CargarComboUsuarios()
        {
            IList<Usuario> listaUsuarios;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                listaUsuarios = this._usuarioServicios.ConsultarTodos();
                this._ventana.Usuarios = listaUsuarios;
                this._ventana.Usuario = this.BuscarUsuarioPorIniciales(listaUsuarios, UsuarioLogeado);


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
        /// Metodo que carga el combo de los Departamentos para una plantilla
        /// </summary>
        private void CargarComboDepartamentos()
        {
            IList<Departamento> listaDepartamentos;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                listaDepartamentos = this._departamentoServicios.ConsultarTodos();
                this._ventana.Departamentos = listaDepartamentos;
                this._ventana.Departamento = this.BuscarDepartamento(listaDepartamentos, UsuarioLogeado.Departamento);

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
        /// Metodo que llena el combo de los idiomas de la ventana
        /// <param name="nuevoMaestro">Bandera para indicar si es un nuevo maestro de plantilla o no</param>
        /// </summary>
        private void CargarComboIdiomas(bool nuevoMaestro)
        {
            IList<Idioma> listaIdiomas;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                listaIdiomas = this._idiomaServicios.ConsultarTodos();
                this._ventana.Idiomas = listaIdiomas;

                if (!nuevoMaestro)
                {
                    this._ventana.Idioma = this.BuscarIdioma(listaIdiomas, ((MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla).Idioma);
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
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGenerarGestionAutomatica,
                Recursos.Ids.fac_GestionAutomatica);
        }

        /// <summary>
        /// Metodo que presenta la ventana para hacer el test de los encabezados, es decir; el SQL y el BAT
        /// </summary>
        public void ProbarEncabezado()
        {
            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                #region CODIGO PARA PROBAR EL SQL -- NO BORRAR!!!!
                //EncabezadoPlantilla SQLEncabezado = (EncabezadoPlantilla)this._ventana.ArchivoEncabezado;
                //String in_database = ConfigurationManager.AppSettings["NombreBaseDatosMaestroPlantillas"];
                //String comandoConsola = ConfigurationManager.AppSettings["ComandoTestMaestroPlantilla"];
                //String usuario_password = ConfigurationManager.AppSettings["UserPasswordMaestroPlantilla"];
                              
                
                //String s = SQLEncabezado.RutaEncabezado + " c:\\bolyter\\ESCLIMDI.txt MP 865096";

                //Process p = new Process();
                //p.StartInfo.UseShellExecute = false;
                //p.StartInfo.RedirectStandardOutput = true;
                //p.StartInfo.FileName = comandoConsola;
                //p.StartInfo.Arguments = string.Format(usuario_password + "@{0} @{1}", in_database, s);
                //bool started = p.Start();
                //string output = p.StandardOutput.ReadToEnd();

                //p.WaitForExit(); 
                #endregion

                if (this._ventana.ArchivoEncabezado != null)
                {
                    //((EncabezadoPlantilla)this._ventana.ArchivoEncabezado).Plantilla = (Plantilla)this._ventana.Plantilla;
                    ((EncabezadoPlantilla)this._ventana.ArchivoEncabezado).MaestroDePlantilla = (MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla;

                    if (this._ventana.ArchivoBat != null)
                      ((EncabezadoPlantilla)this._ventana.ArchivoEncabezado).BatPlantilla = (BatPlantilla)this._ventana.ArchivoBat;  

                    this.Navegar(new AnalizarEncabezadoPlantilla(this._ventana.ArchivoEncabezado, this._ventana));
                }
                else
                    this._ventana.MensajeAlerta("Seleccione un SQL Encabezado para poderlo analizar", 0);


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
        /// Metodo que presenta la ventana para hacer el test de los detalles, es decir; el SQL y el BAT
        /// </summary>
        public void ProbarDetalle()
        {
            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                
                if (this._ventana.ArchivoDetalle != null)
                {
                    ((DetallePlantilla)this._ventana.ArchivoDetalle).Plantilla = (Plantilla)this._ventana.Plantilla;

                    if (this._ventana.ArchivoBatDetalle != null)
                        ((DetallePlantilla)this._ventana.ArchivoDetalle).BatPlantilla = (BatPlantilla)this._ventana.ArchivoBatDetalle;

                    this.Navegar(new AnalizarDetallePlantilla(this._ventana.ArchivoDetalle, this._ventana));
                }
                else
                    this._ventana.MensajeAlerta("Seleccione un SQL para el Detalle para poderlo analizar", 0);


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
        /// Metodo que presenta la ventana de la lista de los filtros de encabezado
        /// </summary>
        public void VerFiltrosEncabezadoPlantilla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaValoresEncabezado(this._ventana.DatosMaestrosPlantilla, this._ventana, this._ventanaPadre));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }



        /// <summary>
        /// Metodo que presenta la ventana de la lista de los filtros de encabezado
        /// </summary>
        public void VerFiltrosDetallePlantilla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaValoresDetalle(this._ventana.DatosMaestrosPlantilla, this._ventana, this._ventanaPadre));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo para guardar un elemento en la tabla de Maestro de Plantillas
        /// </summary>
        public void Aceptar()
        {
            try
            {
                bool exito;
                int numeroRegistros = 0, nuevoValor = 0;


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                MaestroDePlantilla maestroPlantilla = new MaestroDePlantilla();

                maestroPlantilla = CargarMaestroDePlantillaDeLaPantalla();

                if (this._nuevo)
                {

                    if (maestroPlantilla.Plantilla != null)
                    {
                        if (maestroPlantilla.Idioma != null)
                        {
                            IList<MaestroDePlantilla> _plantillas = this._maestroDePlantillaServicios.ConsultarTodos();

                            if (_plantillas.Count > 0)
                            {
                                numeroRegistros = _plantillas.Count;
                                nuevoValor = numeroRegistros + 1;
                                maestroPlantilla.Id = nuevoValor;
                            }
                            else if (_plantillas.Count == 0)
                            {
                                nuevoValor = 1;
                                maestroPlantilla.Id = nuevoValor;
                            }

                            exito = this._maestroDePlantillaServicios.InsertarOModificar(maestroPlantilla, UsuarioLogeado.Hash);
                            if (exito)
                            {
                                this._ventana.MensajeAlerta("Insercion de Maestro de Plantilla ejecutada con exito", 2);
                                this._ventana.DatosMaestrosPlantilla = maestroPlantilla;
                                this._ventana.ActivarBotonVariables(true);
                            }
                        }
                        else
                            this._ventana.MensajeAlerta("Seleccione un Idioma para la Plantilla", 0);
                    }
                    else
                        this._ventana.MensajeAlerta("Debe seleccionar una plantilla", 0);

                }
                else
                {
                    if (maestroPlantilla.Plantilla != null)
                    {
                        if (maestroPlantilla.Idioma != null)
                        {
                            exito = this._maestroDePlantillaServicios.InsertarOModificar(maestroPlantilla, UsuarioLogeado.Hash);
                            if (exito)
                                this._ventana.MensajeAlerta("Maestro de Plantilla modificado con exito", 2);
                        }
                        else
                            this._ventana.MensajeAlerta("Seleccione un Idioma para la Plantilla", 0);
                    }
                    else
                        this._ventana.MensajeAlerta("Debe seleccionar una plantilla", 0);
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
        }



        public MaestroDePlantilla CargarMaestroDePlantillaDeLaPantalla()
        {
           
                     

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                MaestroDePlantilla maestroPlantilla = (MaestroDePlantilla)this._ventana.DatosMaestrosPlantilla;


                maestroPlantilla.Plantilla = this._ventana.Plantilla != null ? (Plantilla)this._ventana.Plantilla : null;

                maestroPlantilla.Referido = this._ventana.Referencia != null ? ((ListaDatosValores)this._ventana.Referencia).Valor : null;

                maestroPlantilla.Criterio = this._ventana.Criterio != null ? ((ListaDatosValores)this._ventana.Criterio).Valor : null;

                maestroPlantilla.Idioma = this._ventana.Idioma != null ? (Idioma)this._ventana.Idioma : null;

                maestroPlantilla.SQL_Encabezado = this._ventana.ArchivoEncabezado != null ?
                    ((EncabezadoPlantilla)this._ventana.ArchivoEncabezado).NombreEncabezado : null;

                maestroPlantilla.SQL_Detalle = this._ventana.ArchivoDetalle != null ?
                    ((DetallePlantilla)this._ventana.ArchivoDetalle).NombreDetalle : null;

                maestroPlantilla.BAT_Encabezado = this._ventana.ArchivoBat != null ?
                    ((BatPlantilla)this._ventana.ArchivoBat).NombreBat : null;

                maestroPlantilla.BAT_Detalle = this._ventana.ArchivoBatDetalle != null ?
                    ((BatPlantilla)this._ventana.ArchivoBatDetalle).NombreBat : null;

                return maestroPlantilla;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                

            
        }



    }
}
