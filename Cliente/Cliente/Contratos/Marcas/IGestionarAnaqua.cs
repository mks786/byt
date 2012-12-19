using System.Windows.Controls;
using System.ComponentModel;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarAnaqua : IPaginaBase
    {
        object Anaqua { get; set; }

        bool HabilitarCampos { set; }
        
        string TextoBotonModificar { get; set; }

        void Mensaje(string mensaje);

        void DatosMarca(string codigoRegistro, string codigoSolicitud);
    }
}
