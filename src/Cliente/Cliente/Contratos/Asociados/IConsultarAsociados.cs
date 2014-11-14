using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Asociados
{
    interface IConsultarAsociados : IPaginaBase
    {
        object AsociadoFiltrar { get; set; }

        object AsociadoSeleccionado { set; get; }

        object Resultados { get; set; }

        object Pais { get; set; }

        object Paises { get; set; }

        object Idioma { get; set; }

        object Idiomas { get; set; }

        object Moneda { get; set; }

        object Monedas { get; set; }

        object Descuento { get; set; }

        object Descuentos { get; set; }

        object TipoCliente { get; set; }

        object TiposClientes { get; set; }

        object OrigenesClientes { get; set; }

        object OrigenCliente { get; set; }

        object Etiqueta { get; set; }

        object Etiquetas { get; set; }

        object DetallePago { get; set; }

        object DetallesPagos { get; set; }

        object Tarifa { get; set; }

        object Tarifas { get; set; }

        string Id { get; set; }

        string NombreAsociado { get; set; }

        string DomicilioAsociado { get; set; }

        object TipoPersona { get; set; }

        object TipoPersonas { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);

        string EmailAsociado { get; set; }

        string Telefono1Asociado { get; set; }

        string AlarmaDescripcionAsociado { get; set; }

        object Conceptos { get; set; }

        object Concepto { get; set; }
    }
}
