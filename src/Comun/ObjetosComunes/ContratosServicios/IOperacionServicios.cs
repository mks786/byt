using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IOperacionServicios : IServicioBase<Operacion>
    {
        IList<Operacion> ConsultarOperacionesPorMarca(Marca marca);

        IList<Operacion> ConsultarOperacionesPorPatente(Patente patente);

        IList<Operacion> ObtenerOperacionPorMarcaYServicio(Operacion operacion);

        IList<Operacion> ObtenerOperacionFiltro(Operacion operacionAuxiliar);
    }
}
