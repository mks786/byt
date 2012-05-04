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
        /// Servicio que obtiene todos los Anualidades
        /// </summary>
        /// <returns>Lista con todos los Anualidades</returns>
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

        public Anualidad ConsultarPorId(Anualidad anualidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica una Anualidad
        /// </summary>
        /// <param name="anualidad">Anualidad que se va a insertar o modificar</param>
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
        /// Servicio que elimina una Anualidad
        /// </summary>
        /// <param name="anualidad">Anualidad que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
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
        /// Servicio que consulta una serie de Anualidads por uno o mas parametros
        /// </summary>
        /// <param name="anualidad">Anualidad que contiene los parametros de la consulta</param>
        /// <returns>Lista de cartas filtradas</returns>
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
    }
}
