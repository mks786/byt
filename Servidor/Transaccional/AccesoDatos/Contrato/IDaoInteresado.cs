using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInteresado : IDaoBase<Interesado, int>
    {

        /// <summary>
        /// Método que autentica un usuario
        /// </summary>
        /// <param name="interesado">usuario a autenticar</param>
        /// <returns>Usuario autenticado</returns>
        Interesado ObtenerInteresadoConTodo(Interesado interesado);


        /// <summary>
        /// Método que obtiene un interesado con uno o mas filtros
        /// </summary>
        /// <param name="interesado">filtros de interesado</param>
        /// <returns>interesado filtrado</returns>
        IList<Interesado> ObtenerInteresadosFiltro(Interesado interesado);


        /// <summary>
        /// Metodo que obtiene el interesado de un poder
        /// </summary>
        /// <param name="poder">Poder solicitado</param>
        /// <returns>Interesado que tiene ese poder</returns>
        Interesado ObtenerInteresadosDeUnPoder(Poder poder);
    }
}
