using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosFusionPatenteTercero;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosFusionPatenteTercero
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Fusion
        /// </summary>
        /// <param name="Fusion">Fusion a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el Fusion en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(FusionPatenteTercero fusion)
        {
            return new ComandoInsertarOModificarFusionPatenteTercero(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Fusions
        /// </summary>
        /// <returns>El Comando para consultar todos los Fusions</returns>
        public static ComandoBase<IList<FusionPatenteTercero>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosFusionPatenteTerceros();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">Fusion que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarFusion(FusionPatenteTercero fusion)
        {
            return new ComandoEliminarFusionPatenteTercero(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Fusion por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<FusionPatenteTercero> ObtenerComandoConsultarPorID(Fusion fusion)
        {
            return new ComandoConsultarFusionPatenteTerceroPorFusion(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="fusion">Fusion a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaFusion(FusionPatenteTercero fusion)
        {
            return new ComandoVerificarExistenciaFusionPatenteTercero(fusion);
        }
    }
}
