using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosBusqueda;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosBusqueda
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los InfoBols
        /// </summary>
        /// <returns>El Comando para consultar todos los InfoBols</returns>
        public static ComandoBase<IList<Busqueda>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasBusquedas();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un InfoBol por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Busqueda> ObtenerComandoConsultarPorID(Busqueda busqueda)
        {
            return new ComandoConsultarBusquedaPorID(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar un InfoBol
        /// </summary>
        /// <param name="infoBol">InfoBol a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Busqueda busqueda)
        {
            return new ComandoInsertarOModificarBusqueda(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un InfoBol
        /// </summary>
        /// <param name="infoBol">InfoBol que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarBusqueda(Busqueda busqueda)
        {
            return new ComandoEliminarBusqueda(busqueda);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="busqueda">InfoBol a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaBusqueda(Busqueda busqueda)
        {
            return new ComandoVerificarExistenciaBusqueda(busqueda);
        }

        ///// <summary>
        ///// Método que devuelve el Comando para consultar la info adicinal por id
        ///// </summary>
        ///// <param name="infoBol">InfoBol a consultar</param>
        ///// <returns>InfoBol que devuelve la consulta</returns>
        //public static ComandoBase<Busqueda> ObtenerComandoConsultarInfoBolPorId(Busqueda busqueda)
        //{
        //    return new ComandoConsultarBusquedaPorID(busqueda);
        //}

        /// <summary>
        /// Metodo que devuelve el comando para consultar todos los infoboles de una marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public static ComandoBase<IList<Busqueda>> ObtenerComandoConsultarBusquedasPorMarca(Marca marca)
        {
            return new ComandoConsultarBusquedasPorMarca(marca);
        }
    }
    
}
