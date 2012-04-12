using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CambioDeDomicilioPatenteServicios : MarshalByRefObject, ICambioDeDomicilioPatenteServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los CambioDeDomicilioPatente
        /// </summary>
        /// <returns>Todos los CambioDeDomicilioPatente</returns>
        public IList<CambioDeDomicilioPatente> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<CambioDeDomicilioPatente> cambioDeDomicilio = ControladorCambioDeDomicilioPatente.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return cambioDeDomicilio;
        }


        public CambioDeDomicilioPatente ConsultarPorId(CambioDeDomicilioPatente entidad)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(CambioDeDomicilioPatente cambioDeDomicilio, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioDeDomicilioPatente.InsertarOModificar(cambioDeDomicilio, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        public bool Eliminar(CambioDeDomicilioPatente cambioDeDomicilio,int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioDeDomicilioPatente.Eliminar(cambioDeDomicilio, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Servicio que verifica la existencia de un CambioDeDomicilioPatente
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente a verificar</param>
        /// <returns>true en caso de existir, false en lo contrario</returns>
        public bool VerificarExistencia(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioDeDomicilioPatente.VerificarExistencia(cambioDeDomicilio);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que consulta una serie de Marcas por uno o mas parametros
        /// </summary>
        /// <param name="marca">Marca que contiene los parametros de la consulta</param>
        /// <returns>Lista de cartas filtradas</returns>
        public IList<CambioDeDomicilioPatente> ObtenerCambioDeDomicilioPatenteFiltro(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<CambioDeDomicilioPatente> cambiosDeDomicilio;

            cambiosDeDomicilio = ControladorCambioDeDomicilioPatente.ConsultarCambioDeDomicilioPatenteFiltro(cambioDeDomicilio);

            return cambiosDeDomicilio;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
