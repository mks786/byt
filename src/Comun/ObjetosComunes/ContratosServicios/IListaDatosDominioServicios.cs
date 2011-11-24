using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IListaDatosDominioServicios : IServicioBase<ListaDatosDominio>
    {
        IList<ListaDatosDominio> ConsultarListaDatosDominioPorParametro(ListaDatosDominio parametro);
    }
}
