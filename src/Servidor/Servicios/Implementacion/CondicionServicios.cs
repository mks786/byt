using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CondicionServicios : MarshalByRefObject, ICondicionServicios 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todas las condiciones
        /// </summary>
        /// <returns>Todas las condiciones</returns>
        public IList<Condicion> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Condicion> condiciones = ControladorCondicion.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return condiciones;
        }


        public Condicion ConsultarPorId(Condicion entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que inserta o modifica una condicion
        /// </summary>
        /// <param name="condicion">condicion a insertar o modificar</param>
        /// <param name="hash">hash del usuario logerad</param>
        /// <returns></returns>
        public bool InsertarOModificar(Condicion condicion, int hash)
        {
            throw new NotImplementedException();
        }


        public bool Eliminar(Condicion entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Condicion entidad)
        {
            throw new NotImplementedException();
        }
    }
}
