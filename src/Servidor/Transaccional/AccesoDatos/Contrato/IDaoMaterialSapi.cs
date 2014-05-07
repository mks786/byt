using System;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMaterialSapi : IDaoBase<MaterialSapi,string>
    {
        /// <summary>
        /// Metodo que obtiene una lista de materiales sapi a traves de un filtro determinado
        /// </summary>
        /// <param name="material">Material Sapi usado como filtro</param>
        /// <returns>Lista de Materiales Sapi que cumplen con los filtros dados</returns>
        IList<MaterialSapi> ObtenerMaterialSapiFiltro(MaterialSapi material);
    }
}
