using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorFechaMarca : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        
        
        /// <summary>
        /// Metodo que retorna una lista de Fechas de una Marca para su Certificado
        /// </summary>
        /// <param name="marca">Marca a Consultar</param>
        /// <returns>Lista de fechas de marca</returns>
        public static IList<FechaMarca> ConsultarFechasPorMarca(Marca marca)
        {
            IList<FechaMarca> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<FechaMarca>> comando = FabricaComandosFechaMarca.ObtenerComandoConsultarFechasPorMarca(marca);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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
            return retorno;
        }
    }
}
