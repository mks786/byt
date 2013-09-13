using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.ReportesMaestro
{
    interface IGestionarReporte : IPaginaBase
    {

        object ReporteDeMarca { get; set; }

        string IdReporte { get; set; }

        string DescripcionReporte { get; set; }

        string TituloReporte { get; set; }

        object Usuarios { get; set; }

        object Usuario { get; set; }

        object Idiomas { get; set; }

        object Idioma { get; set; }

        object CamposReporte { get; set; }

        object CampoReporte { get; set; }

        object CamposSeleccionados { get; set; }

        object CampoSeleccionado { get; set; }

        object TiposDeReporte { get; set; }

        object TipoDeReporte { get; set; }

        bool HabilitarCampos { set; }

        void PintarFiltros();

        void ActivarBotonFiltros(bool valor);

        void Mensaje(string mensaje, int opcion);

        void InicializarVistaReporte();
    }
}
