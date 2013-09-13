using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosVistaReporte;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosVistaReporte
    {
        /// <summary>
        /// Metodo que obtiene todos las vistas de los reportes
        /// </summary>
        /// <returns>Lista de objetos VistaReporte</returns>
        public static ComandoBase<IList<VistaReporte>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosVistasReporte();
        }
    }
}
