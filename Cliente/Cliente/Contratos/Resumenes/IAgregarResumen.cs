
namespace Trascend.Bolet.Cliente.Contratos.Resumenes
{
    interface IAgregarResumen : IPaginaBase
    {
        object Resumen { get; set; }

        void Mensaje(string mensaje);
    }
}