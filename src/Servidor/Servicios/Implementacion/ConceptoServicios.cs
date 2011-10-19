using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class ConceptoServicios : MarshalByRefObject, IConceptoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los conceptos
        /// </summary>
        /// <returns>Lista con todos los conceptos</returns>
        public IList<Concepto> ConsultarTodos()
        {
            IList<Concepto> conceptos;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                conceptos = ControladorConcepto.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return conceptos;
        }

        public Concepto ConsultarPorId(Concepto concepto)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Concepto concepto, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Concepto concepto, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(Concepto entidad)
        {
            throw new NotImplementedException();
        }
    }
}
