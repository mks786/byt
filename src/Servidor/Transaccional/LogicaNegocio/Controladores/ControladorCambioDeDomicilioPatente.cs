using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorCambioDeDomicilioPatente : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los CambioDeDomicilioPatente del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<CambioDeDomicilioPatente> ConsultarTodos()
        {
            IList<CambioDeDomicilioPatente> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CambioDeDomicilioPatente>> comando = FabricaComandosCambioDeDomicilioPatente.ObtenerComandoConsultarTodos();
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
        /// Método que modifica un los datos de un CambioDeDomicilioPatente
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente a modificar</param>
        /// <param name="hash">Hash del CambioDeDomicilioPatente que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(ref CambioDeDomicilioPatente cambioDeDomicilio, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (cambioDeDomicilio.Id == 0)
                {
                    ComandoBase<bool> comandoCambioDeDomicilioContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorCambioDeDomicilioProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_PDOMICILIOS");
                    comandoContadorCambioDeDomicilioProximoValor.Ejecutar();
                    Contador contadorCambioDeDomicilio = comandoContadorCambioDeDomicilioProximoValor.Receptor.ObjetoAlmacenado;

                    comandoCambioDeDomicilioContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorCambioDeDomicilio);
                    cambioDeDomicilio.Id = contadorCambioDeDomicilio.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    //operacion.Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", System.DateTime.Now));
                    operacion.Fecha = cambioDeDomicilio.FechaDomicilio;
                    operacion.Aplicada = 'P';
                    operacion.CodigoAplicada = cambioDeDomicilio.Patente.Id;
                    operacion.Interno = cambioDeDomicilio.Id;
                    operacion.Interesado = cambioDeDomicilio.InteresadoActual;
                    operacion.Servicio = new Servicio("CD");

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<bool> comando = FabricaComandosCambioDeDomicilioPatente.ObtenerComandoInsertarOModificar(cambioDeDomicilio);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    ComandoBase<Patente> comandoMarca = FabricaComandosPatente.ObtenerComandoConsultarPatenteConTodo(cambioDeDomicilio.Patente);
                    comandoMarca.Ejecutar();
                    Patente patente = comandoMarca.Receptor.ObjetoAlmacenado;
                    patente.Interesado = cambioDeDomicilio.InteresadoActual;
                    patente.Agente = cambioDeDomicilio.Agente;
                    patente.Poder = cambioDeDomicilio.Poder;

                    ComandoBase<bool> comandoEditarPatente = FabricaComandosPatente.ObtenerComandoInsertarOModificar(patente);
                    comandoEditarPatente.Ejecutar();
                    bool exitosoPatente = comandoEditarPatente.Receptor.ObjetoAlmacenado;

                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if ((exitoso) && (exitosoPatente))
                    {
                        comandoCambioDeDomicilioContador.Ejecutar();
                        comandoOperacionContador.Ejecutar();
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosCambioDeDomicilioPatente.ObtenerComandoInsertarOModificar(cambioDeDomicilio);
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
        /// Método que consulta un CambioDeDomicilioPatente por su Id
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente con el Id del CambioDeDomicilioPatente buscado</param>
        /// <returns>El CambioDeDomicilioPatente solicitado</returns>
        public static CambioDeDomicilioPatente ConsultarPorId(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            CambioDeDomicilioPatente retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<CambioDeDomicilioPatente> comando = FabricaComandosCambioDeDomicilioPatente.ObtenerComandoConsultarPorID(cambioDeDomicilio);
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
        /// Método que elimina un CambioDeDomicilioPatente
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente a eliminar</param>
        /// <param name="hash">Hash del CambioDeDomicilioPatente que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(CambioDeDomicilioPatente cambioDeDomicilio, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCambioDeDomicilioPatente.ObtenerComandoEliminarCambioDeDomicilio(cambioDeDomicilio);
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
        /// Verifica si el CambioDeDomicilioPatente existe
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCambioDeDomicilioPatente.ObtenerComandoVerificarExistenciaCambioDeDomicilio(cambioDeDomicilio);
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
        /// <param name="CambioDeDomicilioPatente">Fusion a consultar</param>
        /// <returns>Lista de Fusion que cumplen con el filtro</returns>
        public static IList<CambioDeDomicilioPatente> ConsultarCambioDeDomicilioPatenteFiltro(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            IList<CambioDeDomicilioPatente> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CambioDeDomicilioPatente>> comando = FabricaComandosCambioDeDomicilioPatente.ObtenerComandoConsultarCambiosDeDomicilioPatenteFiltro(cambioDeDomicilio);
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