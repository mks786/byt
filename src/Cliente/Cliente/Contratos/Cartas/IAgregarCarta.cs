
namespace Trascend.Bolet.Cliente.Contratos.Cartas
{
    interface IAgregarCarta : IPaginaBase
    {
        object Carta { get; set; }

        void Mensaje(string mensaje);

        object Asociado { get; set; }

        object Asociados { get; set; }

        object Receptor { get; set; }

        object Receptores { get; set; }

        object Remitente { get; set; }

        object Remitentes { get; set; }

        object Responsable { get; set; }

        object Responsables { get; set; }

        object Resumen { get; set; }

        object Resumenes { get; set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        object Persona { get; set; }

        object Personas { get; set; }

        object Medio { get; set; }

        object Medios { get; set; }

        object Anexo { get; set; }

        object Anexos { get; set; }

        object AnexoCarta { get; set; }

        object AnexosCarta { get; set; }

        object AnexoConfirmacion { get; set; }

        object AnexosConfirmacion { get; set; }

        object AnexoConfirmacionCarta { get; set; }

        object AnexosConfirmacionCarta { get; set; }

    }
}