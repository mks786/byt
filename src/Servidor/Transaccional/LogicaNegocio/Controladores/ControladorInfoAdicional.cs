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
    public class ControladorInfoAdicional : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que inserta o modifica una InfoAdicional
        /// </summary>
        /// <param name="InfoAdicional">InfoAdicional a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True: si la modificación fue exitosa; false: en caso contrario</returns>
        public static bool InsertarOModificar(InfoAdicional InfoAdicional, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoInteresadoContador = null;

                
                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;

                string[] IdMarcaTercero = InfoAdicional.Id.Split('-');
                string id = "";
                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = InfoAdicional.Operacion;
                auditoria.Tabla = "MYP_ADICIONAL";
                if (IdMarcaTercero.Count() == 1)
                    id = InfoAdicional.Id.Substring(2, InfoAdicional.Id.Length - 2);
                else
                    id = IdMarcaTercero[1];

                auditoria.Fk = int.Parse(id);


                ComandoBase<bool> comando = FabricaComandosInfoAdicional.ObtenerComandoInsertarOModificar(InfoAdicional);
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
        /// Método que elimina una InfoAdicional
        /// </summary>
        /// <param name="InfoAdicional">InfoAdicional a eliminar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(InfoAdicional InfoAdicional, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosInfoAdicional.ObtenerComandoEliminarInfoAdicional(InfoAdicional);
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
        /// Método que consulta la lista de todos las InfoAdicionals
        /// </summary>
        /// <returns>Lista con todos las InfoAdicionals</returns>
        public static IList<InfoAdicional> ConsultarTodos()
        {
            IList<InfoAdicional> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InfoAdicional>> comando = FabricaComandosInfoAdicional.ObtenerComandoConsultarTodos();
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
        /// Consulta infoAdicional por id
        /// </summary>
        /// <param name="InfoAdicional">InfoAdicional que contiene el id</param>
        /// <returns>InfoAdicinonal que devuelve la consulta</returns>
        public static InfoAdicional ConsultarPorId(InfoAdicional InfoAdicional)
        {
            InfoAdicional retorno = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<InfoAdicional> comando = FabricaComandosInfoAdicional.ObtenerComandoConsultarPorID(InfoAdicional);
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
        /// Verifica si la InfoAdicional existe
        /// </summary>
        /// <param name="InfoAdicional">InfoAdicional a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(InfoAdicional InfoAdicional)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosInfoAdicional.ObtenerComandoVerificarExistenciaInfoAdicional(InfoAdicional);
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
        /// Metodo para obtener la informacion adicional de un grupo de marcas, por el Distingue en Ingles
        /// </summary>
        /// <param name="infoAdicional">Informacion adicional a filtrar que contiene el distingue en ingles a obtener</param>
        /// <returns>Lista de informacion adicional que cumple con la condicion</returns>
        public static IList<InfoAdicional> ObtenerInfoAdicionalDistingueInglesFiltro(InfoAdicional infoAdicional)
        {

            IList<InfoAdicional> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InfoAdicional>> comando = FabricaComandosInfoAdicional.ObtenerComandoObtenerInfoAdicionalDistingueInglesFiltro(infoAdicional);
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

