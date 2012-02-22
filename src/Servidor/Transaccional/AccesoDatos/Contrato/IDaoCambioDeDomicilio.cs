using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeDomicilio : IDaoBase<CambioDeDomicilio, int>
    {
        IList<CambioDeDomicilio> ObtenerCambiosDeDomicilioFiltro(CambioDeDomicilio CambioDeDomicilio);
    }
}
