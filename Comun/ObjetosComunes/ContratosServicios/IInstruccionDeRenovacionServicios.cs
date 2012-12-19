using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInstruccionDeRenovacionServicios : IServicioBase<InstruccionDeRenovacion>
    {
        /// <summary>
        /// Servicio que se encarga de consultar las InstruccionesRenovacion pertenecientes a una marca
        /// </summary>
        /// <param name="marca">Marca a consultar las InstruccionesRenovacion</param>
        /// <returns>Lista de InstruccionesRenovacion de la marca</returns>
        IList<InstruccionDeRenovacion> ConsultarInstruccionesDeRenovacionPorMarca(Marca marca);
    }
}
