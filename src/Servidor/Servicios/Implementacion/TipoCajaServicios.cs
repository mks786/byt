using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class TipoCajaServicios: MarshalByRefObject, ITipoCajaServicios 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los TipoCajas
        /// </summary>
        /// <returns>Lista con todos los TipoCajas</returns>
        public IList<TipoCaja> ConsultarTodos()
        {
            IList<TipoCaja> tipoCaja;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoCaja = ControladorTipoCaja.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return tipoCaja;
        }



        /// <summary>
        /// Servicio que obtiene los tipos de caja de Archivo de acuerdo a lo que se consulte, Marca o Patente
        /// </summary>
        /// <param name="parametro1">Parametro para Marca o Patente Nacional</param>
        /// <returns></returns>
        public IList<TipoCaja> ObtenerTipoCajaMarcaOPatente(String parametro1)
        {
            IList<TipoCaja> tipoCaja = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoCaja = ControladorTipoCaja.ObtenerTipoCajaMarcaOPatente(parametro1);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return tipoCaja;
        }





        public TipoCaja ConsultarPorId(TipoCaja entidad)
        {
            throw new NotImplementedException();
        }

        public IList<TipoCaja> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(TipoCaja entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(TipoCaja entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(TipoCaja entidad)
        {
            throw new NotImplementedException();
        }
    }
}
