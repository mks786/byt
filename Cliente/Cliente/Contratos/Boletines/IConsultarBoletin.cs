
namespace Trascend.Bolet.Cliente.Contratos.Boletines
{
    interface IConsultarBoletin : IPaginaBase
    {
        object Boletin { get; set; }

        bool HabilitarCampos { set; }

        bool DeshabilitarFecha { set; }

        string TextoBotonModificar { get; set; }
    }
}
