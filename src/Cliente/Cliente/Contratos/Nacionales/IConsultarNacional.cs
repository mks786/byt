
namespace Trascend.Bolet.Cliente.Contratos.Nacionales
{
    interface IConsultarNacional : IPaginaBase
    {
        object Nacional { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
