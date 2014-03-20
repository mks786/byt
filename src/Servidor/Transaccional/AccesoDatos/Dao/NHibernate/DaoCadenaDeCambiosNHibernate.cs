using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCadenaDeCambiosNHibernate : DaoBaseNHibernate<CadenaDeCambios,int>, IDaoCadenaDeCambios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene una lista de cadenas de cambios a partir de un filtro
        /// </summary>
        /// <param name="cadenaDeCambios">Cadena de Cambios filtro</param>
        /// <returns>Lista de cadenas de cambios</returns>
        public IList<CadenaDeCambios> ObtenerCadenaDeCambiosFiltro(CadenaDeCambios cadenaDeCambios)
        {
            IList<CadenaDeCambios> cadenasDeCambios = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCadenaDeCambios);

                //Por código
                if ((null != cadenaDeCambios) && (cadenaDeCambios.Id != 0) && (cadenaDeCambios.Id != int.MinValue))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCadenaCambiosId, cadenaDeCambios.Id);
                    variosFiltros = true;
                }

                //Por tipo de cadena de cambios
                if (!string.IsNullOrWhiteSpace(cadenaDeCambios.TipoCambio))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCadenaCambiosTipoCambio, cadenaDeCambios.TipoCambio);
                    variosFiltros = true;
                }

                //Por Codigo de Operacion
                if ((cadenaDeCambios.CodigoOperacion != 0) && (cadenaDeCambios.CodigoOperacion != int.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCadenaCambiosCodigoOperacion, cadenaDeCambios.CodigoOperacion);
                    variosFiltros = true;
                }

                //Por fecha de Cadena de Cambios
                if ((null != cadenaDeCambios.FechaCadenaCambio) && (!cadenaDeCambios.FechaCadenaCambio.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", cadenaDeCambios.FechaCadenaCambio);
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCadenaCambiosFecha, fecha);
                    variosFiltros = true;
                }

                
                filtro += " order by c.FechaCadenaCambio desc";

                IQuery query = Session.CreateQuery(cabecera + filtro);
                cadenasDeCambios = query.List<CadenaDeCambios>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }
            return cadenasDeCambios;
        }
    }
}
