using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeNombrePatente : IDaoBase<CambioDeNombrePatente, int>
    {
        IList<CambioDeNombrePatente> ObtenerCambiosDeNombrePatenteFiltro(CambioDeNombrePatente CambioDeNombre);
    }
}
