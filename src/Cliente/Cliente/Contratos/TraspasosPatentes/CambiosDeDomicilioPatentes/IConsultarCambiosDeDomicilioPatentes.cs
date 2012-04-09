using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.CambiosDeDomicilioPatentes
{
    interface IConsultarCambiosDeDomicilioPatentes : IPaginaBase
    {
        string Id { get; }

        string NombreMarca { set; }

        object CambioDeDomicilioSeleccionada { get; }

        object Resultados { get; set; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object Marcas { get; set; }

        object Marca { get; set; }

        //string IdInteresadoFiltrar { get; }

        //string NombreInteresadoFiltrar { get; }

        //object Interesados { get; set; }

        //object Interesado { get; set; }

        //string DescripcionFiltrar { get; }

        //string FichasFiltrar { get; }

        string Fecha { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string TotalHits { set; }
    }
}
