using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMarcaBaseTerceroServicios : IServicioBase<MarcaBaseTercero>
    {
        /// <summary>
        /// Servicio que se encarga de obtener las Marcas Base Tercero segun un filtro
        /// </summary>
        /// <param name="marcaBaseTercero">Marca base tercero a filtrar</param>
        /// <returns>Lista de Marca Base Tercero</returns>
        IList<MarcaBaseTercero> ObtenerMarcaBaseTerceroFiltro(MarcaBaseTercero marcaBaseTercero);


        /// <summary>
        /// Servicio que se encarga de obtener la Auditoria de la Marca Base Tercero
        /// </summary>
        /// <param name="auditoria">Auditoria a buscar</param>
        /// <returns>Lista de auditorias de la Marca Base Tercero</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);


        /// <summary>
        /// Servicio que se encarga de consultar la Marca Base Tercero con todos sus objetos relacionados
        /// </summary>
        /// <param name="marcaTercero">Marca Base Tercero a consultar</param>
        /// <returns>Marca Base Tercero con sus objetos</returns>
        MarcaBaseTercero ConsultarMarcaConTodo(MarcaBaseTercero marcaTercero);
    }
}
