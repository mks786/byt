using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosAuditoria;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandoAuditoria
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
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Objeto objeto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Objeto>> ObtenerComandoConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
