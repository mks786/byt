using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.CambiosDeNombrePatentes
{
    interface IConsultarCambiosDeNombrePatentes : IPaginaBase
    {
        string Id { get; set; }

        string NombrePatente { set; }

        object CambioDeNombreSeleccionada { get; set; }

        object Resultados { get; set; }

        string IdPatenteFiltrar { get; set; }

        string NombrePatenteFiltrar { get; set; }

        object Patentes { get; set; }

        object Patente { get; set; }

        //string IdInteresadoFiltrar { get; }

        //string NombreInteresadoFiltrar { get; }

        //object Interesados { get; set; }

        //object Interesado { get; set; }

        //string DescripcionFiltrar { get; }

        //string FichasFiltrar { get; }

        string Fecha { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string TotalHits { set; }
    }
}
