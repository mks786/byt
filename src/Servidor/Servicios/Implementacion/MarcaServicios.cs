using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class MarcaServicios : MarshalByRefObject, IMarcaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<Marca> ConsultarTodos()
        {
            IList<Marca> marcas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                marcas = ControladorMarca.ConsultarTodos();

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
            return marcas;
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Marca ConsultarPorId(Marca marca)
        {
            throw new NotImplementedException();
        }

        //-------------------------------------------------------------
        /// <summary>
        /// Servicio que consultar por un campo determinado y 
        /// ordena en forma Ascendente o Descendente
        /// </summary>
        /// <param name="campo">Campo a filtrar</param>
        /// <param name="tipoOrdenamiento">Si el ordenamiento es Ascende o Descendente</param>
        /// <returns>Lista de Entidades</returns>
        public IList<Marca> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
        //-------------------------------------------------------------


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificar(Marca marca, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarca.InsertarOModificar(ref marca, hash);

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
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(Marca marca, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarca.Eliminar(marca, hash);

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
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(Marca marca)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarca.VerificarExistencia(marca);

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
        /// Servicio que se encarga de buscar marcas que cumplan con un filtro
        /// </summary>
        /// <param name="Marca">marca modelo para filtrar</param>
        /// <returns>Lista de marcas que cumplan con el filtro</returns>
        public IList<Marca> ObtenerMarcasFiltro(Marca marca)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Marca> marcas;

                marcas = ControladorMarca.ConsultarMarcasFiltro(marca);
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                return marcas;

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
        /// Servicio que se encarga de obtener las fechas de renovacion de una marca
        /// </summary>
        /// <param name="Marca">Marca a buscar</param>
        /// <param name="fechas">fechas de renovacion de la marca</param>
        /// <returns>Lista de marcas por fecha de renovacion</returns>
        public IList<Marca> ObtenerMarcasPorFechaRenovacion(Marca marca, DateTime[] fechas)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Marca> marcas;

                marcas = ControladorMarca.ObtenerMarcasPorFechaRenovacion(marca, fechas);
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                return marcas;

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
        /// Servicio que se encarga de consultar la auditoria de Marcas
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditoria de la marca</returns>
        public IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Auditoria> auditorias = ControladorMarca.AuditoriaPorFkyTabla(auditoria);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return auditorias;
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
        /// Servicio que se encarga de consultar una marca con todos sus objetos
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <returns>Marca con todos los objetos</returns>
        public Marca ConsultarMarcaConTodo(Marca marca)
        {
            Marca retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                retorno = ControladorMarca.ConsultarMarcaConTodo(marca);

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

            return retorno;
        }


        /// <summary>
        /// Servicio que se encarga de insertar la marca
        /// </summary>
        /// <param name="marca">Marca a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la Marca insertada</returns>
        public int? InsertarOModificarMarca(Marca marca, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarca.InsertarOModificar(ref marca, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (exitoso)
                    return marca.Id;
                else
                    return null;
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
        /// Servicio que se encarga de consultar la vista de recordatorios de marca
        /// </summary>
        /// <param name="RecordatorioVista">recordatorio a consultar</param>
        /// <param name="fechas">fechas de renovacion de marca a filtrar</param>
        /// <returns>la lista de recordatorios</returns>
        public IList<RecordatorioVista> ConsultarRecordatoriosVistaMarca(RecordatorioVista recordatorio, DateTime[] fechas, string localidad)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<RecordatorioVista> retorno = ControladorMarca.ConsultarRecordatoriosVista(recordatorio, fechas, localidad);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return retorno;
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
        /// Servicio que se encarga de consultar la vista de recordatorios de marca con filtro no automático
        /// </summary>
        /// <param name="RecordatorioVista">recordatorio a consultar</param>
        /// <param name="ano">Ano de fecha renovacion a filtrar</param>
        /// <param name="mes">mes de fecha renovación a filtrar</param>
        /// <param name="fechas">fecha desde y hasta de renovación a filtrar</param>
        /// <returns>Lista de marcas para recordatorio filtradas</returns>
        public IList<RecordatorioVista> ConsultarRecordatoriosVistaMarca(RecordatorioVista recordatorio, string ano, string mes, DateTime?[] fechas, string localidad)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<RecordatorioVista> retorno = ControladorMarca.ConsultarRecordatoriosVista(recordatorio, ano, mes, fechas, localidad);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return retorno;
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


        public String ObtenerDistingueDeMarca(Marca marca)
        {
            int idMarca = marca.Id;
            //DATASOURCE y autenticacion a la Base de Datos
            String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
            String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
            String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
            
            //String de conexion a la base de datos
            String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
            //Valor resultante de la consulta
            String valor = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                // Abrimos la conexion con la base de datos
                OracleConnection con = new OracleConnection(connectionString);
                con.Open();
                //Verificamos si la conexion a la base de datos efectivamente esta abierta
                if (con.State.ToString().Equals("Open"))
                {
                    //Creamos la estructura command para ejecutar la instruccion SELECT sobre la tabla
                    OracleCommand cmd = con.CreateCommand();
                    //Llamo al metodo que ejecuta el proceso
                    valor = ObtenerCampoCLOB(cmd, marca);
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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return valor;
        }



        /// <summary>
        /// Servicio que actualiza el campo DISTINGUE de una Marca
        /// </summary>
        /// <param name="marca">Marca que posee el Distingue</param>
        /// <param name="distingueMarca">Distingue de la marca</param>
        /// <returns>True si el proceso se realiza satisfactoriamente; falso, en caso contrario</returns>
        public bool ActualizarDistingueDeMarca(Marca marca, String distingueMarca)
        {
            bool resultado = false;
            int idMarca = marca.Id;
            
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
                    OracleCommand cmd = con.CreateCommand();
                    cmd.Connection = con;
                    resultado = ActualizarCampoCLOB(cmd, marca,distingueMarca);
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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return resultado;

        }


        private String ObtenerCampoCLOB(OracleCommand cmd, Marca marca)
        {
            //Statement que se va a ejecutar sobre la tabla 
            String selectCommand = "SELECT CMARCA,XDISTINGUE FROM MYP_MARCAS WHERE CMARCA=" + marca.Id.ToString();
            String resultado = String.Empty;

            try
            {
                cmd.CommandText = selectCommand;
                OracleDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    //Se lee el registro
                    reader.Read();
                    //Se obtiene el campo como un OracleLob
                    OracleClob clobField = reader.GetOracleClob(1);
                    if (!clobField.IsNull)
                        resultado = clobField.Value;
                    else
                        resultado = null;
                }
                reader.Close();

                
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


        private bool ActualizarCampoCLOB(OracleCommand cmd, Marca marca, String distingueMarca)
        {
            String selectCommand = "UPDATE MYP_MARCAS SET XDISTINGUE = :p1 WHERE CMARCA = " + marca.Id.ToString(); 
            
            bool resultado = false;

            try
            {
                //Recogiendo los parametros antes de actualizar
                OracleParameter param = new OracleParameter();
                param.Direction = System.Data.ParameterDirection.Input;
                param.OracleDbType = OracleDbType.Clob;
                param.ParameterName = "p1";
                param.Value = distingueMarca;

                //Definiendo el command
                cmd.BindByName = true;
                cmd.Parameters.Add(param);
                cmd.CommandText = selectCommand;
                cmd.ExecuteNonQuery();
                resultado = true;


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
        
    }
}
