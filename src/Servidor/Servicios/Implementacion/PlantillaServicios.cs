using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class PlantillaServicios: MarshalByRefObject, IPlantillaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todas las Plantillas
        /// </summary>
        /// <returns>Lista con todas las Plantillas</returns>
        public IList<Plantilla> ConsultarTodos()
        {
            IList<Plantilla> plantillas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                plantillas = ControladorPlantilla.ConsultarTodos();

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

            return plantillas;
        }


        public Plantilla ConsultarPorId(Plantilla entidad)
        {
            throw new NotImplementedException();
        }

        public IList<Plantilla> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Plantilla entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Plantilla entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Plantilla entidad)
        {
            throw new NotImplementedException();
        }


    }
}
