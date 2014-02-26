
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.Poderes
{
    interface IAgregarPoder : IPaginaBase
    {
        object Poder { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        string NombreInteresado { set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        object Agente { get; set; }

        object Agentes { get; set; }

        object Apoderado { get; set; }

        object Apoderados { get; set; }

        bool InteresadoEsEditable { get; set; }

        bool ConInteresado { get; set; }

        string TextoBotonCancelar { get; set; }

        string IdInteresadoConsultar { get; }

        string NombreInteresadoConsultar { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
