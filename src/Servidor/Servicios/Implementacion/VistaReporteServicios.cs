using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class VistaReporteServicios: MarshalByRefObject, IVistaReporteServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todas las vista para los reportes 
        /// </summary>
        /// <returns>Lista de Vistas de Reportes</returns>
        public IList<VistaReporte> ConsultarTodos()
        {
            IList<VistaReporte> vistas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //cajas = ControladorCaja.ConsultarTodos();
                vistas = ControladorVistaReporte.ConsultarTodos();

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
            return vistas;
        }


        public VistaReporte ConsultarPorId(VistaReporte entidad)
        {
            throw new NotImplementedException();
        }

        public IList<VistaReporte> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(VistaReporte entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(VistaReporte entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(VistaReporte entidad)
        {
            throw new NotImplementedException();
        }
    }
}
