using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFechaMarca
{
    public class ComandoInsertarOModificarFechaMarca : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        FechaMarca _fechaMarca;

        /// <summary>
        /// Constructor predeterminado que recibe una fecha de marca
        /// </summary>
        /// <param name="fechaMarca">Fecha de marca a insertar o actualizar</param>
        public ComandoInsertarOModificarFechaMarca(FechaMarca fechaMarca)
        {
            this._fechaMarca = fechaMarca;
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

                IDaoFechaMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFechaMarca();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._fechaMarca));

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
