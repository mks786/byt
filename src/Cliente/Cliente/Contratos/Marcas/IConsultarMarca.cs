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

        object PoderSolicitud { get; set; }

        object PoderDatos { get; set; }

        object Agente { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinConcesion { get; set; }

        object Servicio { get; set; }

        object Detalle { get; set; }

        object Condiciones { get; set; }

        void Mensaje(string mensaje);
    }
}
