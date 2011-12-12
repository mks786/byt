using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IConsultarMarca : IPaginaBase
    {
        object Marca { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        string NombreAsociado { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        object PoderesSolicitud { get; set; }

        object PoderSolicitud { get; set; }

        object PoderesDatos { get; set; }

        object PoderDatos { get; set; }

        object Agentes { get; set; }

        object Agente { get; set; }

        object BoletinesOrdenPublicacion { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        object Servicios { get; set; }

        object Servicio { get; set; }

        object Detalle { get; set; }

        object Detalles { get; set; }

        object Condiciones { get; set; }

        object Condicion { get; set; }

        void Mensaje(string mensaje);
    }
}
