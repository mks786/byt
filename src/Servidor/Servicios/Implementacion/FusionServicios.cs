﻿using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class FusionServicios : MarshalByRefObject, IFusionServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<Fusion> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Fusion> fusiones = ControladorFusion.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return fusiones;
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Fusion ConsultarPorId(Fusion entidad)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificar(Fusion fusion, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorFusion.InsertarOModificar(fusion, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(Fusion fusion,int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorFusion.Eliminar(fusion, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(Fusion fusion)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorFusion.VerificarExistencia(fusion);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que consulta las fusiones segun el filtro
        /// </summary>
        /// <param name="Fusion">fusión filtro</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>
        public IList<Fusion> ObtenerFusionFiltro(Fusion fusion)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Fusion> fusiones;
            
            fusiones = ControladorFusion.ConsultarFusionesFiltro(fusion);

            return fusiones;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
