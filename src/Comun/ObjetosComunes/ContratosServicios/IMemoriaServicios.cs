using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMemoriaServicios : IServicioBase<Memoria>
    {
        IList<Memoria> ConsultarMemoriasPorPatente(Patente patente);

        bool VerificarExistenciaMemoria(Patente patente, Memoria memoria);
    }
}
