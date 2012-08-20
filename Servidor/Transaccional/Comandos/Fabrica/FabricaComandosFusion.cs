using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosFusion;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosFusion
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Fusion
        /// </summary>
        /// <param name="Fusion">Fusion a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el Fusion en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Fusion fusion)
        {
            return new ComandoInsertarOModificarFusion(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Fusions
        /// </summary>
        /// <returns>El Comando para consultar todos los Fusions</returns>
        public static ComandoBase<IList<Fusion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosFusiones();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">Fusion que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarFusion(Fusion fusion)
        {
            return new ComandoEliminarFusion(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Fusion por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Fusion> ObtenerComandoConsultarPorID(Fusion fusion)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="fusion">Fusion a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaFusion(Fusion fusion)
        {
            return new ComandoVerificarExistenciaFusion(fusion);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarFusionesFiltro
        /// </summary>
        /// <param name="fusion">Fusion a consultar</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>
        public static ComandoBase<IList<Fusion>> ObtenerComandoConsultarFusionesFiltro(Fusion fusion)
        {
            return new ComandoConsultarFusionesFiltro(fusion);
        }
    }
}
