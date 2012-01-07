using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using System;
using NHibernate;
using System.Configuration;

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


        public Marca ObtenerMarcaConTodo(Marca marca)
        {
            Marca retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerMarcaConTodo, marca.Id));
                retorno = query.UniqueResult<Marca>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodosUsuariosPorUsuario);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }
    }
}
