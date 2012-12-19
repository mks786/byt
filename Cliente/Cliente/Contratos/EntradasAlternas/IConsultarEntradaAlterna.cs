
namespace Trascend.Bolet.Cliente.Contratos.EntradasAlternas
{
    interface IConsultarEntradaAlterna : IPaginaBase
    {
        object EntradaAlterna { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object Medio { get; set; }

        object Medios { get; set; }

        object Receptor { get; set; }

        object Receptores { get; set; }

        object Remitente { get; set; }

        object Remitentes { get; set; }

        object Categoria { get; set; }

        object Categorias { get; set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        object Persona { get; set; }

        object Personas { get; set; }

        object Horas { get; }

        object Hora { get; set; }

        object Minutos { get; }

        object Minuto { get; set; }

        char GetTipoDestinatario { get; }

        string SetTipoDestinatario { set; }

        string SetHora { set; }

        string SetMinuto { set; }
    }
}
