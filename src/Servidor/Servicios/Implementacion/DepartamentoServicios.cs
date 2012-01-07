using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class DepartamentoServicios : MarshalByRefObject, IDepartamentoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los Departamentos
        /// </summary>
        /// <returns>Todos los departamentos</returns>
        public IList<Departamento> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Departamento> departamentos = ControladorDepartamento.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return departamentos;
        }


        public Departamento ConsultarPorId(Departamento entidad)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Departamento entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Departamento entidad, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(Departamento entidad)
        {
            throw new NotImplementedException();
        }
    }
}
