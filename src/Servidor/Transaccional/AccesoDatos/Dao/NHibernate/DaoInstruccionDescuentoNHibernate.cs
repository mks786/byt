using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoInstruccionDescuentoNHibernate : DaoBaseNHibernate<InstruccionDescuento, int>, IDaoInstruccionDescuento
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<InstruccionDescuento> ObtenerInstruccionesDeDescuentoMarcaOPatente(InstruccionDescuento instruccionDescuento)
        {
            IList<InstruccionDescuento> instrucciones;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInstruccionesDescuentoMarcaOPatente, instruccionDescuento.CodigoOperacion, instruccionDescuento.AplicaA));
                instrucciones = query.List<InstruccionDescuento>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerInfobolesPorMarca);
            }
            finally
            {
                Session.Close();
            }

            return instrucciones;
        }
    }
}
