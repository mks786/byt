using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoArchivoNHibernate : DaoBaseNHibernate<Archivo, string>, IDaoArchivo
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que consulta los contactos por Id
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        public Archivo ConsultarArchivoPorId(Archivo archivo)
        {
            Archivo retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerArchivoPorId, archivo.Id, archivo.TipoDeDocumento));
                retorno = query.UniqueResult<Archivo>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerArchivoPorId);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que obtiene un Archivo de Marca o Patente Internacional
        /// </summary>
        /// <param name="archivo">Archivo a consultar</param>
        /// <returns>Archivo de marca o patente internacional</returns>
        public Archivo ObtenerArchivoDeMarcaOPatenteInternacional(Archivo archivo)
        {
            Archivo retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerArchivoMarcaOPatenteInternacional, archivo.Id, archivo.Aux, archivo.TipoDeDocumento));
                retorno = query.UniqueResult<Archivo>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerArchivoPorId);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


    }
}
