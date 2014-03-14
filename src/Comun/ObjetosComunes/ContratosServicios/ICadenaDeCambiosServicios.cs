using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICadenaDeCambiosServicios : IServicioBase<CadenaDeCambios>
    {
        /// <summary>
        /// Servicio que obtiene un grupo de cadena de cambios dependiendo de un filtro dado
        /// </summary>
        /// <param name="cadenaCambiosFiltro">Cadena de Cambios usada como filtro</param>
        /// <returns>Lista de Cadenas de Cambios resultantes</returns>
        IList<CadenaDeCambios> ObtenerCadenasCambioFiltro(CadenaDeCambios cadenaCambiosFiltro);
    }
}
