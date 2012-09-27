using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFusion : IDaoBase<Fusion, int>
    {

        /// <summary>
        /// Metodo que consulta las fusiones dado unos parametros
        /// </summary>
        /// <param name="Fusion">Fusion con parametros</param>
        /// <returns>Lista de Fusiones solicitados</returns>
        IList<Fusion> ObtenerFusionesFiltro(Fusion Fusion);
    }
}
