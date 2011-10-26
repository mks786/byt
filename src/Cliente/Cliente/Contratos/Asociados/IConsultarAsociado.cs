using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Asociados
{
    interface IConsultarAsociado : IPaginaBase
    {
        object Asociado { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

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

        char GetTipoPersona { get; }

        string SetTipoPersona { set; }

        void ArchivoNoEncontrado();


    }
}
