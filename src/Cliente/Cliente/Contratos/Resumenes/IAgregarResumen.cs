
namespace Trascend.Bolet.Cliente.Contratos.Resumenes
{
    interface IAgregarBoletin : IPaginaBase
    {
        object Resumen { get; set; }

        void Mensaje(string mensaje);
    }
}