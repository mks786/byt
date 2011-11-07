using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CartaServicios : MarshalByRefObject, ICartaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los boletines
        /// </summary>
        /// <returns>Lista con todos los boletines</returns>
        public IList<Carta> ConsultarTodos()
        {
            IList<Carta> carta;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                carta = ControladorCarta.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
        //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion

        //    carta = ControladorCarta.ConsultarTodos();

        //    #region trace
        //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion
        //}
        //catch (ApplicationException ex)
        //{
        //    throw ex;
        //}
        //return carta;


        public Carta ConsultarPorId(Carta carta)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica un nacional
        /// </summary>
        /// <param name="carta">Boletin que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(Carta carta, int hash)
        {
            throw new NotImplementedException();
            //#region trace
            //if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //#endregion

            //bool exitoso = ControladorCarta.InsertarOModificar(carta, hash);

            //#region trace
            //if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //#endregion

            //return exitoso;
        }

        /// <summary>
        /// Servicio que elimina un nacional
        /// </summary>
        /// <param name="carta">Boletin que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(Carta carta, int hash)
        {
            throw new NotImplementedException();
            //#region trace
            //if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //#endregion

            //bool exitoso = ControladorCarta.Eliminar(carta, hash);

            //#region trace
            //if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            //    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //#endregion

            //return exitoso;
        }

        /// <summary>
        /// Servicio que elimina un nacional
        /// </summary>
        /// <param name="carta">Boletin que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool VerificarExistencia(Carta carta)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCarta.VerificarExistencia(carta);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        public IList<Carta> ObtenerCartasFiltro(Carta carta)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Carta> cartas;

            cartas = ControladorCarta.ConsultarCartasFiltro(carta);

            return cartas;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
