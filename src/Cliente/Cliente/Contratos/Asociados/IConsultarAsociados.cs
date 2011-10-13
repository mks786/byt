using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Asociados
{
    interface IConsultarAsociados : IPaginaBase
    {
        object AsociadoFiltrar { get; set; }

        object AsociadoSeleccionado { get; }

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

        object Etiqueta { get; set; }

        object Etiquetas { get; set; }

        object DetallePago { get; set; }

        object DetallesPagos { get; set; }

        object Tarifa { get; set; }

        object Tarifas { get; set; }

        char TipoPersona { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
