
namespace Trascend.Bolet.Cliente.Contratos.Resumenes
{
    interface IConsultarResumen : IPaginaBase
    {
        object Resumen { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
