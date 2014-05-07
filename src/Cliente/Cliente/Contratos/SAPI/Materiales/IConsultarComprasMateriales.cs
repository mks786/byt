using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.SAPI.Materiales
{
    interface IConsultarComprasMateriales : IPaginaBase 
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string IdCompraSapi { get; set; }

        string FechaCompraSapi { get; set; }

        object MaterialesSapi { get; set; }

        object MaterialSapi { get; set; }

        object Resultados { get; set; }

        object CompraSapiSeleccionada { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);

    }
}
