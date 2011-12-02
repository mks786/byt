using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using System;
using NHibernate;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoMarcaNHibernate : DaoBaseNHibernate<Marca, int>, IDaoMarca
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public IList<Marca> ObtenerMarcasFiltro(Marca marca)
        {
            IList<Marca> Marcas = null;
            bool variosFiltros = false;
            string filtro = "";
            string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarca);
            if ((null != marca) && (marca.Id != 0))
            {
                filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaId, marca.Id);
                variosFiltros = true;
            }
            if ((null != marca.Asociado) && (!marca.Asociado.Id.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaIdAsociado, marca.Asociado.Id);
                variosFiltros = true;
            }
            if (!string.IsNullOrEmpty(marca.Fichas))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFichas, marca.Fichas);
            }
            if (!string.IsNullOrEmpty(marca.Descripcion))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaDescripcion, marca.Descripcion);
            }
            if ((null != marca.FechaPublicacion) && (!marca.FechaPublicacion.Equals(DateTime.MinValue)))
            {
                if (variosFiltros)
                    filtro += " and ";
                string fecha = String.Format("{0:dd/MM/yy}", marca.FechaPublicacion);
                string fecha2 = String.Format("{0:dd/MM/yy}", marca.FechaPublicacion.Value.AddDays(1));
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFecha, fecha, fecha2);
            }
            IQuery query = Session.CreateQuery(cabecera + filtro);
            Marcas = query.List<Marca>();
            return Marcas;
        }
    }
}
