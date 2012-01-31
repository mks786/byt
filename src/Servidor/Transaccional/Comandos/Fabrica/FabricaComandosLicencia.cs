using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosLicencia;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosLicencia
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Licencia
        /// </summary>
        /// <param name="Licencia">Licencia a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el Licencia en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Licencia licencia)
        {
            return new ComandoInsertarOModificarLicencia(licencia);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Licencias
        /// </summary>
        /// <returns>El Comando para consultar todos los Licencias</returns>
        public static ComandoBase<IList<Licencia>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosLicencia();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">Licencia que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarLicencia(Licencia licencia)
        {
            return new ComandoEliminarLicencia(licencia);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Licencia por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Licencia> ObtenerComandoConsultarPorID(Licencia licencia)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="licencia">Licencia a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaLicencia(Licencia licencia)
        {
            return new ComandoVerificarExistenciaLicencia(licencia);
        }
    }
}
