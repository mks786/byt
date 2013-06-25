using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class AlmacenServicios : MarshalByRefObject, IAlmacenServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los Almacenes de Archivos
        /// </summary>
        /// <returns>Lista con todos los Almacenes de Archivos</returns>
        public IList<Almacen> ConsultarTodos()
        {
            IList<Almacen> almacenes;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                almacenes = ControladorAlmacen.ConsultarTodos();

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

            return almacenes;
        }


        public Almacen ConsultarPorId(Almacen entidad)
        {
            throw new NotImplementedException();
        }

        public IList<Almacen> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Almacen entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Almacen entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Almacen entidad)
        {
            throw new NotImplementedException();
        }
    }
}
