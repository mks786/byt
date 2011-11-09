using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class EntradaAlternaServicios : MarshalByRefObject, IEntradaAlternaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los entradaAlternas
        /// </summary>
        /// <returns>Lista con todos los entradaAlternas</returns>
        public IList<EntradaAlterna> ConsultarTodos()
        {
            IList<EntradaAlterna> entradaAlternas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                entradaAlternas = ControladorEntradaAlterna.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return entradaAlternas;
        }

        public EntradaAlterna ConsultarPorId(EntradaAlterna entradaAlterna)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica un nacional
        /// </summary>
        /// <param name="entradaAlterna">EntradaAlterna que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(EntradaAlterna entradaAlterna, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorEntradaAlterna.InsertarOModificar(entradaAlterna, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Servicio que elimina un nacional
        /// </summary>
        /// <param name="entradaAlterna">EntradaAlterna que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(EntradaAlterna entradaAlterna, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorEntradaAlterna.Eliminar(entradaAlterna, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        public bool VerificarExistencia(EntradaAlterna entradaAlterna)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorEntradaAlterna.VerificarExistencia(entradaAlterna);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
