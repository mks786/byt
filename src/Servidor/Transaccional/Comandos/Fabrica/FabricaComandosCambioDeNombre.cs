using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCambioDeNombre;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCambioDeNombre
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un CambioNombre
        /// </summary>
        /// <param name="CambioNombre">CambioNombre a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el CambioNombre en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CambioDeNombre cambioDeNombre)
        {
            return new ComandoInsertarOModificarCambioDeNombre(cambioDeNombre);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los CambioNombres
        /// </summary>
        /// <returns>El Comando para consultar todos los CambioNombres</returns>
        public static ComandoBase<IList<CambioDeNombre>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCambioDeNombre();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">CambioNombre que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCambioNombre(CambioDeNombre cambioDeNombre)
        {
            return new ComandoEliminarCambioDeNombre(cambioDeNombre);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un CambioNombre por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<CambioDeNombre> ObtenerComandoConsultarPorID(CambioDeNombre cambioDeNombre)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="cambioNombre">CambioNombre a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCambioNombre(CambioDeNombre cambioDeNombre)
        {
            return new ComandoVerificarExistenciaCambioDeNombre(cambioDeNombre);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarFusionesFiltro
        /// </summary>
        /// <param name="fusion">Fusion a consultar</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>
        public static ComandoBase<IList<CambioDeNombre>> ObtenerComandoConsultarCambiosNombreFiltro(CambioDeNombre cambioDeNombre)
        {
            return new ComandoConsultarCambiosDeNombreFiltro(cambioDeNombre);
        }
    }
}
