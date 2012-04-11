using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMarcaServicios : IServicioBase<Marca>
    {
        IList<Marca> ObtenerMarcasFiltro(Marca Marca);

        IList<Marca> ObtenerMarcasPorFechaRenovacion(Marca Marca, DateTime[] fechas);

        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);

        Marca ConsultarMarcaConTodo(Marca marca);
    }
}
