using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarca
{
    public class ComandoConsultarMarcaPorId : ComandoBase<Marca>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="id">Marca a eliminar</param>
        public ComandoConsultarMarcaPorId(int id)
        {
            _marca = new Marca();
            this._marca.Id = id;
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
                
                IDaoMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMarca();
                this.Receptor = new Receptor<Marca>(dao.ObtenerPorId(this._marca.Id));

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