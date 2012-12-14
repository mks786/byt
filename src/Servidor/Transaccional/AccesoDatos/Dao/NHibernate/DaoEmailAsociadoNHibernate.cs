using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoEmailAsociadoNHibernate : DaoBaseNHibernate<EmailAsociado, int>, IDaoEmailAsociado
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();



        /// <summary>
        /// Metodo que inserta una carta en la base de datos
        /// </summary>
        /// <param name="carta">parametro a insertar</param>
        /// <param name="transaccion">objetoITransicion indica si se realizo el Commit</param>
        /// <returns>Bool si se inserto correctamente, de lo contrario false</returns>
        public bool Insertar(Carta carta, ITransaction transaccion)
        {
            bool exitoso;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Session.Save(carta);
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

        public IList<EmailAsociado> ObtenerEmailAsociadosFiltro(EmailAsociado EmailAsociado)
        {
            throw new NotImplementedException();
        }

        public bool Insertar(EmailAsociado EmailAsociado, ITransaction transaccion)
        {
            throw new NotImplementedException();
        }


        public EmailAsociado ObtenerPorId(string id)
        {
            throw new NotImplementedException();
        }

        public EmailAsociado ObtenerPorIdYBloquear(string id)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(string id)
        {
            throw new NotImplementedException();
        }
    }
}
