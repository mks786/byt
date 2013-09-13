using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoVistaReporteNHibernate: DaoBaseNHibernate<VistaReporte,int>, IDaoVistaReporte
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
