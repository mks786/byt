using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAsociado : IDaoBase<Asociado, int>
    {

        /// <summary>
        /// Consulta un Asociado coon toda su informacion
        /// </summary>
        /// <param name="usuario">Asociado con parametros</param>
        /// <returns>Asociado </returns>
        Asociado ObtenerAsociadoConTodo(Asociado usuario);


        /// <summary>
        /// Consulta los asociados dado unos parametros determinados
        /// </summary>
        /// <param name="asociado">asociado con parametros</param>
        /// <returns>lista de asociados</returns>
        IList<Asociado> ObtenerAsociadosFiltro(Asociado asociado);


        /// <summary>
        /// Consulta si un asociado tiene cartas
        /// </summary>
        /// <param name="asociado">asociado a buscar cartas</param>
        /// <returns>true en caso de tener cartas, false en caso contrario</returns>
        bool VerificarCartasDeAsociado(Asociado asociado);

    }
}
