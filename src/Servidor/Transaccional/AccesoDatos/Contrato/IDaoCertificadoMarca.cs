using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCertificadoMarca: IDaoBase<CertificadoMarca,string>
    {
        CertificadoMarca ConsultarCertificadoMarcaPorId(CertificadoMarca certificado);
    }
}
