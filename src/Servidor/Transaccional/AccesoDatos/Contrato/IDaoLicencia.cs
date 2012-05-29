using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoLicencia : IDaoBase<Licencia, int>
    {

        /// <summary>
        /// Metodo que consulta las licencias dado unos parametros
        /// </summary>
        /// <param name="licencia">lecencia con parametros</param>
        /// <returns>Lista de Licencias con datos solicitados</returns>
        IList<Licencia> ObtenerLicenciasFiltro(Licencia licencia);
    }
}
