using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CompraSapiDetalleServicios : MarshalByRefObject, ICompraSapiDetalleServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de entidades</returns>
        public IList<CompraSapiDetalle> ConsultarTodos()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<CompraSapiDetalle> ConsultarPorOtroCampo(String campoConsultar, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(CompraSapiDetalle entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public CompraSapiDetalle ConsultarPorId(CompraSapiDetalle entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que elimina una Compra de Materiales Sapi
        /// </summary>
        /// <param name="compra">Compra a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la eliminacion fue exitosa; False, en caso contrario</returns>
        public bool Eliminar(CompraSapiDetalle compra, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCompraSapiDetalle.Eliminar(compra, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
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
        }

        /// <summary>
        /// Servicio que inserta o actualiza un reglon de detalle de la Compra de Materiales Sapi
        /// </summary>
        /// <param name="detalleCompra">Renglon de detalle a insertar y/o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza exitosamente; False, en caso contrario</returns>
        public bool InsertarOModificar(CompraSapiDetalle detalleCompra, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCompraSapiDetalle.InsertarOModificar(detalleCompra, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
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
        }


        /// <summary>
        /// Servicio que permite consultar detalles de compra Sapi por filtro
        /// </summary>
        /// <param name="compraDetalle">Detalle de compra Sapi filtro</param>
        /// <returns>Lista de detalles de acuerdo a un filtro determinado</returns>
        public IList<CompraSapiDetalle> ObtenerCompraSapiDetalleFiltro(CompraSapiDetalle compraDetalle)
        {
            IList<CompraSapiDetalle> detallesCompraSapi;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                detallesCompraSapi = ControladorCompraSapiDetalle.ConsultarCompraSapiDetalleFiltro(compraDetalle);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return detallesCompraSapi;
        }
    }
}
