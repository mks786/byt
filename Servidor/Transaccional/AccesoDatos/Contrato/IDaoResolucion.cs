using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoResolucion : IDaoBase<Resolucion, string>
    {

        /// <summary>
        /// Método que verifica la existencia de una resolución
        /// </summary>
        /// <param name="resolucion">Resolucion a buscar</param>
        /// <returns>true en caso de que exista, false en lo contrario</returns>
        bool VerificarExistenciaResolucion(Resolucion resolucion);       
    }
}
