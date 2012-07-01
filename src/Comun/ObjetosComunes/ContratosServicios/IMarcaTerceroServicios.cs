using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMarcaTerceroServicios : IServicioBase<MarcaTercero>
    {
        /// <summary>
        /// Servicio que se encarga de obtener la Marca Tercero basada en un filtro
        /// </summary>
        /// <param name="marcaTercero">Marca tercero filtro</param>
        /// <returns>Lista de marcas tercero que cumplan con el filtro</returns>
        IList<MarcaTercero> ObtenerMarcaTerceroFiltro(MarcaTercero marcaTercero);

        
        /// <summary>
        /// Servicio encargado de consultar la auditoria de Marca Tercero
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de Auditorias</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
        
        
        /// <summary>
        /// Servicio encargado de consultar una marca tercero con todos sus objetos
        /// </summary>
        /// <param name="marcaTercero">Marca Tercero a consultar</param>
        /// <returns>Marca tercero consultada con todos los objetos</returns>
        MarcaTercero ConsultarMarcaConTodo(MarcaTercero marcaTercero);

        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        string InsertarOModificarMarcaTercero(MarcaTercero entidad, int hash);
    }
}
