using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioNombre : IDaoBase<CambioNombre, int>
    {
        IList<CambioNombre> ObtenerCambiosDeNombreFiltro(CambioNombre CambioDeNombre);
    }
}
