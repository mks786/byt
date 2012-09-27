using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Objetos
{
    interface IConsultarObjeto : IPaginaBase
    {
        object Objeto { get; set; }

        string Id { get; }

        string Descripcion { get; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
