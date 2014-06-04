using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class AccionServicios : MarshalByRefObject, IAccionServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que obtiene todas las Acciones
        /// </summary>
        /// <returns>Lista de todas las Acciones</returns>
        public IList<Accion> ConsultarTodos()
        {
            IList<Accion> acciones;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                acciones = ControladorAccion.ConsultarTodos();

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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return acciones;
        }

        public Accion ConsultarPorId(Accion entidad)
        {
            throw new NotImplementedException();
        }

        public IList<Accion> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Accion entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Accion entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Accion entidad)
        {
            throw new NotImplementedException();
        }
    }
}
