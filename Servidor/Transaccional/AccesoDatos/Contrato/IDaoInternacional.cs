using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInternacional : IDaoBase<Internacional, int>
    {

        /// <summary>
        /// Metodo con el que se obtiene el objeto Internacional
        /// </summary>
        /// <param name="id">entero con el que se busca el objeto</param>
        /// <returns>Objeto Internacional</returns>
        Internacional ObtenerPorId(int id);
    }
}
