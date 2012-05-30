using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IContactoServicios : IServicioBase<Contacto>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los Contactos de un asociado
        /// </summary>
        /// <param name="asociado">Asociado a consultar contactos</param>
        /// <returns>Lista de contactos del asociado</returns>
        IList<Contacto> ConsultarContactosPorAsociado(Asociado asociado);
    }
}
