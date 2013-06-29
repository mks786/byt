using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class RegistradorServicios : MarshalByRefObject, IRegistradorServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Registrador> ConsultarTodos()
        {
            IList<Registrador> registradores;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //almacenes = ControladorAlmacen.ConsultarTodos();
                registradores = ControladorRegistrador.ConsultarTodos();

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

            return registradores;
        }


        public Registrador ConsultarPorId(Registrador entidad)
        {
            throw new NotImplementedException();
        }

        public IList<Registrador> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Registrador entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Registrador entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(Registrador entidad)
        {
            throw new NotImplementedException();
        }

    }
}
