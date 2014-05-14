using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPresentacionSapiDetalle : IDaoBase<PresentacionSapiDetalle,int>
    {
        /// <summary>
        /// Metodo que obtiene Solicitudes de Presentacion Sapi por filtro
        /// </summary>
        /// <param name="presentacionSapiDetalle">Presentacion filtro</param>
        /// <returns>Lista de Solicitudes de Presentacion Sapi resultantes de la consulta</returns>
        IList<PresentacionSapiDetalle> ObtenerPresentacionesSapiDetalleFiltro(PresentacionSapiDetalle presentacionSapiDetalle);
    }
}
