using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.ViGestionAsociados;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional
{
    class PresentadorListaDatosPivotSeguimientoCxPInternacional : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IListaDatosPivotSeguimientoCxPInternacional _ventana;
        private ISeguimientoCxPInternacionalServicios _seguimientoCxPInternacionalServicios;
        private IAsociadoServicios _asociadoServicios;
        private IFacInternacionalServicios _facInternacionalServicios;
        private DataTable _datosCrudos;
        private DataTable _SourceTable = new DataTable();
        private IEnumerable<DataRow> _Source = new List<DataRow>();
        private ListaDatosValores _ejeX, _ejeY, _ejeZ;
        private FiltroDataCrudaCxPInternacional _filtroDataCruda;
        private DataTable _dataPivot;
        private IList<String> _columnas = new List<String>();
        private IList<Decimal> _sumatorias = new List<Decimal>();
        private IList<FacInternacional> _facturasDetalle = new List<FacInternacional>();
        private String _totalGlobalUSD;

        /// <summary>
        /// Constructor predeterminado de la clase
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="filtro">Estructura de datos con los filtros utilizados para generar la data</param>
        /// <param name="ejeX">Eje X de filtrado</param>
        /// <param name="ejeY">Eje Y de filtrado</param>
        /// <param name="ejeZ">Eje Z de filtrado</param>
        /// <param name="totalGlobalUSD">Total de moneda internacional a mostrar en la ventana</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorListaDatosPivotSeguimientoCxPInternacional(IListaDatosPivotSeguimientoCxPInternacional ventana, 
                                                                     object filtro, 
                                                                     object ejeX, 
                                                                     object ejeY, 
                                                                     object ejeZ, 
                                                                     object totalGlobalUSD, 
                                                                     object ventanaPadre)
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
                this._totalGlobalUSD = totalGlobalUSD.ToString();
                this._filtroDataCruda = (FiltroDataCrudaCxPInternacional)filtro;

                this._seguimientoCxPInternacionalServicios = 
                    (ISeguimientoCxPInternacionalServicios)Activator.GetObject(typeof(ISeguimientoCxPInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoCxPInternacionalServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);
                
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
        /// Metodo que carga el contenido de la ventana
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

                ActualizarTitulo();

                String ejeX = ((ListaDatosValores)this._ejeX).Valor;
                String ejeY = ((ListaDatosValores)this._ejeY).Valor;
                String ejeZ = ((ListaDatosValores)this._ejeZ).Valor;

                this._filtroDataCruda.EjeX = _ejeX.Valor;
                this._filtroDataCruda.EjeY = _ejeY.Valor;


                this._ventana.TotalHitsDetalle = "0";

                this._datosCrudos = this._seguimientoCxPInternacionalServicios.ObtenerDataCruda(this._filtroDataCruda);

                CalcularTotales(this._datosCrudos);
                                
                this._SourceTable = this._datosCrudos;


                this._Source = this._SourceTable.Rows.Cast<DataRow>();

                DataTable pivotData = PivotData(ejeY, ejeZ, AggregateFunction.Sum, ejeX);

                DataTable pivotDataModificado = FormatearDataTable(pivotData);

                if (pivotDataModificado != null)
                {
                    if (pivotData.Rows.Count > 0)
                    {
                        this._ventana.Resultados = pivotDataModificado.DefaultView;
                        this._ventana.EjesResumen = 
                            ((ListaDatosValores)this._ejeY).Descripcion + " vs. " + ((ListaDatosValores)this._ejeX).Descripcion;
                        this._ventana.TotalHits = (pivotDataModificado.Rows.Count - 1).ToString();
                    }
                }

                FacInternacional facturaInternacionalAux = new FacInternacional();
                facturaInternacionalAux.RevisionAprobada = "SI";
                IList<FacInternacional> facturasInternacionales = 
                    this._facInternacionalServicios.ObtenerFacInternacionalesFiltro(facturaInternacionalAux);
                if (facturasInternacionales.Count > 0)
                {
                    this._ventana.HabilitarBotonVerSeleccion(true);
                    this._ventana.PintarConsolidar();
                }
                else
                    this._ventana.HabilitarBotonVerSeleccion(false);


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

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaResumenDatosSegCxPInternacional,
                Recursos.Ids.fac_SeguimientoCxPInternacional);
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
        /// Metodo que formatea el DataTable resultante de la tabla pivot para presentarlo en la interfaz
        /// Este metodo calcula los totales, ordena los totales de mayor a menor y luego les da el formato para que 
        /// se representen en el DataGrid de la interfaz
        /// </summary>
        /// <param name="pivotData">Data Pivot original</param>
        /// <returns>DataTable formateado</returns>
        private DataTable FormatearDataTable(DataTable pivotData)
        {
            String nombreColumna, valorColumna, nuevoValor, nombreColumnaY;
            nombreColumna = valorColumna = nuevoValor = nombreColumnaY = String.Empty;
            decimal numero, subtotal = 0;
            DataTable datosNuevos;


            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                nombreColumnaY = pivotData.Columns[0].ColumnName;
                DataTable datosFormateados = pivotData;

                //SE CALCULAN TOTALES POR FILAS
                DataTable datosTotalizados = TotalizarDatos(datosFormateados, nombreColumnaY);

                
                //GENERAR DATATABLE CON TODO EN STRING PARA LLEVARLO AL DATAGRID
                this._dataPivot = datosTotalizados;

                //SE CALCULAN LOS TOTALES POR COLUMNA
                CalcularTotalesPorColumna(datosTotalizados);

                datosNuevos = CopiarDatos(datosTotalizados);



                foreach (DataRow fila in datosNuevos.Rows)
                {
                    foreach (DataColumn columna in datosNuevos.Columns)
                    {
                        nombreColumna = columna.ColumnName;
                        valorColumna = fila[nombreColumna].ToString();

                        if ((!nombreColumna.Equals(nombreColumnaY)) && (!valorColumna.Equals("")))
                        {
                            numero = Decimal.Parse(valorColumna);
                            nuevoValor = numero.ToString("N", CultureInfo.CreateSpecificCulture("de-DE"));
                            fila[nombreColumna] = nuevoValor;
                        }

                    }
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

            return datosNuevos;
            //return datosFormateados;
        }


        /// <summary>
        /// Metodo que genera un nuevo DataTable con la columna TOTAL y organiza los datos en forma descendente, tomando en cuenta las
        /// cantidades de la columna TOTAL para organizar los datos. 
        /// </summary>
        /// <param name="datosFormateados">Datos pivot originales</param>
        /// <returns>DataTable con la una nueva columna TOTAL con la sumatoria por columna de las cantidades calculadas en el pivot original y ordenadas dichas cantidades de mayor a menor</returns>
        private DataTable TotalizarDatos(DataTable datosPivotOriginales, String nombreColumnaY)
        {

            try
            {
                String nombreColumna, valorColumna;
                decimal numero = 0, subtotal = 0;

                
                datosPivotOriginales.Columns.Add("Total", typeof(Decimal));

                //Calculando la sumatoria y colocandola en la columna Total
                foreach (DataRow fila in datosPivotOriginales.Rows)
                {
                    foreach (DataColumn columna in datosPivotOriginales.Columns)
                    {
                        nombreColumna = columna.ColumnName;
                        valorColumna = fila[nombreColumna].ToString();

                        if ((!nombreColumna.Equals(nombreColumnaY)) && (!nombreColumna.Equals("Total")) && (!valorColumna.Equals("")))
                        {
                            numero = Decimal.Parse(valorColumna);
                            subtotal += numero;
                        }

                    }

                    fila["Total"] = subtotal;
                    subtotal = 0;

                }

                datosPivotOriginales.DefaultView.Sort = "Total desc";
                DataView TableView = datosPivotOriginales.DefaultView;
                DataTable newTable = TableView.ToTable();

                return newTable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para calcular los totales por columna en el DataTable modificado
        /// </summary>
        /// <param name="datosTotalizados">DataTable modificado</param>
        /// <returns>DataTable con los totales por filas y por columnas</returns>
        private void CalcularTotalesPorColumna(DataTable datosTotalizados)
        {
            String nombrePrimeraColumna = String.Empty, nombreColumna = String.Empty;
            //double sumatoria = 0;
            decimal sumatoria = 0;



            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                nombrePrimeraColumna = datosTotalizados.Columns[0].ColumnName;

                foreach (DataColumn columna in datosTotalizados.Columns)
                {
                    nombreColumna = columna.ColumnName;
                    if (!nombreColumna.Equals(nombrePrimeraColumna))
                    {
                        this._columnas.Add(nombreColumna);
                        foreach (DataRow fila in datosTotalizados.Rows)
                        {
                            String dato = fila[nombreColumna].ToString();
                            if ((dato != null) && (!dato.Equals("")))
                                //sumatoria += double.Parse(fila[nombreColumna].ToString());
                                sumatoria += Decimal.Parse(fila[nombreColumna].ToString());
                        }
                        this._sumatorias.Add(sumatoria);
                        sumatoria = 0;
                    }

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


        }


        /// <summary>
        /// Metodo que genera un nuevo DataTable con todos los datos formateados para luego ser presentado en la interfaz
        /// </summary>
        /// <param name="datosTotalizados">DataTable con todos los datos generados</param>
        /// <returns>DataTable nuevo con los datos debidamente formateados y listo para ser presentado en la interfaz</returns>
        private DataTable CopiarDatos(DataTable datosTotalizados)
        {
            String[] nombreColumnas = null;
            String cadena = String.Empty, nombreColumna = String.Empty, valorColumna = String.Empty, primeraColumna = String.Empty;
            int contador = datosTotalizados.Columns.Count;
            int posicion = 0;
            DataTable newTable = new DataTable();

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                for (int i = 0; i < contador; i++)
                {
                    cadena += datosTotalizados.Columns[i].ColumnName + "&";
                }

                nombreColumnas = cadena.Split('&');

                for (int i = 0; i < nombreColumnas.Length; i++)
                {
                    if (!nombreColumnas[i].Equals(""))
                    {
                        newTable.Columns.Add(nombreColumnas[i], typeof(string));
                    }
                }

                foreach (DataRow fila in datosTotalizados.Rows)
                {
                    DataRow nuevaFila = newTable.NewRow();

                    foreach (DataColumn columna in datosTotalizados.Columns)
                    {
                        nombreColumna = columna.ColumnName;
                        valorColumna = fila[nombreColumna].ToString();
                        nuevaFila[nombreColumna] = valorColumna;

                    }

                    newTable.Rows.Add(nuevaFila);
                }

                primeraColumna = newTable.Columns[0].ColumnName;

                //inserto una nueva fila con los totales
                DataRow filaNueva = newTable.NewRow();

                //filaNueva["CASOCIADO"] = "Totales Periodo";
                filaNueva[primeraColumna] = "Totales Columna";

                foreach (String nombreDeColumna in this._columnas)
                {
                    String valor = this._sumatorias[posicion].ToString();
                    filaNueva[nombreDeColumna] = valor;
                    posicion++;
                }

                newTable.Rows.Add(filaNueva);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return newTable;
        }


        /// <summary>
        /// Metodo que calcula los totales para mostrar en la pantalla del Resumen. 
        /// ESTOS TOTALES SE SACAN DE LA DATA CRUDA TOMANDO EN CUENTA LOS FILTROS
        /// </summary>
        /// <param name="dataTable">DataTable con los datos crudos</param>
        private void CalcularTotales(DataTable datosCrudos)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //double totalUSD = 0, totalBSF = 0;
                decimal totalUSD = 0, totalBSF = 0;

                foreach (DataRow fila in datosCrudos.Rows)
                {
                    foreach (DataColumn columna in datosCrudos.Columns)
                    {
                        if (columna.ColumnName.Equals("MONTO"))
                            //totalUSD += double.Parse(fila["MONTO"].ToString());
                            totalUSD += Decimal.Parse(fila["MONTO"].ToString());
                        //else if (columna.ColumnName.Equals("MONTO_BF"))
                        //    //totalBSF += double.Parse(fila["MONTO_BF"].ToString());
                        //    totalBSF += Decimal.Parse(fila["MONTO_BF"].ToString());
                    }
                }

                this._ventana.TotalDolares = totalUSD.ToString("N");
                //this._ventana.TotalBolivares = totalBSF.ToString("N");
                this._ventana.TotalGlobalDolares = Decimal.Parse(this._totalGlobalUSD).ToString("N");
                //this._ventana.TotalGlobalBolivares = Decimal.Parse(this._totalGlobalBsF).ToString("N");

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

                datosDetalle = this._seguimientoCxPInternacionalServicios.ObtenerDetalle(this._ejeX, this._ejeY, datos, this._filtroDataCruda);

                this._facturasDetalle = GetListFactInternacionalDetalle(datosDetalle);

                if (datosDetalle.Rows.Count > 0)
                {
                    this._ventana.TotalHitsDetalle = datosDetalle.Rows.Count.ToString();
                    this._ventana.ResultadosDetalle = datosDetalle.DefaultView;
                    this._ventana.VisibilidadListaDetalle();
                }
                else
                {
                    this._ventana.TotalHitsDetalle = "0";
                    this._ventana.Mensaje("No hay resultados para los datos seleccionados", 0);
                }


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
        /// Metodo que actualiza el campo ISEL de las proformas seleccionadas en el detalle para que sean consolidadas
        /// <param name="idsFacturasSeleccionadas">Codigos de las facturas seleccionadas en la ventana</param>
        /// </summary>
        public void MarcarProformasSeleccionadas(IList<String> idsFacturasSeleccionadas)
        {
            String idsFacInternacionales = String.Empty;
            //String[] idFacInternacionales = null;
            IList<FacInternacional> proformasInternacionales = new List<FacInternacional>();
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                DesmarcarTodasFacturas(this._facturasDetalle);

                if (idsFacturasSeleccionadas.Count > 0)
                {
                    foreach (String item in idsFacturasSeleccionadas)
                    {
                        FacInternacional facInternacionalAux = new FacInternacional();
                        facInternacionalAux.Id = int.Parse(item);
                        IList<FacInternacional> resultado =
                            this._facInternacionalServicios.ObtenerFacInternacionalesFiltro(facInternacionalAux);
                        proformasInternacionales.Add(resultado[0]);
                    }

                    foreach (FacInternacional factura in proformasInternacionales)
                    {
                        factura.BIsel = true;
                        exitoso = this._facInternacionalServicios.InsertarOModificar(factura, UsuarioLogeado.Hash);
                        if (exitoso)
                        {
                            exitoso = false;
                            continue;
                        }
                        else
                        {
                            this._ventana.Mensaje("Ocurrio un error actualizando la Proforma " + factura.Id.ToString(), 1);
                            break;
                        }

                    }

                    this._ventana.HabilitarBotonVerSeleccion(true);
                    this._ventana.Mensaje("Proformas han sido seleccionadas para consolidar", 2);
                }
                else
                {
                    this._ventana.Mensaje("No hay Proformas para consolidar", 2);
                    this._ventana.HabilitarBotonVerSeleccion(false);
                }

                #region CODIGO ORIGINAL COMENTADO
                //idsFacInternacionales = this._ventana.ObtenerIdsFacInternacional();



                /*if (!string.IsNullOrEmpty(idsFacInternacionales))
                 {
                    idFacInternacionales = idsFacInternacionales.Split(',');

                    foreach (String item in idFacInternacionales)
                    {
                        FacInternacional facInternacionalAux = new FacInternacional();
                        facInternacionalAux.Id = int.Parse(item);
                        IList<FacInternacional> resultado =
                            this._facInternacionalServicios.ObtenerFacInternacionalesFiltro(facInternacionalAux);
                        proformasInternacionales.Add(resultado[0]);
                    }

                    foreach (FacInternacional factura in proformasInternacionales)
                    {
                        factura.BIsel = true;
                        exitoso = this._facInternacionalServicios.InsertarOModificar(factura, UsuarioLogeado.Hash);
                        if (exitoso)
                        {
                            exitoso = false;
                            continue;
                        }
                        else
                        {
                            this._ventana.Mensaje("Ocurrio un error actualizando la Proforma " + factura.Id.ToString(), 1);
                            break;
                        }
                           
                    }

                    this._ventana.Mensaje("Han sido marcadas Proformas para consolidar", 2);

                    //this.Navegar(new FacInternacionalAprobadas(proformasInternacionales,this._ventana));


                }
                else
                    this._ventana.Mensaje("Debe seleccionar al menos una Proforma", 1);*/
                
                #endregion


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
        }


        /// <summary>
        /// Metodo que desmarca todas las FacInternacional que se encuentran en el detalle de la pivot para luego ser marcadas de nuevo
        /// </summary>
        /// <param name="facturasDetalle">Facturas obtenidas en el detalle</param>
        private void DesmarcarTodasFacturas(IList<FacInternacional> facturasDetalle)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacInternacional item in facturasDetalle)
                {
                    item.RevisionAprobada = "NO";
                    exitoso = this._facInternacionalServicios.InsertarOModificar(item, UsuarioLogeado.Hash);
                    if (exitoso)
                    {
                        exitoso = false;
                        continue;
                    }
                    else
                    {
                        this._ventana.Mensaje("Hubo un problema actualizando el campo ISEL de la Factura : " + item.Id.ToString(), 0);
                        break;
                    }

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
        }


        /// <summary>
        /// Metodo para obtener el detalle por columna 
        /// </summary>
        /// <param name="parametrosQuery">Parametros a utilizar en el query</param>
        /// <param name="tablaDatos">Datos del DataGridView</param>
        public void ObtenerDetallesPorColumna(string[] parametrosQuery, DataTable tablaDatos)
        {
            DataTable resultado;
            String parametros = String.Empty;
            String[] parametrosServicio = null;

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String datosPrimeraColumna = ObtenerDatosPrimeraColumna(tablaDatos, parametrosQuery[0]);

                parametros = datosPrimeraColumna + "_" + parametrosQuery[1];

                parametrosServicio = parametros.Split('_');

                resultado = this._seguimientoCxPInternacionalServicios.ObtenerDetalleDeTotales(this._filtroDataCruda, this._ejeX, this._ejeY, parametrosServicio);

                this._facturasDetalle = GetListFactInternacionalDetalle(resultado);

                if (resultado.Rows.Count > 0)
                {
                    this._ventana.TotalHitsDetalle = resultado.Rows.Count.ToString();
                    this._ventana.ResultadosDetalle = resultado.DefaultView;
                    this._ventana.VisibilidadListaDetalle();
                }
                else
                {
                    this._ventana.TotalHitsDetalle = "0";
                    this._ventana.Mensaje("No hay resultados para los datos seleccionados", 0);
                }

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
        /// Metodo que obtiene la lista de Facturas internacionales del detalle a partir del dataTable sobre la vista FAC_CXP_INT_VI
        /// </summary>
        /// <param name="resultado">DataTable con el resultado de la consulta</param>
        /// <returns>Lista de FacInternacional resultado de la consulta</returns>
        private IList<FacInternacional> GetListFactInternacionalDetalle(DataTable resultado)
        {

            IList<FacInternacional> facturasDetalle = new List<FacInternacional>();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (DataRow fila in resultado.Rows)
                {
                    int codigoFactura = int.Parse(fila["CPROFORMA"].ToString());
                    FacInternacional facIntAux = new FacInternacional();
                    facIntAux.Id = codigoFactura;
                    IList<FacInternacional> facturas = this._facInternacionalServicios.ObtenerFacInternacionalesFiltro(facIntAux);
                    if (facturas.Count > 0)
                        facturasDetalle.Add(facturas[0]);
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

            return facturasDetalle;
        }

        /// <summary>
        /// Metodo que extrae los datos para la consulta de la primera columna para extraer los valores de la misma (Asociados_O)
        /// </summary>
        /// <param name="tablaDatos">Datos del DataGridView </param>
        /// <param name="primeraColumna">Nombre de la primera columna que se usara en la consulta</param>
        /// <returns>String con los codigos de los asociados a consultar</returns>
        private string ObtenerDatosPrimeraColumna(DataTable tablaDatos, string primeraColumna)
        {

            String retorno = String.Empty, temp = String.Empty;
            String cadena = String.Empty, valor = String.Empty, aux = String.Empty;
            int contador = 1;
            int cantidadRegistros = tablaDatos.Rows.Count;
            String[] arrAux = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (DataRow fila in tablaDatos.Rows)
                {
                    if (!primeraColumna.Equals("XASOCIADO1"))
                    {
                        valor = fila[primeraColumna].ToString();
                    }
                    else
                    {
                        
                        temp = fila[primeraColumna].ToString();

                        if(VerificarCadenaNumerica(temp))
                            valor = fila[primeraColumna].ToString();
                        else
                            valor = "'" + fila[primeraColumna].ToString() + "'";
                        
                        if ((valor.Contains("-")) && (!primeraColumna.Equals("XASOCIADO1")))
                        {
                            arrAux = valor.Split('-');
                            aux = arrAux[0];
                            valor = String.Empty;
                            valor = aux;
                        }

                    }

                    //if (!valor.Equals("Totales Columna"))
                    if(!valor.Contains("Totales Columna"))
                    {
                        if (contador != cantidadRegistros - 1)
                            cadena += valor + ",";
                        else
                            cadena += valor;
                    }
                    contador++;
                }

                retorno = cadena;

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


            return retorno;
        }

        /// <summary>
        /// Metodo que exporta el contenido del Resumen Pivot de Data cruda a un reporte Excel
        /// </summary>
        /// <param name="tipo">Tipo de Reporte a exportar</param>
        public void ExportarExcel(string tipo)
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (tipo.Equals("Resumen"))
                {
                    this._ventana.ExportarDataGrid(tipo, this._dataPivot);
                }
                else if (tipo.Equals("Detalle"))
                {
                    this._ventana.ExportarDataGrid("Detalle", null);
                }

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


        public String ObtenerTituloReporte(string tipo)
        {
            String tituloReporte = String.Empty;

            if (tipo.Equals("Resumen"))
                tituloReporte = "Reporte Resumen CxP Internacional";
            else if (tipo.Equals("Detalle"))
                tituloReporte = "Reporte Detalle de Facturación CxP Internacional";

            return tituloReporte;
        }


        /// <summary>
        /// Metodo que llama a la ventana que carga todas las proformas con campo ISEL = SI
        /// </summary>
        public void CargarProformasInternacionalesMarcadas()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FacInternacional facturaAux = new FacInternacional();
                facturaAux.RevisionAprobada = "SI";

                IList<FacInternacional> facturasAprobadasConsolidar = this._facInternacionalServicios.ObtenerFacInternacionalesFiltro(facturaAux);

                if (facturasAprobadasConsolidar.Count > 0)
                {
                    this.Navegar(new FacInternacionalAprobadas(facturasAprobadasConsolidar, this._ventana));
                }
                else
                    this._ventana.Mensaje("Debe generar el detalle y seleccionar las Facturas que desea consolidar",0);



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
        /// Metodo para saber si una cadena es numerica o no
        /// </summary>
        /// <returns>True si es numerica, False si es alfanumerica</returns>
        private bool VerificarCadenaNumerica(String cadena)
        {
            bool retorno = false;

            Regex patronNumerico = new Regex("[^0-9]");
            retorno = !patronNumerico.IsMatch(cadena);


            return retorno;
        }


        /// <summary>
        /// Metodo que obtiene el nombre del eje Y seleccionado
        /// </summary>
        /// <returns>Nombre del eje Y seleccionado</returns>
        public string ObtenerEjeYSeleccionado()
        {
            String retorno = String.Empty;

            if (this._ejeY != null)
            {
                ListaDatosValores ejeSeleccionado = (ListaDatosValores)this._ejeY;
                retorno = ejeSeleccionado.Descripcion;
            }

            return retorno;
        }
    }

    //Enumeracion para seleccionar la fucion de agregacion para generar la data pivot
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
