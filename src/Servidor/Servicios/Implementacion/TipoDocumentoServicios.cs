using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class TipoDocumentoServicios: MarshalByRefObject, ITipoDocumentoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los tipoDocumentos
        /// </summary>
        /// <returns>Lista con todos los tipoDocumentos</returns>
        public IList<TipoDocumento> ConsultarTodos()
        {
            IList<TipoDocumento> tipoDocumento;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoDocumento = ControladorTipoDocumento.ConsultarTodos();

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

            return tipoDocumento;
        }



        /// <summary>
        /// Servicio que obtiene los tipos de documentos de Archivo de acuerdo a lo que se consulte, Marca o Patente
        /// </summary>
        /// <param name="parametro1">Parametro para Marca o Patente Nacional</param>
        /// <param name="parametro2">Parametro para Marca o Patente Internacional</param>
        /// <returns></returns>
        public IList<TipoDocumento> ObtenerTipoDocumentoMarcaOPatente(String parametro1, String parametro2)
        {
            IList<TipoDocumento> tipoDocumento = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoDocumento = ControladorTipoDocumento.ObtenerTipoDocumentoMarcaOPatente(parametro1, parametro2);

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

            return tipoDocumento;
        }



        public TipoDocumento ConsultarPorId(TipoDocumento entidad)
        {
            throw new NotImplementedException();
        }

        public IList<TipoDocumento> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(TipoDocumento entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(TipoDocumento entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(TipoDocumento entidad)
        {
            throw new NotImplementedException();
        }
    }
}
