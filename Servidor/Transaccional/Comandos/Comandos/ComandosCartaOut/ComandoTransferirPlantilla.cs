using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCartaOut
{
    class ComandoTransferirPlantilla : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IList<Carta> _cartas;
        private IList<CartaOut> _cartasOut;


        /// <summary>
        /// Metodo Comando que Transfiere una platilla dado las cartas y cartasout
        /// </summary>
        /// <param name="cartas">Lista de cartas</param>
        /// <param name="cartasOut">Lista de CartasOuts</param>
        public ComandoTransferirPlantilla(IList<Carta> cartas,IList<CartaOut> cartasOut)
        {
            this._cartas = cartas;
            this._cartasOut = cartasOut;
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

                IDaoCartaOut dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCartaOut();
                this.Receptor = new Receptor<bool>(dao.TransferirPlantilla(this._cartas, this._cartasOut));

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
