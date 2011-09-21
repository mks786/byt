using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosRol;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosRol
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Rol
        /// </summary>
        /// <param name="rol">Rol a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el rol en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Rol rol)
        {
            return new ComandoInsertarOModificarRol(rol);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un Rol
        /// </summary>
        /// <param name="rol">Rol a eliminar en la base de datos</param>
        /// <returns>El Comando que permite elimnar el rol en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarRol(Rol rol)
        {
            return new ComandoElimarRol(rol);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los roles
        /// </summary>
        /// <returns>El Comando para consultar todos los Roles</returns>
        public static ComandoBase<IList<Rol>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosRoles();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Rol por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Rol> ObtenerComandoConsultarPorID(Rol rol)
        {
            return new ComandoConsultarRolPorId(rol);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar los roles con los agentes
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Rol>> ObtenerComandoConsultarRolesYObjetos()
        {
            return new ComandoConsultarRolesYObjetos();
        }
    }
}
