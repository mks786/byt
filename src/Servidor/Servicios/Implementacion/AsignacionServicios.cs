using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class AsignacionServicios : MarshalByRefObject, IAsignacionServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos las Cartas
        /// </summary>
        /// <returns>Lista con todos los Cartas</returns>
        public IList<Carta> ConsultarTodos()
        {
            IList<Carta> carta;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                carta = ControladorCarta.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return carta;
        }


        //IList<Carta> carta;
        //try
        //{
        //    #region trace
        //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //        logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion

        //    carta = ControladorCarta.ConsultarTodos();

        //    #region trace
        //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //        logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion
        //}
        //catch (ApplicationException ex)
        //{
        //    throw ex;
        //}
        //return carta;

        /// <summary>
        /// Servicio que obtiene las cartas por ID
        /// </summary>
        /// <param name="carta"></param>
        /// <returns>Carta</returns>
        public Carta ConsultarPorId(Carta carta)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica una Carta
        /// </summary>
        /// <param name="carta">Carta que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(Carta carta, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCarta.InsertarOModificar(carta, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que elimina una Carta
        /// </summary>
        /// <param name="carta">Carta que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(Carta carta, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCarta.Eliminar(carta, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que consulta una serie de cartas por uno o mas parametros
        /// </summary>
        /// <param name="carta">Carta que contiene los parametros de la consulta</param>
        /// <returns>Lista de cartas filtradas</returns>
        public IList<Carta> ObtenerCartasFiltro(Carta carta)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Carta> cartas;

            cartas = ControladorCarta.ConsultarCartasFiltro(carta);

            return cartas;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Servicio que se encarga de obtener las asignaciones pertenecientes a una carta
        /// </summary>
        /// <param name="carta">Carta a consultar las asignaciones</param>
        /// <returns>Lista de asignaciones de la carta</returns>
        public IList<Asignacion> ObtenerAsignacionesPorCarta(Carta carta)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Asignacion> asignaciones;

            asignaciones = ControladorAsignacion.ConsultarAsignacionesPorCarta(carta);

            return asignaciones;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }



        IList<Asignacion> IServicioBase<Asignacion>.ConsultarTodos()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Asignacion ConsultarPorId(Asignacion entidad)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificar(Asignacion entidad, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(Asignacion entidad, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(Asignacion entidad)
        {
            throw new NotImplementedException();
        }
    }
}
