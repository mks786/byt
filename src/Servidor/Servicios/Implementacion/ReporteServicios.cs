using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using NLog;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class ReporteServicios: MarshalByRefObject, IReporteServicios
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que consulta todos los Reportes de la tabla de base de datos
        /// </summary>
        /// <returns>Lista de todos los reportes</returns>
        public IList<Reporte> ConsultarTodos()
        {
            IList<Reporte> reportes;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                reportes = ControladorReporte.ConsultarTodos();

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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return reportes;
        }


        

        /// <summary>
        /// Servicio que inserta o actualiza un Reporte
        /// </summary>
        /// <param name="entidad">Reporte a insertar o actualizar</param>
        /// <param name="hash">Hash del Usuario Logueado</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; false en caso contrario</returns>
        public bool InsertarOModificar(Reporte reporte, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

               
                bool exitoso = ControladorReporte.InsertarOModificar(reporte, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        /// <summary>
        /// Serivicio que obtiene una lista de Reportes usando un filtro determinado
        /// </summary>
        /// <param name="reporte">reporte filtro</param>
        /// <returns>Lista de reportes de marca por filtro</returns>
        public IList<Reporte> ObtenerReporteFiltro(Reporte reporte)
        {
            IList<Reporte> reportes;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                reportes = ControladorReporte.ObtenerReporteFiltro(reporte);

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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return reportes;
        }

        /// <summary>
        /// Servicio que ejecuta la consulta del Reporte que se forma en el Cliente
        /// </summary>
        /// <param name="query">Consulta a ejecutar</param>
        /// <returns>DataSet que tiene la informacion resultante de la consulta</returns>
        public DataSet EjecutarQuery(String query)
        {

            DataSet ds = new DataSet();
            String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
            String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
            String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
            String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                OracleConnection con = new OracleConnection(connectionString);
                con.Open();

                if (con.State.ToString().Equals("Open"))
                {
                    OracleDataAdapter oracleAdapter = new OracleDataAdapter(query, con);
                    oracleAdapter.Fill(ds);
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
                throw new ApplicationException(ex.Message);
            }

            return ds;
        }

        
        /// <summary>
        /// Metodo que realiza la consulta del Reporte
        /// </summary>
        /// <param name="query">Consulta construida en el Cliente para este Reporte</param>
        /// <returns>DataSet resultante de dicha consulta</returns>
        private DataSet EjecutarConsultaReporte(OracleConnection con, string query)
        {

            DataSet resultado = new DataSet();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                

               
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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return resultado;
        }

        public bool Eliminar(Reporte entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Reporte entidad)
        {
            throw new NotImplementedException();
        }

        public Reporte ConsultarPorId(Reporte entidad)
        {
            throw new NotImplementedException();
        }

        public IList<Reporte> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }


        

    }
}
