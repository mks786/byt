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
        private DataTable _datosCrudos;
        private DataTable _SourceTable = new DataTable();
        private IEnumerable<DataRow> _Source = new List<DataRow>();
        private ListaDatosValores _ejeX, _ejeY, _ejeZ;
        private FiltroDataCruda _filtroDataCruda;
        private DataTable _dataPivot;


        /// <summary>
        /// Constructor por defecto que recibe el filtro, los ejes y una ventana Padre
        /// </summary>
        /// <param name="ventana"></param>
        /// <param name="filtro"></param>
        /// <param name="ejeX"></param>
        /// <param name="ejeY"></param>
        /// <param name="ejeZ"></param>
        /// <param name="ventanaPadre"></param>
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

                String ejeX = ((ListaDatosValores)this._ejeX).Descripcion;
                String ejeY = ((ListaDatosValores)this._ejeY).Descripcion;
                String ejeZ = ((ListaDatosValores)this._ejeZ).Descripcion;

                this._ventana.TotalHitsDetalle = "0";
                this._datosCrudos = this._seguimientoClientesServicios.ObtenerDataCruda(this._filtroDataCruda);
                this._SourceTable = this._datosCrudos;
                this._Source = this._SourceTable.Rows.Cast<DataRow>();
                
                DataTable pivotData = PivotData(ejeY, ejeZ, AggregateFunction.Sum, ejeX);

                DataTable pivotDataModificado = FormatearDataTable(pivotData);

                if (pivotDataModificado != null)
                {
                    if (pivotData.Rows.Count > 0)
                    {
                        this._ventana.Resultados = pivotDataModificado.DefaultView;
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
            double numero, subtotal = 0;

            nombreColumnaY = pivotData.Columns[0].ColumnName;
            DataTable datosFormateados = pivotData;
            DataTable datosTotalizados = TotalizarDatos(datosFormateados,nombreColumnaY);
            
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

            DataTable datosNuevos = CopiarDatos(datosTotalizados);

            
            
            foreach (DataRow fila in datosNuevos.Rows)
            {
                foreach (DataColumn columna in datosNuevos.Columns)
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

            //this._dataPivot = datosNuevos;

            return datosNuevos;
            //return datosFormateados;
        }



        private DataTable CopiarDatos(DataTable datosTotalizados)
        {
            String[] nombreColumnas = null;
            String cadena = String.Empty, nombreColumna = String.Empty, valorColumna = String.Empty;
            int contador = datosTotalizados.Columns.Count;
            DataTable newTable = new DataTable();

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

            return newTable;
        }

        
        /// <summary>
        /// Metodo que genera un nuevo DataTable con la columna TOTAL y organiza los datos en forma descendente
        /// </summary>
        /// <param name="datosFormateados">Datos pivot originales</param>
        /// <returns></returns>
        private DataTable TotalizarDatos(DataTable datosPivotOriginales, String nombreColumnaY)
        {

            String nombreColumna, valorColumna;
            float numero, subtotal = 0;
            
            datosPivotOriginales.Columns.Add("Total", typeof(float));

            //Calculando la sumatoria y colocandola en la columna Total
            foreach (DataRow fila in datosPivotOriginales.Rows)
            {
                foreach (DataColumn columna in datosPivotOriginales.Columns)
                {
                    nombreColumna = columna.ColumnName;
                    valorColumna = fila[nombreColumna].ToString();

                    if ((!nombreColumna.Equals(nombreColumnaY)) && (!nombreColumna.Equals("Total")) && (!valorColumna.Equals("")))
                    {
                        numero = float.Parse(valorColumna);
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
                this.Navegar(new ConsultarFacGestionesAsociado(asociado));

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
