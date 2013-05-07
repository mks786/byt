using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public abstract class DaoBaseNHibernate<T, Id> : IDaoBase<T, Id>
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ISession _session;
        //private NHibernateHelper _helper;

        //public DaoBaseNHibernate()
        //{
        //    _helper = NHibernateHelper.ObtenerInstanciaNHibernateHelper();
        //}


        /// <summary>
        /// Propiedad para obtener la sesion
        /// </summary>
        public ISession Session
        {
            get
            {
                if (null == _session)
                    _session = NHibernateHelper.OpenSession();
                return _session;
            }

            set { _session = value; }
        }


        /// <summary>
        /// Método que obtiene todos los registros de la entidad
        /// </summary>
        /// <returns>Lista con todos los elementos de la entidad</returns>
        public IList<T> ObtenerTodos()
        {
            IList<T> listaEntidad;

            try
            {
                listaEntidad = Session.CreateCriteria(typeof(T)).AddOrder(Order.Asc("Id")).List<T>();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }

            return listaEntidad;
        }


        /// <summary>
        /// Método que obtiene todos los registros de la entidad ordenada en el orden especificado
        /// </summary>
        /// <param name="parametro">Columna por la que se desea ordenar</param>
        /// <param name="orden">"Asc" para ascendiente, "Desc" para descendiente</param>
        /// <returns>La Lista ordenada en lo especificado</returns>
        public IList<T> ObtenerTodos(string parametro, string orden)
        {
            IList<T> listaEntidad;

            try
            {
                if (orden.Equals("Asc"))
                    listaEntidad = Session.CreateCriteria(typeof(T)).AddOrder(Order.Asc(parametro)).List<T>();
                else
                    listaEntidad = Session.CreateCriteria(typeof(T)).AddOrder(Order.Desc(parametro)).List<T>();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }

            return listaEntidad;
        }


        /// <summary>
        /// Método que obtiene el elemento de la entidad por su id
        /// </summary>
        /// <param name="id">Id de la entidad a requerida</param>
        /// <returns>Entidad requerida</returns>
        public T ObtenerPorId(Id id)
        {
            T entidad;

            try
            {
                entidad = Session.Get<T>(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExObtenerPorId);
            }
            finally
            {
                Session.Close();
            }

            return entidad;
        }


        /// <summary>
        /// Método que verifica si existe una entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public bool VerificarExistencia(Id id)
        {
            bool existe = false;
            T entidad;

            try
            {
                entidad = Session.Get<T>(id);

                if (entidad != null)
                    existe = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExObtenerPorId);
            }
            finally
            {
                Session.Close();
            }

            return existe;
        }


        /// <summary>
        /// Método que obtiene el elemento de la entidad por su id y 
        /// lo bloquea hasta que sea actualizado
        /// </summary>
        /// <param name="id">Id de la entidad a requerida</param>
        /// <returns>Entidad requerida</returns>
        public T ObtenerPorIdYBloquear(Id id)
        {
            T entidad;

            try
            {
                entidad = Session.Get<T>(id, LockMode.Upgrade);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExObtenerPorId);
            }
            finally
            {
                Session.Close();
            }

            return entidad;
        }


        /// <summary>
        /// Método que inserta o modifica una entidad
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>True si fue éxitoso la inserción o modificación, en caso contrario False</returns>
        public bool InsertarOModificar(T entidad)
        {
            bool exitoso;

            try
            {
                ITransaction transaccion = Session.BeginTransaction();
                Session.SaveOrUpdate(entidad);
                transaccion.Commit();
                exitoso = transaccion.WasCommitted;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                //throw new ApplicationException(Recursos.Errores.ExInsertarOModificar);
                throw new ApplicationException(Recursos.Errores.ExInsertarOModificar + ": " + ex.InnerException.Message);
            }
            finally
            {
                Session.Close();
            }

            return exitoso;
        }


        /// <summary>
        /// Elimina la Entidad que se envio
        /// </summary>
        /// <param name="entidad">Elimina la entidad</param>
        /// <returns>True si se elimino, de lo contrario false</returns>
        public bool Eliminar(T entidad)
        {
            bool exitoso;

            try
            {
                ITransaction transaccion = Session.BeginTransaction();
                Session.Delete(entidad);
                transaccion.Commit();
                exitoso = transaccion.WasCommitted;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExEliminando);
            }
            finally
            {
                Session.Close();
            }

            return exitoso;
        }



        ///// <summary>
        ///// Método que cierra la sesion de la BD
        ///// </summary>
        //public void CerrarSesion()
        //{
        //    try
        //    {
        //        _helper.GetSession().Flush();
        //        _helper.GetSession().Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        string a = ex.Message;
        //        throw;
        //    }
        //}
    }
}