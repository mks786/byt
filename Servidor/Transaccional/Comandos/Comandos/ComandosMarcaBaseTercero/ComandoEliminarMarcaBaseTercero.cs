using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarcaBaseTercero
{
    public class ComandoEliminarMarcaBaseTercero : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MarcaBaseTercero _marcaBaseTercero;

        /// <summary>
        /// Metodo que Elimina Marca Tercero
        /// </summary>
        /// <param name="marcaBaseTercero">marcaBaseTercero a eliminar</param>
        public ComandoEliminarMarcaBaseTercero(MarcaBaseTercero marcaBaseTercero)
        {
            this._marcaBaseTercero = marcaBaseTercero;
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

                IDaoMarcaBaseTercero dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMarcaBaseTercero();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._marcaBaseTercero));

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