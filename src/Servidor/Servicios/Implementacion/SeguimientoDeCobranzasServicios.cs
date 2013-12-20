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
    public class SeguimientoDeCobranzasServicios: MarshalByRefObject, ISeguimientoDeCobranzasServicios
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const String cabeceraDataResumenGeneral = "SELECT CODIGO,XASOCIADO,COUNT(NRO_GESTION) AS TOTAL_GES FROM SEG_COB ";
        private const String filtroQueryDataResumenGeneral = "GROUP BY CODIGO,XASOCIADO,AÑO ORDER BY TOTAL_GES ASC";

        /// <summary>
        /// Servicio que consulta la vista SEG_COB para obtener los datos de resumen iniciales segun el filtro dado
        /// </summary>
        /// <param name="filtro">Filtro a ejecutar sobre la base de datos</param>
        /// <returns>DataTable con Datos de Resumen General</returns>
        public DataTable GenerarDatosResumenGeneral(FiltroDataCrudaCobranza filtro)
        {
            DataTable datos = new DataTable();
            String query = String.Empty;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                OracleConnection con = GenerarConexionBD();
                con.Open();

                if (con.State.ToString().Equals("Open"))
                {
                    query = GenerarQueryResumenGeneral(filtro);
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
        /// Servicio que obtiene la data cruda segun los filtros de los ejes. Esta data se usa en la data pivot
        /// </summary>
        /// <param name="filtro">Filtro a ejecutar sobre la base de datos</param>
        /// <returns>DataTable con los datos crudos a usar antes del pivot</returns>
        public DataTable ObtenerDataCruda(FiltroDataCrudaCobranza filtro)
        {
            DataTable datos = new DataTable();
            String query = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                OracleConnection con = GenerarConexionBD();
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
        /// Servicio que obtiene el detalle de los datos presentado en el cuadro Resumen
        /// </summary>
        /// <param name="filtroDetalle">Filtro utilizado para obtener la data cruda</param>
        /// <param name="cadenaFiltroDetalle">Parametros necesarios para la consulta para obtener el detalle de las gestiones segun el cuadro Resumen</param>
        /// <returns>DataTable con la informacion de detalle</returns>
        public DataTable ObtenerDetalle(FiltroDataCrudaCobranza filtroDetalle, String cadenaFiltroDetalle)
        {
            DataTable retorno = new DataTable();
            String query = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                OracleConnection con = GenerarConexionBD();
                //string campoX = ejeX.Valor;
                //string campoY = ejeY.Valor;
                //FiltroDataCrudaCobranza filtro = new FiltroDataCrudaCobranza();
                //filtro.EjeX = campoX;
                //filtro.EjeY = campoY;
                query = GenerarQueryDetalle(filtroDetalle, cadenaFiltroDetalle);
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


        /// <summary>
        /// Servicio que obtiene el detalle por columna de los datos presentados en el cuadro Resumen
        /// </summary>
        /// <param name="filtro">Filtro utilizado para la data cruda usando tambien en este servicio</param>
        /// <param name="ejeX">Parametro para el eje X utilizado en el cuadro Resumen</param>
        /// <param name="ejeY">Parametro para el eje Y utilizado en el cuadro Resumen</param>
        /// <param name="parametros">Parametros filtro para los ejes X y Y</param>
        /// <returns>DataTable con los datos de detalle por columna</returns>
        public DataTable ObtenerDetalleDeTotales(FiltroDataCrudaCobranza filtro, ListaDatosValores ejeX, ListaDatosValores ejeY, String[] parametros)
        {
            
            DataTable retorno = new DataTable();
            String query = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                OracleConnection con = GenerarConexionBD();
                filtro.EjeX = ejeX.Valor;
                filtro.EjeY = ejeY.Valor;

                query = GenerarQueryDetalleTotales(filtro, parametros);

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

        

        


        /// <summary>
        /// Metodo que se configura un objeto conexion a base de datos para luego ejecutar un query sobre la misma
        /// </summary>
        /// <returns></returns>
        private OracleConnection GenerarConexionBD()
        {

            OracleConnection connection = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
                String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
                String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
                String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
                String query = String.Empty;
                
                connection = new OracleConnection(connectionString);
                                  

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

            return connection;
        }


        /// <summary>
        /// Metodo que parsea el query que se utiliza sobre la BD para obtener el Resumen General 
        /// </summary>
        /// <param name="filtro">Filtro seleccionado en la interfaz</param>
        /// <returns>Query a utilizar para obtener los datos de Resumen General</returns>
        private String GenerarQueryResumenGeneral(FiltroDataCrudaCobranza filtro)
        {

            String query = String.Empty, cadenaWhere = "WHERE ", aux = String.Empty;
            StringBuilder cadenaQuery = new StringBuilder();
            int numeroFiltros = 0;
            bool variosFiltros = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                cadenaQuery.Append(cabeceraDataResumenGeneral);

                numeroFiltros = obtenerCantidadFiltros(filtro);

                if (numeroFiltros != 0)
                {
                    
                    if (filtro.Asociado != null)
                    {
                        cadenaWhere += "CODIGO = " + ((Asociado)filtro.Asociado).Id.ToString() ;
                        variosFiltros = true;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Moneda))
                    {
                        if (variosFiltros)
                            cadenaWhere += " AND ";
                        cadenaWhere += "CMONEDA = '" + filtro.Moneda + "'" ;
                        variosFiltros = true;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.MedioGestion))
                    {
                        if (variosFiltros)
                            cadenaWhere += " AND ";
                        cadenaWhere += "MEDIO_GES = '" + filtro.MedioGestion + "'";
                        variosFiltros = true;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Usuario))
                    {
                        if (variosFiltros)
                            cadenaWhere += " AND ";
                        cadenaWhere += "USUARIO = '" + filtro.Usuario + "'";
                        variosFiltros = true;
                    }

                    if (filtro.Anio != 0)
                    {
                        if (variosFiltros)
                            cadenaWhere += " AND ";
                        cadenaWhere += "AÑO = " + filtro.Anio.ToString() + " ";
                        variosFiltros = true;
                    }

                    cadenaWhere += " " + filtroQueryDataResumenGeneral;

                    if (!string.IsNullOrWhiteSpace(filtro.Ordenamiento))
                    {
                        int indiceOrden = cadenaWhere.LastIndexOf("ASC");
                        aux = cadenaWhere.Remove(indiceOrden);
                        aux += filtro.Ordenamiento;
                        cadenaQuery.Append(aux);
                        query = cadenaQuery.ToString();
                    }
                    else
                    {
                        cadenaQuery.Append(cadenaWhere);
                        query = cadenaQuery.ToString();
                    }

                }
                else
                {
                    cadenaQuery.Append(filtroQueryDataResumenGeneral);
                    query = cadenaQuery.ToString();
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

            return query;
        }


        /// <summary>
        /// Metodo que genera el query para obtener de la base de datos la data cruda para el pivot
        /// </summary>
        /// <param name="filtro">Filtro usado para extraer la data cruda</param>
        /// <returns>DataTable con la data cruda para generar el pivot</returns>
        private string GenerarQueryDataCruda(FiltroDataCrudaCobranza filtro)
        {
            
            String queryStr = String.Empty, cadenaAux = String.Empty, cadenaWhere = "WHERE ";
            String CabeceraQuery = String.Empty;
            StringBuilder aux = new StringBuilder();
            int numeroFiltros = 0;
            bool variosFiltros = false;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoCobranzaDataCruda"].ToString();
                aux.Append(CabeceraQuery);
                numeroFiltros = obtenerCantidadFiltros(filtro);

                if (numeroFiltros != 0)
                {

                    if (filtro.Asociado != null)
                    {
                        cadenaAux += "CODIGO = " + ((Asociado)filtro.Asociado).Id.ToString();
                        variosFiltros = true;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Moneda))
                    {
                        if (variosFiltros)
                            cadenaAux += " AND ";
                        cadenaAux += "CMONEDA = '" + filtro.Moneda + "'";
                        variosFiltros = true;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.MedioGestion))
                    {
                        if (variosFiltros)
                            cadenaAux += " AND ";
                        cadenaAux += "MEDIO_GES = '" + filtro.MedioGestion + "'";
                        variosFiltros = true;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Usuario))
                    {
                        if (variosFiltros)
                            cadenaAux += " AND ";
                        cadenaAux += "USUARIO = '" + filtro.Usuario + "'";
                        variosFiltros = true;
                    }


                    if (filtro.Anio != 0)
                    {
                        if (variosFiltros)
                            cadenaAux += " AND ";
                        cadenaAux += "AÑO = " + filtro.Anio.ToString() + " ";
                        variosFiltros = true;
                    }

                    cadenaWhere += cadenaAux + " ORDER BY CODIGO ASC";

                    aux.Append(cadenaWhere);

                    queryStr = aux.ToString();

                    #region CODIGO ORIGINAL COMENTADO
                    //if (!string.IsNullOrWhiteSpace(filtro.Ordenamiento))
                    //{
                    //    int indiceOrden = cadenaWhere.LastIndexOf("ASC");
                    //    aux = cadenaWhere.Remove(indiceOrden);
                    //    aux += filtro.Ordenamiento;
                    //    cadenaQuery.Append(aux);
                    //    query = cadenaQuery.ToString();
                    //}
                    //else
                    //{
                    //    cadenaQuery.Append(cadenaWhere);
                    //    query = cadenaQuery.ToString();
                    //} 
                    #endregion
                }
                else
                {
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
        /// Metodo que genera el Query que se usara para obtener los datos de Detalle
        /// </summary>
        /// <param name="filtro">Filtro a utilizar que posee los ejes X y Y necesarios</param>
        /// <param name="cadenaFiltroDetalle">Cadena con los datos necesarios para filtrar por los ejes X y Y</param>
        /// <returns>Cadena generada para con la consulta a aplicar en BD para obtener el Detalle</returns>
        private string GenerarQueryDetalle(FiltroDataCrudaCobranza filtro, string cadenaFiltroDetalle)
        {
            String queryStr = String.Empty, cadenaAux = String.Empty, cadenaWhere = "WHERE ";
            String CabeceraQuery = String.Empty;
            String[] valoresFiltros = null;
            StringBuilder aux = new StringBuilder();
            int numeroFiltros = 0;
            bool variosFiltros = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoCobranzaDataCruda"].ToString();
                aux.Append(CabeceraQuery);

                valoresFiltros = cadenaFiltroDetalle.Split('&');

                                
                if ((!string.IsNullOrWhiteSpace(filtro.EjeX)) && (!string.IsNullOrWhiteSpace(filtro.EjeY)))
                {

                    if (!string.IsNullOrWhiteSpace(filtro.Moneda))
                    {
                        cadenaAux += "CMONEDA = '" + filtro.Moneda + "' AND ";
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Usuario))
                    {
                        cadenaAux += "USUARIO = '" + filtro.Usuario + "' AND ";
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.MedioGestion))
                    {
                        cadenaAux += "MEDIO_GES = '" + filtro.MedioGestion + "' AND ";
                    }

                    if (filtro.Anio != 0)
                    {
                        cadenaAux += "AÑO =" + filtro.Anio.ToString() + " AND ";
                    }

                    if (filtro.Asociado != null)
                    {
                        cadenaAux += "CODIGO = " + ((Asociado)filtro.Asociado).Id + " AND ";
                    }

                    if(VerificarCadenaNumerica(valoresFiltros[0]))
                    {
                        cadenaAux += filtro.EjeY + "=" + valoresFiltros[0];
                    }
                    else
                    {
                        cadenaAux += filtro.EjeY + "= '" + valoresFiltros[0] + "'";
                    }

                    if (!valoresFiltros[1].Equals("Total"))
                    {
                        if (VerificarCadenaNumerica(valoresFiltros[1]))
                        {
                            cadenaAux += " AND " + filtro.EjeX + "=" + valoresFiltros[1];
                        }
                        else
                        {
                            cadenaAux += " AND " + filtro.EjeX + "='" + valoresFiltros[1] + "'";
                        } 
                    }

                    cadenaAux += " ORDER BY NRO_GESTION ASC";

                    cadenaWhere += cadenaAux;

                    aux.Append(cadenaWhere);

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


        private string GenerarQueryDetalleTotales(FiltroDataCrudaCobranza filtro, string[] parametros)
        {
            String queryStr = String.Empty, cadenaAux = String.Empty, cadenaWhere = "WHERE ";
            String CabeceraQuery = String.Empty;
            String[] valoresFiltros = null;
            StringBuilder aux = new StringBuilder();
            int numeroFiltros = 0;
            bool variosFiltros = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CabeceraQuery = ConfigurationManager.AppSettings["CabSeguimientoCobranzaDataCruda"].ToString();
                aux.Append(CabeceraQuery);

                //valoresFiltros = cadenaFiltroDetalle.Split('&');


                if ((!string.IsNullOrWhiteSpace(filtro.EjeX)) && (!string.IsNullOrWhiteSpace(filtro.EjeY)))
                {

                    if (!string.IsNullOrWhiteSpace(filtro.Moneda))
                    {
                        cadenaAux += "CMONEDA = '" + filtro.Moneda + "' AND ";
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Usuario))
                    {
                        cadenaAux += "USUARIO = '" + filtro.Usuario + "' AND ";
                    }

                    if (filtro.Anio != 0)
                    {
                        cadenaAux += "AÑO =" + filtro.Anio.ToString() + " AND ";
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.MedioGestion))
                    {
                        cadenaAux += "MEDIO_GES = '" + filtro.MedioGestion + "' AND ";
                    }

                    if (filtro.Asociado != null)
                    {
                        cadenaAux += "CODIGO = " + ((Asociado)filtro.Asociado).Id + " AND ";
                    }

                    if (!string.IsNullOrEmpty(parametros[0]))
                    {
                        //cadenaAux += filtro.EjeY + "=" + parametros[0];
                        String valoresParseados = parametros[0].Replace('%', ',');
                        cadenaAux += filtro.EjeY + " IN (" + valoresParseados + ") ";
                    }
                    //else
                    //{
                    //    cadenaAux += filtro.EjeY + "= '" + valoresFiltros[0] + "'";
                    //}

                    if (!parametros[1].Equals("Total"))
                    {
                        if (VerificarCadenaNumerica(parametros[1]))
                        {
                            cadenaAux += " AND " + filtro.EjeX + "=" + parametros[1];
                        }
                        else
                        {
                            cadenaAux += " AND " + filtro.EjeX + "='" + parametros[1] + "'";
                        }
                    }

                    cadenaAux += " ORDER BY NRO_GESTION ASC";

                    cadenaWhere += cadenaAux;

                    aux.Append(cadenaWhere);

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

        
        
        private string ParsearValores(string cadenaParametros)
        {
            String valoresParseados = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                valoresParseados = cadenaParametros.Replace('%', ',');

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return valoresParseados;
        }



        /// <summary>
        /// Metodo que obtiene la cantidad de filtros a aplicar en la consulta
        /// </summary>
        /// <param name="filtro">Filtro a aplicar</param>
        /// <returns>Cantidad de filtros</returns>
        private int obtenerCantidadFiltros(FiltroDataCrudaCobranza filtro)
        {

            int retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                int contador = 0;

                if (filtro.Asociado != null)
                    contador++;
                if (filtro.Moneda != null)
                    contador++;
                if (filtro.Usuario != null)
                    contador++;
                if (filtro.MedioGestion != null)
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

        
    }
}
