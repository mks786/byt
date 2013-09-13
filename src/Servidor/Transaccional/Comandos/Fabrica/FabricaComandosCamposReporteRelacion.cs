using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCamposReporteRelacion;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCamposReporteRelacion
    {
        /// <summary>
        /// Metodo para insertar o actualizar un Campo de un Reporte de Marca en la tabla relacion de campos y reporte de marca
        /// </summary>
        /// <param name="campoReporteDeMarca">Campo del Reporte de Marca a insertar</param>
        /// <returns>True si la operacion se realiza exitosamente; false en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CamposReporteRelacion campoReporte)
        {
            return new ComandoInsertarOModificarCamposReporte(campoReporte);
        }

        /// <summary>
        /// Metodo para consultar los campos de un reporte de marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>Lista de campos de un reporte de marca especifico</returns>
        public static ComandoBase<IList<CamposReporteRelacion>> ObtenerComandoConsultarCamposDeReporte(Reporte reporte)
        {
            return new ComandoConsultarCamposDeReporte(reporte);
        }

        /// <summary>
        /// Metodo para eliminar campos de un reporte de marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>True si el proceso se realiza exitosamente; False en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCamposReporte(Reporte reporte)
        {
            return new ComandoEliminarCamposReporte(reporte);
        }

        /// <summary>
        /// Metodo para obtener el comando para eliminar un Campo Relacion de un Reporte especifico
        /// </summary>
        /// <param name="campo">Campo Relacion a eliminar</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCampoRelacion(CamposReporteRelacion campo)
        {
            return new ComandoEliminarCampoRelacion(campo);
        }
    }
}
