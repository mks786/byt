
namespace Trascend.Bolet.Cliente.Contratos.Agentes
{
    interface IAgregarAgente : IPaginaBase
    {
        object Agente { get; set; }

        char EstadoCivil { get; }

        char Sexo { get; }
    }
}
