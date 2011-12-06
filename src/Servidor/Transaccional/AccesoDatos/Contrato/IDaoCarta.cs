using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCarta : IDaoBase<Carta, int>
    {
        IList<Carta> ObtenerCartasFiltro(Carta carta);
        bool Insertar(Carta carta, ITransaction transaccion);
    }
}
