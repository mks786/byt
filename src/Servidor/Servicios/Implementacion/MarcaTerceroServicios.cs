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


        public MarcaTercero ConsultarPorId(MarcaTercero marcaTercero)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que inserta o modifica un MarcaTercero
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a insertar o modificar</param>
        /// <param name="hash">hash del usuario loggeado</param>
        /// <returns></returns>
        public bool InsertarOModificar(MarcaTercero marcaTercero,List<MarcaBaseTercero> marcasBaseTercero, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorMarcaTercero.InsertarOModificar(marcaTercero,hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que elimina un MarcaTercero
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a eliminar</param>
        /// <param name="hash">Hash del usuario loggeado</param>
        /// <returns></returns>
        public bool Eliminar(MarcaTercero marcaTercero, int hash)
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

        /// <summary>
        /// Método que verifica si un marcaTercero ya existe en el sistema
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a buscar</param>
        /// <returns>true si lo encontro, false en lo contrario</returns>
        public bool VerificarExistencia(MarcaTercero marcaTercero)
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

        IList<MarcaTercero> IMarcaTerceroServicios.ObtenerMarcaTerceroFiltro(MarcaTercero MarcaTercero)
        {
            IList<MarcaTercero> marcas;

            marcas = ControladorMarcaTercero.ConsultarMarcasTerceroFiltro(MarcaTercero);

            return marcas;
        }

        IList<MarcaTercero> IServicioBase<MarcaTercero>.ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        MarcaTercero IServicioBase<MarcaTercero>.ConsultarPorId(MarcaTercero entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica una Marca Tercera
        /// </summary>
        /// <param name="marca">Marca que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        bool IServicioBase<MarcaTercero>.InsertarOModificar(MarcaTercero entidad, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorMarcaTercero.InsertarOModificar(entidad, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Servicio que elimina una Marca
        /// </summary>
        /// <param name="marca">Marca que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        bool IServicioBase<MarcaTercero>.Eliminar(MarcaTercero entidad, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            throw new NotImplementedException();
       //     bool exitoso = ControladorMarcaTercero.Eliminar(entidad, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        bool IServicioBase<MarcaTercero>.VerificarExistencia(MarcaTercero entidad)
        {
            throw new NotImplementedException();
        }


        IList<Auditoria> IMarcaTerceroServicios.AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            throw new NotImplementedException();
        }

        MarcaTercero IMarcaTerceroServicios.ConsultarMarcaConTodo(MarcaTercero marcaTercero)
        {
            throw new NotImplementedException();
        }

    }
}
