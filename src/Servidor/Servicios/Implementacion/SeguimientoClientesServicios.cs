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
    public class SeguimientoClientesServicios : MarshalByRefObject, ISeguimientoClientesServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Servicios

        /// <summary>
        /// Servicio que obtiene los datos crudos para luego ser transformados en tabla pivot
        /// </summary>
        /// <param name="filtro">Filtro para la data cruda</param>
        /// <returns>DataTable con el resultado de la consulta en base de datos; en caso contrario, retorna NULL</returns>
        public DataTable ObtenerDatosSaldos(FiltroDataCruda filtro)
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

                query = GenerarQuery(filtro, null, false);

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
        /// Servicio que obtiene los datos crudos a partir de los datos de los saldos
        /// </summary>
        /// <param name="filtro">Filtro para la data cruda</param>
        /// <returns>DataTable con la data cruda necesaria para obtener los datos pivot</returns>
        public DataTable ObtenerDataCruda(FiltroDataCruda filtro)
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
        /// Servicio que presenta el detalle de una celda seleccionada con todas las facturas del cliente
        /// </summary>
        /// <param name="ejeX">Parametro X a consultar</param>
        /// <param name="ejeY">Parametro Y a consultar</param>
        /// <param name="cadenaFiltroDetalle">Datos a filtrar en la consulta</param>
        /// <returns>DataTable con el detalle de lo seleccionado</returns>
        public DataTable ObtenerDetalle(ListaDatosValores ejeX, ListaDatosValores ejeY, String cadenaFiltroDetalle, FiltroDataCruda filtro)
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

                    query = GenerarQuery(filtro, cadenaFiltroDetalle, true);
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
        /// Servicio que obtiene los datos de detalle de los totales por columna
        /// </summary>
        /// <param name="filtro">Filtro para filtrar los datos</param>
        /// <param name="ejeX">Eje X seleccionado</param>
        /// <param name="ejeY">Eje Y seleccionado</param>
        /// <param name="parametros">Parametros necesarios para el WHERE en la consulta</param>
        /// <returns>DataTable con el detalle por columna</returns>
        public DataTable ObtenerDetalleDeTotales(FiltroDataCruda filtro, ListaDatosValores ejeX, ListaDatosValores ejeY, String[] parametros)
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
        /// Metodo que obtiene los datos CRUDOS necesarios para generar la data pivotal segun los ejes seleccionados usando los filtros de
        /// los saldos generales
        /// </summary>
        /// <param name="filtro">Filtro de los saldos</param>
        /// <returns>DataTable con la data cruda</returns>
        private string GenerarQueryDataCruda(FiltroDataCruda filtro)
        {

            #region Datos de Conexion

            String queryStr = String.Empty;
            String CabeceraQuery = String.Empty;
            String CabeceraQuerySaldos = "SELECT T.* FROM (SELECT CASOCIADO FROM FAC_ASO_SALDO WHERE";
            StringBuilder aux = new StringBuilder();
            String cadenaAux = String.Empty;
            String[] parametrosDetalle = null;
            int numeroFiltros = 0;
            bool variosFiltros = false;

            #endregion

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoClientesDataCruda"].ToString();
                numeroFiltros = ObtenerCantidadFiltros(filtro);

                if (filtro != null)
                {
                    //Cuando esta seleccionado el Asociado
                    if (filtro.Asociado != null)
                    {
                        CabeceraQuery += "(CASOCIADO = " + ((Asociado)filtro.Asociado).Id.ToString() + ")";
                        variosFiltros = true;
                    }

                    //Cuando el filtro tiene un numero de registros definidos - Nunca pasara por aca teniendo asociado seleccionado
                    else if ((numeroFiltros > 0) && (filtro.RangoSuperior != 0))
                    {

                        CabeceraQuery += " CASOCIADO IN (";

                        cadenaAux += CabeceraQuerySaldos;
                        String asociados = ObtenerAsociadosAConsultar(cadenaAux, filtro);
                        parametrosDetalle = asociados.Split('_');
                        String asociadosIn = GenerarListaAsociadosParaConsulta(parametrosDetalle);

                        CabeceraQuery += asociadosIn + ") ";
                        cadenaAux = String.Empty;
                        variosFiltros = true;

                    }

                    else //ESTA OPCION CUANDO NO HAY NADA SELECCIONADO NI UN RANGO SUPERIOR
                    {
                        //EL FILTRO DE LA MONEDA ES FIJO POR LO QUE ES NECESARIO FILTRARLO
                        if (filtro.Moneda != null)
                        {
                            CabeceraQuery += " (CMONEDA ='" + filtro.Moneda + "')";
                            variosFiltros = true;
                        }

                    }


                    if (!string.IsNullOrEmpty(filtro.TipoSaldo))
                    {
                        if (!filtro.TipoSaldo.Equals("TODOS"))
                        {
                            if (variosFiltros)
                                cadenaAux += " AND ";

                            switch (filtro.TipoSaldo)
                            {
                                case ("VENCIDO"):
                                    cadenaAux += "(VENCIDO <> 0)";
                                    break;
                                case ("PORVENCER"):
                                    cadenaAux += "(PORVENCER <> 0)";
                                    break;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(filtro.Departamento))
                    {
                        if (!filtro.Departamento.Equals("TODOS"))
                        {
                            if (variosFiltros)
                                cadenaAux += " AND ";

                            switch (filtro.Departamento)
                            {
                                case ("REPORTES"):
                                    cadenaAux += "(REPORTES IS NOT NULL)";
                                    break;
                                case ("OTROS_DPTOS"):
                                    cadenaAux += "(OTROS_DPTOS <> 0)";
                                    break;
                            }
                        }
                    }

                    CabeceraQuery += cadenaAux;
                    aux.Append(CabeceraQuery);
                    queryStr = aux.ToString();
                    #region Codigo Original
                    //if ((numeroFiltros > 0) && (filtro.RangoSuperior != 0))
                    //{

                    //    //Predomina el Asociado, si este es NULL se hace la otra consulta
                    //    if (filtro.Asociado != null)
                    //    {
                    //        int indexLastCAsociado = CabeceraQuery.LastIndexOf("CASOCIADO");
                    //        cadenaAux = CabeceraQuery.Remove(indexLastCAsociado);
                    //        cadenaAux += "CASOCIADO = " + ((Asociado)filtro.Asociado).Id.ToString();
                    //    }

                    //    else if(filtro.RangoSuperior != 0) 
                    //    {
                    //        aux.Append(CabeceraQuery);

                    //        cadenaAux += CabeceraQuerySaldos;

                    //        if (filtro.Moneda != null)
                    //        {
                    //            if (variosFiltros)
                    //                cadenaAux += " AND ";
                    //            cadenaAux += "(CMONEDA ='" + filtro.Moneda + "')";
                    //            variosFiltros = true;
                    //        }

                    //        if (filtro.RangoSuperior != 0)
                    //        {
                    //            if (variosFiltros)
                    //                cadenaAux += " AND ";
                    //            cadenaAux += "(ROWNUM <=" + filtro.RangoSuperior.ToString() + ")";
                    //            variosFiltros = true;

                    //        }

                    //        String asociados = ObtenerAsociadosAConsultar(cadenaAux);
                    //        parametrosDetalle = asociados.Split('_');
                    //        String asociadosIn = GenerarListaAsociadosParaConsulta(parametrosDetalle);
                    //        aux.Append(asociadosIn);
                    //        aux.Append(")");



                    //        cadenaAux += ")";
                    //    }


                    //    //aux.Append(cadenaAux);
                    //    queryStr = aux.ToString();

                    //}

                    //else 
                    //{
                    //    int indexWhere = CabeceraQuery.LastIndexOf("WHERE");
                    //    cadenaAux = CabeceraQuery.Remove(indexWhere);
                    //    aux.Append(cadenaAux);
                    //    queryStr = aux.ToString();
                    //} 
                    #endregion


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
        /// Metodo que parsea el query segun los filtros enviados desde el cliente
        /// </summary>
        /// <param name="filtro">Filtro enviado desde el cliente con todos sus parametros</param>
        /// <returns>Cadena con el query a ejecutar en la base de datos</returns>
        private string GenerarQuery(FiltroDataCruda filtro, String cadenaDetalle, bool detalle)
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


                if (!detalle)
                {
                    aux.Append("SELECT T.* FROM (");
                    CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoClientes"].ToString();
                }
                else
                {
                    CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoClientesPivot"].ToString();
                    filtro.RangoSuperior = 0;
                }


                aux.Append(CabeceraQuery);

                numeroFiltros = ObtenerCantidadFiltros(filtro);

                if (filtro != null)
                {
                    if (numeroFiltros > 0)
                    {

                        if (filtro.Asociado != null)
                        {
                            if (variosFiltros)
                                cadenaAux += " AND ";
                            cadenaAux += "(CASOCIADO = " + ((Asociado)filtro.Asociado).Id.ToString() + ")";
                            variosFiltros = true;
                        }

                        if (filtro.Moneda != null)
                        {
                            if (variosFiltros)
                                cadenaAux += " AND ";
                            cadenaAux += "(CMONEDA ='" + filtro.Moneda + "')";
                            variosFiltros = true;
                        }

                        if (!string.IsNullOrEmpty(filtro.TipoSaldo))
                        {
                            if (!filtro.TipoSaldo.Equals("TODOS"))
                            {
                                if (variosFiltros)
                                    cadenaAux += " AND ";

                                switch (filtro.TipoSaldo)
                                {
                                    case ("VENCIDO"):
                                        cadenaAux += "(VENCIDO <> 0)";
                                        break;
                                    case ("PORVENCER"):
                                        cadenaAux += "(PORVENCER <> 0)";
                                        break;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(filtro.Departamento))
                        {
                            if (!filtro.Departamento.Equals("TODOS"))
                            {
                                if (variosFiltros)
                                    cadenaAux += " AND ";

                                switch (filtro.Departamento)
                                {
                                    case ("REPORTES"):
                                        cadenaAux += "(REPORTES IS NOT NULL)";
                                        break;
                                    case ("OTROS_DPTOS"):
                                        cadenaAux += "(OTROS_DPTOS <> 0)";
                                        break;
                                }
                            }
                        }

                        if (!detalle)
                        {
                            if (filtro.Moneda.Equals("US"))
                                //cadenaAux += " ORDER BY BSALDO  " + filtro.Ordenamiento;
                                cadenaAux += " ORDER BY BSALDO  " + filtro.Ordenamiento + ")T";
                            else if (filtro.Moneda.Equals("BF"))
                                //cadenaAux += " ORDER BY BSALDO_BF  " + filtro.Ordenamiento;
                                cadenaAux += " ORDER BY BSALDO_BF " + filtro.Ordenamiento + ")T";
                        }

                        if (filtro.RangoSuperior != 0)
                        {
                            cadenaAux += " WHERE ROWNUM <=" + filtro.RangoSuperior.ToString();
                        }

                        if (detalle)
                        {

                            parametrosDetalle = cadenaDetalle.Split('&');

                            if (filtro.EjeY != null)
                            {
                                esNumerica = VerificarCadenaNumerica(parametrosDetalle[0]);
                                if (variosFiltros)
                                    cadenaAux += " AND ";
                                if (esNumerica)
                                {
                                    if (filtro.EjeY.Equals("CASOCIADO"))
                                        cadenaAux += "(" + filtro.EjeY + "= " + parametrosDetalle[0] + ")";
                                    else if (filtro.EjeY.Equals("XASOCIADO"))
                                        cadenaAux += "(" + filtro.EjeY + " LIKE '%" + parametrosDetalle[0] + "%')";
                                }
                                else
                                    cadenaAux += filtro.EjeY + "= '" + parametrosDetalle[0] + "')";

                                variosFiltros = true;
                            }

                            if (filtro.EjeX != null)
                            {
                                esNumerica = VerificarCadenaNumerica(parametrosDetalle[1]);
                                if ((variosFiltros) && (!parametrosDetalle[1].Equals("Total")))
                                    cadenaAux += " AND ";
                                if (!parametrosDetalle[1].Equals("Total"))
                                {
                                    if (esNumerica)
                                        cadenaAux += "(" + filtro.EjeX + "= " + parametrosDetalle[1] + ")";
                                    else
                                        cadenaAux += "(" + filtro.EjeX + "= '" + parametrosDetalle[1] + "')";
                                }

                                variosFiltros = true;

                            }

                        }

                        if (detalle)
                        {
                            cadenaAux += " ORDER BY CASOCIADO DESC";
                        }
                        //else
                        //{
                        //    if(filtro.Moneda.Equals("US"))
                        //        cadenaAux += " ORDER BY BSALDO  " + filtro.Ordenamiento ; 
                        //    else if(filtro.Moneda.Equals("BF"))
                        //        cadenaAux += " ORDER BY BSALDO_BF  " + filtro.Ordenamiento;
                        //}

                        aux.Append(cadenaAux);
                        queryStr = aux.ToString();


                    }

                    else
                    {
                        aux.Append(cadenaAux);
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
        /// Metodo que parsea el query para obtener el detalle de los totales (por columna)
        /// </summary>
        /// <param name="filtro">Filtro enviado desde el cliente con todos sus parametros</param>
        /// <param name="parametros">Parametros necesarios para el WHERE de la consulta</param>
        /// <returns>Cadena con el query a ejecutar en la base</returns>
        private string GenerarQueryDetalleTotales(FiltroDataCruda filtro, string[] parametros)
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

                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoClientesPivot"].ToString();
                filtro.RangoSuperior = 0;
                numeroFiltros = ObtenerCantidadFiltros(filtro);

                aux.Append(CabeceraQuery);

                if (filtro != null)
                {
                    if (numeroFiltros > 0)
                    {
                        if (!string.IsNullOrEmpty(filtro.Moneda))
                        {
                            if (variosFiltros)
                                cadenaAux += " AND ";
                            cadenaAux += "CMONEDA ='" + filtro.Moneda + "'";
                            variosFiltros = true;
                        }
                        if (!string.IsNullOrEmpty(filtro.TipoSaldo))
                        {
                            if (!filtro.TipoSaldo.Equals("TODOS"))
                            {
                                if (variosFiltros)
                                    cadenaAux += " AND ";

                                switch (filtro.TipoSaldo)
                                {
                                    case ("VENCIDO"):
                                        cadenaAux += "VENCIDO <> 0";
                                        break;
                                    case ("PORVENCER"):
                                        cadenaAux += "PORVENCER <> 0";
                                        break;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(filtro.Departamento))
                        {
                            if (!filtro.Departamento.Equals("TODOS"))
                            {
                                if (variosFiltros)
                                    cadenaAux += " AND ";

                                switch (filtro.Departamento)
                                {
                                    case ("REPORTES"):
                                        cadenaAux += "REPORTES IS NOT NULL";
                                        break;
                                    case ("OTROS_DPTOS"):
                                        cadenaAux += "OTROS_DPTOS <> 0";
                                        break;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(filtro.EjeY))
                        {
                            //esNumerica = VerificarCadenaNumerica(parametrosDetalle[0]);
                            if (variosFiltros)
                                cadenaAux += " AND ";
                            if (parametros.Length != 0)
                            {
                                cadenaAux += "CASOCIADO IN (" + parametros[0] + ")";
                            }

                            #region CODIGO ORIGINAL COMENTADO
                            //if (esNumerica)
                            //{
                            //    if (filtro.EjeY.Equals("CASOCIADO"))
                            //        cadenaAux += "(" + filtro.EjeY + "= " + parametrosDetalle[0] + ")";
                            //    else if (filtro.EjeY.Equals("XASOCIADO"))
                            //        cadenaAux += "(" + filtro.EjeY + " LIKE '%" + parametrosDetalle[0] + "%')";
                            //}
                            //else
                            //    cadenaAux += filtro.EjeY + "= '" + parametrosDetalle[0] + "')"; 
                            #endregion

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
        /// Metodo que genera una cadena con los asociados para la clausula IN en la consulta de la data cruda para generar los datos pivot
        /// </summary>
        /// <param name="parametrosDetalle">Arreglo con los codigos de los asociados</param>
        /// <returns>Cadena con los codigos de los asociados separados por comas</returns>
        private string GenerarListaAsociadosParaConsulta(string[] parametrosDetalle)
        {
            String retorno = String.Empty;
            String aux = String.Empty;
            int longitudArray = parametrosDetalle.Length - 1;

            for (int i = 0; i < longitudArray; i++)
            {
                if (i != longitudArray - 1)
                    retorno += parametrosDetalle[i] + ",";
                else if (i == longitudArray - 1)
                    retorno += parametrosDetalle[i];

            }

            return retorno;
        }

        

        /// <summary>
        /// Metodo que obtiene los valores de los campos a consultar en FAC_ASO_SALDO
        /// </summary>
        /// <param name="cadenaAux">Cabecera del Query</param>
        /// <returns></returns>
        private string ObtenerAsociadosAConsultar(string cadenaAux, FiltroDataCruda filtro)
        {
            #region Datos de Conexion a la Base de Datos

            String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
            String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
            String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
            String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
            String query = String.Empty;
            String retorno = String.Empty;
            bool variosFiltros = false;

            #endregion


            if (filtro.Moneda != null)
            {
                cadenaAux += "(CMONEDA ='" + filtro.Moneda + "')";
                variosFiltros = true;
            }

            //if (filtro.RangoSuperior != 0)
            //{
            //    if (variosFiltros)
            //        cadenaAux += " AND ";
            //    cadenaAux += "(ROWNUM <=" + filtro.RangoSuperior.ToString() + ")";
            //    variosFiltros = true;

            //}

            if (!string.IsNullOrEmpty(filtro.TipoSaldo))
            {
                if (!filtro.TipoSaldo.Equals("TODOS"))
                {
                    if (variosFiltros)
                        cadenaAux += " AND ";

                    switch (filtro.TipoSaldo)
                    {
                        case ("VENCIDO"):
                            cadenaAux += "(VENCIDO <> 0)";
                            break;
                        case ("PORVENCER"):
                            cadenaAux += "(PORVENCER <> 0)";
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(filtro.Departamento))
            {
                if (!filtro.Departamento.Equals("TODOS"))
                {
                    if (variosFiltros)
                        cadenaAux += " AND ";

                    switch (filtro.Departamento)
                    {
                        case ("REPORTES"):
                            cadenaAux += "(REPORTES IS NOT NULL)";
                            break;
                        case ("OTROS_DPTOS"):
                            cadenaAux += "(OTROS_DPTOS <> 0)";
                            break;
                    }
                }
            }

            if (filtro.Moneda.Equals("US"))
                cadenaAux += " ORDER BY BSALDO  " + filtro.Ordenamiento + ")T";
            else if (filtro.Moneda.Equals("BF"))
                cadenaAux += " ORDER BY BSALDO_BF " + filtro.Ordenamiento + ")T";

            if (filtro.RangoSuperior != 0)
            {
                cadenaAux += " WHERE ROWNUM <=" + filtro.RangoSuperior.ToString();
            }


            query = cadenaAux;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(query, connection);
                connection.Open();
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    // Always call Read before accessing data. 
                    while (reader.Read())
                    {
                        retorno += reader.GetInt32(0).ToString() + "_";
                    }
                }
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



        /// <summary>
        /// Metodo que permite saber cuantos filtros se seleccionaron para filtrar los datos
        /// </summary>
        /// <param name="filtro">Filtro a utilizar</param>
        /// <returns>Cantidad de filtros seleccionados</returns>
        private int ObtenerCantidadFiltros(FiltroDataCruda filtro)
        {
            int contador = 0;
            int retorno = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (filtro.Anio != 0)
                    contador++;
                if (filtro.Mes != 0)
                    contador++;
                if (filtro.Asociado != null)
                    contador++;
                if (filtro.Moneda != null)
                    contador++;
                if (!string.IsNullOrEmpty(filtro.Departamento))
                    contador++;
                if (!string.IsNullOrEmpty(filtro.TipoSaldo))
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


        


        #endregion


        


        
        
    }
}
