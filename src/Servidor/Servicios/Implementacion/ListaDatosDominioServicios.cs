using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class ListaDatosDominioServicios : MarshalByRefObject, IListaDatosDominioServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los paises
        /// </summary>
        /// <returns>Todos los paises</returns>
        public IList<ListaDatosDominio> ConsultarListaDatosDominioPorParametro(ListaDatosDominio parametro)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<ListaDatosDominio> datos = ControladorListaDatosDominio.ConsultarListaDatosDominioPorParametro(parametro);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return datos;
        }

        public IList<ListaDatosDominio> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public ListaDatosDominio ConsultarPorId(ListaDatosDominio entidad)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(ListaDatosDominio entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(ListaDatosDominio entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(ListaDatosDominio entidad)
        {
            throw new NotImplementedException();
        }
    }
}
