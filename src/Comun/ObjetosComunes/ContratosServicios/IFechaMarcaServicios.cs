using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IFechaMarcaServicios: IServicioBase<FechaMarca>
    {
        /// <summary>
        /// Servicio que obtiene todas las fechas de una Marca para sus Certificados
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <returns>Lista de Fechas de una Marca</returns>
        IList<FechaMarca> ConsultarFechasPorMarca(Marca marca);
    }
}
