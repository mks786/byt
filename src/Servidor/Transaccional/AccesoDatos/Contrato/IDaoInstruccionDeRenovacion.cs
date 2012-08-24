using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInstruccionDeRenovacion : IDaoBase<InstruccionDeRenovacion, int>
    {

        /// <summary>
        /// metodo que consulta las busquedas que tiene una marca
        /// </summary>
        /// <param name="marca">Marca a consultar las busquedas</param>
        /// <returns>Lista de busquedas de la marca solicitada</returns>
        IList<InstruccionDeRenovacion> ObtenerInstruccionesDeRenovacionPorMarca(Marca marca);
    }
}
