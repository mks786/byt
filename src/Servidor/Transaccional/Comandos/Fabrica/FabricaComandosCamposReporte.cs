using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCamposReporte;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCamposReporte
    {
        /// <summary>
        /// Metodo para obtener el comando para recuperar todos los campos de reportes de marcas y patentes
        /// </summary>
        /// <returns>Lista con todos los campos de reportes de marcas y patentes</returns>
        public static ComandoBase<IList<CamposReporte>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCamposReporte();
        }

        /// <summary>
        /// Metodo para obtener el comando para recuperar todos los campos para el reporte de marcas
        /// </summary>
        /// <returns>Lista con todos los campos de reportes de marcas</returns>
        public static ComandoBase<IList<CamposReporte>> ObtenerComandoObtenerCamposReporteDeMarca()
        {
            return new ComandoObtenerCamposReporteDeMarca();
        }


        /// <summary>
        /// Metodo para obtener el comando para recuperar todos los campos para el reporte de patentes
        /// </summary>
        /// <returns>Lista con todos los campos de reportes de patentes</returns>
        public static ComandoBase<IList<CamposReporte>> ObtenerComandoObtenerCamposReporteDePatente()
        {
            return new ComandoObtenerCamposReporteDePatente();
        }
    }
}
