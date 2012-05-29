using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFusionPatente : IDaoBase<FusionPatente, int>
    {

        /// <summary>
        /// Metodo que consulta las fusionePatente dado unos parametros
        /// </summary>
        /// <param name="FusionPatente">FusionPatente con parametros</param>
        /// <returns>Lista de FusionesPatente solicitados</returns>
        IList<FusionPatente> ObtenerFusionesPatenteFiltro(FusionPatente FusionPatente);
    }
}
