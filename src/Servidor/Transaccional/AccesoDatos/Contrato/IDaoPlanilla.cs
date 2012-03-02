using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPlanilla : IDaoBase<Planilla, int>
    {
        bool EjecutarProcedimientoP1(Marca marca, Usuario usuario, int way);

        Planilla EjecutarProcedimientoPID(Usuario usuario);
    }
}
