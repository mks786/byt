using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAsociado : IDaoBase<Asociado, int>
    {

        /// <summary>
        /// Consulta un Asociado coon toda su informacion
        /// </summary>
        /// <param name="usuario">Asociado con parametros</param>
        /// <returns>Asociado </returns>
        Asociado ObtenerAsociadoConTodo(Asociado usuario);


        /// <summary>
        /// Consulta los asociados dado unos parametros determinados
        /// </summary>
        /// <param name="asociado">asociado con parametros</param>
        /// <returns>lista de asociados</returns>
        IList<Asociado> ObtenerAsociadosFiltro(Asociado asociado);


        /// <summary>
        /// Consulta si un asociado tiene cartas
        /// </summary>
        /// <param name="asociado">asociado a buscar cartas</param>
        /// <returns>true en caso de tener cartas, false en caso contrario</returns>
        bool VerificarCartasDeAsociado(Asociado asociado);

        /// <summary>
        /// Método que obtiene los contactos de un asociado
        /// </summary>
        /// <param name="recordatorio">recordatorio a filtrar</param>
        /// /// <param name="fechas">fechas de renovación de marca a filtrar</param>
        /// <returns>lista de recordatorios filtrados</returns>
        IList<ContactosDelAsociadoVista> ObtenerContactosDelAsociado(Asociado asociado, bool todos);

        /// <summary>
        /// Método que obtiene los emails de un asociado
        /// </summary>
        /// <param name="asociado"></param>
        /// <returns></returns>
        IList<EmailAsociado> ObtenerEmailsDelAsociado(Asociado asociado);


        /// <summary>
        /// Metodo que ejecuta el procedimiento
        /// </summary>
        /// <param name="parametro">Parametro a ejectura</param>
        /// <returns>true si se ejecto, de lo contrario false</returns>
        bool EjecutarProcedimiento(ParametroProcedimiento parametro);

    }
}
