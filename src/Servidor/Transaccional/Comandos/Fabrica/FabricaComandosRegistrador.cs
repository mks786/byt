using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosRegistrador;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosRegistrador
    {
        /// <summary>
        /// Metodo que devuelve el comando para consultar todos los Registradores
        /// </summary>
        /// <returns>Comando para consultar todos los registradores</returns>
        public static ComandoBase<IList<Registrador>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosRegistradores();
        }
    }
}
