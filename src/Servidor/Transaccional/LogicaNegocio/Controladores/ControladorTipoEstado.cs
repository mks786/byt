using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorTipoEstado : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los TipoEstado
        /// </summary>
        /// <returns></returns>
        public static IList<TipoEstado> ConsultarTodos()
        {
            IList<TipoEstado> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<TipoEstado>> comando = FabricaComandosTipoEstado.ObtenerComandoConsultarTodos();
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
        /// Método que modifica un los datos de un TipoEstado
        /// </summary>
        /// <param name="usuario">TipoEstado a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(TipoEstado tipoEstado, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosTipoEstado.ObtenerComandoInsertarOModificar(tipoEstado);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
        /// Método que consulta un TipoEstado por su Id
        /// </summary>
        /// <param name="servicio">TipoEstado con el Id del TipoEstado buscado</param>
        /// <returns>El TipoEstado solicitado</returns>
        public static TipoEstado ConsultarPorId(TipoEstado tipoEstado)
        {
            TipoEstado retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<TipoEstado> comando = FabricaComandosTipoEstado.ObtenerComandoConsultarPorID(tipoEstado);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
        /// Método que elimina un TipoEstado
        /// </summary>
        /// <param name="usuario">TipoEstado a eliminar</param>
        /// <param name="hash">Hash del TipoEstado que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(TipoEstado tipoEstado, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosTipoEstado.ObtenerComandoEliminarTipoEstado(tipoEstado);
                comando.Ejecutar();
                exitoso = true;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
        /// Verifica si el TipoEstado existe
        /// </summary>
        /// <param name="tipoEstado">TipoEstado a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(TipoEstado tipoEstado)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosTipoEstado.ObtenerComandoVerificarExistenciaTipoEstado(tipoEstado);
                comando.Ejecutar();
                existe = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            return existe;
        }
    }
}