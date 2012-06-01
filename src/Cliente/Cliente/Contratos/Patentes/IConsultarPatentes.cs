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

        #region Asociado

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string AsociadoFiltro { set; }

        #endregion

        #region Interesado

        string IdInteresadoFiltrar { get; set; }

        string NombreInteresadoFiltrar { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        string InteresadoFiltro { set; }

        #endregion

        #region Boletin

        object BoletinesOrdenPublicacion { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        #endregion

        #region Combobox

        object Servicios { get; set; }

        object Servicio { get; set; }

        object Detalle { get; set; }

        object Detalles { get; set; }

        object Paises { get; set; }
        
        object PaisPrioridad { get; set; }

        #endregion

        bool PrioridadesEstaSeleccionado {get; set;}

        bool BoletinesEstaSeleccionado { get; set; }

        object PatenteParaFiltrar { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string TotalHits { set; }

        void LimpiarCampos();
    }
}
