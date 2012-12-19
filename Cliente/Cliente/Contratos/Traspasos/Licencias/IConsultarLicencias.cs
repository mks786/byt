using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Traspasos.Licencias
{
    interface IConsultarLicencias : IPaginaBase
    {

        string Id { get; set; }

        string NombreMarca { set; }

        object LicenciaSeleccionada { get; set; }

        object Resultados { get; set; }

        string IdMarcaFiltrar { get; set; }

        string NombreMarcaFiltrar { get; set; }

        object Marcas { get; set; }

        object Marca { get; set; }

        string Fecha { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string TotalHits { set; }
        
    }
}
