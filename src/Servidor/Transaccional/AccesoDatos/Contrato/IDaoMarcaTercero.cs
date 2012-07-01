using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMarcaTercero : IDaoBase<MarcaTercero, int>
    {

        /// <summary>
        /// Metodo que consulta las MarcaTercero dado unos parametros
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero </param>
        /// <returns>Todas las MarcaTercero solicitados</returns>
        IList<MarcaTercero> ObtenerMarcaTerceroFiltro(MarcaTercero marcaTercero);


        /// <summary>
        /// Metodo que obtiene el ultimo id de una marca tercero
        /// </summary>
        /// <param name="maxId">id de la marcatercero</param>
        /// <returns>El id a utilizar</returns>
        string ObtenerMaxId(string maxId);


        /// <summary>
        /// Metodo queobtiene el ultimo anexo de la MarcaTercero
        /// </summary>
        /// <param name="maxAnexo">El ultimo Anexo</param>
        /// <returns>El ultimo anexo de la marcatercero</returns>
        int ObtenerMaxAnexo(string maxAnexo);

        /// <summary>
        /// Metodo verifica si existe una marca a tercero con esa clase internacional
        /// </summary>
        /// <param name="claseInt">El ultimo claseInt</param>
        /// <returns>El ultimo claseInt de la marcatercero</returns>
        bool ObtenerClaseInternacionalMarcaTercero(int claseInt, string marcaT,int anexo);
   
    }
}
