using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMemoriaServicios : IServicioBase<Memoria>
    {
        /// <summary>
        /// Servicio que se encarga de consultar las memorias pertenecientes a una Patente
        /// </summary>
        /// <param name="patente">Patente a buscar</param>
        /// <returns>Memorias pertenecientes a la patente</returns>
        IList<Memoria> ConsultarMemoriasPorPatente(Patente patente);


        /// <summary>
        /// Servicio que se encarga de verificar la existencia de una memoria
        /// </summary>
        /// <param name="patente">Patente propietaria de la memoria</param>
        /// <param name="memoria">memoria a buscar</param>
        /// <returns>true en caso de existir, false en caso contrario</returns>
        bool VerificarExistenciaMemoria(Patente patente, Memoria memoria);
    }
}
