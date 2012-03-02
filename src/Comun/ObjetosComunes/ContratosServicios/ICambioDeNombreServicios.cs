using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioDeNombreServicios : IServicioBase<CambioDeNombre>
    {
        IList<CambioDeNombre> ObtenerCambioDeNombreFiltro(CambioDeNombre cambioDeNombre);
    }
}
