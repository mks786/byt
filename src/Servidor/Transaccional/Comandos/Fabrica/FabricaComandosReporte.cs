using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosReporte;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosReporte
    {
        /// <summary>
        /// Metodo para obtener el comando para recuperar todos los Reportes
        /// </summary>
        /// <returns>Lista con todos los reportes</returns>
        public static ComandoBase<IList<Reporte>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosReporte();
        }

        /// <summary>
        /// Metodo para insertar o actualizar un Reporte
        /// </summary>
        /// <param name="reporteDeMarca">Reporte a insertar o actualizar</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; false en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Reporte reporte)
        {
            return new ComandoInsertarOModificarReporte(reporte);
        }

        /// <summary>
        /// Metodo que obtiene una lista de reportes mediante la utilizacion de un reporte filtro
        /// </summary>
        /// <param name="reporteDeMarca">Reporte que sirve como filtro para la consulta</param>
        /// <returns>Lista de Reportes obtenidas mediante un filtro determinado</returns>
        public static ComandoBase<IList<Reporte>> ObtenerComandoObtenerReporteFiltro(Reporte reporte)
        {
            return new ComandoObtenerReporteFiltro(reporte);
        }

        /// <summary>
        /// Metodo que obtiene el comando para consultar la cabecera de un reporte con todos sus componentes
        /// </summary>
        /// <param name="reporte">Reporte a consultar</param>
        /// <returns>Cabecera del reporte con todos sus componentes</returns>
        public static ComandoBase<Reporte> ObtenerComandoConsultarReporteConTodo(Reporte reporte)
        {
            return new ComandoConsultarReporteConTodo(reporte);
        }
    }
}
