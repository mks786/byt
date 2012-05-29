using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMemoria : IDaoBase<Memoria, int>
    {

        /// <summary>
        /// Metodo que obtiene las memorias de una patente
        /// </summary>
        /// <param name="patente">Patente solicitada</param>
        /// <returns>Lista de Memorias  por una patente</returns>
        IList<Memoria> ObtenerMemoriasPorPatente(Patente patente);
    }
}
