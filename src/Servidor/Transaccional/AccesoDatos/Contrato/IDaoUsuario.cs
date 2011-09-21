using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoUsuario : IDaoBase<Usuario, string>
    {
        Usuario Autenticar(Usuario usuario);
    }
}
