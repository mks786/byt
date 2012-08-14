using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInteresadoServicios: IServicioBase<Interesado>
    {
        /// <summary>
        /// Servicio que se encarga de consultar la auditoria del interesado
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditorias pertenecientes al interesado</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);


        /// <summary>
        /// Servicio que se encarga de consultar al interesado con todos sus objetos
        /// </summary>
        /// <param name="interesado">Interesado a buscar</param>
        /// <returns>Interesado con todos los objetos</returns>
        Interesado ConsultarInteresadoConTodo(Interesado interesado);


        /// <summary>
        /// Servicio que se encarga de consultar los interesados por filtro
        /// </summary>
        /// <param name="interesado">Interesado filtro</param>
        /// <returns>Lista de interesados que cumplen con el filtro</returns>
        IList<Interesado> ObtenerInteresadosFiltro(Interesado interesado);


        /// <summary>
        /// Servicio que consulta el interesadi que pertenece a un poder
        /// </summary>
        /// <param name="poder">poder a consultar los interesados</param>
        /// <returns>Interesado perteneciente al poder</returns>
        Interesado ObtenerInteresadosDeUnPoder(Poder poder);

        /// <summary>
        /// Método que se encarga de realizar la insercion de un interesado a base de datos
        /// </summary>
        /// <param name="interesado">Interesado a agregar o modificar</param>
        /// <param name="hash">Codigo hash del usuario que realiza la accion</param>
        /// <returns>Codigo del interesado insertado</returns>
        int? InsertarOModificarInteresado(Interesado interesado, int hash);
    }
}
