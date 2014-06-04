using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCasoBase : IDaoBase<CasoBase,string>
    {
        /// <summary>
        /// Metodo que obtiene los Casos Base de un Caso
        /// </summary>
        /// <param name="casoBase">Caso Base usado como filtro</param>
        /// <returns>Lista de Casos Base de un Caso</returns>
        IList<CasoBase> ObtenerCasosBaseDeCaso(CasoBase casoBase);
    }
}
