using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoTipoDocumento: IDaoBase<TipoDocumento, string>
    {
        /// <summary>
        /// Metodo para obtener los tipos de documentos ya sea por Marca o por Patente
        /// </summary>
        /// <param name="paramtroNacional">Marca o Patente Nacional</param>
        /// <param name="parametroInternacional">Marca o Patente Internacional</param>
        /// <returns>Lista de Tipos de Documentos por Marca o Por Patente</returns>
        IList<TipoDocumento> ObtenerTiposDocumentosMarcaOPatente(string paramtroNacional,string parametroInternacional);
    }
}
