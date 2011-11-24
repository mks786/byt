
namespace Trascend.Bolet.Cliente.Contratos.Agentes
{
    interface IAgregarAgente : IPaginaBase
    {
        object Agente { get; set; }

        char EstadoCivil { get; }

        object Sexo { get; set; }

        object Sexos { get; set; }

        void Mensaje(string mensaje);
    }
}
