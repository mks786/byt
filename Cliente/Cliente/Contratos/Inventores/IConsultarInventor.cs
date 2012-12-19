using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Inventores
{
    interface IConsultarInventor : IPaginaBase
    {
        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object Inventor { get; set; }

        object Paises { get; set; }

        object Pais { get; set; }

        object Nacionalidades { get; set; }

        object Nacionalidad { get; set; }
        
        void mensaje(string mensaje);
    }
}
