using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarAnaqua : IPaginaBase
    {
        object Anaqua { get; set; }

        bool HabilitarCampos { set; }
        
        string TextoBotonModificar { get; set; }

        void Mensaje(string mensaje);
    }
}
