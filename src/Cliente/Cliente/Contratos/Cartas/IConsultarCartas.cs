using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Cartas
{
    interface IConsultarCartas : IPaginaBase
    {
        string Id { get; set; }

        object CartaSeleccionado { get; set; }

        object Resultados { get; set; }

        object CartaFiltrar { get; set; }

        object Responsable { get; set; }

        object Responsables { get; set; }

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string ResumenFiltrar { get; set; }

        string ReferenciaFiltrar { get; set; }

        string Fecha { get; set; }

        string FechaAnexo { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje,int opcion);

        string TotalHits { set; }

        string NombreAsociado { set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        object Medio { get; set; }

        object Medios { get; set; }

        string Tracking { get; set; }

        string AnexoTracking { get; set; }

        //string IdContactoFiltrar { get; set; }

        string NombreContactoFiltrar { get; set; }

        string CorreoContactoFiltrar { get; set; }

        object Contactos { get; set; }

        object Contacto { get; set; }

        string NombreContacto { set; }

        string AsociadoNoRegistrado { get; set; }

        void HabilitarBotonEliminarCartas();
    }
}
