using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Renovaciones
{
    interface IConsultarRenovaciones : IPaginaBase
    {
        string Id { get; set; }       

        object RenovacionSeleccionada { get; }

        object Resultados { get; set; }

        string IdMarcaFiltrar { get; set; }

        string FechaFiltrar { get; set; }

        string NombreMarcaFiltrar { get; set; }

        string RegistroMarcaFiltrar { get; set; }

        object Marcas { get; set; }

        object Marca { get; set; }

        string MarcaFiltrada { get; set; }

        void MostrarBotonVolverAMarca();

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje,int opcion);

        string TotalHits { set; }
    }
}
