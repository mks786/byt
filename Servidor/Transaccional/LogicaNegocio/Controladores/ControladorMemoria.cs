using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorMemoria : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que inserta un los datos de un Memoria
        /// </summary>
        /// <param name="memoria">Memoria a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la insercion fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(Memoria memoria, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoMemoria = FabricaComandosMemoria.ObtenerComandoInsertarOModificar(memoria);

                comandoMemoria.Ejecutar();
                exitoso = comandoMemoria.Receptor.ObjetoAlmacenado;

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
        /// Método que consulta un Memoria por su Id
        /// </summary>
        /// <param name="Memoria">Memoria con el Id del Memoria buscado</param>
        /// <returns>El Memoria solicitado</returns>
        public static Memoria ConsultarPorId(Memoria memoria)
        {
            Memoria retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Memoria> comando = FabricaComandosMemoria.ObtenerComandoConsultarPorID(memoria);
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
        /// Método que elimina un memoria
        /// </summary>
        /// <param name="memoria">Memoria a eliminar</param>
        /// <param name="hash">Hash del memoria que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Memoria memoria, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoMemoria = FabricaComandosMemoria.ObtenerComandoEliminarMemoria(memoria);

                comandoMemoria.Ejecutar();
                exitoso = comandoMemoria.Receptor.ObjetoAlmacenado;

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
        /// Método que devuelve todos los Memoriaes del sistema que poseen el interesado que entra por parametro
        /// </summary>
        /// <param name="patente">Interesado para filtrar la consulta</param>
        /// <returns>Lista de memoriaes que posee ese interesado</returns>
        public static IList<Memoria> ConsultarMemoriasPorPatente(Patente patente)
        {
            IList<Memoria> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Memoria>> comando = FabricaComandosMemoria.ObtenerComandoConsultarMemoriasPorPatente(patente);
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

        public static bool VerificarExistencia(Patente patente, Memoria memoriaAInsertar)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Memoria>> comando = FabricaComandosMemoria.ObtenerComandoConsultarMemoriasPorPatente(patente);
                comando.Ejecutar();
                IList<Memoria> memorias = comando.Receptor.ObjetoAlmacenado;

                foreach (Memoria memoria in memorias)
                {
                    if (memoria.Id == memoriaAInsertar.Id)
                    {
                        existe = true;
                        break;
                    }
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

            return existe;
        }
    }
}