using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosFusionPatente;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosFusionPatente
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un FusionPatente
        /// </summary>
        /// <param name="FusionPatente">FusionPatente a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el FusionPatente en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(FusionPatente fusion)
        {
            return new ComandoInsertarOModificarFusionPatente(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los FusionPatentes
        /// </summary>
        /// <returns>El Comando para consultar todos los FusionPatentes</returns>
        public static ComandoBase<IList<FusionPatente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosFusionesPatente();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">FusionPatente que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarFusionPatente(FusionPatente fusion)
        {
            return new ComandoEliminarFusionPatente(fusion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un FusionPatente por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<FusionPatente> ObtenerComandoConsultarPorID(FusionPatente fusion)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="fusion">FusionPatente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaFusionPatente(FusionPatente fusion)
        {
            return new ComandoVerificarExistenciaFusionPatente(fusion);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarFusionPatenteesFiltro
        /// </summary>
        /// <param name="fusion">FusionPatente a consultar</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>
        public static ComandoBase<IList<FusionPatente>> ObtenerComandoConsultarFusionPatenteesFiltro(FusionPatente fusion)
        {
            return new ComandoConsultarFusionesPatenteFiltro(fusion);
        }
    }
}
