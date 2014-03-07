using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoContactoCxP : IDaoBase<ContactoCxP, int>
    {
        /// <summary>
        /// Metodo que obtiene una lista de ContactosCxP de acuerdo a un filtro dado
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP usado como filtro</param>
        /// <returns>Lista de Contactos CxP encontrados</returns>
        IList<ContactoCxP> ObtenerContactosCxPFiltro(ContactoCxP contactoCxP);
    }
}
