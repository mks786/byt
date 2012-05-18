using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosMemoria;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMemoria
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Memoria
        /// </summary>
        /// <param name="Memoria">Memoria a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar un Memoria en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Memoria memoria)
        {
            return new ComandoInsertarOModificarMemoria(memoria);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un memoria
        /// </summary>
        /// <param name="memoria">Memoria que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarMemoria(Memoria memoria)
        {
            return new ComandoEliminarMemoria(memoria);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Memoria por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Memoria> ObtenerComandoConsultarPorID(Memoria memoria)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Memoriaes que posee un interesado
        /// </summary>
        /// <param name="patente">Interesado para realizar el filtrado</param>
        /// <returns>El Comando para consultar todos los Memoriaes</returns>
        public static ComandoBase<IList<Memoria>> ObtenerComandoConsultarMemoriasPorPatente(Patente patente)
        {
            return new ComandoConsultarMemoriasPorPatente(patente);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Memoriaes que posee un interesado
        /// </summary>
        /// <param name="patente">Interesado para realizar el filtrado</param>
        /// <returns>El Comando para consultar todos los Memoriaes</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaMemoria(Memoria memoria)
        {
            return new ComandoVerificarExistenciaMemoria(memoria);
        }
    }
}
