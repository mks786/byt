﻿using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoAnaquaNHibernate : DaoBaseNHibernate<Anaqua, Marca>, IDaoAnaqua
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
