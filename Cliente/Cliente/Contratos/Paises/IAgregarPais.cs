
namespace Trascend.Bolet.Cliente.Contratos.Paises
{
    interface IAgregarPais : IPaginaBase
    {
        object Pais { get; set; }

        object Region { get; set; }

        object Regiones { get; set; }

        void Mensaje(string mensaje);
    }
}
