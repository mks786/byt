using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IContactoCxPServicios : IServicioBase<ContactoCxP>
    {
        /// <summary>
        /// Servicio que obtiene una lista de ContactosCxP de acuerdo a un filtro dado
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP usado como filtro</param>
        /// <returns>Lista de Contactos CxP encontrados</returns>
        IList<ContactoCxP> ConsultarContactoCxPFiltro(ContactoCxP contactoCxP);
    }
}
