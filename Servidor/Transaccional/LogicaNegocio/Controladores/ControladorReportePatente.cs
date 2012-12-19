using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Text;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorReportePatente : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que consulta un ReportePatente por su Id
        /// </summary>
        /// <param name="ReportePatente">ReportePatente con el Id del pais buscado</param>
        /// <returns>El ReportePatente solicitado</returns>
        public static ReportePatente ConsultarPorId(ReportePatente ReportePatente)
        {
            ReportePatente retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<ReportePatente> comando = FabricaComandosReportePatente.ObtenerComandoConsultarPorId(ReportePatente.Id);
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

        public static ReportePatente EjecutarProcedimiento(ParametroProcedimiento parametro)
        {
            ReportePatente retorno = new ReportePatente();
            bool exitoso;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (parametro.Via == 1)
                {
                    ComandoBase<bool> comandoEliminarReportePatente = FabricaComandosReportePatente.ObtenerComandoEliminarReportePatente(parametro.Id);
                    comandoEliminarReportePatente.Ejecutar();

                }

                ComandoBase<bool> comando = FabricaComandosReportePatente.ObtenerComandoEjecutarProcedimiento(parametro);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                        ComandoBase<ReportePatente> comandoLeerReportePatente = FabricaComandosReportePatente.ObtenerComandoConsultarReportePatentePorCodigoPatente(parametro.Id);
                        comandoLeerReportePatente.Ejecutar();
                        retorno = comandoLeerReportePatente.Receptor.ObjetoAlmacenado;
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
            return retorno;
        }

        private static int SimularPID(string usuario)
        {
            int retorno = 0;

            while (usuario.Length > 1)
            {
                retorno = retorno * 26 + (((int)usuario.Substring(0, 1)[0]) - (int)'A');
                usuario = usuario.Substring(1);
            }

            return retorno;
        }
    }
}