using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoContacto : IDaoBase<Contacto, int>
    {
        IList<Contacto> ObtenerContactosPorAsociado(Asociado asociado);
    }
}
