using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCertificadoMarca
{
    public class ComandoInsertarOModificarCertificado : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CertificadoMarca _certificado;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="certificado">Marca a insertar o modificar</param>
        public ComandoInsertarOModificarCertificado(CertificadoMarca certificado)
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._certificado));

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
