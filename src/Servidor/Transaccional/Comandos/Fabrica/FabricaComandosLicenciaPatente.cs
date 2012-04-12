using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosLicenciaPatente;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosLicenciaPatente
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un LicenciaPatente
        /// </summary>
        /// <param name="LicenciaPatente">LicenciaPatente a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el LicenciaPatente en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(LicenciaPatente licencia)
        {
            return new ComandoInsertarOModificarLicenciaPatente(licencia);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Licencias
        /// </summary>
        /// <returns>El Comando para consultar todos los Licencias</returns>
        public static ComandoBase<IList<LicenciaPatente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosLicenciaPatente();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un agente
        /// </summary>
        /// <param name="agente">LicenciaPatente que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarLicenciaPatente(LicenciaPatente licencia)
        {
            return new ComandoEliminarLicenciaPatente(licencia);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un LicenciaPatente por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<LicenciaPatente> ObtenerComandoConsultarPorID(LicenciaPatente licencia)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="licencia">LicenciaPatente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaLicenciaPatente(LicenciaPatente licencia)
        {
            return new ComandoVerificarExistenciaLicenciaPatente(licencia);
        }
        public static ComandoBase<IList<LicenciaPatente>> ObtenerComandoConsultarLicenciasPatenteFiltro(LicenciaPatente licencia)
        {
            return new ComandoConsultarLicenciasPatenteFiltro(licencia);
        }
    }
}
