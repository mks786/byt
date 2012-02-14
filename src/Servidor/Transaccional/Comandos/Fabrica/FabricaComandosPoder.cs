using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosPoder;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosPoder
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Poder
        /// </summary>
        /// <param name="Poder">Poder a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar un Poder en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Poder poder)
        {
            return new ComandoInsertarOModificarPoder(poder);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Poderes
        /// </summary>
        /// <returns>El Comando para consultar todos los Poderes</returns>
        public static ComandoBase<IList<Poder>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosPoderes();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un poder
        /// </summary>
        /// <param name="poder">Poder que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarPoder(Poder poder)
        {
            return new ComandoEliminarPoder(poder);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Poder por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Poder> ObtenerComandoConsultarPorID(Poder poder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Poderes que posee un interesado
        /// </summary>
        /// <param name="interesado">Interesado para realizar el filtrado</param>
        /// <returns>El Comando para consultar todos los Poderes</returns>
        public static ComandoBase<IList<Poder>> ObtenerComandoConsultarPoderesPorInteresado(Interesado interesado)
        {
            return new ComandoConsultarPoderesPorInteresado(interesado);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Poderes que posee un agente
        /// </summary>
        /// <param name="agente">Agente para realizar el filtrado</param>
        /// <returns>El Comando para consultar todos los Poderes</returns>
        public static ComandoBase<IList<Poder>> ObtenerComandoConsultarPoderesPorAgente(Agente agente)
        {
            return new ComandoConsultarPoderesPorAgente(agente);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un poder con uno o mas filtros
        /// </summary>
        /// <returns>Poder fitlrado</returns>
        public static ComandoBase<IList<Poder>> ObtenerComandoConsultarPoderesFiltro(Poder poder)
        {
            return new ComandoConsultarPoderesFiltro(poder);
        }
    }
}
