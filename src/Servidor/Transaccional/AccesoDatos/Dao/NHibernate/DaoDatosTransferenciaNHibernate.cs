using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;
using System;

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
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerDatosTransferenciaPorAsociado, asociado.Id));
                datosTransferencia = query.List<DatosTransferencia>();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }

            return datosTransferencia;
        }
    }
}
