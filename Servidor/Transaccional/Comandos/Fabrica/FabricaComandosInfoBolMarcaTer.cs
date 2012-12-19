using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInfoBolMarcaTer;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInfoBolMarcaTer
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los InfoBolMarcaTers
        /// </summary>
        /// <returns>El Comando para consultar todos los InfoBolMarcaTers</returns>
        public static ComandoBase<IList<InfoBolMarcaTer>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInfoBolMarcaTers();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un InfoBolMarcaTer por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<InfoBolMarcaTer> ObtenerComandoConsultarPorID(InfoBolMarcaTer infoBol)
        {
            return new ComandoConsultarInfoBolMarcaTerPorID(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar un InfoBolMarcaTer
        /// </summary>
        /// <param name="infoBol">InfoBolMarcaTer a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InfoBolMarcaTer infoBol)
        {
            return new ComandoInsertarOModificarInfoBolMarcaTer(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un InfoBolMarcaTer
        /// </summary>
        /// <param name="infoBol">InfoBolMarcaTer que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarInfoBolMarcaTer(InfoBolMarcaTer infoBol)
        {
            return new ComandoEliminarInfoBolMarcaTer(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="infoBol">InfoBolMarcaTer a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaInfoBolMarcaTer(InfoBolMarcaTer infoBol)
        {
            return new ComandoVerificarExistenciaInfoBolMarcaTer(infoBol);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar la info adicinal por id
        /// </summary>
        /// <param name="infoBol">InfoBolMarcaTer a consultar</param>
        /// <returns>InfoBolMarcaTer que devuelve la consulta</returns>
        public static ComandoBase<InfoBolMarcaTer> ObtenerComandoConsultarInfoBolMarcaTerPorId(InfoBolMarcaTer infoBol)
        {
            return new ComandoConsultarInfoBolMarcaTerPorID(infoBol);
        }

        /// <summary>
        /// Método que devuelve el comando para consultar todos los infoboles de una marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public static ComandoBase<IList<InfoBolMarcaTer>> ObtenerComandoConsultarInfoBolMarcaTeresPorMarca(MarcaTercero marca)
        {
            return new ComandoConsultarInfoBolMarcaTeresPorMarca(marca);
        }
    }
    
}
