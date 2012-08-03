using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosAsignacion;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosAsignacion
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar una asignación
        /// </summary>
        /// <param name="asignacion">Carta a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Asignacion asignacion)
        {
            return new ComandoInsertarOModificarAsignacion(asignacion);
        }

        public static ComandoBase<IList<Asignacion>> ObtenerComandoConsultarAsignacionesPorCarta(Carta carta)
        {
            return new ComandoConsultarAsignacionesPorCarta(carta);
        }

        /// <summary>
        /// Metodo regresa una lista de sasignaciones por un Responsable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ComandoBase<IList<Asignacion>> ObtenerComandoConsultarAsignacionesPorUsuario(Usuario user)
        {
            return new ComandoConsultarAsignacionesPorUsuario(user);
        }

        /// <summary>
        /// Metodo que elimina las asignaciones de una carta
        /// </summary>
        /// <param name="carta">Carta a eliminar sus asignaciones</param>
        /// <returns>True en caso de haber sido correcto, false en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarAsignacionesPorCarta(Carta carta)
        {
            return new ComandoEliminarAsignacionesPorCarta(carta);
        }
    }
}