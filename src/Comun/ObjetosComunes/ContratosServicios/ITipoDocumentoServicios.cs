using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ITipoDocumentoServicios: IServicioBase<TipoDocumento>
    {
        /// <summary>
        /// Servicio para obtener los tipos de documentos de Marcas o los tipos de documentos de Patentes
        /// </summary>
        /// <param name="parametro1">Char para indicar Marca o Patente Nacional</param>
        /// <param name="parametro2">Char para indicar Marca o Patente Internacional</param>
        /// <returns></returns>
        IList<TipoDocumento> ObtenerTipoDocumentoMarcaOPatente(String parametro1, String parametro2);
    }
}
