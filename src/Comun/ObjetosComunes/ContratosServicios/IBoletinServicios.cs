using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IBoletinServicios: IServicioBase<Boletin>
    {
        /// <summary>
        /// Servicio que se encarga de consultar las resoluciones pertenecientes aun boletin
        /// </summary>
        /// <param name="boletin">Boletin a consultar las resoluciones</param>
        /// <returns>Lista de Resoluciones del boletin</returns>
        IList<Resolucion> ConsultarResolucionesDeBoletin(Boletin boletin);
    }
}
