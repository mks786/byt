using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IRemitenteServicios: IServicioBase<Remitente>
    {
        /// <summary>
        /// Servicio que inserta o modifica una nuevo Remitente
        /// </summary>
        /// <param name="remitente">Remitente a insertar o modificar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>Id del Remitente insertado o modificado</returns>
        string InsertarOModificarRemitente(Remitente remitente, int hash);
    }
}
