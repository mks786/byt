using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class ListaDatosValoresServicios : MarshalByRefObject, IListaDatosValoresServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<ListaDatosValores> ConsultarTodos()
        {

            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public ListaDatosValores ConsultarPorId(ListaDatosValores ListaDatosValores)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificar(ListaDatosValores ListaDatosValores, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(ListaDatosValores ListaDatosValores, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(ListaDatosValores ListaDatosValores)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que consulta la Lista de Valores por parametro
        /// </summary>
        /// <param name="listaDatosValores">parametro a buscar</param>
        /// <returns>Lista de Valores</returns>
        public IList<ListaDatosValores> ConsultarListaDatosValoresPorParametro(ListaDatosValores listaDatosValores)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<ListaDatosValores> listasDatosValores = ControladorListaDatosValores.ConsultarListaDatosValoresPorParametro(listaDatosValores);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return listasDatosValores;
        }
    }
}
