using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IConsultarMarcas : IPaginaBase
    {
        string Id { get; }

        object MarcaSeleccionada { get; }

        object Resultados { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string IdCorresponsalFiltrar { get; }

        string NombreCorresponsalFiltrar { get; }

        object Corresponsales { get; set; }

        object Corresponsal { get; set; }

        string IdInteresadoFiltrar { get; }

        string NombreInteresadoFiltrar { get; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        string DescripcionFiltrar { get; }

        //string FichasFiltrar { get; }

        string Fecha { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string AsociadoFiltro { set; }

        string InteresadoFiltro { set; }

        string CorresponsalFiltro { set; }

        string TotalHits { set; }

        object Servicios { get; set; }

        object Servicio { get; set; }

        object Detalle { get; set; }

        object Detalles { get; set; }

        object Pais { get; set; }

        object Paises { get; set; }

        object PaisPrioridad { get; set; }

        object PaisesPrioridad { get; set; }

        object Condicion { get; set; }

        object Condiciones { get; set; }

        object BoletinesOrdenPublicacion { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object Marcas { get; set; }

        object Marca { get; set; }

        string NombreMarca { set; }

    }
}
