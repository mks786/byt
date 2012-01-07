using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class BusquedaServicios : MarshalByRefObject, IBusquedaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos las Búsquedas
        /// </summary>
        /// <returns>Todos las Búsquedas</returns>
        public IList<Busqueda> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Busqueda> busquedas = ControladorBusqueda.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return busquedas;
        }


        /// <summary>
        /// Método que obtiene una Búsqueda por su Id
        /// </summary>
        /// <returns>Búsqueda buscada</returns>
        public Busqueda ConsultarPorId(Busqueda entidad)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return ControladorBusqueda.ConsultarPorId(entidad);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que inserta o modifica una Búsqueda
        /// </summary>
        /// <param name="busqueda">Búsqueda a insertar</param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool InsertarOModificar(Busqueda busqueda, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorBusqueda.InsertarOModificar(busqueda, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que elimina una Búsqueda determinada
        /// </summary>
        /// <param name="busqueda">Búsqueda determinada</param>
        /// <param name="hash"> hash del usuario que esta eliminando</param>
        /// <returns></returns>
        public bool Eliminar(Busqueda busqueda, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorBusqueda.Eliminar(busqueda, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que permite verificar la existencia de una Búsqueda
        /// </summary>
        /// <param name="busqueda">Búsqueda a eliminar</param>
        /// <returns></returns>
        public bool VerificarExistencia(Busqueda busqueda)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorBusqueda.VerificarExistencia(busqueda);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que permite consultar las Búsquedas de una marca determinada
        /// </summary>
        /// <param name="marca">Marca de la cuál se consultarán las Búsquedas</param>
        /// <returns></returns>
        public IList<Busqueda> ConsultarBusquedasPorMarca(Marca marca)
        {            
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Busqueda> busquedas = ControladorBusqueda.ConsultarBusquedasPorMarca(marca);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return busquedas;
        }
    }
}
