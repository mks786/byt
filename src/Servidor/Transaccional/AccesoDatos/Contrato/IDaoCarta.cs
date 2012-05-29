using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCarta : IDaoBase<Carta, int>
    {

        /// <summary>
        /// Metodo que consulta las cartas dado unos parametros
        /// </summary>
        /// <param name="carta">Carta con parametros solicitados</param>
        /// <returns>lista de cartas que cumplen los parametros</returns>
        IList<Carta> ObtenerCartasFiltro(Carta carta);


        /// <summary>
        /// Metodo que inserta una carata en la base de datos
        /// </summary>
        /// <param name="carta">parametro a insertar</param>
        /// <param name="transaccion">objetoITransicion indica si se realizo el Commit</param>
        /// <returns>Bool si se inserto correctamente, de lo contrario false</returns>
        bool Insertar(Carta carta, ITransaction transaccion);
    }
}
