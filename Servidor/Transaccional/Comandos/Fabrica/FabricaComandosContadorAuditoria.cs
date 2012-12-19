using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosContadorAuditoria;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosContadorAuditoria
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Contador
        /// </summary>
        /// <param name="contadorAuditoria">Contador a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(ContadorAuditoria contadorAuditoria)
        {
            return new ComandoInsertarOModificarContadorAuditoria(contadorAuditoria);
        }

        /// <summary>
        /// Método que devuelve el Comando dado el dominio
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<ContadorAuditoria> ObtenerComandoConsultarPorId(string id)
        {
            return new ComandoConsultarPorIdContadorAuditoria(id);
        }
    }
}
