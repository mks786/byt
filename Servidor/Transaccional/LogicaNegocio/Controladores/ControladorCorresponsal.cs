using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorCorresponsal : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los Corresponsals del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<Corresponsal> ConsultarTodos()
        {
            IList<Corresponsal> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Corresponsal>> comando = FabricaComandosCorresponsal.ObtenerComandoConsultarTodos();
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


        ///// <summary>
        ///// Método que inserta o modifica un Corresponsal al sistema
        ///// </summary>
        ///// <param name="corresponsal">Corresponsal a modificar</param>
        ///// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        ///// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        //public static bool InsertarOModificar(Corresponsal corresponsal, int hash)
        //{
        //    bool exitoso = false;

        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<bool> comando = FabricaComandosCorresponsal.ObtenerComandoInsertarOModificar(corresponsal);
        //        comando.Ejecutar();
        //        exitoso = comando.Receptor.ObjetoAlmacenado;

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
        //    return exitoso;
        //}


        public static bool InsertarOModificar(Corresponsal corresponsal, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoInteresadoContador = null;

                // si es una insercion
                if (corresponsal.Operacion.Equals("CREATE"))
                {
                    ComandoBase<ContadorFac> comandoContadorInteresadoProximoValor = FabricaComandosContadorFac.ObtenerComandoConsultarPorId("FAC_CORRESPONSAL");
                    comandoContadorInteresadoProximoValor.Ejecutar();
                    ContadorFac contador = comandoContadorInteresadoProximoValor.Receptor.ObjetoAlmacenado;
                    corresponsal.Id = contador.ProximoValor++;
                    comandoInteresadoContador = FabricaComandosContadorFac.ObtenerComandoInsertarOModificar(contador);
                }


                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = corresponsal.Operacion;
                auditoria.Tabla = "FAC_CORRESPONSAL";
                auditoria.Fk = corresponsal.Id;

                ComandoBase<bool> comando = FabricaComandosCorresponsal.ObtenerComandoInsertarOModificar(corresponsal);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);

                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    comandoAuditoria.Ejecutar();
                    comandoAuditoriaContador.Ejecutar();

                    if (comandoInteresadoContador != null)
                        comandoInteresadoContador.Ejecutar();
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
        /// Método que consulta un Corresponsal por su Id
        /// </summary>
        /// <param name="corresponsal">Corresponsal con el Id del pais buscado</param>
        /// <returns>El Corresponsal solicitado</returns>
        public static Corresponsal ConsultarPorId(Corresponsal corresponsal)
        {
            Corresponsal retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Corresponsal> comando = FabricaComandosCorresponsal.ObtenerComandoConsultarPorID(corresponsal);
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
        /// Método que elimina un Corresponsal
        /// </summary>
        /// <param name="usuario">Corresponsal a eliminar</param>
        /// <param name="hash">Hash del Corresponsal que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Corresponsal corresponsal, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCorresponsal.ObtenerComandoEliminarCorresponsal(corresponsal);
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
        /// Verifica si el Corresponsal existe
        /// </summary>
        /// <param name="corresponsal">Corresponsal a verificar</param>
        /// <returns>True de existir, false en caso contrario</returns>
        public static bool VerificarExistencia(Corresponsal corresponsal)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCorresponsal.ObtenerComandoVerificarExistenciaCorresponsal(corresponsal);
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

    }
}