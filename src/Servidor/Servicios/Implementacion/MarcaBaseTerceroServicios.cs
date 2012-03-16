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

        /// <summary>
        /// Método que obtiene todos los MarcaBaseTerceros
        /// </summary>
        /// <returns>Todos los MarcaBaseTerceros</returns>
        public IList<MarcaBaseTercero> ConsultarTodos()
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


        public MarcaBaseTercero ConsultarPorId(MarcaBaseTercero marcaBaseTercero)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que inserta o modifica un MarcaBaseTercero
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero a insertar o modificar</param>
        /// <param name="hash">hash del usuario loggeado</param>
        /// <returns></returns>
        public bool InsertarOModificar(MarcaBaseTercero marcaBaseTercero, int hash)
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

        /// <summary>
        /// Método que elimina un MarcaBaseTercero
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero a eliminar</param>
        /// <param name="hash">Hash del usuario loggeado</param>
        /// <returns></returns>
        public bool Eliminar(MarcaBaseTercero marcaBaseTercero, int hash)
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

        /// <summary>
        /// Método que verifica si un marcaBaseTercero ya existe en el sistema
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero a buscar</param>
        /// <returns>true si lo encontro, false en lo contrario</returns>
        public bool VerificarExistencia(MarcaBaseTercero marcaBaseTercero)
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

        IList<MarcaBaseTercero> IMarcaBaseTerceroServicios.ObtenerMarcaBaseTerceroFiltro(MarcaBaseTercero MarcaBaseTercero)
        {
            IList<MarcaBaseTercero> marcas;

            marcas = ControladorMarcaBaseTercero.ConsultarMarcasTerceroFiltro(MarcaBaseTercero);

            return marcas;
        }

        IList<MarcaBaseTercero> IServicioBase<MarcaBaseTercero>.ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        MarcaBaseTercero IServicioBase<MarcaBaseTercero>.ConsultarPorId(MarcaBaseTercero entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica una Marca Tercera
        /// </summary>
        /// <param name="marca">Marca que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
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
        /// Servicio que elimina una Marca
        /// </summary>
        /// <param name="marca">Marca que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
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

        bool IServicioBase<MarcaBaseTercero>.VerificarExistencia(MarcaBaseTercero entidad)
        {
            throw new NotImplementedException();
        }


        IList<Auditoria> IMarcaBaseTerceroServicios.AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            throw new NotImplementedException();
        }

        MarcaBaseTercero IMarcaBaseTerceroServicios.ConsultarMarcaConTodo(MarcaBaseTercero marcaBaseTercero)
        {
            throw new NotImplementedException();
        }
    }
}
