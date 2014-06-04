using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoCaso;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTipoCaso
    {
        /// <summary>
        /// Metodo que obtiene el comando necesario para obtener todos los tipos de casos
        /// </summary>
        /// <returns>Comando para realizar la accion determinada</returns>
        public static ComandoBase<IList<TipoCaso>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTiposDeCaso();
        }
    }
}
