using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCertificadoMarca
{
    public class ComandoConsultarCertificadoMarcaPorID : ComandoBase<CertificadoMarca>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        CertificadoMarca _certificado;

        
        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoBol">InfoBol a consultar</param>
        public ComandoConsultarCertificadoMarcaPorID(CertificadoMarca certificado)
        {
            this._certificado = certificado;
        }


        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoCertificadoMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCertificadoMarca();
                this.Receptor = new Receptor<CertificadoMarca>(dao.ConsultarCertificadoMarcaPorId(this._certificado));

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
        }

    }
}
