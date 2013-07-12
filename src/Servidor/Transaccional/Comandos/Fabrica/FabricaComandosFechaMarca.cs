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

        /// <summary>
        /// Metodo estatico que genera los comandos para insertar o actualizar una fecha de marca
        /// </summary>
        /// <param name="fechaMarca">Fecha de Marca a insertar o actualizar</param>
        /// <returns>True si la operacion se realiza correctamente; false, en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(FechaMarca fechaMarca)
        {
            return new ComandoInsertarOModificarFechaMarca(fechaMarca);
        }

        /// <summary>
        /// Metodo estatico que genera los comandos para eliminar una fecha de marca
        /// </summary>
        /// <param name="fechaMarca">Fecha de marca a eliminar</param>
        /// <returns>True si la operacion se realiza correctamente; false, en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarFechaMarca(FechaMarca fechaMarca)
        {
            return new ComandoEliminarFechaMarca(fechaMarca);
        }

    }
}
