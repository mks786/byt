using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAnualidadServicios : IServicioBase<Anualidad>
    {
        /// <summary>
        /// Servicio que se encarga de consultar las anualidades basadas en el filtro
        /// </summary>
        /// <param name="Anualidad">Anualidad filtro</param>
        /// <returns>Lista de anualidades que cumplen con el filtro</returns>
        IList<Anualidad> ObtenerAnualidadesFiltro(Anualidad Anualidad);


        /// <summary>
        /// Servicio que se encarga de consultar el ultimo ID de las anualidades
        /// </summary>
        /// <returns>Ultimo Id de las anualidades</returns>
        int ConsultarUltimoIdAnualidad();


        /// <summary>
        /// Servicio que se encarga de consultar una anualidad con todos sus objetos
        /// </summary>
        /// <param name="Anualidad">Anualidad a consultar</param>
        /// <returns>Anualidad con todos sus objetos</returns>
        Anualidad ConsultarAnualidadConTodo(Anualidad Anualidad);


        /// <summary>
        /// Servicio que se encarga de insertar una nueva anualidad
        /// </summary>
        /// <param name="Patente">Patente a insertar con sus anualidades</param>
        /// <param name="hash">hash del usuario</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        bool InsertarOModificarAnualidad(Patente Patente, int hash);



        /// <summary>
        /// Servicio que se encarga de consultar las anualidades pertenecientes a una patente
        /// </summary>
        /// <param name="Patente">Patente a consultar las anualidades</param>
        /// <returns>Lista de anualidades de la patente</returns>
        IList<Anualidad> ConsultarAnualidadesPorPatente(Patente Patente);
    }
}
