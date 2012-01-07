using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorListaDatosDominio : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static IList<ListaDatosDominio> ConsultarListaDatosDominioPorParametro(ListaDatosDominio parametro)
        {
            IList<ListaDatosDominio> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<ListaDatosDominio>> comando = 
                    FabricaComandosListaDatosDominio.ObtenerComandoConsultarListaDominioPorParametro(parametro);
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
        /// Método que devuelve todos los Usuarios del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<Pais> ConsultarTodos()
        {
            throw new NotImplementedException();
        //    IList<Pais> retorno;
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<IList<Pais>> comando = FabricaComandosPais.ObtenerComandoConsultarTodos();
        //        comando.Ejecutar();
        //        retorno = comando.Receptor.ObjetoAlmacenado;

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }
        //    return retorno;
        }

        /// <summary>
        /// Método que modifica un los datos de un Usuario
        /// </summary>
        /// <param name="usuario">Usuario a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(Pais pais, int hash)
        {
            throw new NotImplementedException();
            //bool exitoso = false;

            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    ComandoBase<bool> comando = FabricaComandosPais.ObtenerComandoInsertarOModificar(pais);
            //    comando.Ejecutar();
            //    exitoso = comando.Receptor.ObjetoAlmacenado;

            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
        /// Método que consulta un pais por su Id
        /// </summary>
        /// <param name="pais">Pais con el Id del pais buscado</param>
        /// <returns>El pais solicitado</returns>
        public static Pais ConsultarPorId(Pais pais)
        {
            throw new NotImplementedException();
            //Pais retorno;

            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    ComandoBase<Pais> comando = FabricaComandosPais.ObtenerComandoConsultarPorID(pais);
            //    comando.Ejecutar();
            //    retorno = comando.Receptor.ObjetoAlmacenado;

            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
        /// Método que elimina un pais
        /// </summary>
        /// <param name="usuario">Pais a eliminar</param>
        /// <param name="hash">Hash del pais que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Pais pais, int hash)
        {
            throw new NotImplementedException();
            //bool exitoso = false;
            //try
            //{
            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion

            //    ComandoBase<bool> comando = FabricaComandosPais.ObtenerComandoEliminarPais(pais);
            //    comando.Ejecutar();
            //    exitoso = true;

            //    #region trace
            //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //        logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //    #endregion
            //}
            //catch (ApplicationException ex)
            //{
            //    logger.Error(ex.Message);
            //    throw ex;
            //}

            //return exitoso;
        }

    }
}