﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBolMarcaTer
{
    public class ComandoEliminarInfoBolMarcaTer : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private InfoBolMarcaTer _infoBol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoBol">infoBol a eliminar</param>
        public ComandoEliminarInfoBolMarcaTer(InfoBolMarcaTer infoBol)
        {
            this._infoBol = infoBol;
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

                IDaoInfoBolMarcaTer dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBolMarcaTer();
                dao.Eliminar(this._infoBol);

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