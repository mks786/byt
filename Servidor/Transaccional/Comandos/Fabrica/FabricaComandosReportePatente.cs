using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosReportePatente;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosReportePatente
    {

        /// <summary>
        /// Método que devuelve el Comando dado el dominio
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<ReportePatente> ObtenerComandoConsultarPorId(int id)
        {
            return new ComandoConsultarReportePatentePorId(id);
        }

        /// <summary>
        /// Método que devuelve el Comando dado el dominio
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<ReportePatente> ObtenerComandoConsultarReportePatentePorCodigoPatente(int idPatente)
        {
            return new ComandoConsultarReportePatentePorCodigoPatente(idPatente);
        }

        public static ComandoBase<bool> ObtenerComandoEjecutarProcedimiento(ParametroProcedimiento parametro)
        {
            return new ComandoEjecutarProcedimiento(parametro);
        }

        public static ComandoBase<ReportePatente> ObtenerComandoConsultarReportePatentePorUsuario(Usuario usuario)
        {
            return new ComandoConsultarReportePatentePorUsuario(usuario);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un ReportePatente
        /// </summary>
        /// <param name="usuario">ReportePatente que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarReportePatente(int idPatente)
        {
            return new ComandoEliminarReportePatente(idPatente);
        }
    }
}
