using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosMedio;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMedio
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un medio
        /// </summary>
        /// <param name="medio">medio a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el medio en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Medio medio)
        {
            return new ComandoInsertarOModificarMedio(medio);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los medios
        /// </summary>
        /// <returns>El Comando para consultar todos los medios</returns>
        public static ComandoBase<IList<Medio>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosMedios();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un medio
        /// </summary>
        /// <param name="usuario">medio que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarAnexo(Medio medio)
        {
            return new ComandoEliminarMedio(medio);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un medio por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Medio> ObtenerComandoConsultarPorID(Medio medio)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">medio a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaAnexo(Medio medio)
        {
            return new ComandoVerificarExistenciaMedio(medio);
        }
    }
}
