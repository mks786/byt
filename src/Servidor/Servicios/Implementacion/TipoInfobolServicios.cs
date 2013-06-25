using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class TipoInfobolServicios : MarshalByRefObject, ITipoInfobolServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que obtiene todos los tipoInfoboles
        /// </summary>
        /// <returns>Lista con todos los tipoInfoboles</returns>
        public IList<TipoInfobol> ConsultarTodos()
        {
            IList<TipoInfobol> tipoInfobol;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoInfobol = ControladorTipoInfobol.ConsultarTodos();

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
            return tipoInfobol;
        }


        /// <summary>
        /// Método que consulta un TipoInfobol por Id
        /// </summary>
        /// <param name="tipoInfobol">Tipo Infobol a consultar</param>
        /// <returns>TipoInfobol devuelta</returns>
        public TipoInfobol ConsultarPorId(TipoInfobol tipoInfobol)
        {
            throw new NotImplementedException();
        }

        //-------------------------------------------------------------
        /// <summary>
        /// Servicio que consultar por un campo determinado y 
        /// ordena en forma Ascendente o Descendente
        /// </summary>
        /// <param name="campo">Campo a filtrar</param>
        /// <param name="tipoOrdenamiento">Si el ordenamiento es Ascende o Descendente</param>
        /// <returns>Lista de Entidades</returns>
        public IList<TipoInfobol> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
        //-------------------------------------------------------------


        /// <summary>
        /// Servicio que insertar o modifica un TipoInfobol
        /// </summary>
        /// <param name="tipoInfobol">TipoInfobol que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(TipoInfobol tipoInfobol, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorTipoInfobol.InsertarOModificar(tipoInfobol, hash);

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
        /// Servicio que elimina un TipoInfobol
        /// </summary>
        /// <param name="tipoInfobol">TipoInfobol que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(TipoInfobol tipoInfobol, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorTipoInfobol.Eliminar(tipoInfobol, hash);

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
        /// Servicio que verifica la existencia de un TipoInfobol
        /// </summary>
        /// <param name="tipoInfobol">Tipo Infobol a verificar</param>
        /// <returns>true en caso de que exista, false en caso contrario</returns>
        public bool VerificarExistencia(TipoInfobol tipoInfobol)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorTipoInfobol.VerificarExistencia(tipoInfobol);

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
    }
}
