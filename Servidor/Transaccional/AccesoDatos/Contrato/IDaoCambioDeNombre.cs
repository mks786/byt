using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeNombre : IDaoBase<CambioDeNombre, int>
    {

        /// <summary>
        /// metodo que consulta los CambioDeNombre dado unos parametros
        /// </summary>
        /// <param name="CambioDeNombre">CambioDeNombre con parametros</param>
        /// <returns>Lista de CambioDeNombres</returns>
        IList<CambioDeNombre> ObtenerCambiosDeNombreFiltro(CambioDeNombre CambioDeNombre);
    }
}
