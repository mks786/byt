using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CompraSapiServicios : MarshalByRefObject, ICompraSapiServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio para insertar o actualizar una Compra de Material Sapi
        /// </summary>
        /// <param name="compra">Compra de Material a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza exitosamente; False, en caso contrario</returns>
        public bool InsertarOModificar(CompraSapi compra, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCompraSapi.InsertarOModificar(compra, hash);

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

        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="compra">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(CompraSapi compra)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //bool exitoso = ControladorCarta.VerificarExistencia(carta);
                bool exitoso = ControladorCompraSapi.VerificarExistencia(compra);

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

        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de entidades</returns>
        public IList<CompraSapi> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        
        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<CompraSapi> ConsultarPorOtroCampo(String campoConsultar, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        

        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public CompraSapi ConsultarPorId(CompraSapi entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que elimina una Compra de Materiales Sapi
        /// </summary>
        /// <param name="compra">Compra a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la eliminacion fue exitosa; False, en caso contrario</returns>
        public bool Eliminar(CompraSapi compra, int hash)
        {
            throw new NotImplementedException();
        }
    }
}
