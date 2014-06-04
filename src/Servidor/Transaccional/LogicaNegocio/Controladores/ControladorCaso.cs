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
    public class ControladorCaso : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que obtiene todo el contenido de la tabla de Casos
        /// </summary>
        /// <returns>Contenido de toda la tabla de Casos</returns>
        public static IList<Caso> ConsultarTodos()
        {
            IList<Caso> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Caso>> comando = FabricaComandosCaso.ObtenerComandoConsultarTodos();
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
        /// Metodo que inserta y/o actualiza un Caso 
        /// </summary>
        /// <param name="caso">Caso a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; False, en caso contrario</returns>
        public static bool InsertarOModificar(ref Caso caso, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoContador = null;

                // si es una insercion
                if (caso.Operacion.Equals("CREATE"))
                {
                    ComandoBase<Contador> comandoContadorProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("PRT_CASOS");
                    comandoContadorProximoValor.Ejecutar();
                    Contador contador = comandoContadorProximoValor.Receptor.ObjetoAlmacenado;
                    caso.Id = contador.ProximoValor++;
                    comandoContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contador);
                }

                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = caso.Operacion;
                auditoria.Tabla = "PRT_CASOS";
                auditoria.Fk =caso.Id;

                ComandoBase<bool> comando = FabricaComandosCaso.ObtenerComandoInsertarOModificar(caso);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);

                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    comandoAuditoria.Ejecutar();
                    comandoAuditoriaContador.Ejecutar();

                    if (comandoContador != null)
                        comandoContador.Ejecutar();
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
        /// Metodo que obtiene una lista de casos dependiendo de un filtro
        /// </summary>
        /// <param name="caso">Caso usado como filtro</param>
        /// <returns>Lista de Casos de acuerdo al filtro especificado</returns>
        public static IList<Caso> ObtenerCasosFiltro(Caso caso)
        {
            IList<Caso> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Caso>> comando = FabricaComandosCaso.ObtenerComandoConsultarCasosFiltro(caso);
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
