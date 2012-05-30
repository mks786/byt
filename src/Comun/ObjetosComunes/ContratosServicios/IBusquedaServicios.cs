using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IBusquedaServicios : IServicioBase<Busqueda>
    {
        /// <summary>
        /// Servicio que se encarga de consultar las búsquedas pertenecientes a una marca
        /// </summary>
        /// <param name="marca">Marca a consultar las búsquedas</param>
        /// <returns>Lista de búsquedas de la marca</returns>
        IList<Busqueda> ConsultarBusquedasPorMarca(Marca marca);
    }
}
