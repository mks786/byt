using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoAlmacenNHibernate : DaoBaseNHibernate<Almacen, string>, IDaoAlmacen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
