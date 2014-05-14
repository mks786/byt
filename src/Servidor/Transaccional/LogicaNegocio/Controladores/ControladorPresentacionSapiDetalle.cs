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
    public class ControladorPresentacionSapiDetalle : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que inserta o actualiza un Renglon de Detalle de la Solicitud de Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiDetalle">Detalle de la Presentacion Sapi a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza exitosamente; False, en caso contrario</returns>
        public static bool InsertarOModificar(PresentacionSapiDetalle presentacionSapiDetalle, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosPresentacionSapiDetalle.ObtenerComandoInsertarOModificar(presentacionSapiDetalle);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

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
        /// Metodo que elimina un renglon de detalle de la Solicitud de Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiDetalle">Detalle de la Presentacion Sapi a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza exitosamente; False, en caso contrario</returns>
        public static bool Eliminar(PresentacionSapiDetalle presentacionSapiDetalle, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosPresentacionSapiDetalle.ObtenerComandoEliminar(presentacionSapiDetalle);
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
        /// Metodo que, usando un filtro, obtiene un conjunto de Solicitudes de Presentaciones SAPI
        /// </summary>
        /// <param name="filtro">Filtro usado para la consulta de Presentaciones por filtro</param>
        /// <returns>Lista de Solicitudes de Presentaciones Sapi de acuerdo a un filtro determinado</returns>
        public static IList<PresentacionSapiDetalle> ConsultarSolicitudesPresetacionSapiFiltro(PresentacionSapiDetalle filtro)
        {
            IList<PresentacionSapiDetalle> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase <IList<PresentacionSapiDetalle>> comando = 
                    FabricaComandosPresentacionSapiDetalle.ObtenerComandoConsultarPresentacionesSapiFiltro(filtro);
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
