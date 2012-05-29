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

    }
}
