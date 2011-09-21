﻿using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoInternacionalNHibernate : DaoBaseNHibernate<Internacional, int>, IDaoInternacional
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
