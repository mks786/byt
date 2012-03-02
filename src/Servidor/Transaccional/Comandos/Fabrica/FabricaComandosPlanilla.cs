using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosPlanilla;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosPlanilla
    {

        /// <summary>
        /// Método que devuelve el Comando dado el dominio
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Planilla> ObtenerComandoConsultarPorId(int id)
        {
            return new ComandoConsultarPlanillaPorId(id);
        }

        public static ComandoBase<bool> ObtenerComandoEjecutarProcedimientoP1(Marca marca, Usuario usuario, int way)
        {
            return new ComandoEjecutarProcedimientoP1(marca, usuario, way);
        }

        public static ComandoBase<Planilla> ObtenerComandoConsultarPlanillaPorUsuario(Usuario usuario)
        {
            return new ComandoConsultarPlanillaPorUsuario(usuario);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un planilla
        /// </summary>
        /// <param name="usuario">planilla que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarPlanilla(Planilla planilla)
        {
            return new ComandoEliminarPlanilla(planilla);
        }
    }
}
