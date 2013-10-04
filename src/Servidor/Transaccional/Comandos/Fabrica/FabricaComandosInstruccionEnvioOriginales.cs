using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosInstruccionEnvioOriginales;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInstruccionEnvioOriginales
    {
        /// <summary>
        /// Metodo que obtiene todas las Instrucciones De Envio de Originales que tiene la tabla MYP_INSTR_EORIGINAL
        /// </summary>
        /// <returns>Lista de Instrucciones de Envios de Originales</returns>
        public static ComandoBase<IList<InstruccionEnvioOriginales>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInstruccionesEnvioOriginales();
        }


        /// <summary>
        /// Metodo que inserta o modifica una Instruccion de Envio de Originales
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales a inserta o actualizar</param>
        /// <returns>True en caso de realizarse correctamente; false en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InstruccionEnvioOriginales instruccion)
        {
            return new ComandoInsertarOModificarInstruccionEnvioOriginales(instruccion);
        }



        /// <summary>
        /// Metodo que obtiene el comando para obtener la instruccion de Envio de Originales de una marca o una patente filtrado por: Codigo de la 
        /// marca o la patente, a que aplica (M de Marca o P de Patente) y que concepto posee (C de Correspondencia o F de Facturacion)
        /// </summary>
        /// <param name="instruccion">Instruccion Envio de Originales filtro</param>
        /// <returns>Clase Comando para obtener la instruccion de envio de originales de una marca o una patente</returns>
        public static ComandoBase<InstruccionEnvioOriginales> ObtenerComandoObtenerInstruccionEnvioOriginales(InstruccionEnvioOriginales instruccion)
        {
            return new ComandoObtenerInstruccionEnvioOriginales(instruccion);
        }
    }
}
