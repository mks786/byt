using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosPatente;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosPatente
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un Patente
        /// </summary>
        /// <param name="patente">Patente a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Patente patente)
        {
            return new ComandoInsertarOModificarPatente(patente);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un Patente
        /// </summary>
        /// <param name="patente">Estado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Patente patente)
        {
            return new ComandoEliminarPatente(patente);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Patentes
        /// </summary>
        /// <returns>Lista con todos los Patentes</returns>
        public static ComandoBase<IList<Patente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasPatentes();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="patente">Patente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaPatente(Patente patente)
        {
            return new ComandoVerificarExistenciaPatente(patente);
        }

        public static ComandoBase<IList<Patente>> ObtenerComandoConsultarPatentesFiltro(Patente patente)
        {
            return new ComandoConsultarPatentesFiltro(patente);
        }

        public static ComandoBase<Patente> ObtenerComandoConsultarPatenteConTodo(Patente patente)
        {
            return new ComandoConsultarPatenteConTodo(patente);
        }

        /// <summary>
        /// Método que devuelve el ComandoConsultarPorId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ComandoConsultarPorId</returns>
        public static ComandoBase<Patente> ObtenerComandoConsultarPorId(int id)
        {
            return new ComandoConsultarPatentePorId(id);
        }
    }
}