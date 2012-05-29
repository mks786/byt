using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInteresado : IDaoBase<Interesado, int>
    {

        /// <summary>
        /// metodo que consulta todos los datos de un interesado
        /// </summary>
        /// <param name="interesado">Interesado con Parametros</param>
        /// <returns>Interesado con toda la informacion</returns>
        Interesado ObtenerInteresadoConTodo(Interesado interesado);


        /// <summary>
        /// Metodo que consulta todos los interesados dado unos parametros
        /// </summary>
        /// <param name="interesado">Interesado con parametros</param>
        /// <returns>Lista de Interesado solicitados</returns>
        IList<Interesado> ObtenerInteresadosFiltro(Interesado interesado);


        /// <summary>
        /// Metodo que obtiene el interesado de un poder
        /// </summary>
        /// <param name="poder">Poder solicitado</param>
        /// <returns>Interesado que tiene ese poder</returns>
        Interesado ObtenerInteresadosDeUnPoder(Poder poder);
    }
}
