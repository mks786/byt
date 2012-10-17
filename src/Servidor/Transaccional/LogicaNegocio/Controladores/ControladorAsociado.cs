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
    public class ControladorAsociado : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que inserta o modifica un boletin
        /// </summary>
        /// <param name="asociado">Asociado a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True: si la modificación fue exitosa; false: en caso contrario</returns>
        public static bool InsertarOModificar(Asociado asociado, int hash)
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
                if (asociado.Operacion.Equals("CREATE"))
                {
                    ComandoBase<ContadorFac> comandoContadorInteresadoProximoValor = FabricaComandosContadorFac.ObtenerComandoConsultarPorId("FAC_ASOCIADOS");
                    comandoContadorInteresadoProximoValor.Ejecutar();
                    ContadorFac contador = comandoContadorInteresadoProximoValor.Receptor.ObjetoAlmacenado;
                    asociado.Id = contador.ProximoValor++;
                    comandoInteresadoContador = FabricaComandosContadorFac.ObtenerComandoInsertarOModificar(contador);
                }


                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = asociado.Operacion;
                auditoria.Tabla = "FAC_ASOCIADOS";
                auditoria.Fk = asociado.Id;

                ComandoBase<bool> comando = FabricaComandosAsociado.ObtenerComandoInsertarOModificar(asociado);
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
        /// Método que elimina un asociado
        /// </summary>
        /// <param name="asociado">Asociado a eliminar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Asociado asociado, int hash)
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
                auditoria.Operacion = asociado.Operacion;
                auditoria.Tabla = "FAC_ASOCIADOS";
                auditoria.Fk = asociado.Id;

                ComandoBase<bool> comandoAsociado = FabricaComandosAsociado.ObtenerComandoEliminarAsociado(asociado);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);


                comandoAsociado.Ejecutar();
                exitoso = comandoAsociado.Receptor.ObjetoAlmacenado;

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

        /// <summary>
        /// Método que consulta la lista de todos los asociados
        /// </summary>
        /// <returns>Lista con todos los boletines</returns>
        public static IList<Asociado> ConsultarTodos()
        {
            IList<Asociado> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Asociado>> comando = FabricaComandosAsociado.ObtenerComandoConsultarTodos();
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
        /// Método que consulta un asociado con todas sus dependencias
        /// </summary>
        /// <returns>Asociado completo</returns>
        public static Asociado ConsultarAsociadoConTodo(Asociado asociado)
        {
            Asociado retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Asociado> comando = FabricaComandosAsociado.ObtenerComandoConsultarAsociadoConTodo(asociado);
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
        /// Método que consulta un Interesado con uno o mas filtros
        /// </summary>
        /// <returns>Interesado Filtrado</returns>
        public static IList<Asociado> ConsultarAsociadosFiltro(Asociado asociado)
        {
            IList<Asociado> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Asociado>> comando = FabricaComandosAsociado.ObtenerComandoConsultarAsociadosFiltro(asociado);
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
        /// Método que consulta si un asociado tiene cartas
        /// </summary>
        /// <param name="asociado">Asociado a verificar</param>
        /// <returns>true en caso de que existe</returns>
        public static bool VerificarCartasPorAsociado(Asociado asociado)
        {
            bool retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosAsociado.ObtenerComandoVerificarCartasPorAsociado(asociado);
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

        public static IList<ContactosDelAsociadoVista> ConsultarContactosDelAsociado(Asociado asociado, bool todos)
        {
            IList<ContactosDelAsociadoVista> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<ContactosDelAsociadoVista>> comando = FabricaComandosAsociado.ObtenerComandoConsultarContactosDelAsociado(asociado, todos);
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

