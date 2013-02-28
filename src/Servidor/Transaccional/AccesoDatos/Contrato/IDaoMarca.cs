using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMarca : IDaoBase<Marca, int>
    {

        /// <summary>
        /// Metodo que consulta las marcas dado unos parametros
        /// </summary>
        /// <param name="marca">marca con parametros</param>
        /// <returns>Lista de marcas solicitadas</returns>
        IList<Marca> ObtenerMarcasFiltro(Marca marca);


        /// <summary>
        /// Metodo que Obtiene la marca con todos sus datos
        /// </summary>
        /// <param name="marca">marca</param>
        /// <returns>Marca con toda su informacion</returns>
        Marca ObtenerMarcaConTodo(Marca marca);

        /// <summary>
        /// Metodo que obtine las marcas dada una fecha de renovacion
        /// </summary>
        /// <param name="marca">marca con parametros</param>
        /// <param name="fechas">fecha como parametro</param>
        /// <returns>la lista de marcas con esa fecha de renovacion</returns>
        IList<Marca> ObtenerMarcasPorFechaRenovacion(Marca marca, DateTime[] fechas);

        /// <summary>
        /// Método que obtiene los recordatorios de marcas
        /// </summary>
        /// <param name="recordatorio">recordatorio a filtrar</param>
        /// /// <param name="fechas">fechas de renovación de marca a filtrar</param>
        /// <returns>lista de recordatorios filtrados</returns>
        IList<RecordatorioVista> ObtenerRecordatoriosVista(RecordatorioVista recordatorio, DateTime[] fechas, string localidad);


        /// <summary>
        /// Método que obtiene los recordatorios de marcas filtro no automático
        /// </summary>
        /// <param name="RecordatorioVista">recordatorio a consultar</param>
        /// <param name="ano">Ano de fecha renovacion a filtrar</param>
        /// <param name="mes">mes de fecha renovación a filtrar</param>
        /// <param name="fechas">fecha desde y hasta de renovación a filtrar</param>
        /// <returns>Lista de marcas para recordatorio filtradas</returns>
        IList<RecordatorioVista> ObtenerRecordatoriosVistaNoAutomatico(RecordatorioVista recordatorio, string ano, string mes, DateTime?[] fechas, string localidad);

    }
}
