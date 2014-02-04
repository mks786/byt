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
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeClientes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeClientes
{
    class PresentadorListaDatosPivotSeguimientoClientes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IListaDatosPivotSeguimientoClientes _ventana;
        private ISeguimientoClientesServicios _seguimientoClientesServicios;
        private IAsociadoServicios _asociadoServicios;
        private IFacFacturaServicios _facFacturaServicios;
        private DataTable _datosCrudos, _datosCrudosModificados;
        private DataTable _SourceTable = new DataTable();
        private IEnumerable<DataRow> _Source = new List<DataRow>();
        private ListaDatosValores _ejeX, _ejeY, _ejeZ;
        private FiltroDataCruda _filtroDataCruda;
        private DataTable _dataPivot;
        private IList<String> _columnas = new List<String>();
        private IList<Decimal> _sumatorias = new List<Decimal>();
        private String _totalGlobalUSD, _totalGlobalBsF;

        /// <summary>
        /// Constructor por defecto que recibe el filtro, los ejes y una ventana Padre
        /// </summary>
        /// <param name="ventana"></param>
        /// <param name="filtro"></param>
        /// <param name="ejeX"></param>
        /// <param name="ejeY"></param>
        /// <param name="ejeZ"></param>
        /// <param name="ventanaPadre"></param>
        public PresentadorListaDatosPivotSeguimientoClientes (IListaDatosPivotSeguimientoClientes ventana, object filtro, object ejeX, object ejeY, object ejeZ, object totalGlobalUSD, object totalGlobalBSF, object ventanaPadre)
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
                this._totalGlobalBsF = totalGlobalBSF.ToString();
                this._filtroDataCruda = (FiltroDataCruda)filtro;

                this._seguimientoClientesServicios = (ISeguimientoClientesServicios)Activator.GetObject(typeof(ISeguimientoClientesServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoClientesServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facFacturaServicios = (IFacFacturaServicios)Activator.GetObject(typeof(IFacFacturaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacFacturaServicios"]);
                
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

                this._ventana.TotalHitsDetalle = "0";
                this._datosCrudos = this._seguimientoClientesServicios.ObtenerDataCruda(this._filtroDataCruda);

                CalcularTotales(this._datosCrudos);

                if (((ListaDatosValores)this._ejeY).Valor.Equals("XASOCIADO"))
                {
                    this._datosCrudosModificados = ModificarDatosCrudos(this._datosCrudos, ((ListaDatosValores)this._ejeY).Valor);
                    this._SourceTable = this._datosCrudosModificados;
                }
                else
                    this._SourceTable = this._datosCrudos;


                this._Source = this._SourceTable.Rows.Cast<DataRow>();
                
                DataTable pivotData = PivotData(ejeY, ejeZ, AggregateFunction.Sum, ejeX);

                DataTable pivotDataModificado = FormatearDataTable(pivotData);

                if (pivotDataModificado != null)
                {
                    if (pivotData.Rows.Count > 0)
                    {
                        this._ventana.Resultados = pivotDataModificado.DefaultView;
                        this._ventana.EjesResumen = ((ListaDatosValores)this._ejeY).Descripcion + " vs. " + ((ListaDatosValores)this._ejeX).Descripcion;
                        this._ventana.TotalHits = (pivotDataModificado.Rows.Count - 1).ToString();
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
        /// Metodo que calcula los totales en dolares y bolivares fuertes para mostrar en la pantalla del Resumen. 
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
                        if(columna.ColumnName.Equals("MONTO"))
                            //totalUSD += double.Parse(fila["MONTO"].ToString());
                            totalUSD += Decimal.Parse(fila["MONTO"].ToString());
                        else if (columna.ColumnName.Equals("MONTO_BF"))
                            //totalBSF += double.Parse(fila["MONTO_BF"].ToString());
                            totalBSF += Decimal.Parse(fila["MONTO_BF"].ToString());
                    }
                }

                this._ventana.TotalDolares = totalUSD.ToString("N");
                this._ventana.TotalBolivares = totalBSF.ToString("N");
                this._ventana.TotalGlobalDolares = Decimal.Parse(this._totalGlobalUSD).ToString("N");
                this._ventana.TotalGlobalBolivares = Decimal.Parse(this._totalGlobalBsF).ToString("N");

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
        /// Metodo que modifica la data cruda segun el campo Y indicado
        /// ESTE METODO SE USA PARA TRATAR LOS DATOS CRUDOS CUANDO EL CALCULO PIVOT DA ERROR EN ALGUNO DE SUS CAMPOS
        /// </summary>
        /// <param name="datosCrudosOriginales"></param>
        /// <param name="nombreCampo"></param>
        /// <returns></returns>
        private DataTable ModificarDatosCrudos(DataTable datosCrudosOriginales, String nombreCampo)
        {
            
            DataTable dataAux = datosCrudosOriginales;
            String valorColumna, valorModificado;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (nombreCampo.Equals("XASOCIADO"))
                {
                    foreach (DataRow fila in dataAux.Rows)
                    {
                        foreach (DataColumn columna in dataAux.Columns)
                        {
                            if (columna.ColumnName.Equals(nombreCampo))
                            {
                                valorColumna = fila[nombreCampo].ToString();
                                if (valorColumna.Contains("'"))
                                {
                                    valorModificado = valorColumna.Replace("'"," ");
                                    //valorModificado = valorColumna;
                                    fila[nombreCampo] = valorModificado;
                                }

                            }
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

            return dataAux;
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

                DataTable datosTotalizados = TotalizarDatos(datosFormateados, nombreColumnaY);

                #region CODIGO ORIGINAL COMENTADO - NO BORRAR
                /*datosFormateados.Columns.Add("Total", typeof(string));

            //Calculando la sumatoria y colocandola en la columna Total
            foreach (DataRow fila in datosFormateados.Rows)
            {
                foreach (DataColumn columna in datosFormateados.Columns)
                {
                    nombreColumna = columna.ColumnName;
                    valorColumna = fila[nombreColumna].ToString();

                    if ((!nombreColumna.Equals(nombreColumnaY)) && (!nombreColumna.Equals("Total")) && (!valorColumna.Equals("")))
                    {
                        numero = float.Parse(valorColumna);
                        subtotal += numero;
                        //nuevoValor = numero.ToString("N", CultureInfo.CreateSpecificCulture("de-DE"));
                        //fila[nombreColumna] = nuevoValor;
                    }

                }

                fila["Total"] = subtotal.ToString();
                //fila["Total"] = subtotal;
                subtotal = 0;

            }

            datosFormateados.DefaultView.Sort = "Total desc";
            DataView TableView = datosFormateados.DefaultView;
            DataTable newTable = TableView.ToTable();*/

                #endregion

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
                    cadena += datosTotalizados.Columns[i].ColumnName + "_";
                }

                nombreColumnas = cadena.Split('_');

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
                //float numero, subtotal = 0;
                //double numero = 0, subtotal = 0;
                decimal numero = 0, subtotal = 0;

                //datosPivotOriginales.Columns.Add("Total", typeof(float));
                //datosPivotOriginales.Columns.Add("Total", typeof(double));
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
                            //numero = double.Parse(valorColumna);
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

                datosDetalle = this._seguimientoClientesServicios.ObtenerDetalle(this._ejeX, this._ejeY, datos, this._filtroDataCruda);

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
                if (tipo.Equals("Resumen"))
                {
                    this._ventana.ExportarDataGrid(tipo, this._dataPivot);
                }
                else if (tipo.Equals("Detalle"))
                {
                    this._ventana.ExportarDataGrid("Detalle", null);
                }
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


        /// <summary>
        /// Metodo que consulta las Gestiones de Cobranza de un Asociado
        /// </summary>
        /// <param name="datos">ID del Asociado</param>
        public void ObtenerFacGestionesAsociado(string datos)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            int idAsociado;
            Asociado asociado = null;

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                idAsociado = int.Parse(datos);
                asociado = this._asociadoServicios.ConsultarAsociadoConTodo(new Asociado(idAsociado));
                this.Navegar(new ConsultarFacGestionesAsociado(asociado,this._ventana));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
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
        /// Metodo que consulta una factura seleccionada en el Detalle
        /// </summary>
        /// <param name="numeroFactura"></param>
        public void ConsultarFacFactura(string numeroFactura)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            int idFactura;
            FacFactura facturaAConsultar = new FacFactura();

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                idFactura = int.Parse(numeroFactura);
                facturaAConsultar.Id = idFactura;
                IList<FacFactura> facturas = this._facFacturaServicios.ObtenerFacFacturasFiltro(facturaAConsultar);

                if (facturas.Count > 0)
                {
                    FacFactura factura = facturas[0];
                    this.Navegar(new ConsultarFacFactura(factura));
                }
                else
                    this._ventana.Mensaje("La Factura seleccionada no existe. Acuda al Administrador del Sistema", 0);


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
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

                String datosPrimeraColumna = ObtenerDatosPrimeraColumna(tablaDatos,parametrosQuery[0]);

                parametros = datosPrimeraColumna + "_" + parametrosQuery[1];

                parametrosServicio = parametros.Split('_');

                resultado = this._seguimientoClientesServicios.ObtenerDetalleDeTotales(this._filtroDataCruda, this._ejeX, this._ejeY, parametrosServicio);

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




        private string ObtenerDatosPrimeraColumna(DataTable tablaDatos, string primeraColumna)
        {

            String retorno = String.Empty;
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
                    if (!primeraColumna.Equals("XASOCIADO"))
                    {
                        valor = fila[primeraColumna].ToString();
                    }
                    else
                    {
                        valor = fila[primeraColumna].ToString();

                        if (valor.Contains("-"))
                        {
                            arrAux = valor.Split('-');
                            aux = arrAux[0];
                            valor = String.Empty;
                            valor = aux;
                        }
                        
                    }
                    if (!valor.Equals("Totales Columna"))
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
