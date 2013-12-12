
namespace Trascend.Bolet.Cliente.Contratos.Interesados
{
    interface IConsultarInteresado : IPaginaBase
    {
        object Interesado { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object TipoPersona { get; set; }

        object TipoPersonas { get; set; }

        object Paises { get; set; }

        object Pais { get; set; }

        object Nacionalidades { get; set; }

        object Nacionalidad { get; set; }

        object Corporaciones { get; set; }

        object Corporacion { get; set; }

        string Ciudad { get; set; }

        string Estado { get; set; }

        object OrigenesClientes { get; set; }

        object OrigenCliente { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
