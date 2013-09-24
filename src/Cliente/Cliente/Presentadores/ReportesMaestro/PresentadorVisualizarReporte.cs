using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using NLog;
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Windows.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;

namespace Trascend.Bolet.Cliente.Presentadores.ReportesMaestro
{
    class PresentadorVisualizarReporte : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IVisualizarReporte _ventana;
        private DataSet _resultados;
        private Reporte _reporte;
        private IFiltroReporteServicios _filtroReporteServicios;

        public PresentadorVisualizarReporte(IVisualizarReporte ventana, object reporte, object resultado, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventana.FocoPredeterminado();
                this._resultados = (DataSet)resultado;
                this._reporte = (Reporte)reporte;
                this._ventana.Reporte = reporte;

                this._filtroReporteServicios = (IFiltroReporteServicios)Activator.GetObject(typeof(IFiltroReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroReporteServicios"]);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + " " + ex.Message, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarReporteDeMarca,
                Recursos.Ids.GeneradorReporteMarca);
        }


        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            DataTable dtGenerado = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<FiltroReporte> listaFiltrosReporte = this._filtroReporteServicios.ConsultarFiltrosReporte(this._reporte);
                
                this._ventana.TituloReporte = ((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES") ? 
                    ((Reporte)this._ventana.Reporte).TituloEspanol : ((Reporte)this._ventana.Reporte).TituloIngles;

                if (listaFiltrosReporte.Count > 0)
                    this._ventana.FiltrosReporte = listaFiltrosReporte;

                //dtGenerado = CrearDatatTable(this._resultados);

                //this._ventana.LlenarDataGrid(dtGenerado);
                this._ventana.LlenarDataGrid(this._resultados.Tables[0]);

                //this._ventana.Resultados = dtGenerado;

                
                if (this._resultados.Tables[0].Rows.Count > 0)
                    this._ventana.TotalHits = this._resultados.Tables[0].Rows.Count.ToString();
                else
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorEjecucionReporte + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        //TERMINAR ESTE METODO
        /// <summary>
        /// Metodo que crea un dataTable a partir del datatable original
        /// </summary>
        /// <param name="dataSet"></param>
        private DataTable CrearDatatTable(DataSet dataSet)
        {
            IList<String> columnas = new List<String>();
            String nombreColumna = String.Empty;
            DataTable dtNuevo = new DataTable("Datos");
            DataRow filaNueva;
            String variable = String.Empty;
            int contador = 0;
            int numeroColumnas = 0; 

            //Lleno la lista con los nombres de las columnas del DataTable original
            foreach (DataColumn columna in dataSet.Tables[0].Columns)
            {
                nombreColumna = columna.ColumnName;
                columnas.Add(nombreColumna);
            }

            //Agrego al nuevo DataTable las columnas 
            foreach (String  item in columnas)
            {
                DataColumn columnaDt = new DataColumn(item, typeof(string));
                columnaDt.MaxLength = 100; 
                dtNuevo.Columns.Add(item);
            }

            //Se recorre el DataTable original para llenar el nuevo DataTable
            foreach (DataRow fila in dataSet.Tables[0].Rows)
            {
                filaNueva = dtNuevo.NewRow();
                foreach (String nombreCol in columnas)
                {
                    String valor = fila[nombreCol].ToString().Trim();
                    filaNueva[nombreCol] = valor;
                }
                dtNuevo.Rows.Add(filaNueva);
                
            }

            return dtNuevo;

        }


        /// <summary>
        /// Metodo que toma el contenido del DataGridView de la pantalla y lo exporta a una Hoja de Excel
        /// </summary>
        public void ExportarAExcel()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                this._ventana.ExportarDataGrid();
                //this._ventana.Mensaje("Reporte generado", 1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorEjecucionReporte + ex.Message, true);
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

        public String ObtenerTituloReporte()
        {
            String tituloReporte = String.Empty;
        
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tituloReporte = this._reporte.Idioma.Id.Equals("ES") ? this._reporte.TituloEspanol : this._reporte.TituloIngles;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorEjecucionReporte + ex.Message, true);
            }

            return tituloReporte;

        }


        private void InspeccionarDataSet(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            int cont = 0;

            foreach (DataColumn columna in ds.Tables[0].Columns)
            {
                string nombreColumna = columna.ColumnName;
                System.Type tipo = columna.DataType;
            }
            
        }
    }
}
