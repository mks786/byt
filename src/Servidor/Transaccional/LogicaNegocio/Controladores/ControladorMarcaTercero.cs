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
    public class ControladorMarcaTercero : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los MarcaTerceros del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<MarcaTercero> ConsultarTodos()
        {
            IList<MarcaTercero> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<MarcaTercero>> comando = FabricaComandosMarcaTercero.ObtenerComandoConsultarTodos();
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
        /// Método que inserta o modifica un marcaTercero al sistema
        /// </summary>
        /// <param name="usuario">MarcaTercero a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static string InsertarOModificarMarcaTercero(MarcaTercero marcaTercero, int hash)
        {
            bool exitoso = false;
            string id;
            string contadorStr;
            int contador;
            int indice;
            IList<MarcaBaseTercero> listaMarcaBaseTer = null;
            string idResultante;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Auditoria auditoria = new Auditoria();
                ComandoBase<ContadorAuditoria> comandoContadorAuditoriaPoximoValor = FabricaComandosContadorAuditoria.ObtenerComandoConsultarPorId("SEG_AUDITORIA");

                comandoContadorAuditoriaPoximoValor.Ejecutar();
                ContadorAuditoria contadorAuditoria = comandoContadorAuditoriaPoximoValor.Receptor.ObjetoAlmacenado;


                auditoria.Id = contadorAuditoria.ProximoValor++;
                auditoria.Usuario = ObtenerUsuarioPorHash(hash).Id;
                auditoria.Fecha = System.DateTime.Now;
                auditoria.Operacion = marcaTercero.Operacion;
                auditoria.Tabla = "MYP_MARCAS_TER";
                auditoria.Fk = marcaTercero.Anexo;
                auditoria.Fks = marcaTercero.Id;

                ComandoBase<bool> comandoAuditoria = FabricaComandosAuditoria.ObtenerComandoInsertarOModificar(auditoria);
                ComandoBase<bool> comandoAuditoriaContador = FabricaComandosContadorAuditoria.ObtenerComandoInsertarOModificar(contadorAuditoria);
           


                if (marcaTercero.Id == null) //Cuando se agrega un Registro nuevo
                {
                    id = (marcaTercero.Descripcion[0].ToString().ToUpper());// + marcaTercero.Descripcion[1].ToString().ToUpper());


                    if ((id == "E") && (marcaTercero.Descripcion[1].ToString().ToUpper() == "T"))
                    {
                        ComandoBase<string> idBuscado = FabricaComandosMarcaTercero.ObtenerComandoConsultarMarcaTerceroMaxId("ET");
                        idBuscado.Ejecutar();

                        indice = idBuscado.Receptor.ObjetoAlmacenado.Length - 3;
                        contadorStr = idBuscado.Receptor.ObjetoAlmacenado.Substring(3, indice);
                        contador = int.Parse(contadorStr) + 1;
                        if (idBuscado.Receptor.ObjetoAlmacenado[3] == '0')
                            marcaTercero.Id = idBuscado.Receptor.ObjetoAlmacenado.Substring(0, 4) + contador.ToString();
                        else
                            marcaTercero.Id = idBuscado.Receptor.ObjetoAlmacenado.Substring(0, 3) + contador.ToString();
                        marcaTercero.Anexo = 1;

                    }
                    else
                    {
                        ComandoBase<string> idBuscado = FabricaComandosMarcaTercero.ObtenerComandoConsultarMarcaTerceroMaxId(id);
                        idBuscado.Ejecutar();

                        indice = idBuscado.Receptor.ObjetoAlmacenado.Length - 2;
                        contadorStr = idBuscado.Receptor.ObjetoAlmacenado.Substring(2, indice);
                        contador = int.Parse(contadorStr) + 1;
                        //Este proceso se hace para mantener el formato de 3 digitos del numero de la Marca a Tercero
                        if (contador.ToString().Length == 1)
                        {
                            //contadorStr = "00" + contadorStr;
                            idResultante = "00" + contador.ToString();
                            marcaTercero.Id = idBuscado.Receptor.ObjetoAlmacenado.Substring(0, 2) + idResultante;
                        }
                        else if (contador.ToString().Length == 2)
                        {
                            //contadorStr = "0" + contadorStr;
                            idResultante = "0" + contador.ToString();
                            marcaTercero.Id = idBuscado.Receptor.ObjetoAlmacenado.Substring(0, 2) + idResultante;
                        }
                        else
                        {
                            marcaTercero.Id = idBuscado.Receptor.ObjetoAlmacenado.Substring(0, 2) + contador.ToString();
                        }
                        marcaTercero.Anexo = 1;
                    }
                    
                    if (marcaTercero.Internacional != null)
                        if (marcaTercero.Internacional.Id == 0) 
                            marcaTercero.Internacional = null;

                    if(marcaTercero.Nacional != null)
                        if (marcaTercero.Nacional.Id == 0)
                            marcaTercero.Nacional = null;

                    if(marcaTercero.MarcasBaseTercero != null)
                        listaMarcaBaseTer = marcaTercero.MarcasBaseTercero;
                    //marcaTercero.MarcasBaseTercero = null;

                    ComandoBase<bool> comando = FabricaComandosMarcaTercero.ObtenerComandoInsertarOModificar(marcaTercero);
                    comando.Ejecutar();
                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if (exitoso)
                    {
                        if (listaMarcaBaseTer != null)
                        {
                            if (listaMarcaBaseTer.Count() != 0)
                            {
                                ContadorFac contadorSecuencia = new ContadorFac();
                                ComandoBase<ContadorFac> maxSecuencia =
                                    FabricaComandosContadorFac.ObtenerComandoConsultarPorId("MYP_MARCAS_BASE_TER");
                                foreach (MarcaBaseTercero aux in listaMarcaBaseTer)
                                {
                                    aux.MarcaTercero = marcaTercero;
                                    maxSecuencia.Ejecutar();
                                    int secuencia = maxSecuencia.Receptor.ObjetoAlmacenado.ProximoValor;
                                    secuencia++;
                                    contadorSecuencia.Id = "MYP_MARCAS_BASE_TER";
                                    contadorSecuencia.ProximoValor = secuencia;
                                    aux.Id = secuencia;

                                    ComandoBase<bool> comandoMTB =
                                        FabricaComandosMarcaBaseTercero.ObtenerComandoInsertarOModificar(aux);
                                    comandoMTB.Ejecutar();
                                    exitoso = comandoMTB.Receptor.ObjetoAlmacenado;

                                    if ((exitoso))
                                    {
                                        ComandoBase<bool> comandoSec =
                                            FabricaComandosContadorFac.ObtenerComandoInsertarOModificar(contadorSecuencia);
                                        comandoSec.Ejecutar();
                                    }

                                }


                            }
                        }
                    }


                }
                else //Cuando se se Modifica un Registro
                {

                    //ComandoBase<bool> nuevoAnexo =
                    //    FabricaComandosMarcaTercero.ObtenerComandoConsultarClaseInternacional(
                    //        marcaTercero.Internacional.Id, marcaTercero.Id, marcaTercero.Anexo);
                    //nuevoAnexo.Ejecutar();
                    //if (nuevoAnexo.Receptor.ObjetoAlmacenado)
                    //{
                    //    ComandoBase<int> idAnexo = FabricaComandosMarcaTercero.ObtenerComandoConsultarMarcaTerceroMaxAnexo(marcaTercero.Id);
                    //    idAnexo.Ejecutar();
                    //    marcaTercero.Anexo = idAnexo.Receptor.ObjetoAlmacenado + 1;
                    //}
                    ContadorFac contadorSecuencia = new ContadorFac();
                    List<MarcaBaseTercero> marcasBaseTerceroModificada = new List<MarcaBaseTercero>();
                    ComandoBase<ContadorFac> maxSecuencia =
                        FabricaComandosContadorFac.ObtenerComandoConsultarPorId("MYP_MARCAS_BASE_TER");
                    ComandoBase<List<MarcaBaseTercero>> marcasBase =
                        FabricaComandosMarcaBaseTercero.ObtenerComandoConsultarTodosPorId(marcaTercero.Id,
                                                                                          marcaTercero.Anexo);
                    marcasBase.Ejecutar();
                    List<MarcaBaseTercero> marcasBaseEnBase = marcasBase.Receptor.ObjetoAlmacenado;
                    if (null != marcaTercero.MarcasBaseTercero)
                    {
                        if ((marcaTercero.MarcasBaseTercero.Count() != 0))
                        {


                            //Recorre las marcaBase que han sido seleccionadas en el Presentador
                            foreach (MarcaBaseTercero marcaBaseTercero in marcaTercero.MarcasBaseTercero)
                            {

                                bool bandera = false;
                                marcaBaseTercero.MarcaTercero = new MarcaTercero();
                                marcaBaseTercero.MarcaTercero.Id = marcaTercero.Id;
                                marcaBaseTercero.MarcaTercero.Anexo = marcaTercero.Anexo;
                                if (marcaBaseTercero.Id == 0)
                                {
                                    maxSecuencia.Ejecutar();
                                    int secuencia = maxSecuencia.Receptor.ObjetoAlmacenado.ProximoValor;
                                    secuencia++;
                                    contadorSecuencia.Id = "MYP_MARCAS_BASE_TER";
                                    contadorSecuencia.ProximoValor = secuencia;
                                    marcaBaseTercero.Id = secuencia;
                                    bandera = true;
                                }

                                //Recorre las marcaBase que tiene guardad en base de datos
                                foreach (MarcaBaseTercero MarcaEnBd in marcasBaseEnBase)
                                {
                                    bool bandera2 = false;

                                    //Se recorre y compara lo que se encuentra en la base de datos
                                    //con lo que se selecciono para saber si hay que eliminar algun registro
                                    foreach (MarcaBaseTercero marcaBaseTercero2 in marcaTercero.MarcasBaseTercero)
                                    {
                                        if (marcaBaseTercero2.Id == MarcaEnBd.Id)
                                            bandera2 = true;

                                    }
                                    //si Bandera2 no cambia a true es por que no fue seleccionado o fue eliminado
                                    //de la lista en el rpesentador una marcabase
                                    if (!bandera2)
                                    {
                                        ComandoBase<bool> comando2 =
                                            FabricaComandosMarcaBaseTercero.ObtenerComandoEliminarMarcaBaseTercero(
                                                MarcaEnBd);
                                        comando2.Ejecutar();
                                    }
                                }
                                marcasBaseTerceroModificada.Add(marcaBaseTercero);



                                //ComandoBase<bool> comandoMbt = FabricaComandosMarcaBaseTercero.ObtenerComandoInsertarOModificar(marcaBaseTercero);
                                //comandoMbt.Ejecutar();
                                //exitoso = comandoMbt.Receptor.ObjetoAlmacenado;

                                ComandoBase<bool> comandoMTB =
                                    FabricaComandosMarcaBaseTercero.ObtenerComandoInsertarOModificar(marcaBaseTercero);
                                comandoMTB.Ejecutar();
                                exitoso = comandoMTB.Receptor.ObjetoAlmacenado;



                                if ((bandera) && (exitoso))
                                {
                                    ComandoBase<bool> comandoSec =
                                        FabricaComandosContadorFac.ObtenerComandoInsertarOModificar(contadorSecuencia);
                                    comandoSec.Ejecutar();
                                }

                            }
                            //       }
                        }
                        else
                        {
                            foreach (MarcaBaseTercero marca in marcasBaseEnBase)
                            {
                                ComandoBase<bool> comando2 =
                                    FabricaComandosMarcaBaseTercero.ObtenerComandoEliminarMarcaBaseTercero(marca);
                                comando2.Ejecutar();
                                marcaTercero.MarcasBaseTercero = null;
                            }
                        }
                    }

                    marcaTercero.MarcasBaseTercero = new List<MarcaBaseTercero>();

                    ComandoBase<bool> comando = FabricaComandosMarcaTercero.ObtenerComandoInsertarOModificar(marcaTercero);
                    comando.Ejecutar();
                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if(exitoso)
                    {
                        comandoAuditoria.Ejecutar();
                        comandoAuditoriaContador.Ejecutar();

                        
                    }

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
            string retorno = marcaTercero.Id+"/"+marcaTercero.Anexo;
            return retorno;
        }

        /// <summary>
        /// Método que consulta un MarcaTercero por su Id
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero con el Id del pais buscado</param>
        /// <returns>El MarcaTercero solicitado</returns>
        public static MarcaTercero ConsultarPorId(MarcaTercero marcaTercero)
        {
            MarcaTercero retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<MarcaTercero> comando = FabricaComandosMarcaTercero.ObtenerComandoConsultarPorID(marcaTercero);
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
        /// Método que elimina un MarcaTercero
        /// </summary>
        /// <param name="usuario">MarcaTercero a eliminar</param>
        /// <param name="hash">Hash del MarcaTercero que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(MarcaTercero marcaTercero, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosMarcaTercero.ObtenerComandoEliminarMarcaTercero(marcaTercero);
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
        /// Verifica si el MarcaTercero existe
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a verificar</param>
        /// <returns>True de existir, false en caso contrario</returns>
        public static bool VerificarExistencia(MarcaTercero marcaTercero)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosMarcaTercero.ObtenerComandoVerificarExistenciaMarcaTercero(marcaTercero);
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

        public static int ObtenerUltimoAnexoMarcaTercero(string idMarcaTercero)
        {
            int existe = 0;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<int> comando = FabricaComandosMarcaTercero.ObtenerComandoConsultarMarcaTerceroMaxAnexo(idMarcaTercero);
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

        public static IList<MarcaTercero> ConsultarMarcasTerceroFiltro(MarcaTercero marcaTercero)
        {
            IList<MarcaTercero> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<MarcaTercero>> comando = FabricaComandosMarcaTercero.ObtenerComandoConsultarMarcasTerceroFiltro(marcaTercero);
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