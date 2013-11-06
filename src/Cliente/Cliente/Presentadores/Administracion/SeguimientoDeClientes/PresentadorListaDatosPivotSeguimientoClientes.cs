using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeClientes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeClientes
{
    class PresentadorListaDatosPivotSeguimientoClientes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IListaDatosPivotSeguimientoClientes _ventana;
        private ISeguimientoClientesServicios _seguimientoClientesServicios;
        private DataTable _datosCrudos;
        private DataTable _SourceTable = new DataTable();
        private IEnumerable<DataRow> _Source = new List<DataRow>();
        private ListaDatosValores _ejeX, _ejeY, _ejeZ;
        private FiltroDataCruda _filtroDataCruda;

        public PresentadorListaDatosPivotSeguimientoClientes (IListaDatosPivotSeguimientoClientes ventana, object filtro, object ejeX, object ejeY, object ejeZ, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ejeX = (ListaDatosValores)ejeX;
                this._ejeY = (ListaDatosValores)ejeY;
                this._ejeZ = (ListaDatosValores)ejeZ;
                this._filtroDataCruda = (FiltroDataCruda)filtro;

                this._seguimientoClientesServicios = (ISeguimientoClientesServicios)Activator.GetObject(typeof(ISeguimientoClientesServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoClientesServicios"]);


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

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                String ejeX = ((ListaDatosValores)this._ejeX).Descripcion;
                String ejeY = ((ListaDatosValores)this._ejeY).Descripcion;
                String ejeZ = ((ListaDatosValores)this._ejeZ).Descripcion;

                this._datosCrudos = this._seguimientoClientesServicios.ObtenerDataCruda(this._filtroDataCruda);
                this._SourceTable = this._datosCrudos;
                this._Source = this._SourceTable.Rows.Cast<DataRow>();
                
                DataTable pivotData = PivotData(ejeY, ejeZ, AggregateFunction.Sum, ejeX);

                DataTable pivotData1 = FormatearDataTable(pivotData);

                if (pivotData1 != null)
                {
                    if (pivotData.Rows.Count > 0)
                    {
                        this._ventana.Resultados = pivotData.DefaultView;
                        //this._ventana.FormatearDataGrid();
                        this._ventana.TotalHits = pivotData.Rows.Count.ToString();
                    }
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
            
        }

        
        /// <summary>
        /// Metodo que formatea el DataTable resultante de la tabla pivot para presentarlo en la interfaz
        /// </summary>
        /// <param name="pivotData">Data Pivot original</param>
        /// <returns>DataTable formateado</returns>
        private DataTable FormatearDataTable(DataTable pivotData)
        {
            DataTable datosFormateados = pivotData;
            String nombreColumna, valorColumna, nuevoValor, nombreColumnaY;
            nombreColumna = valorColumna = nuevoValor = nombreColumnaY = String.Empty;
            double numero;

            nombreColumnaY = pivotData.Columns[0].ColumnName;

            foreach (DataRow fila in pivotData.Rows)
            {
                foreach (DataColumn columna in datosFormateados.Columns)
                {
                    nombreColumna = columna.ColumnName;
                    valorColumna = fila[nombreColumna].ToString();

                    if ((!nombreColumna.Equals(nombreColumnaY)) && (!valorColumna.Equals("")))
                    {
                        numero = float.Parse(valorColumna);
                        nuevoValor = numero.ToString("N", CultureInfo.CreateSpecificCulture("de-DE"));
                        fila[nombreColumna] = nuevoValor;
                    }
                        
                }
            }

            return datosFormateados;
        }

        /// <summary>
        /// Metodo para saber si una cadena es numerica o no
        /// </summary>
        /// <returns>True si es numerica, False si es alfanumerica</returns>
        private bool VerificarCadenaNumerica(String cadena)
        {
            bool retorno = false;

            Regex patronNumerico = new Regex("[^0-9]");
            retorno = patronNumerico.IsMatch(cadena);


            return retorno;
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaResumenDatosSegClientes,
                Recursos.Ids.fac_SeguimientoClientes);
        }


        /// <summary>
        /// Metodo que genera una tabla pivot a partir de un DataTable
        /// </summary>
        /// <param name="rowField">Eje Y de la tabla Pivot</param>
        /// <param name="dataField">Datos numericos que se van a sumar</param>
        /// <param name="aggregate">Funcion de agregacion que se va a seleccionar para generar la tabla pivot</param>
        /// <param name="columnFields">Campo X que se va a tomar para el eje X</param>
        /// <returns>DataTable pivot</returns>
        public DataTable PivotData(string rowField, string dataField, AggregateFunction aggregate, params string[] columnFields)
        {
            DataTable dt = new DataTable();

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string Separator = ".";
                List<string> rowList = _Source.Select(x => x[rowField].ToString()).Distinct().ToList();
                // Gets the list of columns .(dot) separated.
                var colList = _Source.Select(x => (columnFields.Select(n => x[n]).Aggregate((a, b) => a += Separator + b.ToString())).ToString()).Distinct().OrderBy(m => m);

                dt.Columns.Add(rowField);
                foreach (var colName in colList)
                    dt.Columns.Add(colName);  // Cretes the result columns.//

                foreach (string rowName in rowList)
                {
                    DataRow row = dt.NewRow();
                    row[rowField] = rowName;
                    foreach (string colName in colList)
                    {
                        string strFilter = rowField + " = '" + rowName + "'";
                        string[] strColValues = colName.Split(Separator.ToCharArray(), StringSplitOptions.None);
                        for (int i = 0; i < columnFields.Length; i++)
                            strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                        row[colName] = GetData(strFilter, dataField, aggregate);
                    }
                    dt.Rows.Add(row);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return dt;
        }


        public DataTable PivotData(string rowField, string dataField, AggregateFunction aggregate, bool showSubTotal, params string[] columnFields)
        {
            DataTable dt = new DataTable();
            string Separator = ".";
            List<string> rowList = _Source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            List<string> colList = _Source.Select(x => columnFields.Aggregate((a, b) => x[a].ToString() + Separator + x[b].ToString())).Distinct().OrderBy(m => m).ToList();

            if (showSubTotal && columnFields.Length > 1)
            {
                string totalField = string.Empty;
                for (int i = 0; i < columnFields.Length - 1; i++)
                    totalField += columnFields[i] + "(Total)" + Separator;
                List<string> totalList = _Source.Select(x => totalField + x[columnFields.Last()].ToString()).Distinct().OrderBy(m => m).ToList();
                colList.InsertRange(0, totalList);
            }

            dt.Columns.Add(rowField);
            colList.ForEach(x => dt.Columns.Add(x));

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                row[rowField] = rowName;
                foreach (string colName in colList)
                {
                    string filter = rowField + " = '" + rowName + "'";
                    string[] colValues = colName.Split(Separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        if (!colValues[i].Contains("(Total)"))
                            filter += " and " + columnFields[i] + " = '" + colValues[i] + "'";
                    row[colName] = GetData(filter, dataField, colName.Contains("(Total)") ? AggregateFunction.Sum : aggregate);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// Retrives the data for matching RowField value and ColumnFields values with Aggregate function applied on them.
        /// </summary>
        /// <param name="Filter">DataTable Filter condition as a string</param>
        /// <param name="DataField">The column name which needs to spread out in Data Part of the Pivoted table</param>
        /// <param name="Aggregate">Enumeration to determine which function to apply to aggregate the data</param>
        /// <returns></returns>
        private object GetData(string Filter, string DataField, AggregateFunction Aggregate)
        {
            try
            {
                DataRow[] FilteredRows = _SourceTable.Select(Filter);
                object[] objList = FilteredRows.Select(x => x.Field<object>(DataField)).ToArray();

                switch (Aggregate)
                {
                    case AggregateFunction.Average:
                        return GetAverage(objList);
                    case AggregateFunction.Count:
                        return objList.Count();
                    case AggregateFunction.Exists:
                        return (objList.Count() == 0) ? "False" : "True";
                    case AggregateFunction.First:
                        return GetFirst(objList);
                    case AggregateFunction.Last:
                        return GetLast(objList);
                    case AggregateFunction.Max:
                        return GetMax(objList);
                    case AggregateFunction.Min:
                        return GetMin(objList);
                    case AggregateFunction.Sum:
                        return GetSum(objList);
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                return "#Error";
            }
        }

        private object GetAverage(object[] objList)
        {
            return objList.Count() == 0 ? null : (object)(Convert.ToDecimal(GetSum(objList)) / objList.Count());
        }
        private object GetSum(object[] objList)
        {
            return objList.Count() == 0 ? null : (object)(objList.Aggregate(new decimal(), (x, y) => x += Convert.ToDecimal(y)));
        }
        private object GetFirst(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.First();
        }
        private object GetLast(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Last();
        }
        private object GetMax(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Max();
        }
        private object GetMin(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Min();
        }



        /// <summary>
        /// Metodo que genera los datos de detalle
        /// </summary>
        /// <param name="datos">Datos que se usan para generar el Query en el servidor</param>
        public void CargarDatosDetalle(string datos)
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                DataTable datosDetalle = new DataTable();

                //datosDetalle = this._seguimientoClientesServicios.ObtenerDetalle(this._filtroDataCruda, datos);

                datosDetalle = this._seguimientoClientesServicios.ObtenerDetalle(this._ejeX, this._ejeY, datos);

                if (datosDetalle.Rows.Count > 0)
                {
                    this._ventana.ResultadosDetalle = datosDetalle.DefaultView;
                    this._ventana.VisibilidadListaDetalle();
                }
                else
                    this._ventana.Mensaje("No hay resultados para los datos seleccionados",0);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que exporta el contenido del Resumen y/o del Detalle a un archivo Excel
        /// </summary>
        /// <param name="tipo">Tipo de Reporte a exportar</param>
        public void ExportarExcel(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                this._ventana.ExportarDataGrid(tipo);
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

        public String ObtenerTituloReporte(string tipo)
        {
            String tituloReporte = String.Empty;

            if (tipo.Equals("Resumen"))
                tituloReporte = "Reporte Resumen de Datos";
            else if (tipo.Equals("Detalle"))
                tituloReporte = "Reporte Detalle de Facturación por Asociado";

            return tituloReporte;
        }
    }

    public enum AggregateFunction
    {
        Count = 1,
        Sum = 2,
        First = 3,
        Last = 4,
        Average = 5,
        Max = 6,
        Min = 7,
        Exists = 8
    }
}
