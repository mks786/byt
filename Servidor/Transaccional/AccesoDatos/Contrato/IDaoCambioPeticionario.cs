using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioPeticionario : IDaoBase<CambioPeticionario, int>
    {

        /// <summary>
        /// Metodo que Consulta los CambiosDePeticionarios dado unos parametros
        /// </summary>
        /// <param name="CambioPeticionario">CambioDePeticionario con parametros</param>
        /// <returns>Lista de CambioDePeticionarios</returns>
        IList<CambioPeticionario> ObtenerCambiosPeticionarioFiltro(CambioPeticionario CambioPeticionario);
    }
}
