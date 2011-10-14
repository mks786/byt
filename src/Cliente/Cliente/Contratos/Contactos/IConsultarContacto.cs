using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Contactos
{
    interface IConsultarContacto : IPaginaBase
    {
        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object Contacto { get; set; }
    }
}
