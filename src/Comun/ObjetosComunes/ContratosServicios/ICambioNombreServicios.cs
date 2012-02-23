using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioNombreServicios : IServicioBase<CambioNombre>
    {
        IList<CambioNombre> ObtenerCambioNombreFiltro(CambioNombre CambioNombre);
    }
}
