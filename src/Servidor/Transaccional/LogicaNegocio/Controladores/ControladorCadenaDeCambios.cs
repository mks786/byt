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
    public class ControladorCadenaDeCambios : ControladorBase 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que inserta y/o actualiza una Cadena de Cambios especifica
        /// </summary>
        /// <param name="cadenaCambios">Cadena de Cambios a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realizo correctamente; False, en caso contrario</returns>
        public static bool InsertarOModificar(CadenaDeCambios cadenaCambios, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //ComandoBase<bool> comando = FabricaComandosArchivo.ObtenerComandoInsertarOModificar(Archivo);
                ComandoBase<bool> comando = FabricaComandosCadenaDeCambios.ObtenerComandoInsertarOModificar(cadenaCambios);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

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
            return exitoso;
        }


        /// <summary>
        /// Metodo que obtiene un grupo de cadena de cambios dependiendo de un filtro dado
        /// </summary>
        /// <param name="cadenasDeCambios">Cadena de Cambios usada como filtro</param>
        /// <returns>Lista de Cadenas de Cambios resultantes</returns>
        public static IList<CadenaDeCambios> ObtenerCadenasCambioFiltro(CadenaDeCambios cadenaCambiosFiltro)
        {
            IList<CadenaDeCambios> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CadenaDeCambios>> comando = FabricaComandosCadenaDeCambios.ObtenerComandoConsultarCadenasCambioFiltro(cadenaCambiosFiltro);
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
