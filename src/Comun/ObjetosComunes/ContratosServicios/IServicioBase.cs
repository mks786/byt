using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IServicioBase<Entidad>
    {
        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        IList<Entidad> ConsultarTodos();


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Entidad ConsultarPorId(Entidad entidad);

        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Servicio que consulta una entidad por un campo especifico y se 
        /// indica el tipo de ordenamiento
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="tipoOrdenamiento"></param>
        /// <returns></returns>
        IList<Entidad> ConsultarPorOtroCampo(String campoEntidad, String tipoOrdenamiento);

        //-----------------------------------------------------------------------------------


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        bool InsertarOModificar(Entidad entidad, int hash);


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        bool Eliminar(Entidad entidad, int hash);


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        bool VerificarExistencia(Entidad entidad);
    }
}
