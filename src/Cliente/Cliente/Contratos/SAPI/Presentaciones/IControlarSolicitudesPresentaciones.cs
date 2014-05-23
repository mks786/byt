using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones
{
    interface IControlarSolicitudesPresentaciones : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string FechaSolicitudPresentacion { get; set; }

        string FechaPresentacionAnteSAPI { get; set; }

        object Dptos { get; set; }

        object Dpto { get; set; }

        object Documentos { get; set; }

        object Documento { get; set; }

        string CodigoExpPresentacion { get; set; }

        object StatusTodos { get; set; }

        object StatusSeleccionado { get; set; }

        object Usuarios { get; set; }

        object Usuario { get; set; }

        object Gestores { get; set; }

        object Gestor { get; set; }

        object GestoresRegistro { get; set; }

        object GestorRegistro { get; set; }

        string FechaConfirmacion { get; set; }

        string TotalHits { set; }

        object Resultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        void MostrarCamposRegistroEvento(string tipoBandera);

        void OcultarCamposRegistroEvento(string tipoBandera);

        void ExportarDatosConsolidadosExcel(DataTable datosResumen);

    }
}
