using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFusionMarcaTercero : IDaoBase<FusionMarcaTercero, int>
    {
        FusionMarcaTercero FusionConsultarFusionMarcaTerceroPorFusion(Fusion fusion);
    }
}
