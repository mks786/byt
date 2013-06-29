using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoDocumento;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTipoDocumento
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los tipos de documentos sin discriminacion
        /// </summary>
        /// <returns>Lista con todos los tipoInfoboles</returns>
        public static ComandoBase<IList<TipoDocumento>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTipoDocumento();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los tipo de documento ya sea por Marca o por patente
        /// </summary>
        /// <returns>Lista con todos los tipoInfoboles</returns>
        public static ComandoBase<IList<TipoDocumento>> ObtenerComandoObtenerTipoDocumentoMarcaOPatente(String parametro1, String parametro2)
        {
            //return new ComandoConsultarTodosTipoDocumento();
            return new ComandoObtenerTipoDocumentoMarcaOPatente(parametro1, parametro2);
        }

    }
}
