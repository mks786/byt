
namespace Trascend.Bolet.Cliente.Contratos.EstadosMarca
{
    interface IAgregarEstadoMarca : IPaginaBase
    {
        object EstadoMarca { get; set; }

        void Mensaje(string mensaje);
    }
}
