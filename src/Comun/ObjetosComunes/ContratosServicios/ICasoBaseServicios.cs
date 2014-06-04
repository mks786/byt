using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICasoBaseServicios : IServicioBase<CasoBase>
    {
        /// <summary>
        /// Servicio que obtiene los Casos Base de un Caso en particular
        /// </summary>
        /// <param name="casoBase">Caso Base filtro</param>
        /// <returns>Lista de Casos Base un caso en particular</returns>
        IList<CasoBase> ObtenerCasosBasePorCaso(CasoBase casoBase);
    }
}
