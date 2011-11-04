using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CartaOutServicios : MarshalByRefObject, ICartaOutServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los boletines
        /// </summary>
        /// <returns>Lista con todos los boletines</returns>
        public IList<CartaOut> ConsultarTodos()
        {
            IList<CartaOut> cartaOut;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                cartaOut = ControladorCartaOut.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return cartaOut;
        }

        public CartaOut ConsultarPorId(CartaOut carta)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica un nacional
        /// </summary>
        /// <param name="carta">Boletin que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(CartaOut carta, int hash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que elimina un nacional
        /// </summary>
        /// <param name="carta">Boletin que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(CartaOut carta, int hash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que elimina un nacional
        /// </summary>
        /// <param name="carta">Boletin que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool VerificarExistencia(CartaOut carta)
        {
            throw new NotImplementedException();
        }
    }
}
