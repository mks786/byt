
namespace Trascend.Bolet.Cliente.Contratos.Medios
{
    interface IAgregarMedio : IPaginaBase
    {
        object Medio { get; set; }

        void Mensaje(string mensaje);
    }
}
