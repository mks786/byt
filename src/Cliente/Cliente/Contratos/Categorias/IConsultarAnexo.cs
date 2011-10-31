using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Categorias
{
    interface IConsultarCategoria : IPaginaBase
    {
        object Categoria { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }        
    }
}
