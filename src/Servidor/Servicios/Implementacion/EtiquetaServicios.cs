using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class EtiquetaServicios : MarshalByRefObject, IEtiquetaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos las etiquetas
        /// </summary>
        /// <returns>Lista con todos las etiquetas</returns>
        public IList<Etiqueta> ConsultarTodos()
        {
            IList<Etiqueta> etiquetas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                etiquetas = ControladorEtiqueta.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return etiquetas;
        }

        public Etiqueta ConsultarPorId(Etiqueta etiqueta)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Etiqueta etiqueta, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Etiqueta etiqueta, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(Etiqueta entidad)
        {
            throw new NotImplementedException();
        }
    }
}
