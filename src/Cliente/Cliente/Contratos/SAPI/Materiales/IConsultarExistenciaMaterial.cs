using System;
using System.Collections.Generic;
using System.Data;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.SAPI.Materiales
{
    interface IConsultarExistenciaMaterial : IPaginaBase
    {
        object MaterialSapi { get; set; }

        object MaterialIds { get; set; }

        object MaterialId { get; set; }

        object MaterialDescripciones { get; set; }

        object MaterialDescripcion { get; set; }

        object MaterialTipos { get; set; }

        object MaterialTipo { get; set; }

        object MaterialDepartamentos { get; set; }

        object MaterialDepartamento { get; set; }

        object Resultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ExportarDatosExcel(DataTable datosFiltrados);
    }
}
