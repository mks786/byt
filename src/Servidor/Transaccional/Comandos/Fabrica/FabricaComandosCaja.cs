using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCaja;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCaja
    {
        /// <summary>
        /// Metodo para obtener el comando para consultar todas las Cajas
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Caja>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCajas();
        }
    }
}
