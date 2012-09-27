
namespace Trascend.Bolet.Cliente.Contratos.Nacionales
{
    interface IAgregarNacional: IPaginaBase
    {
        object Nacional { get; set; }

        void Mensaje(string mensaje);
    }
}