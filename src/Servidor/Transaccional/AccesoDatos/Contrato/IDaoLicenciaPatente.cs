using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoLicenciaPatente : IDaoBase<LicenciaPatente, int>
    {
        IList<LicenciaPatente> ObtenerLicenciasPatenteFiltro(LicenciaPatente licencia);
    }
}
