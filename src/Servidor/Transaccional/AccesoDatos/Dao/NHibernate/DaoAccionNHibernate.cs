using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoAccionNHibernate : DaoBaseNHibernate<Accion,string>, IDaoAccion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
