using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMaterialSapi
{
    public class ComandoConsultarMaterialesSapiFiltro : ComandoBase<IList<MaterialSapi>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MaterialSapi _material;

        /// <summary>
        /// Constructor predeterminado 
        /// </summary>
        /// <param name="material">Material Sapi usado como filtro</param>
        public ComandoConsultarMaterialesSapiFiltro(MaterialSapi material)
        {
            this._material = material;
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

                IDaoMaterialSapi dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMaterialSapi();
                this.Receptor = new Receptor<IList<MaterialSapi>>(dao.ObtenerMaterialSapiFiltro(this._material));

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
