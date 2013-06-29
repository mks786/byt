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


        public IList<CertificadoMarca> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public IList<CertificadoMarca> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(CertificadoMarca entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(CertificadoMarca entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(CertificadoMarca entidad)
        {
            throw new NotImplementedException();
        }


    }
}
