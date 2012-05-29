using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeNombrePatente : IDaoBase<CambioDeNombrePatente, int>
    {


        /// <summary>
        /// metodo que consulta los CambioDeNombrePatente dado unos parametros
        /// </summary>
        /// <param name="CambioDeNombre">CambioDeNombrePatente  con parametros</param>
        /// <returns>Lista de CambioDeNombresPatente </returns>
        IList<CambioDeNombrePatente> ObtenerCambiosDeNombrePatenteFiltro(CambioDeNombrePatente CambioDeNombre);
    }
}
