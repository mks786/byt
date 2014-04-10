using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorRemitente : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los Remitentes del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<Remitente> ConsultarTodos()
        {
            IList<Remitente> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase <IList<Remitente>> comando = FabricaComandosRemitente.ObtenerComandoConsultarTodos();
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
        /// Método que inserta un los datos de un Remitente
        /// </summary>
        /// <param name="remitente">Remitente a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la insercion fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(ref Remitente remitente, int hash)
        {
            bool exitoso = false;
            int longitudId = 4; //Esto es la cantidad de caracteres que tendra el Id segun Base de Datos

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoRemitenteContador = null;

                // si es una insercion
                if (remitente.Operacion.Equals("CREATE"))
                {
                    ComandoBase<Contador> comandoContadorRemitenteProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("REMITENTE");
                    comandoContadorRemitenteProximoValor.Ejecutar();
                    Contador contador = comandoContadorRemitenteProximoValor.Receptor.ObjetoAlmacenado;
                    String idContador = contador.ProximoValor++.ToString().Trim();
                    int strLenght = idContador.Length;
                    if (strLenght < longitudId)
                    {
                        String strId = idContador.PadLeft(4, '0');
                        remitente.Id = strId;
                    }
                    else if (strLenght == longitudId)
                    {
                        remitente.Id = idContador;
                    }
                    else
                    {
                        throw new ApplicationException("El Id del Remitente está restringido a sólo 4 dígitos, consulte con el Administrador del Sistema");
                    }

                    //remitente.Id = contador.ProximoValor++.ToString();
                    comandoRemitenteContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contador);
                }

                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");
                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;

                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = remitente.Operacion;
                auditoria.Tabla = "REMITENTE";
                auditoria.Fk = int.Parse(remitente.Id);

                ComandoBase<bool> comando = FabricaComandosRemitente.ObtenerComandoInsertarOModificar(remitente);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);

                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    comandoAuditoria.Ejecutar();
                    comandoAuditoriaContador.Ejecutar();
                    if (comandoRemitenteContador != null)
                        comandoRemitenteContador.Ejecutar();
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
        /// Método que consulta un remitente por su Id
        /// </summary>
        /// <param name="remitente">Remitente con el Id del remitente buscado</param>
        /// <returns>El remitente solicitado</returns>
        public static Remitente ConsultarPorId(Remitente remitente)
        {
            Remitente retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Remitente> comando = FabricaComandosRemitente.ObtenerComandoConsultarPorID(remitente);
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
        /// Método que elimina un Remitente
        /// </summary>
        /// <param name="remitente">Remitente a eliminar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Remitente remitente, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosRemitente.ObtenerComandoEliminarRemitente(remitente);
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
    }
}