using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using Diginsoft.Bolet.ObjetosComunes.Entidades;
namespace Trascend.Bolet.Servicios.Implementacion
{
    public class DetallePagoServicios : MarshalByRefObject, IDetallePagoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<DetallePago> ConsultarTodos()
        {
            IList<DetallePago> detallesPagos;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                detallesPagos = ControladorDetallePago.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
            return detallesPagos;
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DetallePago ConsultarPorId(DetallePago detallePago)
        {
            throw new NotImplementedException();
        }

        //-------------------------------------------------------------
        /// <summary>
        /// Servicio que consulta por un campo determinado y 
        /// ordena en forma Ascendente o Descendente
        /// </summary>
        /// <param name="campo">Campo a filtrar</param>
        /// <param name="tipoOrdenamiento">Si el ordenamiento es Ascende o Descendente</param>
        /// <returns>Lista de Entidades</returns>
        public IList<DetallePago> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
        //-------------------------------------------------------------


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificar(DetallePago entidad, int hash)
        {
            //#Region "trace"
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            {
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            }
            //#End Region

            bool exitoso = Diginsoft.Bolet.LogicaNegocio.Controladores.ControladorDetallePago.InsertarOModificar(entidad, hash);

            //#Region "trace"
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            {
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            }
            //#End Region

            return exitoso;
        }


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(DetallePago entidad, int hash)
        {
            //#Region "trace"
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            {
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            }
            //#End Region

            bool exitoso = Diginsoft.Bolet.LogicaNegocio.Controladores.ControladorDetallePago.Eliminar(entidad, hash);

            //#Region "trace"
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            {
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            }
            //#End Region

            return exitoso;
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(DetallePago entidad)
        {
            //#Region "trace"
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            {
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            }
            //#End Region

            bool exitoso = Diginsoft.Bolet.LogicaNegocio.Controladores.ControladorDetallePago.VerificarExistencia(entidad);

            //#Region "trace"
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
            {
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            }
            //#End Region

            return exitoso;
        }
    }
}
