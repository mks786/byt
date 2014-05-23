
namespace Trascend.Bolet.Cliente.Contratos.Remitentes
{
    interface IConsultarRemitente : IPaginaBase
    {
        object Remitente { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        char GetTipoRemitente { get; }

        string SetTipoRemitente { set; }

        object Paises { get; set; }

        object Pais { get; set; }

        void MostrarBotonSeleccionarRemitente();
    }
}
