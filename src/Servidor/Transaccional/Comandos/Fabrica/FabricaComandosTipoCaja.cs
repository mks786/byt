using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoCaja;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTipoCaja
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los tipoCaja
        /// </summary>
        /// <returns>Lista con todos los tipoCaja</returns>
        public static ComandoBase<IList<TipoCaja>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTipoCaja();
        }


        public static ComandoBase<IList<TipoCaja>> ObtenerComandoObtenerTipoCajaMarcaOPatente(String parametro1)
        {
            return new ComandoObtenerTipoCajaMarcaOPatente(parametro1);
        }
    }
}
