﻿using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCambioDeDomicilioNHibernate : DaoBaseNHibernate<CambioDeDomicilio, int>, IDaoCambioDeDomicilio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
            
    }
}