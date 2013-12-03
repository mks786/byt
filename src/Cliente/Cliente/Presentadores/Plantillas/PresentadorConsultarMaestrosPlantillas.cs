using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Plantillas
{
    class PresentadorConsultarMaestrosPlantillas : PresentadorBase
    {
        
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarMaestrosPlantillas _ventana;
        private IIdiomaServicios _idiomaServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IMaestroDePlantillaServicios _maestroDePlantillaServicios;
        private IUsuarioServicios _usuarioServicios;
        private int _filtroValido;
        private IList<ListaDatosValores> _listaReferidos;
        private IList<ListaDatosValores> _listaCriterios;
        private IList<Usuario> _listaUsuarios;


        public PresentadorConsultarMaestrosPlantillas(IConsultarMaestrosPlantillas ventana)
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
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._maestroDePlantillaServicios = (IMaestroDePlantillaServicios)Activator.GetObject(typeof(IMaestroDePlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaestroDePlantillaServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                
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

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMaestroPlantilla,
                Recursos.Ids.MaestroPlantilla);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMaestroPlantilla, "");

                CargarCombos();

                CargarUsuarios(false);

                CargarArchivosEncabezado();

                CargarArchivosDetalle();

                this._ventana.TotalHits = "0";

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
        /// Metodo que carga los usuarios en el combo correspondiente
        /// </summary>
        private void CargarUsuarios(bool recargar)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!recargar)
                {   
                    _listaUsuarios = this._usuarioServicios.ConsultarTodos();
                    _listaUsuarios = this.FiltrarUsuariosRepetidos(_listaUsuarios);
                    this._ventana.Usuarios = _listaUsuarios; 
                }

                this._ventana.Usuario = this.BuscarUsuarioPorIniciales(_listaUsuarios, UsuarioLogeado);

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
        /// Metodo que carga los combos de la ventana
        /// </summary>
        private void CargarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
                this._ventana.Idiomas = idiomas;

                IList<ListaDatosValores> referencias = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiReferenciaPlantilla));
                this._ventana.Referidos = referencias;
                this._listaReferidos = referencias;

                IList<ListaDatosValores> criterios = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCriteriosPlantilla));
                this._ventana.Criterios = criterios;
                this._listaCriterios = criterios;

                IList<Usuario> listaUsuarios = this._usuarioServicios.ConsultarTodos();
                listaUsuarios = this.FiltrarUsuariosRepetidos(listaUsuarios);
                this._ventana.Usuarios = listaUsuarios;
                this._ventana.Usuario = this.BuscarUsuarioPorIniciales(listaUsuarios, UsuarioLogeado);




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
        /// Metodo que carga el combo de archivos de encabezado
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

                this._ventana.Encabezados = archivos;

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
        /// Metodo que carga el combo de los archivos SQL de Detalle del Maestro de Plantillas
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

                this._ventana.Detalles = archivos;

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
        /// Metodo que realiza la consulta en la tabla de los maestros de plantillas de acuerdo a un filtro
        /// </summary>
        public void Consultar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._filtroValido = 0;

                MaestroDePlantilla maestroPlantillaAuxiliar = new MaestroDePlantilla();
                IList<MaestroDePlantilla> listaDeMaestrosDePlantilla = new List<MaestroDePlantilla>();

                maestroPlantillaAuxiliar = ObtenerMaestroPlantillaParaFiltro();

                if (this._filtroValido >= 2)
                {

                    listaDeMaestrosDePlantilla = this._maestroDePlantillaServicios.ObtenerMaestroDePlantillaFiltro(maestroPlantillaAuxiliar);


                    foreach (MaestroDePlantilla maestroPlantilla in listaDeMaestrosDePlantilla)
                    {
                        ListaDatosValores referencia = this.BuscarTipoReferenciaYCriterio(this._listaReferidos, maestroPlantilla.Referido);
                        maestroPlantilla.TipoReferencia = referencia;
                        ListaDatosValores criterio = this.BuscarTipoReferenciaYCriterio(this._listaCriterios, maestroPlantilla.Criterio);
                        maestroPlantilla.TipoCriterio = criterio;
                    }

                    this._ventana.Resultados = listaDeMaestrosDePlantilla;
                    if (listaDeMaestrosDePlantilla.Count > 0)
                        this._ventana.TotalHits = listaDeMaestrosDePlantilla.Count.ToString();

                    if (listaDeMaestrosDePlantilla.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);

                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);

                

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
        /// Metodo que obtiene el Maestro de Plantilla filtro para hacer la consulta
        /// </summary>
        /// <returns>Maestro de Plantilla a consultar</returns>
        private MaestroDePlantilla ObtenerMaestroPlantillaParaFiltro()
        {
            MaestroDePlantilla maestroPlantilla = new MaestroDePlantilla();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._ventana.IdMaestroPlantilla.Equals(""))
                {
                    Plantilla plantilla = new Plantilla();
                    plantilla.Id = int.Parse(this._ventana.IdMaestroPlantilla);
                    maestroPlantilla.Plantilla = plantilla;
                    this._filtroValido = 2;
                }
                else
                    maestroPlantilla.Plantilla = null;

                if ((this._ventana.Idioma != null) && (!((Idioma)this._ventana.Idioma).Id.Equals("NGN")))
                {
                    maestroPlantilla.Idioma = (Idioma)this._ventana.Idioma;
                    this._filtroValido = 2;
                }
                else
                    maestroPlantilla.Idioma = null;

                if ((this._ventana.Usuario != null) && (!((Usuario)this._ventana.Usuario).Id.Equals("")))
                {
                    maestroPlantilla.Usuario = (Usuario)this._ventana.Usuario;
                    this._filtroValido = 2;
                }
                else
                    maestroPlantilla.Usuario = null;

                if (this._ventana.Referido != null)
                {
                    maestroPlantilla.Referido = ((ListaDatosValores)this._ventana.Referido).Valor;
                    this._filtroValido = 2;
                }
                else
                    maestroPlantilla.Referido = null;

                if (this._ventana.Criterio != null)
                {
                    maestroPlantilla.Criterio = ((ListaDatosValores)this._ventana.Criterio).Valor;
                    this._filtroValido = 2;
                }
                else
                    maestroPlantilla.Criterio = null;

                if ((this._ventana.Encabezado != null) && (!((EncabezadoPlantilla)this._ventana.Encabezado).NombreEncabezado.Equals("")))
                {
                    maestroPlantilla.SQL_Encabezado = ((EncabezadoPlantilla)this._ventana.Encabezado).NombreEncabezado;
                    this._filtroValido = 2;
                }
                else
                    maestroPlantilla.SQL_Encabezado = null;

                if ((this._ventana.Detalle != null) && (!((DetallePlantilla)this._ventana.Detalle).NombreDetalle.Equals("")))
                {
                    maestroPlantilla.SQL_Detalle = ((DetallePlantilla)this._ventana.Detalle).NombreDetalle;
                    this._filtroValido = 2;
                }
                else
                    maestroPlantilla.SQL_Detalle = null;

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

            return maestroPlantilla;
        }


        public void LimpiarCampos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana.Resultados = null;
                this._ventana.TotalHits = "0";

                this._ventana.IdMaestroPlantilla = null;
                this._ventana.Idioma = null;
                this._ventana.Referido = null;
                this._ventana.Criterio = null;
                this._ventana.Encabezado = null;
                this._ventana.Detalle = null;

                this._ventana.Usuario = null;
                CargarUsuarios(true);
                

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
        /// Metodo que lleva la ventana para Gestionar un Maestro de Plantilla
        /// </summary>
        public void IrConsultarMaestroDePlantilla()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.MaestroDePlantillaSeleccionado != null)
                    this.Navegar(new GestionarMaestroPlantilla(this._ventana.MaestroDePlantillaSeleccionado, this._ventana));

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
    }
}
