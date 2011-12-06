using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCartaNHibernate : DaoBaseNHibernate<Carta, int>, IDaoCarta
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Carta> ObtenerCartasFiltro(Carta carta)
        {
            IList<Carta> cartas = null;
            bool variosFiltros = false;
            string filtro = "";
            string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCarta);
            if ((null != carta) && (carta.Id != 0))
            {
                filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaId, carta.Id);
                variosFiltros = true;
            }
            if ((null != carta.Asociado) && (!carta.Asociado.Id.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaIdAsociado, carta.Asociado.Id);
                variosFiltros = true;
            }
            if ((null != carta.Resumen) && (!carta.Resumen.Descripcion.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaResumen, carta.Resumen.Descripcion);
            }
            if ((null != carta.Fecha) && (!carta.Fecha.Equals(DateTime.MinValue)))
            {
                if (variosFiltros)
                    filtro += " and ";
                string fecha = String.Format("{0:dd/MM/yy}", carta.Fecha);
                string fecha2 = String.Format("{0:dd/MM/yy}", carta.Fecha.AddDays(1));
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaFecha, fecha, fecha2);
            }
            IQuery query = Session.CreateQuery(cabecera + filtro);
            cartas = query.List<Carta>();
            return cartas;
        }

        /// <summary>
        /// Método que inserta o modifica una entidad
        /// </summary>
        /// <param name="carta">Entidad a modificar</param>
        /// <returns>True si fue éxitoso la inserción o modificación, en caso contrario False</returns>
        public bool Insertar(Carta carta, ITransaction transaccion)
        {
            bool exitoso;

            try
            {
                Session.Save(carta);
                transaccion.Commit();
                exitoso = transaccion.WasCommitted;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExInsertarOModificar);
            }
            finally
            {
                Session.Close();
            }

            return exitoso;
        }
    }
}
