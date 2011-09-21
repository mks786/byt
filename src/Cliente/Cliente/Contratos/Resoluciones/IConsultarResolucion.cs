
namespace Trascend.Bolet.Cliente.Contratos.Resoluciones
{
    interface IConsultarResolucion : IPaginaBase
    {
        object Resolucion { get; set; }

        bool HabilitarCampos { set; }

        bool DeshabilitarFecha { set; }

        string TextoBotonModificar { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }
    }
}
