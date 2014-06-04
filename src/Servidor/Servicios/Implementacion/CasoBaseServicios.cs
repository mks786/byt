using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CasoBaseServicios : MarshalByRefObject,ICasoBaseServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que inserta o actualiza un Caso Base
        /// </summary>
        /// <param name="casoBase">Caso Base a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realizo correctamente; False, en caso contrario</returns>
        public bool InsertarOModificar(CasoBase casoBase, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCasoBase.InsertarOModificar(casoBase, hash);

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
        /// Servicio que elimina un Caso Base 
        /// </summary>
        /// <param name="casoBase">Caso Base a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realizo correctamente; False, en caso contrario</returns>
        public bool Eliminar(CasoBase casoBase, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCasoBase.Eliminar(casoBase, hash);

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
        /// Servicio que obtiene los Casos Base de un Caso en particular
        /// </summary>
        /// <param name="casoBase">Caso Base filtro</param>
        /// <returns>Lista de Casos Base un caso en particular</returns>
        public IList<CasoBase> ObtenerCasosBasePorCaso(CasoBase casoBase)
        {
            IList<CasoBase> casosBase;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                casosBase = ControladorCasoBase.ObtenerCasosBasePorCaso(casoBase);

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

            return casosBase;
        }


        public IList<CasoBase> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public CasoBase ConsultarPorId(CasoBase entidad)
        {
            throw new NotImplementedException();
        }

        public IList<CasoBase> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        

        

        public bool VerificarExistencia(CasoBase entidad)
        {
            throw new NotImplementedException();
        }
    }
}
