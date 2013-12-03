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
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacGestiones;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.ViGestionAsociados;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeCobranzas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeCobranzas
{
    class PresentadorListaDatosPivotSeguimientoCobranzas : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IListaDatosPivotSeguimientoCobranzas _ventana;
        private ISeguimientoDeCobranzasServicios _seguimientoDeCobranzasServicios;
        private IAsociadoServicios _asociadoServicios;
        private IFacGestionServicios _facGestionServicios;
        private DataTable _datosCrudos;
        private DataTable _SourceTable = new DataTable();
        private IEnumerable<DataRow> _Source = new List<DataRow>();
        private ListaDatosValores _ejeX, _ejeY, _ejeZ;
        private FiltroDataCrudaCobranza _filtroDataCruda;
        private DataTable _dataPivot;
        private IList<String> _columnas = new List<String>();
        private IList<Decimal> _sumatorias = new List<Decimal>();

        public PresentadorListaDatosPivotSeguimientoCobranzas(IListaDatosPivotSeguimientoCobranzas ventana, object filtro, object ejeX, object ejeY, object ejeZ, object ventanaPadre)
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
                this._filtroDataCruda = (FiltroDataCrudaCobranza)filtro;

                this._seguimientoDeCobranzasServicios = (ISeguimientoDeCobranzasServicios)Activator.GetObject(typeof(ISeguimientoDeCobranzasServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoDeCobranzasServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
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

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                this._ventana.TotalHitsDetalle = "0";
                String ejeX = ((ListaDatosValores)this._ejeX).Valor;
                String ejeY = ((ListaDatosValores)this._ejeY).Valor;
                String ejeZ = ((ListaDatosValores)this._ejeZ).Valor;

                this._datosCrudos = this._seguimientoDeCobranzasServicios.ObtenerDataCruda(this._filtroDataCruda);
                this._SourceTable = this._datosCrudos;
                this._Source = this._SourceTable.Rows.Cast<DataRow>();

                DataTable pivotData = PivotData(ejeY, ejeZ, AggregateFunction.Count, ejeX);

                DataTable pivotDataModificado = ModificarDataTable(pivotData);

                if (pivotDataModificado != null)
                {
                    if (pivotDataModificado.Rows.Count > 0)
                    {
                        this._ventana.Resultados = pivotDataModificado.DefaultView;
                        this._ventana.EjesResumen = ((ListaDatosValores)this._ejeY).Descripcion + " vs. " + ((ListaDatosValores)this._ejeX).Descripcion;
                        this._ventana.TotalHits = pivotDataModificado.Rows.Count.ToString();
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


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaResumenDatosSegCobranza,
                Recursos.Ids.fac_SeguimientoCobranza);
        }

        /// <summary>
        /// Metodo que modifica la estructura del DataTable original que contiene la data pivot
        /// Incluye la columna Total (Horizontal y Vertical)
        /// </summary>
        /// <param name="datosOriginales"></param>
        /// <returns></returns>
        private DataTable ModificarDataTable(DataTable datosOriginales)
        {

            DataTable datosModificados = new DataTable();
            String nombreColumnaY = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                nombreColumnaY = datosOriginales.Columns[0].ColumnName;
                datosModificados = TotalizarDatos(datosOriginales,nombreColumnaY);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return datosModificados;
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


                CalcularTotalesPorColumna(newTable);

                DataTable dataTotalizada = CopiarDatos(newTable);

                return dataTotalizada;

                //return newTable;
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
                    cadena += datosTotalizados.Columns[i].ColumnName + "*";
                }

                nombreColumnas = cadena.Split('*');

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

                DataRow filaNueva = newTable.NewRow();

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
        /// Metodo que permite exportar el contenido de los datagrid de Resumen y Detalle a Excel
        /// </summary>
        /// <param name="tipo">Tipo de contenido a exportar</param>
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


        /// <summary>
        /// Metodo que asigna el titulo del Reporte en Excel segun el tipo de contenido a exportar
        /// </summary>
        /// <param name="tipo">Tipo de contenido a exportar</param>
        /// <returns></returns>
        public String ObtenerTituloReporte(string tipo)
        {
            String tituloReporte = String.Empty;

            if (tipo.Equals("Resumen"))
                tituloReporte = "Resumen Cuantitativo";
            else if (tipo.Equals("Detalle"))
                tituloReporte = "Reporte Detalle de Gestiones de Cobranza por Asociado";

            return tituloReporte;
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

                FiltroDataCrudaCobranza filtroDetalle = this._filtroDataCruda;

                filtroDetalle.EjeX = this._ejeX.Valor;

                filtroDetalle.EjeY = this._ejeY.Valor;

                datosDetalle = this._seguimientoDeCobranzasServicios.ObtenerDetalle(filtroDetalle, datos); 

                if (datosDetalle.Rows.Count > 0)
                {
                    this._ventana.TotalHitsDetalle = datosDetalle.Rows.Count.ToString();
                    this._ventana.ResultadosDetalle = datosDetalle.DefaultView;
                    this._ventana.VisibilidadListaDetalle();
                }
                else
                    this._ventana.Mensaje("No hay resultados para los datos seleccionados", 0);


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
        /// Metodo que lanza un mensaje de error cuando la actividad es invalida o no puede realizarse
        /// </summary>
        public void LanzarAvisoError()
        {
            this._ventana.Mensaje("No se puede realizar la operacion, consulte con su administrador", 0);
        }


        /// <summary>
        /// Metodo que llama a la ventana para poder consultar una gestion de cobranza seleccionada en el grid de Detalle
        /// </summary>
        /// <param name="codigoAsociado"></param>
        /// <param name="numeroGestion"></param>
        public void ConsultarFacGestion(string codigoAsociado, string numeroGestion)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            int idAsociado, idFacGestion;
            FacGestion facGestion = new FacGestion();

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                idAsociado = int.Parse(codigoAsociado);
                idFacGestion = int.Parse(numeroGestion);
                Asociado facGestionAsociado = new Asociado();
                facGestionAsociado.Id = idAsociado;

                Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo(facGestionAsociado);
                facGestion.Asociado = asociado;
                facGestion.Id = idFacGestion;


                IList<FacGestion> gestiones = this._facGestionServicios.ObtenerFacGestionesFiltro(facGestion);

                if (gestiones.Count > 0)
                {
                    FacGestion gestionEncontrada = gestiones[0];
                    this.Navegar(new ConsultarFacGestion(gestionEncontrada));
                }
                else
                    this._ventana.Mensaje("La Gestión seleccionada no existe. Acuda al Administrador del Sistema", 0);


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
        /// Metodo que usa los parametros obtenidos en la interfaz para hacer la consulta de detalles por columna
        /// </summary>
        /// <param name="parametrosQuery"></param>
        /// <param name="tablaDatos"></param>
        public void ObtenerDetallesPorColumna(string[] parametrosQuery, DataTable tablaDatos)
        {

            DataTable resultado;
            String parametros = String.Empty, datosPrimeraColumna=String.Empty;
            String[] parametrosServicio = null;

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                datosPrimeraColumna = ObtenerDatosPrimeraColumna(tablaDatos, parametrosQuery[0]);

                parametros = datosPrimeraColumna + "*" + parametrosQuery[1];

                parametrosServicio = parametros.Split('*');

                resultado = this._seguimientoDeCobranzasServicios.ObtenerDetalleDeTotales(this._filtroDataCruda, this._ejeX, this._ejeY, parametrosServicio);

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
            catch (Exception ex)
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
        /// Metodo que extrae los valores a filtrar del eje Y cuando se consulta el detalle por columna
        /// </summary>
        /// <param name="tablaDatos">DataTable con los datos</param>
        /// <param name="primeraColumna">Columna que representa el eje Y con sus datos</param>
        /// <returns>Conjunto de valores del eje Y que se usaran para filtrar en la consulta del detalle por columna</returns>
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
                    //if (!primeraColumna.Equals("XASOCIADO"))
                    //{
                    valor = fila[primeraColumna].ToString();
                    

                    //}
                    //else
                    //{
                    //    valor = fila[primeraColumna].ToString();

                    //    if (valor.Contains("-"))
                    //    {
                    //        arrAux = valor.Split('-');
                    //        aux = arrAux[0];
                    //        valor = String.Empty;
                    //        valor = aux;
                    //    }

                    //}
                    if (!valor.Equals("Totales Columna"))
                    {
                        if (contador != cantidadRegistros - 1)
                            if(VerificarCadenaNumerica(valor))
                                cadena += valor + "%";
                            else
                                cadena += "'" + valor + "'%";
                        else
                            if(VerificarCadenaNumerica(valor))
                                cadena += valor;
                            else
                                cadena += "'"+valor+"'";
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
