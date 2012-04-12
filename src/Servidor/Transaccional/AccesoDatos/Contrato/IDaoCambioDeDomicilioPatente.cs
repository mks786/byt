using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioDeDomicilioPatente : IDaoBase<CambioDeDomicilioPatente, int>
    {
        IList<CambioDeDomicilioPatente> ObtenerCambiosDeDomicilioPatenteFiltro(CambioDeDomicilioPatente CambioDeDomicilio);
    }
}
