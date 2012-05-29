using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoOperacion : IDaoBase<Operacion, int>
    {

        /// <summary>
        /// Metodo que consulta las operaciones por una marca
        /// </summary>
        /// <param name="marca">Marca</param>
        /// <returns>Lista de operaciones solicitadas</returns>
        IList<Operacion> ObtenerOperacionesPorMarca(Marca marca);


        /// <summary>
        /// Metodo que consulta las operaciones por una patente
        /// </summary>
        /// <param name="patente">Patente</param>
        /// <returns>Lista de operaciones solicitadas</returns>
        IList<Operacion> ObtenerOperacionesPorPatente(Patente patente);


        /// <summary>
        /// Metodo que obtiene las Marcas y servicios de esa operacion
        /// </summary>
        /// <param name="operacion">Operacion ha solicitar</param>
        /// <returns>Lista de operaciones solicitadas</returns>
        IList<Operacion> ObtenerOperacionesPorMarcaYServicio(Operacion operacion);


        /// <summary>
        /// Metodo que consulta las operaciones por parametros
        /// </summary>
        /// <param name="operacion">Operacion con parameteros</param>
        /// <returns>Lista de operaciones solicitaos</returns>
        IList<Operacion> ObtenerOperacionesFiltro(Operacion operacion);
    }
}
