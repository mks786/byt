using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoBase<T, Id>
    {
        IList<T> ObtenerTodos();

        IList<T> ObtenerTodos(string parametro, string orden);

        T ObtenerPorId(Id id);

        T ObtenerPorIdYBloquear(Id id);

        bool InsertarOModificar(T entidad);


        /// <summary>
        /// mrdsfsdfsdfsdfsdfds
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        bool Eliminar(T entidad);

        bool VerificarExistencia(Id id);

    }
}
