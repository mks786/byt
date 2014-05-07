using System;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMaterialSapiServicios: IServicioBase<MaterialSapi>
    {
        /// <summary>
        /// Servicio que obtiene una lista de Materiales Sapi a traves de un filtro determinado
        /// </summary>
        /// <param name="material">Material Sapi usado como filtro</param>
        /// <returns>Lista de Materiales Sapi que cumplan con los filtros seleccionados</returns>
        IList<MaterialSapi> ObtenerMaterialSapiFiltro(MaterialSapi material);
    }
}
