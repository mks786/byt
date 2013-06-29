using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class FechaMarcaServicios: MarshalByRefObject, IFechaMarcaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<FechaMarca> ConsultarFechasPorMarca(Marca marca)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<FechaMarca> fechas = ControladorFechaMarca.ConsultarFechasPorMarca(marca);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return fechas;
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


       
        public IList<FechaMarca> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }


        public bool InsertarOModificar(FechaMarca entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(FechaMarca entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(FechaMarca entidad)
        {
            throw new NotImplementedException();
        }

        public FechaMarca ConsultarPorId(FechaMarca entidad)
        {
            throw new NotImplementedException();
        }

        public IList<FechaMarca> ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
