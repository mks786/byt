using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCambioNombre;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCambioNombre
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un CambioNombre
        /// </summary>
        /// <param name="CambioNombre">CambioNombre a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el CambioNombre en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CambioNombre cambioNombre)
        {
            return new ComandoInsertarOModificarCambioNombre(cambioNombre);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los CambioNombres
        /// </summary>
        /// <returns>El Comando para consultar todos los CambioNombres</returns>
        public static ComandoBase<IList<CambioNombre>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCambioNombre();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">CambioNombre que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCambioNombre(CambioNombre cambioNombre)
        {
            return new ComandoEliminarCambioNombre(cambioNombre);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un CambioNombre por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<CambioNombre> ObtenerComandoConsultarPorID(CambioNombre cambioNombre)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="cambioNombre">CambioNombre a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCambioNombre(CambioNombre cambioNombre)
        {
            return new ComandoVerificarExistenciaCambioNombre(cambioNombre);
        }
    }
}
