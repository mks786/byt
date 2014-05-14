using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorPresentacionSapi : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que inserta o actualiza el Encabezado de la Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiEncabezado">Encabezado de la Presentacion Sapi a actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion es realizada con exito; False, en caso contrario</returns>
        public static bool InsertarOModificar(ref PresentacionSapi presentacionSapiEncabezado, int hash)
        {
            bool exitoso = false;
            int idContador = 0;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comandoContador = null;

                // si es una insercion
                if (presentacionSapiEncabezado.Operacion.Equals("CREATE"))
                {
                    ComandoBase<Contador> comandoContadorProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("SAPI_PRESENTACION_ENC");
                    comandoContadorProximoValor.Ejecutar();
                    Contador contador = comandoContadorProximoValor.Receptor.ObjetoAlmacenado;
                    presentacionSapiEncabezado.Id = contador.ProximoValor;
                    idContador = contador.ProximoValor++;
                    comandoContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contador);
                }


                ComandoBase<bool> comando = FabricaComandosPresentacionSapi.ObtenerComandoInsertarOModificar(presentacionSapiEncabezado);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    if (comandoContador != null)
                        comandoContador.Ejecutar();
                }

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
    }
}
