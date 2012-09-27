using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosBusqueda;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosBusqueda
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos las Búsqueda
        /// </summary>
        /// <returns>El Comando para consultar todos las Búsqueda</returns>
        public static ComandoBase<IList<Busqueda>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasBusquedas();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar una Búsqueda por su ID
        /// </summary>
        /// <returns>Búsqueda buscada</returns>
        public static ComandoBase<Busqueda> ObtenerComandoConsultarPorID(Busqueda busqueda)
        {
            return new ComandoConsultarBusquedaPorID(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar una Búsqueda
        /// </summary>
        /// <param name="infoBol">Búsqueda a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion de una Búsqueda</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Busqueda busqueda)
        {
            return new ComandoInsertarOModificarBusqueda(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un Búsqueda
        /// </summary>
        /// <param name="infoBol">Búsqueda que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarBusqueda(Busqueda busqueda)
        {
            return new ComandoEliminarBusqueda(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia de una Búsqueda
        /// </summary>
        /// <param name="busqueda">Búsqueda a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaBusqueda(Busqueda busqueda)
        {
            return new ComandoVerificarExistenciaBusqueda(busqueda);
        }

        /// <summary>
        /// Método que devuelve el comando para consultar todos las Búsqueda de una marca
        /// </summary>
        /// <param name="marca">Marca a la cual se le consultarán las Búsquedas</param>
        /// <returns></returns>
        public static ComandoBase<IList<Busqueda>> ObtenerComandoConsultarBusquedasPorMarca(Marca marca)
        {
            return new ComandoConsultarBusquedasPorMarca(marca);
        }
    }
    
}
