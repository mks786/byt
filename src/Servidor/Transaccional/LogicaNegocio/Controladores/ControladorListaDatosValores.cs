using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorListaDatosValores : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los Usuarios del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<ListaDatosValores> ConsultarTodos()
        {
            throw new NotImplementedException();
            //IList<ListaDatosValores> retorno;
            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    ComandoBase<IList<ListaDatosValores>> comando = FabricaComandosListaDatosValores.ObtenerComandoConsultarTodos();
            //    comando.Ejecutar();
            //    retorno = comando.Receptor.ObjetoAlmacenado;

            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion
            //}
            //catch (ApplicationException ex)
            //{
            //    logger.Error(ex.Message);
            //    throw ex;
            //}
            //return retorno;
        }

        /// <summary>
        /// Método que modifica un los datos de un Usuario
        /// </summary>
        /// <param name="ListaDatosValores">Usuario a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(ListaDatosValores ListaDatosValores, int hash)
        {

            throw new NotImplementedException();
            //bool exitoso = false;

            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    ComandoBase<bool> comando = FabricaComandosListaDatosValores.ObtenerComandoInsertarOModificar(ListaDatosValores);
            //    comando.Ejecutar();
            //    exitoso = comando.Receptor.ObjetoAlmacenado;

            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion
            //}
            //catch (ApplicationException ex)
            //{
            //    logger.Error(ex.Message);
            //    throw ex;
            //}
            //return exitoso;
        }

        /// <summary>
        /// Método que consulta Listas de Datos de Valores por un parametro
        /// </summary>
        /// <param name="ListaDatosValores">ListaDatosValores con el parametro de la lista de ListaDatosValores buscada</param>
        /// <returns>La lista de ListaDatosValores solicitada</returns>
        public static IList<ListaDatosValores> ConsultarListaDatosValoresPorParametro(ListaDatosValores ListaDatosValores)
        {
            IList<ListaDatosValores> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<ListaDatosValores>> comando = FabricaComandosListaDatosValores.ObtenerComandoConsultarListaDatosValoresPorParametro(ListaDatosValores);
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
        /// Método que elimina un ListaDatosValores
        /// </summary>
        /// <param name="usuario">ListaDatosValores a eliminar</param>
        /// <param name="hash">Hash del ListaDatosValores que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(ListaDatosValores ListaDatosValores, int hash)
        {
            throw new NotImplementedException();
            //bool exitoso = false;
            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    ComandoBase<bool> comando = FabricaComandosListaDatosValores.ObtenerComandoEliminarListaDatosValores(ListaDatosValores);
            //    comando.Ejecutar();
            //    exitoso = true;

            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion
            //}
            //catch (ApplicationException ex)
            //{
            //    logger.Error(ex.Message);
            //    throw ex;
            //}

            //return exitoso;
        }

        /// <summary>
        /// Verifica si el ListaDatosValores existe
        /// </summary>
        /// <param name="ListaDatosValores">ListaDatosValores a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(ListaDatosValores ListaDatosValores)
        {

            throw new NotImplementedException();
            //bool existe = false;
            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    ComandoBase<bool> comando = FabricaComandosListaDatosValores.ObtenerComandoVerificarExistenciaListaDatosValores(ListaDatosValores);
            //    comando.Ejecutar();
            //    existe = comando.Receptor.ObjetoAlmacenado;

            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion
            //}
            //catch (ApplicationException ex)
            //{
            //    logger.Error(ex.Message);
            //    throw ex;
            //}

            //return existe;
        }
    }
}