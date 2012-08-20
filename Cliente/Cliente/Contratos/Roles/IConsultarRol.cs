using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Roles
{
    interface IConsultarRol : IPaginaBase
    {
        object Rol { get; set; }

        string Id { get; }

        string Descripcion { get; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
