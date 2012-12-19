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
    public class ControladorPlanilla : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que consulta un Planilla por su Id
        /// </summary>
        /// <param name="planilla">Planilla con el Id del pais buscado</param>
        /// <returns>El Planilla solicitado</returns>
        public static Planilla ConsultarPorId(Planilla planilla)
        {
            Planilla retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Planilla> comando = FabricaComandosPlanilla.ObtenerComandoConsultarPorId(planilla.Id);
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

        public static Planilla EjecutarProcedimiento(ParametroProcedimiento parametro)
        {
            Planilla retorno = new Planilla();
            bool exitoso;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                int id = SimularPID(parametro.Usuario.Iniciales);

                if (parametro.Via == 1)
                {
                    ComandoBase<bool> comandoEliminarPlanilla = FabricaComandosPlanilla.ObtenerComandoEliminarPlanilla(new Planilla(id));
                    comandoEliminarPlanilla.Ejecutar();

                }

                ComandoBase<bool> comando = FabricaComandosPlanilla.ObtenerComandoEjecutarProcedimiento(parametro);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    //ComandoBase<Planilla> ComandoPlanillaABuscar = FabricaComandosPlanilla.ObtenerComandoConsultarPlanillaPorUsuario(usuario);
                    //ComandoPlanillaABuscar.Ejecutar();
                    //Planilla planilla = ComandoPlanillaABuscar.Receptor.ObjetoAlmacenado;

                    //Funcion de base de datos a la cual hay que realizar el llamado a través
                    //de NHibernate-----------------------------------------------------------
                    Planilla planilla = new Planilla(SimularPID(parametro.Usuario.Iniciales));
                    //------------------------------------------------------------------------

                    if (planilla != null)
                    {
                        ComandoBase<Planilla> comandoLeerPlanilla = FabricaComandosPlanilla.ObtenerComandoConsultarPorId(planilla.Id);
                        comandoLeerPlanilla.Ejecutar();
                        retorno = comandoLeerPlanilla.Receptor.ObjetoAlmacenado;
                    }
                    else
                        retorno = null;
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