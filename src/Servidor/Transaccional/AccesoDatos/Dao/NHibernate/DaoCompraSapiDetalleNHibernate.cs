using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCompraSapiDetalleNHibernate : DaoBaseNHibernate<CompraSapiDetalle,int>, IDaoCompraSapiDetalle 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene los detalles de compra Sapi por filtro
        /// </summary>
        /// <param name="compraSapiDetalle">Detalle de Compra Sapi usado como filtro</param>
        /// <returns>Lista de detalles de compra Sapi resultantes</returns>
        public IList<CompraSapiDetalle> ObtenerCompraSapiDetalleFiltro(CompraSapiDetalle compraSapiDetalle)
        {
            IList<CompraSapiDetalle> detallesCompra = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCompraSapiDetalle);

                //Por código de Compra SAPI
                if ((compraSapiDetalle.Compra != null) && (compraSapiDetalle.Compra.Id != int.MinValue))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerDetalleCompraSapiIdCompra, compraSapiDetalle.Compra.Id);
                    variosFiltros = true;
                }


                if ((compraSapiDetalle.Compra != null) && (compraSapiDetalle.Compra.FechaCompra != null))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", compraSapiDetalle.Compra.FechaCompra);
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetalleCompraSapiFechaCompra, fecha);
                    variosFiltros = true;
                }

                if ((compraSapiDetalle.Material != null) && (!compraSapiDetalle.Material.Id.Equals("NGN")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetalleCompraSapiIdMaterial, compraSapiDetalle.Material.Id);
                    variosFiltros = true;
                }

                if (!filtro.Equals(String.Empty))
                {
                    filtro += " order by c.Compra.FechaCompra DESC";
                }
                
                IQuery query = Session.CreateQuery(cabecera + filtro);
                detallesCompra = query.List<CompraSapiDetalle>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Error al consultar Detalles de Compra SAPI por filtro: " + ex.Message);
            }
            finally
            {
                Session.Close();
            }
            return detallesCompra;
        }
    }
}
