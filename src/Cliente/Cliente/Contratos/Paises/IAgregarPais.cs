
namespace Trascend.Bolet.Cliente.Contratos.Paises
{
    interface IAgregarPais : IPaginaBase
    {
        object Pais { get; set; }

        void Mensaje(string mensaje);
    }
}
