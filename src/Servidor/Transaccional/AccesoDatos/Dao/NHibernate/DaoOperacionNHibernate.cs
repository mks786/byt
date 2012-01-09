using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoOperacionNHibernate : DaoBaseNHibernate<Operacion, int>, IDaoOperacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Operacion> ObtenerOperacionesPorMarca(Marca marca)
        {
            IList<Operacion> Operaciones;

            try
            {
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerOperacionesPorMarcas, marca.Id));
                Operaciones = query.List<Operacion>();
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

            return Operaciones;
        }
    }
}
