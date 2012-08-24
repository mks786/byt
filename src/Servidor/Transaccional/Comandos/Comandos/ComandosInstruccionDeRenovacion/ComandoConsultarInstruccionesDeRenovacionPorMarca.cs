using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionDeRenovacion
{
    class ComandoConsultarInstruccionesDeRenovacionPorMarca : ComandoBase<IList<InstruccionDeRenovacion>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;


        /// <summary>
        /// Metodo Comando que Consulta las instruccionDeRenovacions pertenecientes a una marca
        /// </summary>
        /// <param name="marca">Marca a consultar sus instruccionDeRenovacions</param>
        public ComandoConsultarInstruccionesDeRenovacionPorMarca(Marca marca)
        {
            this._marca = marca;

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

                IDaoInstruccionDeRenovacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionDeRenovacion();
                this.Receptor = new Receptor<IList<InstruccionDeRenovacion>>(dao.ObtenerInstruccionesDeRenovacionPorMarca(this._marca));

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
