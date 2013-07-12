using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CertificadoMarcaServicios: MarshalByRefObject, ICertificadoMarcaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="certificado">Certificado de Marca a consultar</param>
        /// <returns>Certificado de Marca en base de datos</returns>
        public CertificadoMarca ConsultarPorId(CertificadoMarca certificado)
        {
            CertificadoMarca retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                retorno = ControladorCertificadoMarca.ConsultarPorId(certificado);

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
            return retorno;

        }


        /// <summary>
        /// Servicio que obtiene todas las auditorias de un Certificado de Marca especifico
        /// </summary>
        /// <param name="auditoria">Auditoria </param>
        /// <returns></returns>
        public IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Auditoria> auditorias = ControladorCertificadoMarca.AuditoriaPorFkyTabla(auditoria);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return auditorias;
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


        public IList<CertificadoMarca> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public IList<CertificadoMarca> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio para Insertar o Modificar un Certificado de Marca
        /// </summary>
        /// <param name="certificado">Certificado a insertar o a actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>Confirmacion de insercion, true si se realizo; false en caso contrario</returns>
        public bool InsertarOModificar(CertificadoMarca certificado, int hash)
        {
            //throw new NotImplementedException();
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                
                bool exitoso = ControladorCertificadoMarca.InsertarOModificar(ref certificado, hash);

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


        /// <summary>
        /// Servicio para Eliminar un Certificado de Marca
        /// </summary>
        /// <param name="certificado">Certificado de Marca a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si se elimino correctamente, false en caso contrario</returns>
        public bool Eliminar(CertificadoMarca certificado, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //bool exitoso = ControladorInteresado.Eliminar(certificado, hash);
                bool exitoso = ControladorCertificadoMarca.Eliminar(certificado, hash);

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

        public bool VerificarExistencia(CertificadoMarca entidad)
        {
            throw new NotImplementedException();
        }


    }
}
