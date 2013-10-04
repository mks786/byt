using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
namespace Trascend.Bolet.Servicios.Implementacion
{
    public class InstruccionDescuentoServicios : MarshalByRefObject, IInstruccionDescuentoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        
        public IList<InstruccionDescuento> ConsultarTodos()
        {
            IList<InstruccionDescuento> instrucciones;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instrucciones = ControladorInstruccionDescuento.ConsultarTodos();

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
            return instrucciones;
        }


        public bool InsertarOModificar(InstruccionDescuento instruccion, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorInstruccionDescuento.InsertarOModificar(instruccion, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
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
        }


        /// <summary>
        /// Servicio que obtiene todas las instrucciones de Descuento de una marca o de una patente cualquiera
        /// </summary>
        /// <param name="instruccionFiltro">Instruccion de Descuento filtro</param>
        /// <returns>Lista de Instrucciones de Descuento de una marca o de una patente</returns>
        public IList<InstruccionDescuento> ObtenerInstruccionesDeDescuentoMarcaOPatente(InstruccionDescuento instruccionFiltro)
        {
            IList<InstruccionDescuento> instrucciones;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instrucciones = ControladorInstruccionDescuento.ObtenerInstruccionesDeDescuentoMarcaOPatente(instruccionFiltro);

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
            return instrucciones;
        }


        public IList<InstruccionDescuento> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(InstruccionDescuento entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(InstruccionDescuento entidad)
        {
            throw new NotImplementedException();
        }


        public InstruccionDescuento ConsultarPorId(InstruccionDescuento archivo)
        {
            throw new NotImplementedException();
        }


    }
}
