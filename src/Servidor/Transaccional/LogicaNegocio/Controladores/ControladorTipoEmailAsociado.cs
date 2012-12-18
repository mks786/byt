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
    public class ControladorTipoEmailAsociado : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool VerificarExistencia(TipoEmailAsociado TipoEmailAsociado)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosTipoEmailAsociado.ObtenerComandoVerificarExistenciaTipoEmailAsociado(TipoEmailAsociado);
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

        /// <summary>
        /// Método que consulta la lista de todos los boletines
        /// </summary>
        /// <returns>Lista con todos los boletines</returns>
        public static IList<TipoEmailAsociado> ConsultarTodos()
        {
            IList<TipoEmailAsociado> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<TipoEmailAsociado>> comando = FabricaComandosTipoEmailAsociado.ObtenerComandoConsultarTodos();
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
        ///// Método que inserta o modifica un boletin
        ///// </summary>
        ///// <param name="boletin">Boletin a insertar o modificar</param>
        ///// <param name="hash">Hash del usuario que realiza la operacion</param>
        ///// <returns>True: si la modificación fue exitosa; false: en caso contrario</returns>
        public static bool InsertarOModificar(TipoEmailAsociado TipoEmailAsociado, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = TipoEmailAsociado.Operacion;
                auditoria.Tabla = "FAC_ASO_TIPO_COR";
                auditoria.Fk = int.Parse(TipoEmailAsociado.Id);

                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);
                ComandoBase<bool> comandoInsertar = FabricaComandosTipoEmailAsociado.ObtenerComandoInsertarOModificar(TipoEmailAsociado);

                comandoInsertar.Ejecutar();

                exitoso = comandoInsertar.Receptor.ObjetoAlmacenado;

                if (exitoso) {
                    comandoAuditoria.Ejecutar();
                    comandoAuditoriaContador.Ejecutar();
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


        ///// <summary>
        ///// Método que elimina un boletin
        ///// </summary>
        ///// <param name="boletin">Boletin a eliminar</param>
        ///// <param name="hash">Hash del usuario que realiza la operacion</param>
        ///// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(TipoEmailAsociado TipoEmailAsociado, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = TipoEmailAsociado.Operacion;
                auditoria.Tabla = "FAC_ASO_TIPO_COR";
                auditoria.Fk = int.Parse(TipoEmailAsociado.Id);

                ComandoBase<bool> comandoTipoEmailAsociado = FabricaComandosTipoEmailAsociado.ObtenerComandoEliminarTipoEmailAsociado(TipoEmailAsociado);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);

                comandoTipoEmailAsociado.Ejecutar();
                exitoso = comandoTipoEmailAsociado.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    comandoAuditoria.Ejecutar();
                    comandoAuditoriaContador.Ejecutar();
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
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<IList<Boletin>> comando = FabricaComandosBoletin.ObtenerComandoConsultarTodos();
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
        //}

    }
}

