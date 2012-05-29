using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoRol : IDaoBase<Rol, string>
    {

        /// <summary>
        /// Método que obtiene los roles y los objetos asociados por los mismos
        /// </summary>
        /// <returns>Lista de Roles con sus objetos</returns>
        IList<Rol> ObtenerRolesYObjetos();
    }
}
