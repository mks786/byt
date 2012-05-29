using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMarcaBaseTercero : IDaoBase<MarcaBaseTercero, int>
    {

        /// <summary>
        /// Metodo que consulta las MarcaBaseTercero dado unos parametros
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero </param>
        /// <returns>Todas las MarcaBaseTercero solicitados</returns>
        IList<MarcaBaseTercero> ObtenerMarcaBaseTerceroFiltro(MarcaBaseTercero marcaBaseTercero);


        /// <summary>
        /// metodo que obtiene el ultimo insert de la secuencia insertada
        /// </summary>
        /// <returns>El numero mayor en la base de datos</returns>
        int ObtenerMaxSecuencia();


        /// <summary>
        /// Metodo que consulta las MarcaBaseTercero de una MarcaTercero
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero con los Datos de MarcaTercero</param>
        /// <returns>Todas las MarcaBaseTercero de la MarcaTercero</returns>
        List<MarcaBaseTercero> ObtenerTodosPorId(MarcaBaseTercero marcaBaseTercero);

    
    }
}
