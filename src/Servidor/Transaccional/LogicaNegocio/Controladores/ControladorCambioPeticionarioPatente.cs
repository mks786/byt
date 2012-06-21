using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorCambioPeticionarioPatente : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los CambioPeticionarioPatente del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<CambioPeticionarioPatente> ConsultarTodos()
        {
            IList<CambioPeticionarioPatente> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CambioPeticionarioPatente>> comando = FabricaComandosCambioPeticionarioPatente.ObtenerComandoConsultarTodos();
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
        /// Método que modifica un los datos de un CambioPeticionarioPatente
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente a modificar</param>
        /// <param name="hash">Hash del CambioPeticionarioPatente que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(ref CambioPeticionarioPatente cambioPeticionario, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (cambioPeticionario.Id == 0)
                {
                    ComandoBase<bool> comandoCambioPeticionarioContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorCambioPeticionarioProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_PPETICIONARIOS");
                    comandoContadorCambioPeticionarioProximoValor.Ejecutar();
                    Contador contadorCambioPeticionario = comandoContadorCambioPeticionarioProximoValor.Receptor.ObjetoAlmacenado;

                    comandoCambioPeticionarioContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorCambioPeticionario);
                    cambioPeticionario.Id = contadorCambioPeticionario.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    operacion.Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", System.DateTime.Now));
                    operacion.Aplicada = 'P';
                    operacion.CodigoAplicada = cambioPeticionario.Patente.Id;
                    operacion.Interno = cambioPeticionario.Id;
                    operacion.Interesado = cambioPeticionario.InteresadoActual;
                    operacion.Servicio = new Servicio("CT");

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<bool> comando = FabricaComandosCambioPeticionarioPatente.ObtenerComandoInsertarOModificar(cambioPeticionario);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    ComandoBase<Patente> comandoMarca = FabricaComandosPatente.ObtenerComandoConsultarPatenteConTodo(cambioPeticionario.Patente);
                    comandoMarca.Ejecutar();
                    Patente patente = comandoMarca.Receptor.ObjetoAlmacenado;
                    patente.Interesado = cambioPeticionario.InteresadoActual;
                    patente.Agente = cambioPeticionario.AgenteActual;
                    patente.Poder = cambioPeticionario.PoderActual;

                    ComandoBase<bool> comandoEditarPatente = FabricaComandosPatente.ObtenerComandoInsertarOModificar(patente);
                    comandoEditarPatente.Ejecutar();
                    bool exitosoPatente = comandoEditarPatente.Receptor.ObjetoAlmacenado;

                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if (exitoso)
                    {
                        comandoCambioPeticionarioContador.Ejecutar();
                        comandoOperacionContador.Ejecutar();
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosCambioPeticionarioPatente.ObtenerComandoInsertarOModificar(cambioPeticionario);
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
        /// Método que consulta un CambioPeticionarioPatente por su Id
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente con el Id del CambioPeticionarioPatente buscado</param>
        /// <returns>El CambioPeticionarioPatente solicitado</returns>
        public static CambioPeticionarioPatente ConsultarPorId(CambioPeticionarioPatente cambioPeticionario)
        {
            CambioPeticionarioPatente retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<CambioPeticionarioPatente> comando = FabricaComandosCambioPeticionarioPatente.ObtenerComandoConsultarPorID(cambioPeticionario);
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
        /// Método que elimina un CambioPeticionarioPatente
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente a eliminar</param>
        /// <param name="hash">Hash del CambioPeticionarioPatente que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(CambioPeticionarioPatente cambioPeticionario, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCambioPeticionarioPatente.ObtenerComandoEliminarCambioPeticionarioPatente(cambioPeticionario);
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
        /// Verifica si el CambioPeticionarioPatente existe
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente a verificar</param>
        /// <returns>True de existir, false en caso contrario</returns>
        public static bool VerificarExistencia(CambioPeticionarioPatente cambioPeticionario)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCambioPeticionarioPatente.ObtenerComandoVerificarExistenciaCambioPeticionarioPatente(cambioPeticionario);
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

        /// <summary>
        /// Consulta los Fusion que cumplan con los filtros establecidos en el objeto enviado
        /// </summary>
        /// <param name="CambioDeDomicilio">Fusion a consultar</param>
        /// <returns>Lista de Fusion que cumplen con el filtro</returns>
        public static IList<CambioPeticionarioPatente> ConsultarCambioPeticionarioPatenteFiltro(CambioPeticionarioPatente cambioPeticionario)
        {
            IList<CambioPeticionarioPatente> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CambioPeticionarioPatente>> comando = FabricaComandosCambioPeticionarioPatente.ObtenerComandoConsultarCambioPeticionarioPatenteFiltro(cambioPeticionario);
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