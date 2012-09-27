
namespace Trascend.Bolet.Cliente.Contratos.Resoluciones
{
    interface IAgregarResolucion : IPaginaBase
    {
        object Resolucion { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        void Mensaje(string mensaje);
    }
}