using System;
using System.Windows.Controls;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones
{
    interface IConsultarPresentacionesSapi : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string FechaSolicitudPresentacion { get; set; }

        object DptosSolicitudPresentacion { get; set; }

        object DptoSolicitudPresentacion { get; set; }

        object DctosPresentacion { get; set; }

        object DctoPresentacion { get; set; }

        string CodigoExpPresentacion { get; set; }

        object StatusPresentacion { get; set; }

        object StatusPresentacionSeleccionado { get; set; }

        object UsuariosPresentacion { get; set; }

        object UsuarioPresentacion { get; set; }

        string TotalHits { set; }

        object Resultados { get; set; }

        object SolicitudPresentacionSeleccionada { get; set; }

        void Mensaje(string mensaje, int opcion);

    }
}
