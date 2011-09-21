using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoRol : IDaoBase<Rol, string>
    {
        IList<Rol> ObteneRolesYObjetos();
    }
}
