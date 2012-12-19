using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeDomicilioPatente : IDaoBase<CambioDeDomicilioPatente, int>
    {

        /// <summary>
        /// Metodo que consulta los Cambios de Domicilios Patentes dado unos parametros
        /// </summary>
        /// <param name="CambioDeDomicilio">CambioDeDomicilioPatentes con parametros</param>
        /// <returns>una lista de CambioDeDomicilioPatentes</returns>
        IList<CambioDeDomicilioPatente> ObtenerCambiosDeDomicilioPatenteFiltro(CambioDeDomicilioPatente CambioDeDomicilio);
    }
}
