using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoReportePatente : IDaoBase<ReportePatente, int>
    {

        /// <summary>
        /// Metodo que ejecuta el procedimiento
        /// </summary>
        /// <param name="parametro">Parametro a ejectura</param>
        /// <returns>true si se ejecto, de lo contrario false</returns>
        bool EjecutarProcedimiento(ParametroProcedimiento parametro);


        /// <summary>
        /// Método que ejecuta un procedimiento en base de datos
        /// </summary>
        /// <param name="usuario">parámetro que contiene todos los datos necesarios para ejecutar el procedimiento</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        ReportePatente EjecutarProcedimientoPID(Usuario usuario);


        /// <summary>
        /// Método que devuelve un reporte por codigo de la patente
        /// </summary>
        /// <param name="idPatente"></param>
        /// <returns></returns>
        ReportePatente ObtenerPorCodigoPatente(int idPatente);


        /// <summary>
        /// Método que devuelve un reporte por codigo de la patente
        /// </summary>
        /// <param name="idPatente"></param>
        /// <returns></returns>
        bool EliminarPorCodigoPatente(int idPatente);
    }
}
