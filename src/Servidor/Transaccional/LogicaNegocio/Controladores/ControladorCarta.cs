using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorCarta : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool VerificarExistencia(Carta carta)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCarta.ObtenerComandoVerificarExistenciaCarta(carta);
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
        ///// <summary>
        ///// Método que inserta o modifica un boletin
        ///// </summary>
        ///// <param name="boletin">Boletin a insertar o modificar</param>
        ///// <param name="hash">Hash del usuario que realiza la operacion</param>
        ///// <returns>True: si la modificación fue exitosa; false: en caso contrario</returns>
        //public static bool InsertarOModificar(Boletin boletin, int hash)
        //{
        //    bool exitoso = false;
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<bool> comando = FabricaComandosBoletin.ObtenerComandoInsertarOModificar(boletin);
        //        comando.Ejecutar();
        //        exitoso = true;

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }
        //    return exitoso;
        //}

        ///// <summary>
        ///// Método que elimina un boletin
        ///// </summary>
        ///// <param name="boletin">Boletin a eliminar</param>
        ///// <param name="hash">Hash del usuario que realiza la operacion</param>
        ///// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        //public static bool Eliminar(Boletin boletin, int hash)
        //{
        //    bool exitoso = false;
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<bool> comando = FabricaComandosBoletin.ObtenerComandoEliminarBoletin(boletin);
        //        comando.Ejecutar();
        //        exitoso = true;

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }

        //    return exitoso;
        //}

        ///// <summary>
        ///// Método que consulta la lista de todos los boletines
        ///// </summary>
        ///// <returns>Lista con todos los boletines</returns>
        //public static IList<Boletin> ConsultarTodos()
        //{
        //    IList<Boletin> retorno;

        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<IList<Boletin>> comando = FabricaComandosBoletin.ObtenerComandoConsultarTodos();
        //        comando.Ejecutar();
        //        retorno = comando.Receptor.ObjetoAlmacenado;

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }

        //    return retorno;
        //}
    }
}

