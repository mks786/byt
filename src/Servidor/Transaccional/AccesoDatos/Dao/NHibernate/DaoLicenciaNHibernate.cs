using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoLicenciaNHibernate : DaoBaseNHibernate<Licencia, int>, IDaoLicencia
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
