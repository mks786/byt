using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorJustificacion : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que inserta o modifica una modificacion
        /// </summary>
        /// <param name="agente">Justificacion a insertar/modificar</param>
        /// <param name="hash"></param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(Justificacion justificacion, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosJustificacion.ObtenerComandoInsertarOModificar(justificacion);
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
        /// Metodo que elimina una justificacion
        /// </summary>
        /// <param name="justificacion">Justificacion a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Justificacion justificacion, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //ComandoBase<bool> comando = FabricaComandosJustificacion.ObtenerComandoInsertarOModificar(justificacion);
                ComandoBase<bool> comando = FabricaComandosJustificacion.ObtenerComandoEliminar(justificacion);
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
    }
}