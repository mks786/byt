using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IServicioBase<Entidad>
    {
        IList<Entidad> ConsultarTodos();

        Entidad ConsultarPorId(Entidad entidad);

        bool InsertarOModificar(Entidad entidad, int hash);

        bool Eliminar(Entidad entidad, int hash);

        bool VerificarExistencia(Entidad entidad);
    }
}
