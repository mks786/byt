
namespace Trascend.Bolet.Cliente.Contratos.Resumenes
{
    interface IConsultarBoletin : IPaginaBase
    {
        object Resumen { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
