using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Memorias
{
    interface IConsultarMemoria : IPaginaBase
    {
        object Memoria { get; set; }

        string SetPatente { set; }

        object TipoMensaje { get; set; }

        object TiposMensajes { get; set; }

        object FormatosDocumentos { get; set; }

        object FormatoDocumento { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        void ArchivoNoEncontrado();
    }
}
