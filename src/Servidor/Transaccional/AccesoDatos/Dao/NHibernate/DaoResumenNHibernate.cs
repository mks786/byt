using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoResumenNHibernate : DaoBaseNHibernate<Resumen, string>, IDaoResumen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();        
    }
}
