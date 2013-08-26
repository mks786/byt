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
        private IIdiomaServicios _idiomaServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IPlantillaServicios _plantillaServicios;
        private IMaestroDePlantillaServicios _maestroDePlantillaServicios;


        /// <summary>
        /// Constructor por defecto que recibe una ventana IGestionarMaestroPlantilla
        /// </summary>
        /// <param name="ventana">Ventana actual a llamar</param>
        public PresentadorGestionarMaestroPlantilla(IGestionarMaestroPlantilla ventana)
        {

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
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
                CargarComboPlantillas();
                CargarComboIdiomas();
                CargarReferencias();
                CargarCriterios();
                CargarComboDepartamentos();
                CargarComboUsuarios();
                CargarArchivosEncabezado();
                CargarArchivosDetalle();
                CargarArchivosBAT("encabezado");
                CargarArchivosBAT("detalle");


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
        /// </summary>
        private void CargarComboPlantillas()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Plantilla> listaPlantillas = this._plantillaServicios.ConsultarTodos();
                this._ventana.Plantillas = listaPlantillas;
                
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
        /// </summary>
        private void CargarArchivosEncabezado()
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
        /// </summary>
        private void CargarArchivosBAT(String tipoDeBat)
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
                    this._ventana.ArchivosBat = archivos;
                else if (tipoDeBat.Equals("detalle"))
                    this._ventana.ArchivosBatDetalle = archivos;

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
        /// </summary>
        private void CargarArchivosDetalle()
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
        /// </summary>
        private void CargarCriterios()
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
        /// </summary>
        private void CargarReferencias()
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
        /// </summary>
        private void CargarComboIdiomas()
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
                    ((EncabezadoPlantilla)this._ventana.ArchivoEncabezado).Plantilla = (Plantilla)this._ventana.Plantilla;

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

            this.Navegar(new ListaValoresEncabezado(this._ventana.Plantilla, this._ventana));

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

            this.Navegar(new ListaValoresDetalle(this._ventana.Plantilla, this._ventana));

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

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                MaestroDePlantilla maestroPlantilla = new MaestroDePlantilla();

                maestroPlantilla = CargarMaestroDePlantillaDeLaPantalla();

                if (maestroPlantilla.Plantilla != null)
                {
                    exito = this._maestroDePlantillaServicios.InsertarOModificar(maestroPlantilla, UsuarioLogeado.Hash);
                    if (exito)
                        this._ventana.MensajeAlerta("Insercion o modificacion de plantilla ejecutada con exito", 2);
                }
                else
                    this._ventana.MensajeAlerta("Debe seleccionar una plantilla", 0);


                
                    


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

                MaestroDePlantilla maestroPlantilla = new MaestroDePlantilla();


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
