using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class MarcaBaseTerceroServicios : MarshalByRefObject, IMarcaBaseTerceroServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public IList<MarcaBaseTercero> ConsultarTodos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<MarcaBaseTercero> marcaBaseTerceros = ControladorMarcaBaseTercero.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return marcaBaseTerceros;
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
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public MarcaBaseTercero ConsultarPorId(MarcaBaseTercero marcaBaseTercero)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificar(MarcaBaseTercero marcaBaseTercero, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarcaBaseTercero.InsertarOModificar(marcaBaseTercero, hash);

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
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(MarcaBaseTercero marcaBaseTercero, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarcaBaseTercero.Eliminar(marcaBaseTercero, hash);

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
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(MarcaBaseTercero marcaBaseTercero)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMarcaBaseTercero.VerificarExistencia(marcaBaseTercero);

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
        /// Servicio que se encarga de obtener las Marcas Base Tercero segun un filtro
        /// </summary>
        /// <param name="marcaBaseTercero">Marca base tercero a filtrar</param>
        /// <returns>Lista de Marca Base Tercero</returns>
        IList<MarcaBaseTercero> IMarcaBaseTerceroServicios.ObtenerMarcaBaseTerceroFiltro(MarcaBaseTercero MarcaBaseTercero)
        {
            try
            {
                IList<MarcaBaseTercero> marcas;

                marcas = ControladorMarcaBaseTercero.ConsultarMarcasTerceroFiltro(MarcaBaseTercero);

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
        IList<MarcaBaseTercero> IServicioBase<MarcaBaseTercero>.ConsultarTodos()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        MarcaBaseTercero IServicioBase<MarcaBaseTercero>.ConsultarPorId(MarcaBaseTercero entidad)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        bool IServicioBase<MarcaBaseTercero>.InsertarOModificar(MarcaBaseTercero entidad, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            throw new NotImplementedException();
            //   bool exitoso = ControladorMarcaBaseTercero.InsertarOModificar(entidad, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        bool IServicioBase<MarcaBaseTercero>.Eliminar(MarcaBaseTercero entidad, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            throw new NotImplementedException();
            //     bool exitoso = ControladorMarcaBaseTercero.Eliminar(entidad, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        bool IServicioBase<MarcaBaseTercero>.VerificarExistencia(MarcaBaseTercero entidad)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que se encarga de obtener la Auditoria de la Marca Base Tercero
        /// </summary>
        /// <param name="auditoria">Auditoria a buscar</param>
        /// <returns>Lista de auditorias de la Marca Base Tercero</returns>
        IList<Auditoria> IMarcaBaseTerceroServicios.AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que se encarga de consultar la Marca Base Tercero con todos sus objetos relacionados
        /// </summary>
        /// <param name="marcaTercero">Marca Base Tercero a consultar</param>
        /// <returns>Marca Base Tercero con sus objetos</returns>
        MarcaBaseTercero IMarcaBaseTerceroServicios.ConsultarMarcaConTodo(MarcaBaseTercero marcaBaseTercero)
        {
            throw new NotImplementedException();
        }
    }
}
