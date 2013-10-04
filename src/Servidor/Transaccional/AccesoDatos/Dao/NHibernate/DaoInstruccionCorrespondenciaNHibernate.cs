using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoInstruccionCorrespondenciaNHibernate : DaoBaseNHibernate<InstruccionCorrespondencia,int>, IDaoInstruccionCorrespondencia
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene una instruccion de correspondencia por codigo, aplica a y concepto. 
        /// Este metodo aplica para las Marcas y para las Patentes
        /// </summary>
        /// <param name="instruccionCorrespondencia">Instruccion de Correspondencia a consultar</param>
        /// <returns>Instruccion de Correspondencia de acuerdo a los filtros; NULL en caso contrario</returns>
        public InstruccionCorrespondencia ObtenerInstruccionDeCorrespondencia(InstruccionCorrespondencia instruccion)
        {
            InstruccionCorrespondencia retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInstruccionDeCorrespondenciaMarcaOPatente, instruccion.Id, instruccion.AplicadaA, instruccion.Concepto));
                retorno = query.UniqueResult<InstruccionCorrespondencia>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerArchivoPorId);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }

    }
}
