using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInfoAdicional : IPaginaBase
    {
        object InfoAdicional { get; set; }

        bool HabilitarCampos { set; }
        
        string TextoBotonModificar { get; set; }

        bool Mensaje(string mensaje);
    }
}
