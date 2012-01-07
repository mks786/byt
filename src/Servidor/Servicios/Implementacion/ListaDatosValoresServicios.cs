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

        public IList<ListaDatosValores> ConsultarTodos()
        {

            throw new NotImplementedException();
        }

        public ListaDatosValores ConsultarPorId(ListaDatosValores ListaDatosValores)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(ListaDatosValores ListaDatosValores, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(ListaDatosValores ListaDatosValores, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(ListaDatosValores ListaDatosValores)
        {
            throw new NotImplementedException();
        }

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
