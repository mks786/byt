using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInventor;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInventor
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un anexo
        /// </summary>
        /// <param name="anexo">Inventor a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el anexo en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Inventor anexo)
        {
            return new ComandoInsertarOModificarInventor(anexo);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Inventores
        /// </summary>
        /// <returns>El Comando para consultar todos los Inventores</returns>
        public static ComandoBase<IList<Inventor>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInventores();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un Inventor
        /// </summary>
        /// <param name="usuario">Inventor que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarInventor(Inventor anexo)
        {
            return new ComandoEliminarInventor(anexo);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Inventor por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Inventor> ObtenerComandoConsultarPorID(Inventor anexo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">Inventor a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaInventor(Inventor anexo)
        {
            return new ComandoVerificarExistenciaInventor(anexo);
        }

        public static ComandoBase<IList<Inventor>> ObtenerComandoConsultarInventoresPorPatente(Patente patente)
        {
            return new ComandoConsultarInventoresPorPatente(patente);
        }
    }
}
