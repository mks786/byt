using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class JustificacionServicios : MarshalByRefObject, IJustificacionServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IList<Justificacion> IServicioBase<Justificacion>.ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public Justificacion ConsultarPorId(Justificacion entidad)
        {
            throw new NotImplementedException();
        }
 
        /// <summary>
        /// Metodo que inserta o modifica una justificacion
        /// </summary>
        /// <param name="justificacion">Justificacion a Insertar/Modificar</param>
        /// <param name="hash"></param>
        /// <returns>Booleano, true - exitoso, false - fallido</returns>
        public bool InsertarOModificar(Justificacion justificacion, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorJustificacion.InsertarOModificar(justificacion, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        public bool Eliminar(Justificacion entidad, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(Justificacion entidad)
        {
            throw new NotImplementedException();
        }
    }
}
