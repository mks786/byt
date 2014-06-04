using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICasoServicios : IServicioBase<Caso>
    {
        /// <summary>
        /// Servicio que inserta y/o actualiza un Caso 
        /// </summary>
        /// <param name="caso">Caso a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>Id del caso insertado o actualizado; en caso contrario, retorna NULL</returns>
        int? InsertarOModificarCaso(Caso caso, int hash);

        /// <summary>
        /// Servicio que obtiene una lista de casos dependiendo de un filtro
        /// </summary>
        /// <param name="caso">Caso usado como filtro</param>
        /// <returns>Lista de Casos de acuerdo al filtro especificado</returns>
        IList<Caso> ObtenerCasosFiltro(Caso caso);


        /// <summary>
        /// Servicio encargado de consultar la auditoria de Casos
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de Auditorias</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);


    }
}
