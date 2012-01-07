using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class IdiomaServicios : MarshalByRefObject, IIdiomaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los idiomas
        /// </summary>
        /// <returns>Lista con todos los idiomas</returns>
        public IList<Idioma> ConsultarTodos()
        {
            IList<Idioma> idiomas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                idiomas = ControladorIdioma.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return idiomas;
        }

        public Idioma ConsultarPorId(Idioma idioma)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Idioma idioma, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Idioma idioma, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(Idioma entidad)
        {
            throw new NotImplementedException();
        }
    }
}
