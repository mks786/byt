
namespace Trascend.Bolet.Cliente.Contratos.Interesados
{
    interface IConsultarInteresado : IPaginaBase
    {
        object Interesado { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        char GetTipoPersona { get; }

        string SetTipoPersona { set; }

        object Paises { get; set; }

        object Pais { get; set; }

        object Nacionalidades { get; set; }

        object Nacionalidad { get; set; }

        object Corporaciones { get; set; }

        object Corporacion { get; set; }
    }
}
