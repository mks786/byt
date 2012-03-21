using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoResolucion : IDaoBase<Resolucion, string>
    {
        bool VerificarExistenciaResolucion(Resolucion resolucion);       
    }
}
