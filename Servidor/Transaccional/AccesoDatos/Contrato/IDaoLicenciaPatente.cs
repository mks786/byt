using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoLicenciaPatente : IDaoBase<LicenciaPatente, int>
    {

        /// <summary>
        /// Metodo que consulta las LicenciaPatentes dado unos parametros
        /// </summary>
        /// <param name="licencia">LicenciaPatentes con parametros</param>
        /// <returns>Lista de LicenciaPatentes con datos solicitados</returns>
        IList<LicenciaPatente> ObtenerLicenciasPatenteFiltro(LicenciaPatente licencia);
    }
}
