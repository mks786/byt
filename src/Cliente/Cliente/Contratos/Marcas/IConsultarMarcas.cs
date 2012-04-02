using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IConsultarMarcas : IPaginaBase
    {
        string Id { get; set; }

        object MarcaSeleccionada { get; }

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

        string IdMarcaFiltrar { get; set; }

        string NombreMarcaFiltrar { get; set; }

        object Marcas { get; set; }

        object Marca { get; set; }

        string NombreMarca { set; }

        bool InternacionalEstaSeleccionado { get; set; }

        bool NacionalEstaSeleccionado { get; set; }

        bool TYREstaSeleccionado { get; set; }

        bool IndicadoresEstaSeleccionado { get; set; }

        bool PrioridadesEstaSeleccionado { get; set; }

        bool BoletinesEstaSeleccionado { get; set; }

        #region TYR

        string CodigoRegistro { get; set; }

        string FechaRegistro { get; set; }

        bool RenovadoPorOtroTramitante { get; set; }

        bool InstruccionesDeRenovacion { get; set; }

        string ExpCambioPendiente { get; set; }

        #endregion

        void GestionarVisibilidadFiltroInternacional(bool visibilidad);

        void GestionarVisibilidadFiltroNacional(bool visibilidad);

        void GestionarVisibilidadLimpiarFiltros();
    }
}
