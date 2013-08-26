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
    public class ControladorFiltroPlantilla: ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que consulta la lista de todos los filtros de todas las plantillas
        /// </summary>
        /// <returns>Lista con todos las monedas</returns>
        public static IList<FiltroPlantilla> ConsultarTodos()
        {
            IList<FiltroPlantilla> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<FiltroPlantilla>> comando = FabricaComandosFiltroPlantilla.ObtenerComandoConsultarTodos();
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
       /// Metodo que devuelve la lista de los filtros de encabezado de una plantilla especifica
       /// </summary>
       /// <param name="plantilla">Plantilla a consultar</param>
       /// <returns>Lista de filtros de encabezado de una plantilla especifica</returns>
       public static IList<FiltroPlantilla> ConsultarFiltrosEncabezadoPlantilla(Plantilla plantilla)
       {
           IList<FiltroPlantilla> retorno;
           try
           {
               #region trace
               if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                   logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
               #endregion

               ComandoBase<IList<FiltroPlantilla>> comando = FabricaComandosFiltroPlantilla.ObtenerComandoConsultarFiltrosEncabezadoPlantilla(plantilla);
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
       /// Metodo que devuelve la lista de los filtros de detalle de una plantilla especifica
       /// </summary>
       /// <param name="plantilla">Plantilla a consultar</param>
       /// <returns>Lista de filtros de detalle de una plantilla especifica</returns>
       public static IList<FiltroPlantilla> ConsultarFiltrosDetallePlantilla(Plantilla plantilla)
       {
           IList<FiltroPlantilla> retorno;
           try
           {
               #region trace
               if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                   logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
               #endregion

               ComandoBase<IList<FiltroPlantilla>> comando = FabricaComandosFiltroPlantilla.ObtenerComandoConsultarFiltrosDetallePlantilla(plantilla);
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
       /// Metodo que inserta o actualiza un filtro de plantilla 
       /// </summary>
       /// <param name="filtro">Filtro de Plantilla a insertar o modificar</param>
       /// <param name="hash">Hash del usuario logueado</param>
       /// <returns>True si la operacion se realiza satisfactoriamente, false en caso contrario</returns>
       public static bool InsertarOModificar(FiltroPlantilla filtro, int hash)
       {
           bool exitoso = false;
           try
           {
               #region trace
               if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                   logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
               #endregion

               ComandoBase<bool> comando = FabricaComandosFiltroPlantilla.ObtenerComandoInsertarOModificar(filtro);
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
       /// Metodo que elimina de base de datos un filtro de una plantilla especifica
       /// </summary>
       /// <param name="filtro">Filtro a eliminar</param>
       /// <param name="hash">Hash del usuario logueado</param>
       /// <returns>True si la operacion se realiza satisfactoriamente; false en caso contrario</returns>
       public static bool Eliminar(FiltroPlantilla filtro, int hash)
       {
           bool exitoso = false;
           try
           {
               #region trace
               if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                   logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
               #endregion

               ComandoBase<bool> comando = FabricaComandosFiltroPlantilla.ObtenerComandoEliminarFiltroPlantilla(filtro);
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



    }
}
