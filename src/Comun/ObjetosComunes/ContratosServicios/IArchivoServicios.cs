using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IArchivoServicios : IServicioBase<Archivo>
    {
        /// <summary>
        /// Servicio para obtener el archivo de una marca internacional
        /// </summary>
        /// <param name="parametro1">Char para indicar Marca o Patente Nacional</param>
        /// <param name="parametro2">Char para indicar Marca o Patente Internacional</param>
        /// <returns></returns>
        Archivo ObtenerArchivoDeMarcaOPatenteInternacional(Archivo archivo);
    }
}
