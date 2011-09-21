using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Paises
{
    interface IConsultarPais : IPaginaBase
    {
        object Pais { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }        
    }
}
