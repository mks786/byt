using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCartaOutNHibernate : DaoBaseNHibernate<CartaOut, int>, IDaoCartaOut
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<CartaOut> ObtenerCartasOutsFiltro(CartaOut carta)
        {
            IList<CartaOut> cartas = null;
            bool variosFiltros = false;
            string filtro = "";
            string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCartaOut);
            if ((null != carta) && (carta.Status != null))
            {
                filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaOutStatus, carta.Status);
                variosFiltros = true;
            }
            if ((null != carta) && (null != carta.Id) && (!carta.Id.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaOutId, carta.Id);
                variosFiltros = true;
            }
            if (carta.Asociado!=0)
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaIdAsociado, carta.Asociado);
                variosFiltros = true;
            }
            if ((null != carta.Fecha) && (!carta.Fecha.Equals(DateTime.MinValue)))
            {
                if (variosFiltros)
                    filtro += " and ";
                string fecha = String.Format("{0:dd/MM/yy}", carta.Fecha);
                DateTime fechaPrueba = Convert.ToDateTime(fecha);
                string fecha2 = String.Format("{0:dd/MM/yy}", fechaPrueba.AddDays(1));
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaOutFecha, fecha, fecha2);
            }

            IQuery query = Session.CreateQuery(cabecera + filtro);
            cartas = query.List<CartaOut>();
            return cartas;
        }


///---------------------------------------------------------------------------------------------------------------------------------------------------

        public bool TransferirPlantilla(IList<Carta> cartas, IList<CartaOut> cartasOut)
        {
            ITransaction transaccion = Session.BeginTransaction();
            int index = 0;
            try
            {
                foreach (Carta carta in cartas)
                {
                    cartasOut[index].Status = 'M';

                    Session.SaveOrUpdate(cartasOut[index]);
                    Session.SaveOrUpdate(carta);

                    index++;
                }
                transaccion.Commit();
                return transaccion.WasCommitted;
            }
            catch (Exception e)
            {
                transaccion.Rollback();
                return false;
            }
        }
    }
}
