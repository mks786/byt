
namespace Trascend.Bolet.Cliente.Contratos.Agentes
{
    interface IAgregarAgente : IPaginaBase
    {
        object Agente { get; set; }

        object EstadosCivil { get; set; }

        object EstadoCivil { get; set; }

        object Sexo { get; set; }

        object Sexos { get; set; }

        void Mensaje(string mensaje);
    }
}
