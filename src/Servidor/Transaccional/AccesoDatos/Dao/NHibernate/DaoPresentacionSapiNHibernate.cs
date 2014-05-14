using System;
using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoPresentacionSapiNHibernate : DaoBaseNHibernate<PresentacionSapi,int>, IDaoPresentacionSapi
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
