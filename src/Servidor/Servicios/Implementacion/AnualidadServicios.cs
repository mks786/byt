using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    class AnualidadServicios : MarshalByRefObject, IAnualidadServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<Anualidad> ConsultarTodos()
        {
            IList<Anualidad> anualidades;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                anualidades = ControladorAnualidad.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return anualidades;
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Anualidad ConsultarPorId(Anualidad anualidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica una Anualidad
        /// </summary>
        /// <param name="patente">Anualidad que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(Patente patente, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorAnualidad.InsertarOModificar(patente, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="anualidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(Anualidad anualidad, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorAnualidad.Eliminar(anualidad, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que se encarga de insertar una nueva anualidad
        /// </summary>
        /// <param name="Patente">Patente a insertar con sus anualidades</param>
        /// <param name="hash">hash del usuario</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificarAnualidad(Patente patente, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno;
            try
            {
                retorno = ControladorAnualidad.InsertarOModificarAnualidad(patente, hash);


                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }

            return retorno;
        }


        //public bool VerificarExistencia(Anualidad anualidad)
        //{
        //    #region trace
        //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //        logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion

        //    bool exitoso = ControladorAnualidad.VerificarExistencia(anualidad);

        //    #region trace
        //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //        logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion

        //    return exitoso;
        //}


        /// <summary>
        /// Servicio que se encarga de consultar las anualidades basadas en el filtro
        /// </summary>
        /// <param name="Anualidad">Anualidad filtro</param>
        /// <returns>Lista de anualidades que cumplen con el filtro</returns>
        public IList<Anualidad> ObtenerAnualidadesFiltro(Anualidad anualidad)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anualidad> anualidades;

            anualidades = ControladorAnualidad.ConsultarAnualidadesFiltro(anualidad);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return anualidades;


        }


        /// <summary>
        /// Servicio que se encarga de consultar el ultimo ID de las anualidades
        /// </summary>
        /// <returns>Ultimo Id de las anualidades</returns>
        public int ConsultarUltimoIdAnualidad()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            int anualidades;

            anualidades = ControladorAnualidad.ConsultarUltimoIdAnualidad();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return anualidades;


        }


        //public IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria)
        //{
        //    #region trace
        //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //        logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion

        //    IList<Auditoria> auditorias = ControladorAnualidad.AuditoriaPorFkyTabla(auditoria);

        //    #region trace
        //    if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //        logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion

        //    return auditorias;
        //}


        //public Anualidad ConsultarAnualidadConTodo(Anualidad anualidad)
        //{
        //    Anualidad retorno;
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        retorno = ControladorAnualidad.ConsultarAnualidadConTodo(anualidad);

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        ////    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }

        //    return retorno;
        //}



        public Anualidad ConsultarAnualidadConTodo(Anualidad Anualidad)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(Anualidad entidad)
        {
            throw new NotImplementedException();
        }




        public bool InsertarOModificar(Anualidad entidad, int hash)
        {
            throw new NotImplementedException();
        }


        public IList<Anualidad> ConsultarAnualidadesPorPatente(Patente patente)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anualidad> anualidades;

            anualidades = ControladorAnualidad.ConsultarAnualidadesPorPatente(patente);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return anualidades;

        }
    }
}
