using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CambioPeticionarioPatenteServicios : MarshalByRefObject, ICambioPeticionarioPatenteServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los CambioPeticionarioPatente
        /// </summary>
        /// <returns>Todos los CambioPeticionarioPatente</returns>
        public IList<CambioPeticionarioPatente> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<CambioPeticionarioPatente> cambioPeticionario = ControladorCambioPeticionarioPatente.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return cambioPeticionario;
        }


        public CambioPeticionarioPatente ConsultarPorId(CambioPeticionarioPatente entidad)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(CambioPeticionarioPatente cambioPeticionario, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioPeticionarioPatente.InsertarOModificar(cambioPeticionario, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        public bool Eliminar(CambioPeticionarioPatente cambioPeticionario,int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioPeticionarioPatente.Eliminar(cambioPeticionario, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        public bool VerificarExistencia(CambioPeticionarioPatente cambioPeticionario)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCambioPeticionarioPatente.VerificarExistencia(cambioPeticionario);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Servicio que consulta una serie de CambioPeticionarioPatente por uno o mas parametros
        /// </summary>
        /// <param name="marca">CambioPeticionarioPatente que contiene los parametros de la consulta</param>
        /// <returns>Lista de CambioPeticionarioPatente filtradas</returns>
        public IList<CambioPeticionarioPatente> ObtenerCambioPeticionarioFiltro(CambioPeticionarioPatente CambioPeticionarioPatente)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<CambioPeticionarioPatente> cambiosDePeticionario;

            cambiosDePeticionario = ControladorCambioPeticionarioPatente.ConsultarCambioPeticionarioPatenteFiltro(CambioPeticionarioPatente);

            return cambiosDePeticionario;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

    }
}
