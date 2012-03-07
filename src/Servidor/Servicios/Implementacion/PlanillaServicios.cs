using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class PlanillaServicios : MarshalByRefObject, IPlanillaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public IList<Planilla> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public Planilla ConsultarPorId(Planilla entidad)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Planilla retorno = ControladorPlanilla.ConsultarPorId(entidad);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        public bool InsertarOModificar(Planilla entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Planilla entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Planilla entidad)
        {
            throw new NotImplementedException();
        }

        public Planilla ImprimirProcedimiento(ParametroProcedimiento parametro)
        {
            Planilla retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                retorno = ControladorPlanilla.EjecutarProcedimiento(parametro);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return retorno;
        }
    }
}
