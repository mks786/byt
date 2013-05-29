using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

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

        object TipoPersona { get; set; }

        object TipoPersonas { get; set; }

        void pintarJustificacion();

        void pintarContacto();

        void pintarDatosTransferencia();

        void pintarAuditoria();

        void pintarCorrespondencia();

        void pintarEmails();

        void pintarConectividad();

        void ArchivoNoEncontrado();

        string SaldoVencidoSolicitud { set; }

        string SaldoPorVencerSolicitud { set; }

        string TotalSolicitud { set; }

        string MSaldoPendiente { set; }


        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        object ListaContactos { get; set; }

        object ContactoSeleccionado { get; }

        bool? ChkVerContactos { get; }

        void Mensaje(string mensaje);

        void DesactivarVerListaContactos();

        void ArchivoNoEncontrado(string mensaje);

    }
}
