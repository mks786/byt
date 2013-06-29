using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosFechaMarca;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosFechaMarca
    {
        /// <summary>
        /// Metodo estatico que genera los comandos para consultar las fechas por marca
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <returns>Comando para consultar fechas por marca</returns>
        public static ComandoBase<IList<FechaMarca>> ObtenerComandoConsultarFechasPorMarca(Marca marca)
        {
            return new ComandoConsultarFechasPorMarca(marca);
        }
    }
}
