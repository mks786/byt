using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosListaDatosValores;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosListaDatosValores
    {
        /// <summary>
        /// Método que devulve una lista de valores dado un parametro
        /// </summary>
        /// <param name="listaDatosValores">Objeto que contiene el parametro para el filtro</param>
        /// <returns>Lista de valores que devuele la consulta por parametro</returns>
        public static ComandoBase<IList<ListaDatosValores>> ObtenerComandoConsultarListaDatosValoresPorParametro(ListaDatosValores listaDatosValores)
        {
            return new ComandoConsultarListaDatosValoresPorParametro(listaDatosValores);
        }
    }
}
