using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInfoBol;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInfoBol
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los InfoBols
        /// </summary>
        /// <returns>El Comando para consultar todos los InfoBols</returns>
        public static ComandoBase<IList<InfoBol>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInfoBols();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un InfoBol por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<InfoBol> ObtenerComandoConsultarPorID(InfoBol infoBol)
        {
            return new ComandoConsultarInfoBolPorID(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar un InfoBol
        /// </summary>
        /// <param name="infoBol">InfoBol a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InfoBol infoBol)
        {
            return new ComandoInsertarOModificarInfoBol(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un InfoBol
        /// </summary>
        /// <param name="infoBol">InfoBol que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarInfoBol(InfoBol infoBol)
        {
            return new ComandoEliminarInfoBol(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="infoBol">InfoBol a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaInfoBol(InfoBol infoBol)
        {
            return new ComandoVerificarExistenciaInfoBol(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar la info adicinal por id
        /// </summary>
        /// <param name="infoBol">InfoBol a consultar</param>
        /// <returns>InfoBol que devuelve la consulta</returns>
        public static ComandoBase<InfoBol> ObtenerComandoConsultarInfoBolPorId(InfoBol infoBol)
        {
            return new ComandoConsultarInfoBolPorID(infoBol);
        }

        /// <summary>
        /// Método que devuelve el comando para consultar todos los infoboles de una marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public static ComandoBase<IList<InfoBol>> ObtenerComandoConsultarInfoBolesPorMarca(Marca marca)
        {
            return new ComandoConsultarInfoBolesPorMarca(marca);
        }
    }
    
}
