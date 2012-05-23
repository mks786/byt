using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.MarcasTercero
{
    interface IConsultaMarcasTercero : IPaginaBase
    {
        string Id { get; set; }

        object MarcaTerceroSeleccionada { get; }

        object Resultados { get; set; }

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string IdCorresponsalFiltrar { get; set; }

        string NombreCorresponsalFiltrar { get; set; }

        object Corresponsales { get; set; }

        object Corresponsal { get; set; }

        string IdInteresadoFiltrar { get; set; }

        string NombreInteresadoFiltrar { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        string DescripcionFiltrar { get; set; }

        //string FichasFiltrar { get; }

        string Fecha { get; set; }

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

        object BoletinesOrdenPublicacion { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        bool NacionalEstaSeleccionado { get; set; }

        bool BoletinesEstaSeleccionado { get; set; }

        object MarcaTerceroParaFiltrar { get; set; }

        void GestionarVisibilidadFiltroNacional(bool visibilidad);

        void GestionarVisibilidadLimpiarFiltros();

        void LimpiarCampos();
    }
}
