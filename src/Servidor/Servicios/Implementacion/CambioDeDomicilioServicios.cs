using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CambioDeDomicilioServicios : MarshalByRefObject, ICambioDeDomicilioServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los CambioDeDomicilio
        /// </summary>
        /// <returns>Todos los CambioDeDomicilio</returns>
        public IList<CambioDeDomicilio> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<CambioDeDomicilio> cambioDeDomicilio = ControladorCambioDeDomicilio.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return cambioDeDomicilio;
        }


        public CambioDeDomicilio ConsultarPorId(CambioDeDomicilio entidad)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(CambioDeDomicilio cambioDeDomicilio, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioDeDomicilio.InsertarOModificar(cambioDeDomicilio, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        public bool Eliminar(CambioDeDomicilio cambioDeDomicilio,int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioDeDomicilio.Eliminar(cambioDeDomicilio, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Servicio que verifica la existencia de un CambioDeDomicilio
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio a verificar</param>
        /// <returns>true en caso de existir, false en lo contrario</returns>
        public bool VerificarExistencia(CambioDeDomicilio cambioDeDomicilio)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioDeDomicilio.VerificarExistencia(cambioDeDomicilio);

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
        public IList<CambioDeDomicilio> ObtenerCambioDeDomicilioFiltro(CambioDeDomicilio cambioDeDomicilio)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<CambioDeDomicilio> cambiosDeDomicilio;

            cambiosDeDomicilio = ControladorCambioDeDomicilio.ConsultarCambioDeDomicilioFiltro(cambioDeDomicilio);

            return cambiosDeDomicilio;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
