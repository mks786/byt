
namespace Trascend.Bolet.Cliente.Contratos.Asociados
{
    interface IAgregarAsociado : IPaginaBase
    {
        object Asociado { get; set; }

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

        object FormaPago { get; set; }

        object FormasPagos { get; set; }

        object Tarifa { get; set; }

        object Tarifas { get; set; }

        char TipoPersona { get; }
    }
}
