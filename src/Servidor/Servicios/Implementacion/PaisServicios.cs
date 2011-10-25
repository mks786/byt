using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class PaisServicios : MarshalByRefObject, IPaisServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los paises
        /// </summary>
        /// <returns>Todos los paises</returns>
        public IList<Pais> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Pais> paises = ControladorPais.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return paises;
        }


        public Pais ConsultarPorId(Pais entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que inserta o modifica un país
        /// </summary>
        /// <param name="pais">País a insertar o modificar</param>
        /// <param name="hash">hash del usuario logerad</param>
        /// <returns></returns>
        public bool InsertarOModificar(Pais pais, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorPais.InsertarOModificar(pais, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que elimina un pais
        /// </summary>
        /// <param name="pais">País a eliminar</param>
        /// <param name="hash">Hash del usuario logeado</param>
        /// <returns></returns>
        public bool Eliminar(Pais pais,int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorPais.Eliminar(pais, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        public bool VerificarExistencia(Pais pais)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorPais.VerificarExistencia(pais);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
