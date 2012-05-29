using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IRolServicios: IServicioBase<Rol>
    {

        /// <summary>
        /// Servicio que consulta los roles y objetos relacionados con los mismos
        /// </summary>
        /// <returns>Lista de Roles</returns>
        IList<Rol> ConsultarRolesYObjetos();
    }
}
