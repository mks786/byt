using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorSolicitudSapi : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que inserta o actualiza una lista de Detalles de Solicitud de Materiales SAPI
        /// </summary>
        /// <param name="solicitudesMateriales">Renglones de Detalle de las Solicitudes Materiales</param>
        /// <param name="operacion">Operacion de Base de Datos (CREATE o MODIFY)</param>
        /// <param name="hash">Hash del Usuario Logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False, en caso contrario</returns>
        public static bool InsertarOModificar(ref IList<SolicitudSapi> solicitudesMateriales, string operacion, int hash)
        {
            bool exitoso = false;
            int idContador;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoContador = null;

                // si es una insercion
                if (operacion.Equals("CREATE"))
                {
                    ComandoBase<Contador> comandoContadorProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("SAPI_MOVIMIENTO_MAT");
                    comandoContadorProximoValor.Ejecutar();
                    Contador contador = comandoContadorProximoValor.Receptor.ObjetoAlmacenado;
                    foreach (SolicitudSapi solicitudSapi in solicitudesMateriales)
                    {
                        solicitudSapi.Id = contador.ProximoValor;
                    }

                    idContador = contador.ProximoValor++;
                    comandoContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contador);
                }

                foreach (SolicitudSapi itemSolicitudSapi in solicitudesMateriales)
                {
                    ComandoBase<bool> comando = FabricaComandosSolicitudSapi.ObtenerComandoInsertarOModificar(itemSolicitudSapi);
                    comando.Ejecutar();
                    exitoso = comando.Receptor.ObjetoAlmacenado;
                }

                if (exitoso)
                {
                    if (comandoContador != null)
                        comandoContador.Ejecutar();
                }

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
            return exitoso;
        }

        /// <summary>
        /// Metodo que obtiene un grupo de Solicitudes Sapi de acuerdo a un filtro aplicado
        /// </summary>
        /// <param name="solicitudAux">Solicitud Sapi usada como filtro</param>
        /// <returns>Lista de Solicitudes Sapi que cumplen con el filtro</returns>
        public static IList<SolicitudSapi> ObtenerSolicitudesSapiFiltro(SolicitudSapi solicitudAux)
        {

            IList<SolicitudSapi> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<SolicitudSapi>> comando = FabricaComandosSolicitudSapi.ObtenerComandoConsultarSolicitudesSapiFiltro(solicitudAux);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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

            return retorno;
        }


        /// <summary>
        /// Metodo que elimina una Solicitud de Material Sapi de la base de datos
        /// </summary>
        /// <param name="solicitud">Solicitud Sapi a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True, si la operacion se realiza correctamente; False, en caso contrario</returns>
        public static bool Eliminar(SolicitudSapi solicitud, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosSolicitudSapi.ObtenerComandoEliminarSolicitudMaterialSapi(solicitud);
                comando.Ejecutar();
                exitoso = true;

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

            return exitoso;
        }

        /// <summary>
        /// Metodo que obtiene los Movimientos tipo SOLICITUD pendientes por ENTREGAR Y RECIBIR
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi filtro</param>
        /// <returns>Lista de Movimientos tipo SOLICITUD pendientes por ENTREGAR Y RECIBIR</returns>
        public static IList<SolicitudSapi> ObtenerSolicitudesSapiPendientesFiltro(SolicitudSapi solicitudSapi)
        {
            IList<SolicitudSapi> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<SolicitudSapi>> comando = 
                    FabricaComandosSolicitudSapi.ObtenerComandoConsultarSolicitudesSapiPendientesFiltro(solicitudSapi);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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

            return retorno;
        }
    }
}
