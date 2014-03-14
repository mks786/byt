using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCadenaDeCambios;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCadenaDeCambios
    {
        /// <summary>
        /// Metodo para obtener el objeto comando para insertar y/o actualizar una Cadena de Cambios
        /// </summary>
        /// <param name="cadenaCambios">Cadena de Cambios a insertar o actualizar</param>
        /// <returns>Comando a ejecutar</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CadenaDeCambios cadenaCambios)
        {
            return new ComandoInsertarOModificarCadenaDeCambios(cadenaCambios);
        }

        /// <summary>
        /// Metodo para obtener el objeto comando para consultar las cadenas de cambio por filtro
        /// </summary>
        /// <param name="cadenaCambiosFiltro">Cadena de Cambios que sirve de filtro</param>
        /// <returns>Comando a ejecutar</returns>
        public static ComandoBase<IList<CadenaDeCambios>> ObtenerComandoConsultarCadenasCambioFiltro(CadenaDeCambios cadenaCambios)
        {
            return new ComandoConsultarCadenasCambiosFiltro(cadenaCambios);
        }
    }
}
