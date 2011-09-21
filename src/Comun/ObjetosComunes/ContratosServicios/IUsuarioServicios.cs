using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IUsuarioServicios: IServicioBase<Usuario>
    {
        Usuario Autenticar(Usuario usuario);

        void CerrarSession(int hash);
    }
}
