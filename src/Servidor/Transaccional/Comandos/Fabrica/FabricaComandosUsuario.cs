using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosUsuario;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosUsuario
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los usuarios
        /// </summary>
        /// <returns>El Comando para consultar todos los Usuarios</returns>
        public static ComandoBase<IList<Usuario>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosUsuarios();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Usuario por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Usuario> ObtenerComandoConsultarPorID(Usuario usuario)
        {
            return new ComandoConsultarUsuarioPorID(usuario);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar un Usuario
        /// </summary>
        /// <param name="usuario">Usuario a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Usuario usuario)
        {
            return new ComandoInsertarOModificarUsuario(usuario);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un usuario
        /// </summary>
        /// <param name="usuario">Usuario que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarUsuario(Usuario usuario)
        {
            return new ComandoEliminarUsuario(usuario);
        }

        /// <summary>
        /// Método que devuelve el Comando para autentificar por usuario
        /// </summary>
        /// <param name="usuario">Usuario que se va a autentificar</param>
        /// <returns>Comando para realizar la autentificacion</returns>
        public static ComandoBase<Usuario> ObtenerComandoAutenticarUsuario(Usuario usuario)
        {
            return new ComandoAutentificarUsuario(usuario);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="usuario">Usuario a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaUsuario(Usuario usuario)
        {
            return new ComandoVerificarExistenciaUsuario(usuario);
        }
    }
    
}
