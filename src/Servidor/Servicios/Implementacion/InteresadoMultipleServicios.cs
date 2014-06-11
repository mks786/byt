using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class InteresadoMultipleServicios : MarshalByRefObject, IInteresadoMultipleServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Servicio que obtiene TODOS los interesados vinculados a las patentes sin ningun distingo
        /// Este servicio SOLO es para fines referenciales
        /// </summary>
        /// <returns>Lista de todos los interesados vinculados a las patentes</returns>
        public IList<InteresadoMultiple> ConsultarTodos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<InteresadoMultiple> interesados = ControladorInteresadoMultiple.ConsultarTodos();
                
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return interesados;
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
        /// Servicio para obtener los Interesados asociados a una patente especifica
        /// </summary>
        /// <param name="patente">Patente usada para filtrar los interesados asociados a la misma</param>
        /// <returns>Lista de interesados asociados a una patente especifica</returns>
        public IList<InteresadoMultiple> ConsultarInteresadosDePatente(Patente patente)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<InteresadoMultiple> interesados = ControladorInteresadoMultiple.ConsultarInteresadosDePatente(patente);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return interesados;
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
        /// Servicio qeu obtiene los Interesados asociados a una marca especifica
        /// </summary>
        /// <param name="marca">Marca usada para filtrar los interesados asociados a la misma</param>
        /// <returns>Lista de interesados asociados a una marca especifica</returns>
        public IList<InteresadoMultiple> ConsultarInteresadosDeMarca(Marca marca)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<InteresadoMultiple> interesados = ControladorInteresadoMultiple.ConsultarInteresadosDeMarca(marca);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return interesados;
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



        public InteresadoMultiple ConsultarPorId(InteresadoMultiple entidad)
        {
            throw new NotImplementedException();
        }

        public IList<InteresadoMultiple> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(InteresadoMultiple interesadoPatente)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que inserta o actualiza la entidad InteresadoPatente
        /// </summary>
        /// <param name="interesadoPatente">Objeto InteresadoPatente a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza exitosamente; False, en caso contrario.</returns>
        public bool InsertarOModificar(InteresadoMultiple interesadoPatente, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorInteresadoMultiple.InsertarOModificar(interesadoPatente, hash);

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

        public bool Eliminar(InteresadoMultiple interesadoPatente, int hash)
        {
            throw new NotImplementedException();
        }
    }
}
