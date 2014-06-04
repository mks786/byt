using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class TipoCasoServicios : MarshalByRefObject, ITipoCasoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todo el contenido de la tabla de los Tipos de Casos
        /// </summary>
        /// <returns>Lista de todos los Tipos de Casos</returns>
        public IList<TipoCaso> ConsultarTodos()
        {
            IList<TipoCaso> tipoCasos;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoCasos = ControladorTipoCaso.ConsultarTodos();

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

            return tipoCasos;
        }

        public TipoCaso ConsultarPorId(TipoCaso entidad)
        {
            throw new NotImplementedException();
        }

        public IList<TipoCaso> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(TipoCaso entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(TipoCaso entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(TipoCaso entidad)
        {
            throw new NotImplementedException();
        }
    }
}
