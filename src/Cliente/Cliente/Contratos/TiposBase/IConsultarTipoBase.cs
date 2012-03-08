
namespace Trascend.Bolet.Cliente.Contratos.TiposBase
{
    interface IConsultarTipoBase : IPaginaBase
    {
        object TipoBase { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
