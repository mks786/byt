
namespace Trascend.Bolet.Cliente.Contratos.EntradasAlternas
{
    interface IAgregarEntradaAlterna : IPaginaBase
    {
        object EntradaAlterna { get; set; }

        void Mensaje(string mensaje);

        object Medio { get; set; }

        object Medios { get; set; }

        object Receptor { get; set; }

        object Receptores { get; set; }

        object Remitente { get; set; }

        object Remitentes { get; set; }

        object Categoria { get; set; }

        object Categorias { get; set; }

        char TipoDestinatario { get; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        object Persona { get; set; }

        object Personas { get; set; }

        object Horas { get; }

        object Hora { get; set; }

        object Minutos { get; }

        object Minuto { get; set; }

        object TiposAcuse { get; set; }

        object TipoAcuse { get; set; }

        string Destinatario { get; set; }

        void BorrarCeros();

        void MostrarCampoDestinatario();

        void OcultarCampoDestinatario();
    }
}