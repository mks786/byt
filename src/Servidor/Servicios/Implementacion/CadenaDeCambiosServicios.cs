using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CadenaDeCambiosServicios: MarshalByRefObject, ICadenaDeCambiosServicios 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        
        public IList<CadenaDeCambios> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public IList<CadenaDeCambios> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public CadenaDeCambios ConsultarPorId(CadenaDeCambios archivo)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(CadenaDeCambios entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que inserta y/o actualiza una Cadena de Cambios especifica
        /// </summary>
        /// <param name="cadenaCambios">Cadena de Cambios a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realizo correctamente; False, en caso contrario</returns>
        public bool InsertarOModificar(CadenaDeCambios cadenaCambios, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCadenaDeCambios.InsertarOModificar(cadenaCambios, hash);

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
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }
        }

        public bool Eliminar(CadenaDeCambios entidad, int hash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que obtiene un grupo de cadena de cambios dependiendo de un filtro dado
        /// </summary>
        /// <param name="cadenaCambiosFiltro">Cadena de Cambios usada como filtro</param>
        /// <returns>Lista de Cadenas de Cambios resultantes</returns>
        public IList<CadenaDeCambios> ObtenerCadenasCambioFiltro(CadenaDeCambios cadenaCambiosFiltro)
        {

            IList<CadenaDeCambios> cadenasDeCambios;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                cadenasDeCambios = ControladorCadenaDeCambios.ObtenerCadenasCambioFiltro(cadenaCambiosFiltro);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return cadenasDeCambios;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }
        }
    }
}
