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
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCarta.ObtenerComandoVerificarExistenciaCarta(carta);
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
        public static IList<Carta> ConsultarTodos()
        {
            IList<Carta> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Carta>> comando = FabricaComandosCarta.ObtenerComandoConsultarTodos();
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
        public static bool InsertarOModificar(Carta carta, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoInsertarOModificarContadorAsignacion = null;

                if ((null != carta.Asignaciones) && (carta.Asignaciones.Count != 0))
                {
                    ComandoBase<ContadorAsignacion> comandoContadorAsignacionPoximoValor = FabricaComandosContadorAsignacion.ObtenerComandoConsultarPorId("ASIGNACION");

                    comandoContadorAsignacionPoximoValor.Ejecutar();
                    ContadorAsignacion contadorAsignacion = comandoContadorAsignacionPoximoValor.Receptor.ObjetoAlmacenado;

                    foreach (Asignacion asignacion in carta.Asignaciones)
                    {
                        if (asignacion.Id == 0)
                        {
                            asignacion.Id = contadorAsignacion.ProximoValor++;
                            asignacion.Carta = carta;
                        }
                    }

                    comandoInsertarOModificarContadorAsignacion = FabricaComandosContadorAsignacion.ObtenerComandoInsertarOModificar(contadorAsignacion);

                }

                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = carta.Operacion;
                auditoria.Tabla = "ENTRADA";
                auditoria.Fk = carta.Id;

                ComandoBase<bool> comando = FabricaComandosCarta.ObtenerComandoInsertarOModificar(carta);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);

                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    comandoAuditoria.Ejecutar();
                    comandoAuditoriaContador.Ejecutar();

                    if (comandoInsertarOModificarContadorAsignacion != null)
                    {
                        comandoInsertarOModificarContadorAsignacion.Ejecutar();
                        foreach (Asignacion asignacion in carta.Asignaciones)
                        {
                            ComandoBase<bool> comandoAsignacion = FabricaComandosAsignacion.ObtenerComandoInsertarOModificar(asignacion);
                            asignacion.Iniciales = asignacion.Responsable.Iniciales;
                            comandoAsignacion.Ejecutar();
                        }
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
            return exitoso;
        }

        ///// <summary>
        ///// Método que elimina un boletin
        ///// </summary>
        ///// <param name="boletin">Boletin a eliminar</param>
        ///// <param name="hash">Hash del usuario que realiza la operacion</param>
        ///// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Carta carta, int hash)
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
                auditoria.Operacion = carta.Operacion;
                auditoria.Tabla = "ENTRADA";
                auditoria.Fk = carta.Id;

                ComandoBase<bool> comandoInteresado = FabricaComandosCarta.ObtenerComandoEliminarCarta(carta);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);


                comandoInteresado.Ejecutar();
                exitoso = comandoInteresado.Receptor.ObjetoAlmacenado;

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

        public static IList<Carta> ConsultarCartasFiltro(Carta carta)
        {
            IList<Carta> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Carta>> comando = FabricaComandosCarta.ObtenerComandoConsultarCartasFiltro(carta);
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

