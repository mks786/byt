using System.Windows.Controls;
using System.Windows;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInfoBol : IPaginaBase
    {
        object InfoBol { get; set; }

        object Boletin { get; set; }

        object Boletines { get; set; }

        object Tomo { get; set; }

        object Tomos { get; set; }

        object Tipo { get; set; }

        object Tipos { get; set; }

        object Cambio { get; set; }

        object Cambios { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        void OculatarControlesAlAgregar();

        string TextoCambio { get; set; }

        void BorrarTextoCambio();

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void ActivarBotonRevisar(bool estado);

    }
}
