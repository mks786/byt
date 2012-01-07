using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using NLog;
using System;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static IList<Usuario> _usuarios = new List<Usuario>();

        /// <summary>
        /// Método que crea la sesión del cliente
        /// </summary>
        /// <param name="usuario">Usuario autenticado</param>
        /// <returns>Usuario autenticado con el hash</returns>
        public static Usuario CrearSesion(Usuario usuario)
        {
            bool agregar = true;
            usuario.Hash = usuario.GetHashCode();

            foreach (Usuario usu in _usuarios)
            {
                if (usu.Id == usuario.Id)
                    agregar = false;
            }

            if (agregar)
                _usuarios.Add(usuario);

            System.Console.WriteLine("Usuario agregado: " + usuario.Hash);
            return usuario;
        }

        /// <summary>
        /// Método que obtiene el usuario autenticado por su hash
        /// </summary>
        /// <param name="hash">Hash del usuario autenticado</param>
        /// <returns>El usuario autenticado</returns>
        public static Usuario ObtenerUsuarioPorHash(int hash)
        {
            Usuario retorno = null;

            foreach (Usuario usuario in _usuarios)
                if (hash == usuario.Hash)
                {
                    retorno = usuario;
                    break;
                }

            return retorno;
        }

        /// <summary>
        /// Método que cierra la sesion del cliente
        /// </summary>
        /// <param name="hash"></param>
        public static void CerrarSesion(int hash)
        {
            foreach (Usuario usuario in _usuarios)
                if (hash == usuario.Hash)
                {
                    _usuarios.Remove(usuario);

                    System.Console.WriteLine("Usuario eliminado: " + usuario.Hash);
                    break;
                }
        }
        
        /// <summary>
        /// Método que devuleve la lista de auditorias que presenta una entidad
        /// </summary>
        /// <param name="auditoria">Auditoria que tiene los parametros para filtrar</param>
        /// <returns>Lista de todas las auditorias que presenta una entidad</returns>
        public static IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            IList<Auditoria> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Auditoria>> comando = FabricaComandosAuditoria.ObtenerComandoAuditoriaPorFkyTabla(auditoria);
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
