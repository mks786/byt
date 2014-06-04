using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CasoServicios : MarshalByRefObject, ICasoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todo el contenido de la tabla de Casos
        /// </summary>
        /// <returns>Contenido de toda la tabla de Casos</returns>
        public IList<Caso> ConsultarTodos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Caso> casos = ControladorCaso.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return casos;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ":" + ex.Message);
            }
        }


        /// <summary>
        /// Servicio que inserta y/o actualiza un Caso 
        /// </summary>
        /// <param name="caso">Caso a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>Id del caso insertado o actualizado; en caso contrario, retorna NULL</returns>
        public int? InsertarOModificarCaso(Caso caso, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCaso.InsertarOModificar(ref caso, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (exitoso)
                    return caso.Id;
                else
                    return null;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ":" + ex.Message);
            }
        }
        

        /// <summary>
        /// Servicio que inserta y/o actualiza un Caso 
        /// </summary>
        /// <param name="caso">Caso a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; False, en caso contrario</returns>
        public bool InsertarOModificar(Caso caso, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                exitoso = ControladorCaso.InsertarOModificar(ref caso, hash);

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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ":" + ex.Message);
            }

            return exitoso;
        }


        /// <summary>
        /// Servicio que obtiene una lista de casos dependiendo de un filtro
        /// </summary>
        /// <param name="caso">Caso usado como filtro</param>
        /// <returns>Lista de Casos de acuerdo al filtro especificado</returns>
        public IList<Caso> ObtenerCasosFiltro(Caso caso)
        {
            IList<Caso> casos;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                casos = ControladorCaso.ObtenerCasosFiltro(caso);

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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ":" + ex.Message);
            }

            return casos;
        }


        /// <summary>
        /// Servicio encargado de consultar la auditoria de Casos
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de Auditorias</returns>
        public IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Auditoria> auditorias = ControladorCaso.AuditoriaPorFkyTabla(auditoria);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return auditorias;
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



        public bool Eliminar(Caso entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Caso entidad)
        {
            throw new NotImplementedException();
        }

        public Caso ConsultarPorId(Caso entidad)
        {
            throw new NotImplementedException();
        }

        public IList<Caso> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
    }
}
