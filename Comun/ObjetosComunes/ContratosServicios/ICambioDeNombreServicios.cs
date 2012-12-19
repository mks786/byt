using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioDeNombreServicios : IServicioBase<CambioDeNombre>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los CambioDeNombre segun el filtro
        /// </summary>
        /// <param name="CambioPeticionario">CambioDeNombre filtro</param>
        /// <returns>CambioDeNombre que cumplen con el filtro</returns>
        IList<CambioDeNombre> ObtenerCambioDeNombreFiltro(CambioDeNombre cambioDeNombre);


        /// <summary>
        /// Servicio que se encarga de insertar la CambioDeNombre
        /// </summary>
        /// <param name="marca">CambioDeNombre a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la CambioDeNombre insertada</returns>
        int? InsertarOModificarCambioNombre(CambioDeNombre cambioNombre, int hash);
    }
}
