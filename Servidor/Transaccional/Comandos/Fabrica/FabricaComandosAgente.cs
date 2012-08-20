using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosAgente;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosAgente
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Agente
        /// </summary>
        /// <param name="Agente">Agente a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el Agente en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Agente agente)
        {
            return new ComandoInsertarOModificarAgente(agente);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Agentes
        /// </summary>
        /// <returns>El Comando para consultar todos los Agentes</returns>
        public static ComandoBase<IList<Agente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosAgentes();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">Agente que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarAgente(Agente agente)
        {
            return new ComandoEliminarAgente(agente);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Agente por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Agente> ObtenerComandoConsultarPorID(Agente Agente)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para los agentes con sus poderes
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Agente>> ObtenerComandoConsultarAgentesYPoderes()
        {
            return new ComandoConsultarAgentesYPoderes();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="agente">Agente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaAgente(Agente agente)
        {
            return new ComandoVerificarExistenciaAgente(agente);
        }

        public static ComandoBase<IList<Agente>> ObtenerComandoConsultarAgentesFiltro(Agente agente)
        {
            return new ComandoConsultarAgentesFiltro(agente);
        }

        public static ComandoBase<IList<Agente>> ObtenerComandoConsultarAgentesDeUnPoder(Poder poder)
        {
            return new ComandoConsultarAgentesDeUnPoder(poder);
        }

        public static ComandoBase<IList<Agente>> ObtenerComandoConsultarAgentesSinPoderesFiltro(Agente agente)
        {
            return new ComandoConsultarAgentesSinPoderesFiltro(agente);
        }
    }
}
