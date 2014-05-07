using System;
using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoMaterialSapiNHibernate : DaoBaseNHibernate<MaterialSapi,string>,IDaoMaterialSapi 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene una lista de materiales sapi a traves de un filtro determinado
        /// </summary>
        /// <param name="material">Material Sapi usado como filtro</param>
        /// <returns>Lista de Materiales Sapi que cumplen con los filtros dados</returns>
        public IList<MaterialSapi> ObtenerMaterialSapiFiltro(MaterialSapi material)
        {
            IList<MaterialSapi> materiales = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMaterialSapi);

                if ((null != material) && (!material.Id.Equals("")) && (!material.Id.Equals("NGN")))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerMaterialSapiId, material.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(material.Descripcion))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaterialSapiDescripcion, material.Descripcion);
                    variosFiltros = true;
                }


                if (!string.IsNullOrEmpty(material.Tipo))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaterialSapiTipo, material.Tipo);
                    variosFiltros = true;
                }

                if ((material.Departamento != null) && (!material.Departamento.Id.Equals("NGN")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaterialSapiDepartamento, material.Departamento.Id);
                    variosFiltros = true;
                }
                

                //filtro += " order by c.Asociado DESC, c.Id DESC";

                IQuery query = Session.CreateQuery(cabecera + filtro);
                materiales = query.List<MaterialSapi>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Error al consultar Materiales Sapi por filtro: " + ex.Message);
            }
            finally
            {
                Session.Close();
            }

            return materiales;
        }
    }
}
