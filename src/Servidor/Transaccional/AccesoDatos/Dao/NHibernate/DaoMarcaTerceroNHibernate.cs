﻿using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoMarcaTerceroNHibernate : DaoBaseNHibernate<MarcaTercero, int>,  IDaoMarcaTercero
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
