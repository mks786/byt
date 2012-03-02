using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeNombre : IDaoBase<CambioDeNombre, int>
    {
        IList<CambioDeNombre> ObtenerCambiosDeNombreFiltro(CambioDeNombre CambioDeNombre);
    }
}
