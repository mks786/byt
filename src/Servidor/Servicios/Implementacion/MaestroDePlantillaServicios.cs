using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class MaestroDePlantillaServicios: MarshalByRefObject, IMaestroDePlantillaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        
        /// <summary>
        /// Servicio que Inserta o Modifica los valores de un Maestro de Plantilla 
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de plantilla a insertar o modificar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si el proceso se completa correctamente; false, en caso contrario.</returns>
        public bool InsertarOModificar(MaestroDePlantilla maestroPlantilla, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMaestroDePlantilla.InsertarOModificar(maestroPlantilla, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
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
        }


        public IList<MaestroDePlantilla> ConsultarTodos()
        {
            throw new NotImplementedException();
        }


        public MaestroDePlantilla ConsultarPorId(MaestroDePlantilla entidad)
        {
            throw new NotImplementedException();
        }


        public IList<MaestroDePlantilla> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }


        public bool Eliminar(MaestroDePlantilla entidad, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(MaestroDePlantilla entidad)
        {
            throw new NotImplementedException();
        }




    }
}
