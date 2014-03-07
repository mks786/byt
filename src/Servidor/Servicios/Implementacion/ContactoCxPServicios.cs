using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class ContactoCxPServicios: MarshalByRefObject, IContactoCxPServicios 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los Contactos CxP de la tabla FAC_CONTACTO_CXP
        /// </summary>
        /// <returns>Lista de todos los registros que se encuentran en la tabla FAC_CONTACTO_CXP</returns>
        public IList<ContactoCxP> ConsultarTodos()
        {
            IList<ContactoCxP> contactosCxP;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                contactosCxP = ControladorContactoCxP.ConsultarTodos();

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
            return contactosCxP;
        }

        /// <summary>
        /// Servicio que obtiene una lista de ContactosCxP de acuerdo a un filtro dado
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP usado como filtro</param>
        /// <returns>Lista de Contactos CxP encontrados</returns>
        public IList<ContactoCxP> ConsultarContactoCxPFiltro(ContactoCxP contactoCxP)
        {
            IList<ContactoCxP> contactosCxP;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                contactosCxP = ControladorContactoCxP.ConsultarContactoCxPFiltro(contactoCxP);

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
            return contactosCxP;
        }

        /// <summary>
        /// Servicio que inserta o actualiza un ContactoCxP
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion fue realizada con exito; False, en caso contrario</returns>
        public bool InsertarOModificar(ContactoCxP contactoCxP, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorContactoCxP.InsertarOModificar(contactoCxP, hash);

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

        public bool Eliminar(ContactoCxP entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(ContactoCxP entidad)
        {
            throw new NotImplementedException();
        }

        public ContactoCxP ConsultarPorId(ContactoCxP contactoCxP)
        {
            throw new NotImplementedException();
        }

        public IList<ContactoCxP> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
    }
}
