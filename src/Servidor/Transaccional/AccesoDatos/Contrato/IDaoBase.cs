using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoBase<T, Id>
    {
        IList<T> ObtenerTodos();

        T ObtenerPorId(Id id);

        T ObtenerPorIdYBloquear(Id id);

        bool InsertarOModificar(T entidad);

        bool Eliminar(T entidad);

        bool VerificarExistencia(Id id);

    }
}
