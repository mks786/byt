using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoBase<T, Id>
    {
        /// <summary>
        /// Obtiene todos los objetos de cualquier entidad Generica
        /// </summary>
        /// <returns>Una lista Generica del Objeto solicitado</returns>
        IList<T> ObtenerTodos();


        /// <summary>
        /// Método que obtiene todos los registros de la entidad ordenada en el orden especificado
        /// </summary>
        /// <param name="parametro">Columna por la que se desea ordenar</param>
        /// <param name="orden">"Asc" para ascendiente, "Desc" para descendiente</param>
        /// <returns>La Lista ordenada en lo especificado</returns>
        IList<T> ObtenerTodos(string parametro, string orden);


        /// <summary>
        /// Obtiene un objeto generico dado un id
        /// </summary>
        /// <param name="id">id del objeto que se solicita</param>
        /// <returns>Id del Objeto</returns>
        T ObtenerPorId(Id id);


        /// <summary>
        /// Método que obtiene el elemento de la entidad por su id y 
        /// lo bloquea hasta que sea actualizado
        /// </summary>
        /// <param name="id">Id de la entidad a requerida</param>
        /// <returns>Entidad requerida</returns>
        T ObtenerPorIdYBloquear(Id id);


        /// <summary>
        /// Método que inserta o modifica una entidad
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>True si fue éxitoso la inserción o modificación, en caso contrario False</returns>
        bool InsertarOModificar(T entidad);


        /// <summary>
        /// Elimina la Entidad que se envio
        /// </summary>
        /// <param name="entidad">Elimina la entidad</param>
        /// <returns>True si se elimino, de lo contrario false</returns>
        bool Eliminar(T entidad);


        /// <summary>
        /// Método que verifica si existe una entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        bool VerificarExistencia(Id id);

    }
}
