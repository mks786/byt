using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCertificadoMarcaNHibernate : DaoBaseNHibernate<CertificadoMarca, string>, IDaoCertificadoMarca
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que consulta un certificado de marca por Id
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        public CertificadoMarca ConsultarCertificadoMarcaPorId(CertificadoMarca certificado)
        {
            CertificadoMarca retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerCertificadoMarcaPorId, certificado.IdMarca));
                retorno = query.UniqueResult<CertificadoMarca>();

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
