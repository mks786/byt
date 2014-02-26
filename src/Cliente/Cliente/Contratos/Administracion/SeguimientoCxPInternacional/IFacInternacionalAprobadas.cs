using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IFacInternacionalAprobadas : IPaginaBase
    {
        object FacturasAutorizadas { get; set; }

        object FacturasSeleccionadas { get; set; }

        string TotalMontoAprobado { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);

        void HabilitarBotonActualizar(bool estado);

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        bool ExportarListadoFacturasAprobadas(string tituloReporte, System.Data.DataTable datosExportar);
    }
}
