using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoDatosTransferenciaNHibernate : DaoBaseNHibernate<DatosTransferencia, int>, IDaoDatosTransferencia
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public IList<DatosTransferencia> ObtenerDatosTransferenciaPorAsociado(Asociado asociado)
        {
            IList<DatosTransferencia> datosTransferencia;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerDatosTransferenciaPorAsociado, asociado.Id));
                datosTransferencia = query.List<DatosTransferencia>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerDatosTransferenciaPorAsociado);
            }
            finally
            {
                Session.Close();
            }

            return datosTransferencia;
        }
    }
}
