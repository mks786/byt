
namespace Trascend.Bolet.Cliente.Contratos.Cartas
{
    interface IConsultarCarta : IPaginaBase
    {
        object Carta { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object Asociado { get; set; }

        string NombreAsociado { get; set; }

        string CodigoAsociado { get; set; }

        string ContadorCartas { get; set; }

        string idAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Acuse { get; set; }

        object AcuseLista { get; set; }

        object Receptor { get; set; }

        object Receptores { get; set; }

        object Remitente { get; set; }

        object Remitentes { get; set; }

        object Responsable { get; set; }

        object Responsables { get; set; }

        object ResponsableList { get; set; }

        object ResponsablesList { get; set; }

        object Resumen { get; set; }

        object Resumenes { get; set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        object Persona { get; set; }

        object Personas { get; set; }

        object Medio { get; set; }

        object Medios { get; set; }

        //object MedioTracking { get; set; }

        //object MediosTracking { get; set; }

        object MedioTrackingConfirmacion { get; set; }

        object MediosTrackingConfirmacion { get; set; }

        object Anexo { get; set; }

        object Anexos { get; set; }

        object AnexoCarta { get; set; }

        object AnexosCarta { get; set; }

        object AnexoConfirmacion { get; set; }

        object AnexosConfirmacion { get; set; }

        object AnexoConfirmacionCarta { get; set; }

        object AnexosConfirmacionCarta { get; set; }

        string FormatoTracking { get; set; }

        string FormatoTrackingConfirmacion { get; set; }

        void SetFechaConfirmacion(string fecha);
    }
}
