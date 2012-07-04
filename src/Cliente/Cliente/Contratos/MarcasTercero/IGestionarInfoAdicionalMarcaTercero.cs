using System.Windows.Controls;
using System.Windows;

namespace Trascend.Bolet.Cliente.Contratos.MarcasTercero
{
    interface IGestionarInfoAdicionalMarcaTercero : IPaginaBase
    {
        object InfoAdicional { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        void PintarAuditoria();

        void OculatarControlesAlAgregar();

        string Tab { get; }
    }
}
