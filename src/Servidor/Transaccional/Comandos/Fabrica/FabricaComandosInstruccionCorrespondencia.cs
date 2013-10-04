using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosInstruccionCorrespondencia;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInstruccionCorrespondencia
    {
        /// <summary>
        /// Metodo que obtiene todas las Instrucciones De Correspondencia que tiene la tabla MYP_INSTR_EMAIL
        /// </summary>
        /// <returns>Lista de Instrucciones de Envios de Originales</returns>
        public static ComandoBase<IList<InstruccionCorrespondencia>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInstruccionesCorrespondencia();
        }

        /// <summary>
        /// Metodo que obtiene el comando para insertar o actualizar una Instruccion por Correspondencia
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia a insertar o actualizar</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InstruccionCorrespondencia instruccion)
        {
            return new ComandoInsertarOModificarInstruccionCorrespondencia(instruccion);
        }


        /// <summary>
        /// Metodo para obtener la instruccion de correspondencia tomando en cuenta el codigo de la Marca o Patente, 
        /// a que se aplica y el concepto de la instruccion
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia a consultar</param>
        /// <returns>Instruccion de Correspondencia de la marca o la patente segun la aplicacion y el concepto; NULL en caso contrario</returns>
        public static ComandoBase<InstruccionCorrespondencia> ObtenerComandoObtenerInstruccionCorrespondencia(InstruccionCorrespondencia instruccion)
        {
            return new ComandoObtenerInstruccionCorrespondencia(instruccion);
        }
    }
}
