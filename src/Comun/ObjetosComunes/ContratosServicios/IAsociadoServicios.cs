using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAsociadoServicios: IServicioBase<Asociado>
    {

        /// <summary>
        /// Servicio que se encarga consultar la auditoria de un Asociado
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditorias del asociado</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);


        /// <summary>
        /// Servicio que se encarga de consultar el asociado con todos sus objetos
        /// </summary>
        /// <param name="asociado">Asociado a buscar</param>
        /// <returns>Asociado con todos sus objetos</returns>
        Asociado ConsultarAsociadoConTodo(Asociado asociado);


        /// <summary>
        /// Servicio que se encarga de obtener un asociado basado en el filtro
        /// </summary>
        /// <param name="asociado">Asociado filtro</param>
        /// <returns>Lista de asociados que cumplen con el filtro</returns>
        IList<Asociado> ObtenerAsociadosFiltro(Asociado asociado);


        /// <summary>
        /// Método que verifica si un asociado tiene cartas
        /// </summary>
        /// <param name="asociado">asociado a verificar</param>
        /// <returns>true en caso correcto</returns>
        bool VerificarCartasPorAsociado(Asociado asociado);




        /// <summary>
        /// Servicio que se encarga de consultar los contactos de un asociado en una vista
        /// </summary>
        /// <param name="asociado">Asociado con los parametros a filtrar</param>
        /// <returns>Lista de ContactosDelAsociadoVista del asociado</returns>
        IList<ContactosDelAsociadoVista> ConsultarContactosDelAsociado(Asociado asociado, bool todos);




        /// <summary>
        /// Servicio que se encarga de consultar los emails de un Asociado
        /// </summary>
        /// <param name="asociado"></param>
        /// <returns></returns>
        IList<EmailAsociado> ConsultarEmailsDelAsociado(Asociado asociado);


        /// <summary>
        /// Servicio que se encarga de insertar el asociado
        /// </summary>
        /// <param name="marca">asociado a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id del asociado insertado</returns>
        int? InsertarOModificarAsociado(Asociado asociado, int hash);
    }
}
