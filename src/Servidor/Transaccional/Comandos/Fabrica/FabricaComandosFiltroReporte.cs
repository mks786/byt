using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosFiltroReporte;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosFiltroReporte
    {
        /// <summary>
        /// Metodo para insertar o actualizar un filtro de reporte de marca
        /// </summary>
        /// <param name="filtroReporteDeMarca">Filtro de Reporte de Marca a actualizar</param>
        /// <returns>True si la operacion es satisfactoria; false en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(FiltroReporte filtroReporte)
        {
            return new ComandoInsertarOModificarFiltroReporte(filtroReporte);
        }


        /// <summary>
        /// Metodo para obtener los filtros de un Reporte de Marca
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>Lista de filtros definidos para el reporte de marca especifico</returns>
        public static ComandoBase<IList<FiltroReporte>> ObtenerComandoConsultarFiltrosReporte(Reporte reporte)
        {
            return new ComandoConsultarFiltrosReporte(reporte);
        }

        /// <summary>
        /// Metodo que elimina un filtro de un reporte determinado
        /// </summary>
        /// <param name="filtro">Filtro de Reporte a eliminar</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarFiltroDeReporte(FiltroReporte filtro)
        {
            return new ComandoEliminarFiltroDeReporte(filtro);
        }
    }
}
