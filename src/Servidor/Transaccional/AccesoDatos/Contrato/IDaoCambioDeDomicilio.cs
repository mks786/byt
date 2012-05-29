using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeDomicilio : IDaoBase<CambioDeDomicilio, int>
    {

        /// <summary>
        /// Metodo que consulta los Cambios de Domicilios dado unos parametros
        /// </summary>
        /// <param name="CambioDeDomicilio">CambioDeDomicilio con parametros</param>
        /// <returns>una lista de CambioDeDomicilio</returns>
        IList<CambioDeDomicilio> ObtenerCambiosDeDomicilioFiltro(CambioDeDomicilio CambioDeDomicilio);
    }
}
