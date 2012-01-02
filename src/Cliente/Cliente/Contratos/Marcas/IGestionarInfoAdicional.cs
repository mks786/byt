using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInfoAdicional : IPaginaBase
    {
        object InfoAdicional { get; set; }

        bool HabilitarCampos { set; }
        
        string TextoBotonModificar { get; set; }

        void Mensaje(string mensaje);
    }
}
