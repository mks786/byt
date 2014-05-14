using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class PresentacionSapiServicios : MarshalByRefObject, IPresentacionSapiServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que inserta o actualiza el Encabezado de la Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiEncabezado">Encabezado de la Presentacion Sapi a actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion es realizada con exito; False, en caso contrario</returns>
        public bool InsertarOModificar(PresentacionSapi presentacionSapiEncabezado, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorPresentacionSapi.InsertarOModificar(ref presentacionSapiEncabezado, hash);

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
        /// Servicio que inserta o actualiza el encabezado de una Solicitud de Presentacion SAPI
        /// </summary>
        /// <param name="presentacionSapi">Encabezado de la Presentacion SAPI a insertar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; False, en caso contrario</returns>
        public bool InsertarOModificarPresentacionSapi(ref PresentacionSapi presentacionSapi, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorPresentacionSapi.InsertarOModificar(ref presentacionSapi, hash);

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

        public IList<PresentacionSapi> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public PresentacionSapi ConsultarPorId(PresentacionSapi entidad)
        {
            throw new NotImplementedException();
        }

        public IList<PresentacionSapi> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(PresentacionSapi entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(PresentacionSapi entidad)
        {
            throw new NotImplementedException();
        }
    }
}
