using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoContacto : IDaoBase<Contacto, int>
    {

        /// <summary>
        /// Metodo que Consulta los Contactos que tiene un Asociado
        /// </summary>
        /// <param name="asociado">Asociado</param>
        /// <returns>Lista de Contactos del asociado solicitado</returns>
        IList<Contacto> ObtenerContactosPorAsociado(Asociado asociado);


        Contacto ConsultarContactoPorId(Contacto contacto);


        IList<Contacto> ObtenerContactosFiltro(Contacto contacto);
    }
}
