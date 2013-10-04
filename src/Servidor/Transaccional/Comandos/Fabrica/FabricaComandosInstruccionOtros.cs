using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosInstruccionOtros;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInstruccionOtros
    {
        /// <summary>
        /// Metodo para obtener el comando para consultar todas las instrucciones no tipificadas
        /// </summary>
        /// <returns>Lista de instrucciones no tipificadas</returns>
        public static ComandoBase<IList<InstruccionOtros>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasInstruccionesNoTipificadas();
        }

        /// <summary>
        /// Metodo para obtener el comando para consultar todas las instrucciones no tipificadas de una marca o una patente
        /// </summary>
        /// <param name="instruccionNoTipificada">Instruccion No Tipificada filtro</param>
        /// <returns>Comando para consultar todas las instrucciones no tipificadas de una marca o de una patente</returns>
        public static ComandoBase<IList<InstruccionOtros>> ObtenerComandoConsultarInstruccionesNoTipificadasPorFiltro(InstruccionOtros instruccionNoTipificada)
        {
            return new ComandoConsultarInstruccionesNoTipificadasPorCodigo(instruccionNoTipificada);
        }

        /// <summary>
        /// Metodo para obtener el comando para insertar o actualizar una instruccion no tipificada de marca o de patente
        /// </summary>
        /// <param name="instruccion">Instruccion no tipificada a insertar o a actualizar</param>
        /// <returns>Comando para insertar la instruccion no tipificada</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InstruccionOtros instruccion)
        {
            return new ComandoInsertarOModificarInstruccionNoTipificada(instruccion);
        }
    }
}
