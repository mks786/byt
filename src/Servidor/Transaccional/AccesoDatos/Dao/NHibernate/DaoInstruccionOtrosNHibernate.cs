using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoInstruccionOtrosNHibernate : DaoBaseNHibernate<InstruccionOtros,int>, IDaoInstruccionOtros
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene en el Dao las Instrucciones No Tipificadas de una marca o patente
        /// </summary>
        /// <param name="instruccionOtros">Instruccion no tipificada que sirve de filtro</param>
        /// <returns>Lista de instrucciones no tipificadas de una marca o de una patente</returns>
        public IList<InstruccionOtros> ObtenerInstruccionesNoTipificadasPorCodigo(InstruccionOtros instruccionOtros)
        {
            IList<InstruccionOtros> instrucciones;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInstruccionesNoTipificadas, instruccionOtros.Cod_MarcaOPatente, instruccionOtros.AplicaA));
                instrucciones = query.List<InstruccionOtros>();

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
