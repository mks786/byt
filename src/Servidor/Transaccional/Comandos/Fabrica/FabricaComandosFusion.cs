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
        /// Método que devuelve el Comando para agregar un Agente
        /// </summary>
        /// <param name="Agente">Agente a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el Agente en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Fusion fusion)
        {
            return new ComandoInsertarOModificarFusion(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Agentes
        /// </summary>
        /// <returns>El Comando para consultar todos los Agentes</returns>
        public static ComandoBase<IList<Fusion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosFusiones();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">Agente que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarFusion(Fusion fusion)
        {
            return new ComandoEliminarFusion(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Agente por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Fusion> ObtenerComandoConsultarPorID(Fusion fusion)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="fusion">Agente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaFusion(Fusion fusion)
        {
            return new ComandoVerificarExistenciaFusion(fusion);
        }
    }
}
