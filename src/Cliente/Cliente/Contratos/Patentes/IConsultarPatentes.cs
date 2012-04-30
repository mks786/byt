using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Patentes
{
    interface IConsultarPatentes : IPaginaBase
    {
        string Id { get; set; }

        object Resultados { get; set; }

        string NombrePatente { get; }

        object Patentes { get; set; }

        object Patente { get; set; }

        string Fecha { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string TotalHits { set; }

        void LimpiarCampos();
    }
}
