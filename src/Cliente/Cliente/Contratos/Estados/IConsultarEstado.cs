
namespace Trascend.Bolet.Cliente.Contratos.Estados
{
    interface IConsultarEstado : IPaginaBase
    {
        object Estado { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
