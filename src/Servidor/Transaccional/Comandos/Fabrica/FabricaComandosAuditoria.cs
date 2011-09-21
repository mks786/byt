using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosAuditoria;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosAuditoria
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Auditoria
        /// </summary>
        /// <param name="auditoria">Auditoria a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Auditoria auditoria)
        {
            return new ComandoInsertarOModificarAuditoria(auditoria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditoria"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarAuditoria(Auditoria auditoria)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Auditoria>> ObtenerComandoConsultarTodos()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Metodo que devuleve la auditoria de una tabla y una clave foranea
        /// </summary>
        /// <param name="auditoria">Auditoria que contiene la Fk y la Tabla</param>
        /// <returns>Lista de auditorias que cumplen esos parametros</returns>
        public static ComandoBase<IList<Auditoria>> ObtenerComandoAuditoriaPorFkyTabla(Auditoria auditoria)
        {
            return new ComandoAuditoriaPorFkyTabla(auditoria);
        }
    }
}
