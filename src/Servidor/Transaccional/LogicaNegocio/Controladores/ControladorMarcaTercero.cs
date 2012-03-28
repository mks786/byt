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
        public static bool InsertarOModificar(MarcaTercero marcaTercero, int hash)
        {
            bool exitoso = false;
            string id;
            string contadorStr;
            int contador;
            int indice;


            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (marcaTercero.Id == null) //Cuando se agrega un Registro
                {
                    id = (marcaTercero.Descripcion[0].ToString().ToUpper());// + marcaTercero.Descripcion[1].ToString().ToUpper());

                    ComandoBase<string> idBuscado = FabricaComandosMarcaTercero.ObtenerComandoConsultarMarcaTerceroMaxId(id);
                    idBuscado.Ejecutar();
                    indice = idBuscado.Receptor.ObjetoAlmacenado.Length - 2;
                    contadorStr = idBuscado.Receptor.ObjetoAlmacenado.Substring(2, indice);
                    contador = int.Parse(contadorStr) + 1;
                    marcaTercero.Id = idBuscado.Receptor.ObjetoAlmacenado.Substring(0, 2) + contador.ToString();
                    marcaTercero.Anexo = 1;
                    if ((id == "E") && (marcaTercero.Descripcion[1].ToString().ToUpper() == "T"))
                    {


                    }

                    if (marcaTercero.MarcasBaseTercero != null)
                    { 
                        List<MarcaBaseTercero> marcasBaseTerceroModificada = new List<MarcaBaseTercero>();
                        ComandoBase<int> idSecuencia = FabricaComandosMarcaBaseTercero.ObtenerComandoConsultarMarcaTerceroMaxSecuencia();
                        idSecuencia.Ejecutar();
                        int secuencia = idSecuencia.Receptor.ObjetoAlmacenado;
                        foreach (MarcaBaseTercero marcaBaseTercero in marcaTercero.MarcasBaseTercero)
                        {
                            marcaBaseTercero.Id = marcaTercero.Id;
                            marcaBaseTercero.Anexo = marcaTercero.Anexo;
                            secuencia++;
                            marcaBaseTercero.Secuencia = secuencia;
                            marcasBaseTerceroModificada.Add(marcaBaseTercero);
                            //ComandoBase<bool> comandoMbt = FabricaComandosMarcaBaseTercero.ObtenerComandoInsertarOModificar(marcaBaseTercero);
                            //comandoMbt.Ejecutar();
                            //exitoso = comandoMbt.Receptor.ObjetoAlmacenado;

                        }
                        marcaTercero.MarcasBaseTercero = marcasBaseTerceroModificada;
                    }
                    ComandoBase<bool> comando = FabricaComandosMarcaTercero.ObtenerComandoInsertarOModificar(marcaTercero);
                    comando.Ejecutar();
                    exitoso = comando.Receptor.ObjetoAlmacenado;
                }
                else //Cuando se Crea se Modifica un Registro
                {

                    ComandoBase<int> idAnexo = FabricaComandosMarcaTercero.ObtenerComandoConsultarMarcaTerceroMaxAnexo(marcaTercero.Id);
                    idAnexo.Ejecutar();
                    marcaTercero.Anexo = idAnexo.Receptor.ObjetoAlmacenado + 1;
                    ComandoBase<int> idSecuencia = FabricaComandosMarcaBaseTercero.ObtenerComandoConsultarMarcaTerceroMaxSecuencia();
                    idSecuencia.Ejecutar();

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