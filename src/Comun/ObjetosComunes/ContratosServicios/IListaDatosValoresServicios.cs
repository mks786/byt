using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IListaDatosValoresServicios : IServicioBase<ListaDatosValores>
    {
        IList<ListaDatosValores> ConsultarListaDatosValoresPorParametro(ListaDatosValores listaDatosValores);
    }
}
