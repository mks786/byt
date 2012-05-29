using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPlanilla : IDaoBase<Planilla, int>
    {
        bool EjecutarProcedimiento(ParametroProcedimiento parametro);


        /// <summary>
        /// Método que ejecuta un procedimiento en base de datos
        /// </summary>
        /// <param name="usuario">parámetro que contiene todos los datos necesarios para ejecutar el procedimiento</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        Planilla EjecutarProcedimientoPID(Usuario usuario);
    }
}
