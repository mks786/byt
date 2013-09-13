using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.ReportesMaestro;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.ReportesMaestro
{
    class PresentadorConsultarReportes : PresentadorBase
    {
        
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarReportes _ventana;
        private IUsuarioServicios _usuarioServicios;
        private IIdiomaServicios _idiomaServicios;
        private IReporteServicios _reporteDeMarcaServicios;
        private IVistaReporteServicios _vistaReporteServicios;
        private int _filtroValido;

        /// <summary>
        /// Constructor predeterminado que solamente recibe la ventana actual
        /// </summary>
        /// <param name="ventana">Ventana IConsultarReportesDeMarca</param>
        public PresentadorConsultarReportes(IConsultarReportes ventana)
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
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._reporteDeMarcaServicios = (IReporteServicios)Activator.GetObject(typeof(IReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ReporteServicios"]);
                this._vistaReporteServicios = (IVistaReporteServicios)Activator.GetObject(typeof(IVistaReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["VistaReporteServicios"]);

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarReportesDeMarca,
                Recursos.Ids.GeneradorReporteMarca);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarReportesDeMarca, "");

                CargarVistasDisponibles();

                CargarUsuarios();

                CargarIdiomas();

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
        /// Metodo que carga los nombres de las vistas disponibles para los Reportes
        /// </summary>
        public void CargarVistasDisponibles()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<VistaReporte> vistasDisponibles = this._vistaReporteServicios.ConsultarTodos();
                this._ventana.TiposDeReporte = vistasDisponibles;

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
        /// Metodo que carga el combo de los idiomas en la ventana de ConsultarReportesDeMarca
        /// </summary>
        public void CargarIdiomas()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
                this._ventana.Idiomas = idiomas;

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
        /// Metodo que carga el combo de los usuarios en la ventana ConsultarReportesDeMarca
        /// </summary>
        public void CargarUsuarios()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                this._ventana.Usuarios = usuarios;

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

                Reporte ReporteDeMarcaAuxiliar = new Reporte();
                ReporteDeMarcaAuxiliar.Id = int.MinValue;
                IList<Reporte> listaDeReportesDeMarca = new List<Reporte>();

                ReporteDeMarcaAuxiliar = ObtenerReporteParaFiltro();

                if (this._filtroValido >= 2)
                {
                    
                    listaDeReportesDeMarca = this._reporteDeMarcaServicios.ObtenerReporteFiltro(ReporteDeMarcaAuxiliar);

                    this._ventana.Resultados = listaDeReportesDeMarca;
                    if(listaDeReportesDeMarca.Count > 0)
                        this._ventana.TotalHits = listaDeReportesDeMarca.Count.ToString();

                    if (listaDeReportesDeMarca.Count == 0)
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
        /// Metodo que obtiene el objeto ReporteDeMarca a filtrar en la base de datos
        /// </summary>
        /// <returns>Objeto ReporteDeMarca a filtrar en base de datos</returns>
        private Reporte ObtenerReporteParaFiltro()
        {
            Reporte reporteDeMarca = new Reporte();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._ventana.IdReporte.Equals(""))
                {
                    reporteDeMarca.Id = int.Parse(this._ventana.IdReporte);
                    this._filtroValido = 2;
                }
                else
                    reporteDeMarca.Id = int.MinValue;

                if (!this._ventana.DescripcionReporte.Equals(""))
                {
                    reporteDeMarca.Descripcion = this._ventana.DescripcionReporte;
                    this._filtroValido = 2;
                }
                else
                    reporteDeMarca.Descripcion = null;

                if (!this._ventana.TituloEnEspanol.Equals(""))
                {
                    reporteDeMarca.TituloEspanol = this._ventana.TituloEnEspanol;
                    this._filtroValido = 2;
                }
                else
                    reporteDeMarca.TituloEspanol = null;

                if (!this._ventana.TituloEnIngles.Equals(""))
                {
                    reporteDeMarca.TituloIngles = this._ventana.TituloEnIngles;
                    this._filtroValido = 2;
                }
                else
                    reporteDeMarca.TituloIngles = null;

                if ((null != this._ventana.Idioma) && (!((Idioma)this._ventana.Idioma).Id.Equals("NGN")))
                {
                    reporteDeMarca.Idioma = (Idioma)this._ventana.Idioma;
                    this._filtroValido = 2;
                }

                if ((null != this._ventana.Usuario) && (!((Usuario)this._ventana.Usuario).Id.Equals("NGN")))
                {
                    reporteDeMarca.Usuario = ((Usuario)this._ventana.Usuario).NombreCompleto;
                    this._filtroValido = 2;
                }

                if ((null != this._ventana.TipoDeReporte) && (((VistaReporte)this._ventana.TipoDeReporte).Id != 0))
                {
                    reporteDeMarca.VistaReporte = (VistaReporte)this._ventana.TipoDeReporte;
                    this._filtroValido = 2;
                }




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

            return reporteDeMarca;
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

                this._ventana.IdReporte = null;
                this._ventana.DescripcionReporte = null;
                this._ventana.TituloEnEspanol = null;
                this._ventana.TituloEnIngles = null;
                this._ventana.Idioma = null;
                this._ventana.Usuario = null;
                this._ventana.TipoDeReporte = null;
                
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
        /// Metodo que ordena una columna en el grid de los resultados
        /// </summary>
        /// <param name="gridViewColumnHeader">Columna a ordenar</param>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                this._ventana.ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            this._ventana.ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que carga un Reporte Seleccionado
        /// </summary>
        public void IrConsultarReporteDeMarca()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ReporteDeMarcaSeleccionado != null)
                    this.Navegar(new GestionarReporte(this._ventana.ReporteDeMarcaSeleccionado, this._ventana));

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
        /// Metodo que llama a la pantalla para ingresar los valores de los filtros para ejecutar la consulta
        /// </summary>
        public void GestionarValoresParaFiltros()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ReporteDeMarcaSeleccionado != null)
                    this.Navegar(new GestionarValoresFiltrosDeReporte(this._ventana.ReporteDeMarcaSeleccionado, this._ventana));
                else
                    this._ventana.Mensaje("Seleccione un Reporte para ejecutar", 0);

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
