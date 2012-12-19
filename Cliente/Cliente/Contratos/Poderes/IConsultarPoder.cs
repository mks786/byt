
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.Poderes
{
    interface IConsultarPoder : IPaginaBase
    {
        object Poder { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        void ArchivoNoEncontrado();

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string NombreInteresado { set; }

        object Agente { get; set; }

        object Agentes { get; set; }

        object Apoderado { get; set; }

        object Apoderados { get; set; }

        string IdInteresadoConsultar { get; }

        string NombreInteresadoConsultar { get; }

        bool ConfirmarModificacion(string mensaje);
    }
}
