using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorInteresadoMultiple : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene TODOS los interesados vinculados a las patentes de la tabla MYP_PATENTES
        /// </summary>
        /// <returns>Lista de todos los interesados de la tabla MYP_INTERESADOS_PAT</returns>
        public static IList<InteresadoMultiple> ConsultarTodos()
        {
            IList<InteresadoMultiple> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InteresadoMultiple>> comando = FabricaComandosInteresadoMultiple.ObtenerComandoConsultarTodos();
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
        /// Metodo para obtener los Interesados asociados a una patente especifica
        /// </summary>
        /// <param name="patente">Patente usada para filtrar los interesados asociados a la misma</param>
        /// <returns>Lista de interesados asociados a una patente especifica</returns>
        public static IList<InteresadoMultiple> ConsultarInteresadosDePatente(Patente patente)
        {
            IList<InteresadoMultiple> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InteresadoMultiple>> comando = 
                    FabricaComandosInteresadoMultiple.ObtenerComandoConsultarInteresadosDePatente(patente);

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
        /// Metodo para insertar o actualizar un objeto de la entidad InteresadoPatente
        /// </summary>
        /// <param name="interesadoPatente">Objeto InteresadoPatente a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True, si la operacion  se realiza exitosamente; False, en caso contrario</returns>
        public static bool InsertarOModificar(InteresadoMultiple interesadoPatente, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosInteresadoMultiple.ObtenerComandoInsertarOModificar(interesadoPatente);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

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
        /// Metodo que obtiene los Interesados asociados a una marca especifica
        /// </summary>
        /// <param name="marca">Marca usada para filtrar los interesados asociados a la misma</param>
        /// <returns>Lista de interesados asociados a una marca especifica</returns>
        public static IList<InteresadoMultiple> ConsultarInteresadosDeMarca(Marca marca)
        {
            IList<InteresadoMultiple> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InteresadoMultiple>> comando =
                    FabricaComandosInteresadoMultiple.ObtenerComandoConsultarInteresadosDeMarca(marca);

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
