using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMarcaServicios : IServicioBase<Marca>
    {
        /// <summary>
        /// Servicio que se encarga de buscar marcas que cumplan con un filtro
        /// </summary>
        /// <param name="Marca">marca modelo para filtrar</param>
        /// <returns>Lista de marcas que cumplan con el filtro</returns>
        IList<Marca> ObtenerMarcasFiltro(Marca Marca);


        /// <summary>
        /// Servicio que se encarga de obtener las fechas de renovacion de una marca
        /// </summary>
        /// <param name="Marca">Marca a buscar</param>
        /// <param name="fechas">fechas de renovacion de la marca</param>
        /// <returns>Lista de marcas por fecha de renovacion</returns>
        IList<Marca> ObtenerMarcasPorFechaRenovacion(Marca Marca, DateTime[] fechas);


        /// <summary>
        /// Servicio que se encarga de consultar la auditoria de Marcas
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditoria de la marca</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);


        /// <summary>
        /// Servicio que se encarga de consultar una marca con todos sus objetos
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <returns>Marca con todos los objetos</returns>
        Marca ConsultarMarcaConTodo(Marca marca);


        /// <summary>
        /// Servicio que se encarga de insertar la marca
        /// </summary>
        /// <param name="marca">Marca a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la Marca insertada</returns>
        int? InsertarOModificarMarca(Marca marca, int hash);


        /// <summary>
        /// Servicio que se encarga de obtener los recordatorios
        /// </summary>
        /// <param name="RecordatorioVista">Recordatorio con parámetros a filtrar</param>
        /// <param name="fechas">fechas de renovacion marca a filtrar</param>
        /// <returns>Lista de marcas por fecha de renovacion</returns>
        IList<RecordatorioVista> ConsultarRecordatoriosVistaMarca(RecordatorioVista recordatorio, DateTime[] fechas);
    }
}
