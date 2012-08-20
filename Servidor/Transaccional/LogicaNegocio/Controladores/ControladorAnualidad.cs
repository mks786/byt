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
    public class ControladorAnualidad : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que inserta o modifica una Anualidad
        /// </summary>
        /// <param name="anualidad">Anualidad a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True: si la modificación fue exitosa; false: en caso contrario</returns>
        public static bool InsertarOModificar(Patente patente, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (patente.Anualidades.Count() != 0)
                {
                    IList<Anualidad> anualidades = patente.Anualidades;

                    ComandoBase<bool> comando = FabricaComandosAnualidad.ObtenerComandoInsertarOModificar(anualidades[0]);
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
        /// Método que elimina una Anualidad
        /// </summary>
        /// <param name="anualidad">Anualidad a eliminar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Anualidad anualidad, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosAnualidad.ObtenerComandoEliminarObjeto(anualidad);
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
        /// Método que consulta la lista de todos las Anualidads
        /// </summary>
        /// <returns>Lista con todos las Anualidads</returns>
        public static IList<Anualidad> ConsultarTodos()
        {
            IList<Anualidad> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Anualidad>> comando = FabricaComandosAnualidad.ObtenerComandoConsultarTodos();
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
        /// Método que consulta la lista de todos las Anualidads
        /// </summary>
        /// <returns>Lista con todos las Anualidads</returns>
        public static int ConsultarUltimoIdAnualidad()
        {
            int retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<int> comando = FabricaComandosAnualidad.obtenerUltimoIdAnualidad();
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

        public static IList<Anualidad> ConsultarAnualidadesFiltro(Anualidad anualidad)
        {
            IList<Anualidad> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Anualidad>> comando = FabricaComandosAnualidad.ObtenerComandoConsultarAnualidadesFiltro(anualidad);
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

        public static IList<Anualidad> ConsultarAnualidadesPorPatente(Patente patente)
        {
            IList<Anualidad> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Anualidad>> comando = FabricaComandosAnualidad.ObtenerComandoConsultarAnualidadesPorPatente(patente);
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
        /// Método que inserta o modifica una Anualidad
        /// </summary>
        /// <param name="anualidad">Anualidad a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True: si la modificación fue exitosa; false: en caso contrario</returns>
        public static bool InsertarOModificarAnualidad(Patente patente, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Anualidad anualidad = new Anualidad();
                anualidad.Id = patente.Id;
                ComandoBase<IList<Anualidad>> anualidadBase = FabricaComandosAnualidad.ObtenerComandoConsultarAnualidadesFiltro(anualidad);
                anualidadBase.Ejecutar();
                IList<Anualidad> anualidadesEnBase = anualidadBase.Receptor.ObjetoAlmacenado;
                ComandoBase<int> UltimoIdAnualidad = FabricaComandosAnualidad.obtenerUltimoIdAnualidad();
                UltimoIdAnualidad.Ejecutar();
                int contador = UltimoIdAnualidad.Receptor.ObjetoAlmacenado;
                bool bandera3 = true;

                if (patente.Anualidades.Count() != 0)
                {
                    IList<Anualidad> anualidades = patente.Anualidades;


                    //Recorre las anualidades obtenidas del presentador
                    foreach (Anualidad anualidad1 in patente.Anualidades)
                    {
                        bool bandera = false;
                        if (anualidad1.Id == 0)
                        {
                            contador++;
                            anualidad1.Id = contador;
                            bandera = true;
                        }

                        if (bandera3)
                        {
                            // Recorre las marcaBase que tiene guardad en base de datos
                            foreach (Anualidad AnualidadEnBd in anualidadesEnBase)
                            {
                                bool bandera2 = false;

                                //Se recorre y compara lo que se encuentra en la base de datos
                                //con lo que se selecciono para saber si hay que eliminar algun registro
                                foreach (Anualidad anualidad2 in patente.Anualidades)
                                {
                                    if (anualidad2.Id == AnualidadEnBd.Id)
                                        bandera2 = true;

                                }
                                //si Bandera2 no cambia a true es por que no fue seleccionado o fue eliminado
                                //de la lista en el rpesentador una marcabase
                                if (!bandera2)
                                {
                                    ComandoBase<bool> comando2 =
                                    FabricaComandosAnualidad.ObtenerComandoEliminarObjeto(AnualidadEnBd);
                                    comando2.Ejecutar();
                                }


                            }
                            bandera3 = false;
                        }


                        ComandoBase<bool> comando = FabricaComandosAnualidad.ObtenerComandoInsertarOModificar(anualidad1);
                        comando.Ejecutar();
                        exitoso = comando.Receptor.ObjetoAlmacenado;
                        if ((bandera) && (exitoso))
                        {
                            //ComandoBase<bool> comandoSec = FabricaComandosContadorFac.ObtenerComandoInsertarOModificar(contadorSecuencia);
                            //comandoSec.Ejecutar();
                        }
                    }



                }
                else
                {
                    // Borra todos los registros de la bd
                    foreach (Anualidad AnualidadEnBd in anualidadesEnBase)
                    {

                        ComandoBase<bool> comando2 =
                        FabricaComandosAnualidad.ObtenerComandoEliminarObjeto(AnualidadEnBd);
                        comando2.Ejecutar();

                    }
                }


            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
            return exitoso;
        }




        #region sin utilizar

        ///// <summary>
        ///// Verifica si la Anualidad existe
        ///// </summary>
        ///// <param name="anualidad">Anualidad a verificar</param>
        ///// <returns>True de existir, false en caso conrario</returns>
        //public static bool VerificarExistencia(Anualidad anualidad)
        //{
        //    bool existe = false;
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<bool> comando = FabricaComandosAnualidad.ObtenerComandoVerificarExistenciaAnualidad(anualidad);
        //        comando.Ejecutar();
        //        existe = comando.Receptor.ObjetoAlmacenado;

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }

        //    return existe;
        //}



        ///// <summary>
        ///// Método que consulta una anualidad con todas sus dependencias
        ///// </summary>
        ///// <returns>anualidad completo</returns>
        //public static Anualidad ConsultarAnualidadConTodo(Anualidad anualidad)
        //{
        //    Anualidad retorno;

        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        ComandoBase<Anualidad> comando = FabricaComandosAnualidad.ObtenerComandoConsultarAnualidadConTodo(anualidad);
        //        comando.Ejecutar();
        //        retorno = comando.Receptor.ObjetoAlmacenado;


        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }

        //    return retorno;
        //}

        #endregion

    }
}

