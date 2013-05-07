using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class MarcaTerceroServicios : MarshalByRefObject, IMarcaTerceroServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los MarcaTerceros
        /// </summary>
        /// <returns>Todos los MarcaTerceros</returns>
        public IList<MarcaTercero> ConsultarTodos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<MarcaTercero> marcaTerceros = ControladorMarcaTercero.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return marcaTerceros;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        public MarcaTercero ConsultarPorId(MarcaTercero marcaTercero)
        {
            throw new NotImplementedException();
        }

        //-------------------------------------------------------------
        /// <summary>
        /// Servicio que consulta por un campo determinado y 
        /// ordena en forma Ascendente o Descendente
        /// </summary>
        /// <param name="campo">Campo a filtrar</param>
        /// <param name="tipoOrdenamiento">Si el ordenamiento es Ascende o Descendente</param>
        /// <returns>Lista de Entidades</returns>
        public IList<MarcaTercero> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
        //-------------------------------------------------------------


        /// <summary>
        /// Método que inserta o modifica un MarcaTercero
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a insertar o modificar</param>
        /// <param name="hash">hash del usuario loggeado</param>
        /// <returns></returns>
       // public string InsertarOModificar(MarcaTercero marcaTercero, List<MarcaBaseTercero> marcasBaseTercero, int hash)
            public string InsertarOModificarMarcaTercero(MarcaTercero marcaTercero,  int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string exitoso = ControladorMarcaTercero.InsertarOModificarMarcaTercero(marcaTercero, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        /// <summary>
        /// Método que elimina un MarcaTercero
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a eliminar</param>
        /// <param name="hash">Hash del usuario loggeado</param>
        /// <returns></returns>
        public bool Eliminar(MarcaTercero marcaTercero, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarcaTercero.Eliminar(marcaTercero, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        /// <summary>
        /// Método que verifica si un marcaTercero ya existe en el sistema
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a buscar</param>
        /// <returns>true si lo encontro, false en lo contrario</returns>
        public bool VerificarExistencia(MarcaTercero marcaTercero)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarcaTercero.VerificarExistencia(marcaTercero);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        public int ObtenerUltimoAnexoMarcaTercero(string idMarcaTercero)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                int exitoso = ControladorMarcaTercero.ObtenerUltimoAnexoMarcaTercero(idMarcaTercero);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }

        /// <summary>
        /// Servicio que se encarga de obtener la Marca Tercero basada en un filtro
        /// </summary>
        /// <param name="marcaTercero">Marca tercero filtro</param>
        /// <returns>Lista de marcas tercero que cumplan con el filtro</returns>
        IList<MarcaTercero> IMarcaTerceroServicios.ObtenerMarcaTerceroFiltro(MarcaTercero MarcaTercero)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                IList<MarcaTercero> marcas;

                marcas = ControladorMarcaTercero.ConsultarMarcasTerceroFiltro(MarcaTercero);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                return marcas;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        IList<MarcaTercero> IServicioBase<MarcaTercero>.ConsultarTodos()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        MarcaTercero IServicioBase<MarcaTercero>.ConsultarPorId(MarcaTercero entidad)
        {
            throw new NotImplementedException();
        }


        ///// <summary>
        ///// Servicio que insertar o modifica una Marca Tercera
        ///// </summary>
        ///// <param name="marca">Marca que se va a insertar o modificar</param>
        ///// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        ///// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        //bool IServicioBase<MarcaTercero>.InsertarOModificar(MarcaTercero entidad, int hash)
        //{
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        string exitoso = ControladorMarcaTercero.InsertarOModificar(entidad, hash);

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        return exitoso;
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
        //    }
        //}


        /// <summary>
        /// Servicio que elimina una Marca
        /// </summary>
        /// <param name="marca">Marca que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        bool IServicioBase<MarcaTercero>.Eliminar(MarcaTercero entidad, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                throw new NotImplementedException();
                //bool exitoso = ControladorMarcaTercero.Eliminar(entidad, hash);

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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        bool IServicioBase<MarcaTercero>.VerificarExistencia(MarcaTercero entidad)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio encargado de consultar la auditoria de Marca Tercero
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de Auditorias</returns>
        IList<Auditoria> IMarcaTerceroServicios.AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Auditoria> auditorias = ControladorMarcaTercero.AuditoriaPorFkyTabla(auditoria);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return auditorias;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        /// <summary>
        /// Servicio encargado de consultar una marca tercero con todos sus objetos
        /// </summary>
        /// <param name="marcaTercero">Marca Tercero a consultar</param>
        /// <returns>Marca tercero consultada con todos los objetos</returns>
        MarcaTercero IMarcaTerceroServicios.ConsultarMarcaConTodo(MarcaTercero marcaTercero)
        {
            throw new NotImplementedException();
        }



        public bool InsertarOModificar(MarcaTercero entidad, int hash)
        {
            throw new NotImplementedException();
        }
    }
}
