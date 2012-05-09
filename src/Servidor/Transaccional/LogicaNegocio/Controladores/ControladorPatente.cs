﻿using System;
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
    public class ControladorPatente : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que inserta o modifica una Patente
        /// </summary>
        /// <param name="patente">Patente a insertar o modificar</param>
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

                ComandoBase<bool> comandoInteresadoContador = null;

                // si es una insercion
                if (patente.Operacion.Equals("CREATE"))
                {
                    ComandoBase<Contador> comandoContadorInteresadoProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_MARCAS");
                    comandoContadorInteresadoProximoValor.Ejecutar();
                    Contador contador = comandoContadorInteresadoProximoValor.Receptor.ObjetoAlmacenado;
                    patente.Id = contador.ProximoValor++;
                    comandoInteresadoContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contador);
                }

                //ComandoBase<bool> comandoAnaqua = FabricaComandosAnaqua.ObtenerComandoInsertarOModificar(patente.Anaqua);
                //comandoAnaqua.Ejecutar();

                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = patente.Operacion;
                auditoria.Tabla = "MYP_MARCAS";
                auditoria.Fk = patente.Id;

                ComandoBase<bool> comando = FabricaComandosPatente.ObtenerComandoInsertarOModificar(patente);
                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);
                
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                if (exitoso)
                {
                    comandoAuditoria.Ejecutar();
                    comandoAuditoriaContador.Ejecutar();
                    
                    if (comandoInteresadoContador != null)
                        comandoInteresadoContador.Ejecutar();
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
        /// Método que elimina una Patente
        /// </summary>
        /// <param name="patente">Patente a eliminar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Patente patente, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosPatente.ObtenerComandoEliminarObjeto(patente);
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
        /// Método que consulta la lista de todos las Patentes
        /// </summary>
        /// <returns>Lista con todos las Patentes</returns>
        public static IList<Patente> ConsultarTodos()
        {
            IList<Patente> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Patente>> comando = FabricaComandosPatente.ObtenerComandoConsultarTodos();
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
        /// Verifica si la Patente existe
        /// </summary>
        /// <param name="patente">Patente a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(Patente patente)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosPatente.ObtenerComandoVerificarExistenciaPatente(patente);
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

        public static IList<Patente> ConsultarPatentesFiltro(Patente patente)
        {
            IList<Patente> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Patente>> comando = FabricaComandosPatente.ObtenerComandoConsultarPatentesFiltro(patente);
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
        /// Método que consulta una patente con todas sus dependencias
        /// </summary>
        /// <returns>patente completo</returns>
        public static Patente ConsultarPatenteConTodo(Patente patente)
        {
            Patente retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Patente> comando = FabricaComandosPatente.ObtenerComandoConsultarPatenteConTodo(patente);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

                ComandoBase<InfoAdicional> comandoInfoAdicional = FabricaComandosInfoAdicional.ObtenerComandoConsultarInfoAdicionalPorId(new InfoAdicional("M." + retorno.Id));
                comandoInfoAdicional.Ejecutar();
                retorno.InfoAdicional = comandoInfoAdicional.Receptor.ObjetoAlmacenado;

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
                                    if (anualidad1.Id ==0)
                                    {
                                        contador++;
                                        anualidad1.Id = contador;
                                        bandera =true;
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
                                      if ((bandera)&&(exitoso))
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


    }
}

