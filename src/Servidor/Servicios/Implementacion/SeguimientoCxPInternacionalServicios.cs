using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using NLog;
using Oracle.DataAccess.Client;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Text;
using System.Text.RegularExpressions;


namespace Trascend.Bolet.Servicios.Implementacion
{
    public class SeguimientoCxPInternacionalServicios: MarshalByRefObject, ISeguimientoCxPInternacionalServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Servicios

        /// <summary>
        /// Servicio que obtiene el Resumen General de Saldos de CxP Internacional de la vista FAC_ASO_SALDO_CXP
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <returns>DataTable con el Resumen General de Saldo de CxP Internacional</returns>
        public DataTable ObtenerResumenGeneralCxPInternacional(FiltroDataCrudaCxPInternacional filtro)
        {
            DataTable datos = new DataTable();

            #region Datos de Conexion a la Base de Datos

            String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
            String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
            String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
            String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
            String query = String.Empty;

            #endregion

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                query = GenerarQueryResumenGeneral(filtro);

                OracleConnection con = new OracleConnection(connectionString);
                con.Open();
                if (con.State.ToString().Equals("Open"))
                {
                    OracleDataAdapter oracleAdapter = new OracleDataAdapter(query, con);
                    oracleAdapter.Fill(datos);
                    oracleAdapter.Dispose();
                    con.Close();
                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }

            return datos;
        }


        /// <summary>
        /// Servicio que obtiene la data cruda para generar la tabla pivot (Tabla Resumen de Datos) a partir de la vista 
        /// FAC_CXP_INT_VI
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <returns>DataTable con la data necesaria para generar la Tabla de Resumen de Datos</returns>
        public DataTable ObtenerDataCruda(FiltroDataCrudaCxPInternacional filtro)
        {
            DataTable datos = new DataTable();

            #region Datos de Conexion a la Base de Datos

            String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
            String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
            String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
            String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
            String query = String.Empty;

            #endregion

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                OracleConnection con = new OracleConnection(connectionString);
                query = GenerarQueryDataCruda(filtro);
                con.Open();


                if (con.State.ToString().Equals("Open"))
                {

                    OracleDataAdapter oracleAdapter = new OracleDataAdapter(query, con);
                    oracleAdapter.Fill(datos);
                    oracleAdapter.Dispose();
                    con.Close();
                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }

            return datos;
        }


        /// <summary>
        /// Servicio que presenta el detalle de una celda seleccionada con las CxP Internacional
        /// </summary>
        /// <param name="ejeX">Parametro X a consultar</param>
        /// <param name="ejeY">Parametro Y a consultar</param>
        /// <param name="cadenaFiltroDetalle">Datos a filtrar en la consulta</param>
        /// <returns>DataTable con el detalle de lo seleccionado</returns>
        public DataTable ObtenerDetalle(ListaDatosValores ejeX, ListaDatosValores ejeY, String cadenaFiltroDetalle, FiltroDataCrudaCxPInternacional filtro)
        {
            DataTable retorno = new DataTable();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                #region Datos de Conexion a la Base de Datos

                String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
                String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
                String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
                String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
                String query = String.Empty;

                #endregion

                OracleConnection con = new OracleConnection(connectionString);
                con.Open();

                if (con.State.ToString().Equals("Open"))
                {
                    string campoX = ejeX.Valor;
                    string campoY = ejeY.Valor;
                    filtro.EjeX = campoX;
                    filtro.EjeY = campoY;

                    query = GenerarQueryDetalle(filtro, cadenaFiltroDetalle, true);
                    OracleDataAdapter oracleAdapter = new OracleDataAdapter(query, con);
                    oracleAdapter.Fill(retorno);
                    oracleAdapter.Dispose();
                    con.Close();
                }


                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }

            return retorno;
        }

        
        /// <summary>
        /// Servicio que obtiene los datos de detalle de los totales POR COLUMNA
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <param name="ejeX">Eje X implementado para la consulta de la data cruda</param>
        /// <param name="ejeY">Eje Y implementado para la consulta de la data cruda</param>
        /// <param name="parametros">Parametros necesarios para el WHERE en la consulta</param>
        /// <returns>DataTable con los datos de detalles por totales verticales</returns>
        public DataTable ObtenerDetalleDeTotales(FiltroDataCrudaCxPInternacional filtro,
                                                 ListaDatosValores ejeX,
                                                 ListaDatosValores ejeY,
                                                 String[] parametros)
        {

            DataTable retorno = new DataTable();
            #region Datos de Conexion a la Base de Datos

            String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
            String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
            String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
            String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
            String query = String.Empty;

            #endregion

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string campoX = ejeX.Valor;
                string campoY = ejeY.Valor;
                filtro.EjeX = campoX;
                filtro.EjeY = campoY;

                query = GenerarQueryDetalleTotales(filtro, parametros);

                OracleConnection con = new OracleConnection(connectionString);
                con.Open();

                if (con.State.ToString().Equals("Open"))
                {
                    OracleDataAdapter oracleAdapter = new OracleDataAdapter(query, con);
                    oracleAdapter.Fill(retorno);
                    oracleAdapter.Dispose();
                    con.Close();
                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }

            return retorno;
        }

        


        #endregion


        #region Metodos

        /// <summary>
        /// Metodo que parsea la consulta para obtener el Resumen General de Saldos de acuerdo a un filtro 
        /// VISTA FAC_ASO_SALDO_CXP
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <returns>Query a ejecutar sobre la vista FAC_ASO_SALDO_CXP</returns>
        private string GenerarQueryResumenGeneral(FiltroDataCrudaCxPInternacional filtro)
        {

            String queryStr = String.Empty;
            String CabeceraQuery = String.Empty;
            StringBuilder aux = new StringBuilder();
            String cadenaAux = String.Empty;
            //String[] parametrosDetalle = null;
            //int numeroFiltros = 0;
            bool variosFiltros = false;
            //bool esNumerica = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                aux.Append("SELECT T.* FROM (");
                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoCxPInternacional"].ToString();
                aux.Append(CabeceraQuery);

                //numeroFiltros = ObtenerCantidadFiltros(filtro);

                if (filtro.Asociado != null)
                {
                    if (variosFiltros)
                        cadenaAux += " AND ";
                    cadenaAux += "(CASOCIADO = " + ((Asociado)filtro.Asociado).Id.ToString() + ")";
                    variosFiltros = true;
                }

                
                if (!string.IsNullOrEmpty(filtro.TipoDeuda))
                {
                    if (!filtro.TipoDeuda.Equals("TODOS"))
                    {
                        if (variosFiltros)
                            cadenaAux += " AND ";

                        switch (filtro.TipoDeuda)
                        {
                            case ("VENCIDO"):
                                cadenaAux += "(VENCIDO <> 0)";
                                break;
                            case ("PORVENCER"):
                                cadenaAux += "(POR_VENCER <> 0)";
                                break;
                        }
                    }
                }


                if (cadenaAux.Length > 0)
                    cadenaAux = "WHERE " + cadenaAux;

                if (filtro.TipoDeuda.Equals("TODOS"))
                {
                    cadenaAux += " ORDER BY MONTO " + filtro.Ordenamiento + ")T"; 
                }
                else if (filtro.TipoDeuda.Equals("VENCIDO"))
                {
                    cadenaAux += " ORDER BY VENCIDO " + filtro.Ordenamiento + ")T"; 
                }
                else if (filtro.TipoDeuda.Equals("PORVENCER"))
                {
                    cadenaAux += " ORDER BY POR_VENCER " + filtro.Ordenamiento + ")T"; 
                }

                if (filtro.RangoSuperior != 0)
                {
                    cadenaAux += " WHERE ROWNUM <=" + filtro.RangoSuperior.ToString();
                }

                aux.Append(cadenaAux);
                queryStr = aux.ToString();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return queryStr;
        }

        /// <summary>
        /// Metodo que parsea la consulta para obtener la data cruda para hacer la Tabla de Resumen de Datos (pivot) a partir de un
        /// filtro dado, sobre la vista FAC_CXP_INT_VI
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <returns>DataTable con los datos necesarios para generar el pivot</returns>
        private string GenerarQueryDataCruda(FiltroDataCrudaCxPInternacional filtro)
        {

            String queryStr = String.Empty, _asociadosStr = String.Empty, _queryTodos = String.Empty;
            String CabeceraQuery = String.Empty;
            StringBuilder aux = new StringBuilder();
            String cadenaAux = String.Empty;
            int numeroFiltros = 0;
            bool variosFiltros = false;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoCxPInternacionalDataCruda"].ToString();
                //numeroFiltros = ObtenerCantidadFiltros(filtro);

                if (filtro != null)
                {
                    //Cuando esta seleccionado el Asociado
                    if (filtro.Asociado != null)
                    {
                        CabeceraQuery += "(CASOCIADO_O = " + ((Asociado)filtro.Asociado).Id.ToString() + ")";
                        //variosFiltros = true;
                    }

                    //Cuando el filtro tiene un numero de registros definidos - Nunca pasara por aca teniendo asociado seleccionado
                    else if ((filtro.Asociados != null) && (filtro.RangoSuperior != 0))
                    {
                        _asociadosStr = GenerarStrDeAsociadosParaConsulta(filtro.Asociados);
                        CabeceraQuery += " CASOCIADO_O IN (" + _asociadosStr + ")";
                        //variosFiltros = true;
                    }
                    else if (filtro.TipoDeuda.Equals("VENCIDO"))
                    {
                        CabeceraQuery += " (VENCIDA = 'SI') ";
                    }
                    else if (filtro.TipoDeuda.Equals("PORVENCER"))
                    {
                        CabeceraQuery += " (VENCIDA = 'NO') ";
                    }

                    else //ESTA OPCION CUANDO NO HAY NADA SELECCIONADO NI UN RANGO SUPERIOR
                    {
                        int indiceWhere = CabeceraQuery.IndexOf("WHERE");
                        cadenaAux = CabeceraQuery.Remove(indiceWhere);
                        CabeceraQuery = String.Empty;
                        CabeceraQuery = cadenaAux;
                    }
                    
                    aux.Append(CabeceraQuery);

                    
                    cadenaAux = " ORDER BY CASOCIADO_O DESC";
                    aux.Append(cadenaAux);
                    queryStr = aux.ToString();
                    
                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return queryStr;
        }


        /// <summary>
        /// Metodo que genera el query del detalle para ver las CxP Internacionales 
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <param name="cadenaFiltroDetalle">Valores usados para hacer el WHERE en la consulta tomando en cuenta los ejes</param>
        /// <param name="p"></param>
        /// <returns>DataTable con los CxP Internacional del detalle tomando en cuenta los filtros</returns>
        private string GenerarQueryDetalle(FiltroDataCrudaCxPInternacional filtro, String cadenaFiltroDetalle, bool p)
        {

            String queryStr = String.Empty;
            String CabeceraQuery = String.Empty;
            StringBuilder aux = new StringBuilder();
            String cadenaAux = String.Empty;
            String[] parametrosDetalle = null;
            
            int numeroFiltros = 0;
            //bool variosFiltros = false;
            //bool esNumerica = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoCxPInternacionalDataCruda"].ToString();

                if (filtro != null)
                {
                    #region CODIGO ORIGINAL COMENTADO - NO BORRAR
                    //Cuando esta seleccionado el Asociado
                    /*if (filtro.Asociado != null)
                    {
                        CabeceraQuery += "(CASOCIADO_O = " + ((Asociado)filtro.Asociado).Id.ToString() + ")";
                    }

                    else if ((filtro.Asociados != null) && (filtro.RangoSuperior != 0))
                    {
                        _asociadosStr = GenerarStrDeAsociadosParaConsulta(filtro.Asociados);
                        CabeceraQuery += " CASOCIADO_O IN (" + _asociadosStr + ")";
                    }
                        
                    else //ESTA OPCION CUANDO NO HAY NADA SELECCIONADO NI UN RANGO SUPERIOR
                    {
                        int indiceWhere = CabeceraQuery.IndexOf("WHERE");
                        cadenaAux = CabeceraQuery.Remove(indiceWhere);
                        CabeceraQuery = String.Empty;
                        CabeceraQuery = cadenaAux;
                    }*/
                    
                    #endregion


                    if (!filtro.EjeY.Equals("XASOCIADO1"))
                        parametrosDetalle = cadenaFiltroDetalle.Split('&');
                    else
                    {
                        char[] caracteresCadena = cadenaFiltroDetalle.ToCharArray();

                        for (int i = 0; i < caracteresCadena.Length; i++)
                        {
                            if (caracteresCadena[i].Equals('&'))
                            {
                                numeroFiltros++;
                            }
                        }

                        if (numeroFiltros == 1)
                            parametrosDetalle = cadenaFiltroDetalle.Split('&');
                        else
                        {
                            int ultimaPosicion = cadenaFiltroDetalle.LastIndexOf('&');
                            string str1 = cadenaFiltroDetalle.Remove(ultimaPosicion);
                            string str2 = cadenaFiltroDetalle.Substring(ultimaPosicion+1);
                            parametrosDetalle = new String[2];
                            parametrosDetalle[0] = str1;
                            parametrosDetalle[1] = str2;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.EjeY))
                    {
                        if (VerificarCadenaNumerica(parametrosDetalle[0]))
                            cadenaAux += "(" + filtro.EjeY + "= " + parametrosDetalle[0] + ") AND ";
                        else
                            cadenaAux += "(" + filtro.EjeY + "= '" + parametrosDetalle[0] + "') AND ";
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.EjeX))
                    {
                        if (!parametrosDetalle[1].Equals("Total"))
                            cadenaAux += "(" + filtro.EjeX + "= " + parametrosDetalle[1] + ") ";
                        else
                        {
                            int indiceUltimoAnd = cadenaAux.LastIndexOf("AND");
                            String strAux = cadenaAux.Remove(indiceUltimoAnd);
                            cadenaAux = String.Empty;
                            cadenaAux = strAux;
                        }

                    }

                    CabeceraQuery += cadenaAux;
                    aux.Append(CabeceraQuery);
                    cadenaAux = " ORDER BY FRECEPCION DESC";
                    aux.Append(cadenaAux);
                    queryStr = aux.ToString();

                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return queryStr;
        }


        /// <summary>
        /// Metodo que genera el String correspondiente al query para obtener los datos de detalles verticales
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <param name="parametros">Valores a utilizar en el WHERE de la consulta</param>
        /// <returns>String del query a aplicar en la base de datos</returns>
        private string GenerarQueryDetalleTotales(FiltroDataCrudaCxPInternacional filtro, string[] parametros)
        {

            String queryStr = String.Empty;
            String CabeceraQuery = String.Empty;
            StringBuilder aux = new StringBuilder();
            String cadenaAux = String.Empty;
            String[] parametrosDetalle = null;
            int numeroFiltros = 0;
            bool variosFiltros = false;
            bool esNumerica = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoCxPInternacionalDataCruda"].ToString();
                filtro.RangoSuperior = 0;
                numeroFiltros = ObtenerCantidadFiltros(filtro);

                aux.Append(CabeceraQuery);

                if (filtro != null)
                {
                    if (numeroFiltros > 0)
                    {
                        
                        if (!string.IsNullOrEmpty(filtro.EjeY))
                        {
                            if (variosFiltros)
                                cadenaAux += " AND ";
                            if (parametros.Length != 0)
                            {
                                if(!filtro.EjeY.Equals("XASOCIADO1"))
                                    cadenaAux += "CASOCIADO_O IN (" + parametros[0] + ")";
                                else
                                    cadenaAux += "XASOCIADO1 IN (" + parametros[0] + ")";
                            }

                            

                            variosFiltros = true;
                        }

                        if (!string.IsNullOrEmpty(filtro.EjeX))
                        {
                            esNumerica = VerificarCadenaNumerica(parametros[1]);

                            if ((variosFiltros) && (!parametros[1].Equals("Total")))
                                cadenaAux += " AND ";
                            if (!parametros[1].Equals("Total"))
                            {
                                if (esNumerica)
                                    cadenaAux += "(" + filtro.EjeX + "= " + parametros[1] + ")";
                                else
                                    cadenaAux += "(" + filtro.EjeX + "= '" + parametros[1] + "')";
                            }

                            variosFiltros = true;

                        }

                        aux.Append(cadenaAux);
                        aux.Append(" ORDER BY FRECEPCION DESC");
                        queryStr = aux.ToString();

                    }
                }


                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return queryStr;
        }

        
        /// <summary>
        /// Metodo que toma el arreglo de asociados del filtro y lo usa para obtener la Data Cruda para hacer la tabla pivot
        /// </summary>
        /// <param name="asociadosArray">Arreglo de codigos de Asociados</param>
        /// <returns>Cadena con codigo de asociados separados por comas</returns>
        private string GenerarStrDeAsociadosParaConsulta(string[] asociadosArray)
        {
            String _listaAsociadosStr = String.Empty, aux = String.Empty;
            int cantAsociados = asociadosArray.Length;
            int i = 1;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (String item in asociadosArray)
                {
                    if (i < cantAsociados)
                    {
                        aux += item + ",";
                        i++;
                    }
                    else if (i == cantAsociados)
                    {
                        aux += item;
                    }
                }

                _listaAsociadosStr = aux;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return _listaAsociadosStr;
        }

        /// <summary>
        /// Metodo que obtiene la cantidad de campos filtros pasados en la entidad del filtro
        /// </summary>
        /// <param name="filtro">Filtro a utilizar en la consulta</param>
        /// <returns>Cantidad de filtros a usar</returns>
        private int ObtenerCantidadFiltros(FiltroDataCrudaCxPInternacional filtro)
        {
            int contador = 0;
            int retorno = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //if (filtro.Anio != 0)
                //    contador++;
                //if (filtro.Mes != 0)
                //    contador++;
                if (filtro.Asociado != null)
                    contador++;
                //if (!string.IsNullOrEmpty(filtro.Departamento))
                //    contador++;
                if (!string.IsNullOrEmpty(filtro.TipoDeuda))
                    contador++;
                if (filtro.RangoSuperior != 0)
                    contador++;
                if (filtro.EjeX != null)
                    contador++;
                if (filtro.EjeY != null)
                    contador++;

                retorno = contador;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
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

        #endregion

    }
}
