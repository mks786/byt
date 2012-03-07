using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPlanilla : IDaoBase<Planilla, int>
    {
        bool EjecutarProcedimiento(ParametroProcedimiento parametro);

        Planilla EjecutarProcedimientoPID(Usuario usuario);
    }
}
