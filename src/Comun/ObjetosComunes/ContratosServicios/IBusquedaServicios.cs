using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IBusquedaServicios : IServicioBase<Busqueda>
    {
        IList<Busqueda> ConsultarBusquedasPorMarca(Marca marca);
    }
}
