using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorLicenciaPatente : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los Usuarios del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<LicenciaPatente> ConsultarTodos()
        {
            IList<LicenciaPatente> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
               
                    ComandoBase<IList<LicenciaPatente>> comando = FabricaComandosLicenciaPatente.ObtenerComandoConsultarTodos();
                    comando.Ejecutar();
                    retorno = comando.Receptor.ObjetoAlmacenado;                

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
            return retorno;
        }

        /// <summary>
        /// Método que modifica un los datos de un Usuario
        /// </summary>
        /// <param name="licencia">Usuario a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(ref LicenciaPatente licencia, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (licencia.Id == 0)
                {
                    ComandoBase<bool> comandoLicenciaContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorLicenciaProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_PLICENCIAS");
                    comandoContadorLicenciaProximoValor.Ejecutar();
                    Contador contadorLicencia = comandoContadorLicenciaProximoValor.Receptor.ObjetoAlmacenado;

                    comandoLicenciaContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorLicencia);
                    licencia.Id = contadorLicencia.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    operacion.Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", System.DateTime.Now));
                    operacion.Aplicada = 'P';
                    operacion.CodigoAplicada = licencia.Patente.Id;
                    operacion.Interno = licencia.Id;
                    operacion.Servicio = new Servicio("LU");

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<bool> comando = FabricaComandosLicenciaPatente.ObtenerComandoInsertarOModificar(licencia);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    ComandoBase<Patente> comandoMarca = FabricaComandosPatente.ObtenerComandoConsultarPatenteConTodo(licencia.Patente);
                    comandoMarca.Ejecutar();
                    Patente patente = comandoMarca.Receptor.ObjetoAlmacenado;
                    patente.Interesado = licencia.InteresadoLicenciatario;
                    patente.Agente = licencia.AgenteLicenciatario;
                    patente.Poder = licencia.PoderLicenciatario;

                    ComandoBase<bool> comandoEditarPatente = FabricaComandosPatente.ObtenerComandoInsertarOModificar(patente);
                    comandoEditarPatente.Ejecutar();
                    bool exitosoPatente = comandoEditarPatente.Receptor.ObjetoAlmacenado;


                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if ((exitoso) && (exitosoPatente))
                    {
                        comandoLicenciaContador.Ejecutar();
                        comandoOperacionContador.Ejecutar();
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosLicenciaPatente.ObtenerComandoInsertarOModificar(licencia);
                    comando.Ejecutar();
                    exitoso = comando.Receptor.ObjetoAlmacenado;
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
            return exitoso;
        }

        /// <summary>
        /// Método que consulta un LicenciaPatente por su Id
        /// </summary>
        /// <param name="LicenciaPatente">LicenciaPatente con el Id del LicenciaPatente buscado</param>
        /// <returns>El LicenciaPatente solicitado</returns>
        public static LicenciaPatente ConsultarPorId(LicenciaPatente licencia)
        {
            LicenciaPatente retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<LicenciaPatente> comando = FabricaComandosLicenciaPatente.ObtenerComandoConsultarPorID(licencia);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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
            return retorno;
        }

        /// <summary>
        /// Método que elimina un licencia
        /// </summary>
        /// <param name="usuario">LicenciaPatente a eliminar</param>
        /// <param name="hash">Hash del licencia que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(LicenciaPatente licencia, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosLicenciaPatente.ObtenerComandoEliminarLicenciaPatente(licencia);
                comando.Ejecutar();
                exitoso = true;

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

            return exitoso;
        }

        /// <summary>
        /// Verifica si el licencia existe
        /// </summary>
        /// <param name="licencia">LicenciaPatente a verificar</param>
        /// <returns>True de existir, false en caso contrario</returns>
        public static bool VerificarExistencia(LicenciaPatente licencia)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosLicenciaPatente.ObtenerComandoVerificarExistenciaLicenciaPatente(licencia);
                comando.Ejecutar();
                existe = comando.Receptor.ObjetoAlmacenado;

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

            return existe;
        }

        public static IList<LicenciaPatente> ConsultarLicenciasFiltro(LicenciaPatente licencia)
        {
            IList<LicenciaPatente> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<LicenciaPatente>> comando = FabricaComandosLicenciaPatente.ObtenerComandoConsultarLicenciasPatenteFiltro(licencia);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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

            return retorno;
        }
    }
}