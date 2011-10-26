
namespace Trascend.Bolet.Cliente.Contratos.Paises
{
    interface IAgregarPais : IPaginaBase
    {
        object Pais { get; set; }

        string Region { get; }

        void Mensaje(string mensaje);
    }
}
