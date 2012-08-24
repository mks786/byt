using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInstruccionDeRenovacion;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInstruccionDeRenovacion
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos las Búsqueda
        /// </summary>
        /// <returns>El Comando para consultar todos las Búsqueda</returns>
        public static ComandoBase<IList<InstruccionDeRenovacion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasInstruccionesDeRenovacion();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar una Búsqueda por su ID
        /// </summary>
        /// <returns>Búsqueda buscada</returns>
        public static ComandoBase<InstruccionDeRenovacion> ObtenerComandoConsultarPorId(InstruccionDeRenovacion busqueda)
        {
            return new ComandoConsultarInstruccionDeRenovacionPorId(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar una Búsqueda
        /// </summary>
        /// <param name="infoBol">Búsqueda a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion de una Búsqueda</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InstruccionDeRenovacion busqueda)
        {
            return new ComandoInsertarOModificarInstruccionDeRenovacion(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un Búsqueda
        /// </summary>
        /// <param name="infoBol">Búsqueda que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarInstruccionDeRenovacion(InstruccionDeRenovacion busqueda)
        {
            return new ComandoEliminarInstruccionDeRenovacion(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia de una Búsqueda
        /// </summary>
        /// <param name="busqueda">Búsqueda a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaInstruccionDeRenovacion(InstruccionDeRenovacion busqueda)
        {
            return new ComandoVerificarExistenciaInstruccionDeRenovacion(busqueda);
        }

        /// <summary>
        /// Método que devuelve el comando para consultar todos las Búsqueda de una marca
        /// </summary>
        /// <param name="marca">Marca a la cual se le consultarán las Búsquedas</param>
        /// <returns></returns>
        public static ComandoBase<IList<InstruccionDeRenovacion>> ObtenerComandoConsultarInstruccionDeRenovacionsPorMarca(Marca marca)
        {
            return new ComandoConsultarInstruccionesDeRenovacionPorMarca(marca);
        }
    }
    
}
