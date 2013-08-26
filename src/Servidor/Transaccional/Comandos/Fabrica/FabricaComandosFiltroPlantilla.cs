using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosFiltroPlantilla;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosFiltroPlantilla
    {
        /// <summary>
        /// Metodo para obtener el comando para consultar todos los Filtros de una Plantilla 
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<FiltroPlantilla>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosFiltroPlantilla();
        }

        /// <summary>
        /// Metodo estatico para obtener los filtros de encabezado de una plantilla dada
        /// </summary>
        /// <param name="plantilla">Plantilla seleccionada</param>
        /// <returns>Lista de los filtros de encabezado de una plantilla especifica</returns>
        public static ComandoBase<IList<FiltroPlantilla>> ObtenerComandoConsultarFiltrosEncabezadoPlantilla(Plantilla plantilla)
        {
            return new ComandoConsultarFiltrosEncabezadoPlantilla(plantilla);
        }


        /// <summary>
        /// Metodo estatico para obtener los filtros de encabezado de una plantilla dada
        /// </summary>
        /// <param name="plantilla">Plantilla seleccionada</param>
        /// <returns>Lista de los filtros de encabezado de una plantilla especifica</returns>
        public static ComandoBase<IList<FiltroPlantilla>> ObtenerComandoConsultarFiltrosDetallePlantilla(Plantilla plantilla)
        {
            return new ComandoConsultarFiltrosDetallePlantilla(plantilla);
        }


        /// <summary>
        /// Metodo estatico para insertar y/o modificar un filtro de una plantilla
        /// </summary>
        /// <param name="filtro">Filtro a inserta y/o modificar de la plantilla seleccionada</param>
        /// <returns>True si la operacion se realiza correctamente; false en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(FiltroPlantilla filtro)
        {
            return new ComandoInsertarOModificarFiltroPlantilla(filtro);
        }


        /// <summary>
        /// Metodo estatico para eliminar un filtro de una plantilla
        /// </summary>
        /// <param name="filtro">Filtro a eliminar de la plantilla seleccionada</param>
        /// <returns>True si la operacion se realiza correctamente; false en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarFiltroPlantilla(FiltroPlantilla filtro)
        {
            return new ComandoEliminarFiltroPlantilla(filtro);
        }
    }
}
