using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorCesion : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos las Cesiones del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<Cesion> ConsultarTodos()
        {
            IList<Cesion> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Cesion>> comando = FabricaComandosCesion.ObtenerComandoConsultarTodos();
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

        /// <summary>
        /// Método que modifica un los datos de una Cesion
        /// </summary>
        /// <param name="cesion">Cesion a modificar</param>
        /// <param name="hash">Hash de la Cesion que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(Cesion cesion, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (cesion.Id == 0)
                {
                    ComandoBase<bool> comandoCesionContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorCesionProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_MCESIONES");
                    comandoContadorCesionProximoValor.Ejecutar();
                    Contador contadorCesion = comandoContadorCesionProximoValor.Receptor.ObjetoAlmacenado;

                    comandoCesionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorCesion);
                    cesion.Id = contadorCesion.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    operacion.Fecha = System.DateTime.Now;
                    operacion.Aplicada = 'M';
                    operacion.CodigoAplicada = cesion.Marca.Id;
                    operacion.Interno = cesion.Id;
                    operacion.Servicio = new Servicio("CS");

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<bool> comando = FabricaComandosCesion.ObtenerComandoInsertarOModificar(cesion);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if (exitoso)
                    {
                        comandoCesionContador.Ejecutar();
                        comandoOperacionContador.Ejecutar();
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosCesion.ObtenerComandoInsertarOModificar(cesion);
                    comando.Ejecutar();
                    exitoso = comando.Receptor.ObjetoAlmacenado;
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

        /// <summary>
        /// Método que consulta una Cesion por su Id
        /// </summary>
        /// <param name="cesion">Cesion con el Id de la Cesion buscada</param>
        /// <returns>La Cesion solicitada</returns>
        public static Cesion ConsultarPorId(Cesion cesion)
        {
            Cesion retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Cesion> comando = FabricaComandosCesion.ObtenerComandoConsultarPorID(cesion);
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

        /// <summary>
        /// Método que elimina una Cesion
        /// </summary>
        /// <param name="cesion">Cesion a eliminar</param>
        /// <param name="hash">Hash de la cesion que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Cesion cesion, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCesion.ObtenerComandoEliminarCesion(cesion);
                comando.Ejecutar();
                exitoso = true;

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
        /// Verifica si la Cesion existe
        /// </summary>
        /// <param name="cesion">Cesion a verificar</param>
        /// <returns>True de existir, false en caso contrario</returns>
        public static bool VerificarExistencia(Cesion cesion)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCesion.ObtenerComandoVerificarExistenciaCesion(cesion);
                comando.Ejecutar();
                existe = comando.Receptor.ObjetoAlmacenado;

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

            return existe;
        }

        public static IList<Cesion> ConsultarCesionesFiltro(Cesion cesion)
        {
            IList<Cesion> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Cesion>> comando = FabricaComandosCesion.ObtenerComandoConsultarCesionesFiltro(cesion);
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