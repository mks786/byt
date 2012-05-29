using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioPeticionarioPatente : IDaoBase<CambioPeticionarioPatente, int>
    {

        /// <summary>
        /// Metodo que Consulta los CambiosDePeticionarioPatente dado unos parametros
        /// </summary>
        /// <param name="CambioPeticionario">CambioDePeticionarioPatente con parametros</param>
        /// <returns>Lista de CambioDePeticionariosPatente</returns>
        IList<CambioPeticionarioPatente> ObtenerCambiosPeticionarioPatenteFiltro(CambioPeticionarioPatente CambioPeticionario);
    }
}
