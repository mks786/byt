using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Traspasos.Fusiones
{
    interface IGestionarFusion : IPaginaBase
    {
        object Fusion { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        string IdInteresadoSobrevivienteFiltrar { get; }

        string NombreInteresadoSobrevivienteFiltrar { get; }

        string IdAgenteFiltrar { get; }

        string NombreAgenteFiltrar { get; }

        //ListView Marcas

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

        //-----------

        //ListView InteresadoEntre

        string IdInteresadoEntreFiltrar { get; }

        string NombreInteresadoEntreFiltrar { get; }

        object InteresadosEntreFiltrados { get; set; }

        object InteresadoEntreFiltrado { get; set; }

        //-----------

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string NombreMarca { set; }

        object Marca { get; set; }

        string NombreInteresadoEntre { set; }

        object InteresadoEntre { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }
    }
}
