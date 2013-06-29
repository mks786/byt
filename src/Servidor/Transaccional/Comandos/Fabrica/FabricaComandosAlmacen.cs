using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosAlmacen;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosAlmacen
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los tipoInfobol
        /// </summary>
        /// <returns>Lista con todos los tipoInfoboles</returns>
        public static ComandoBase<IList<Almacen>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosAlmacenes();
        }
    }
}
