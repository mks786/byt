using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoTipoEmailAsociadoNHibernate : DaoBaseNHibernate<TipoEmailAsociado, string>, IDaoTipoEmailAsociado
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que inserta una TipoEmailAsociado en la base de datos
        /// </summary>
        /// <param name="TipoEmailAsociado">parametro a insertar</param>
        /// <param name="transaccion">objetoITransicion indica si se realizo el Commit</param>
        /// <returns>Bool si se inserto correctamente, de lo contrario false</returns>
        public bool Insertar(TipoEmailAsociado TipoEmailAsociado, ITransaction transaccion)
        {
            bool exitoso;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Session.Save(TipoEmailAsociado);
                transaccion.Commit();
                exitoso = transaccion.WasCommitted;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
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


        public TipoEmailAsociado ObtenerPorId(string id)
        {
            throw new NotImplementedException();
        }

        public TipoEmailAsociado ObtenerPorIdYBloquear(string id)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(string id)
        {
            throw new NotImplementedException();
        }
    }
}
