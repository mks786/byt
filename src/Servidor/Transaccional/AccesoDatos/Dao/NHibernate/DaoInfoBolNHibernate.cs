using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoInfoBolNHibernate : DaoBaseNHibernate<InfoBol, int>, IDaoInfoBol
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<InfoBol> ObtenerInfoBolesPorMarca(Marca marca)
        {
            IList<InfoBol> infoBoles;

            try
            {
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInfoBolesPorMarcas, marca.Id));
                infoBoles = query.List<InfoBol>();
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

            return infoBoles;
        }
    }
}
