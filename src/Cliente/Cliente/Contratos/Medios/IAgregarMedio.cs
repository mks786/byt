
using System.IO;
namespace Trascend.Bolet.Cliente.Contratos.Medios
{
    interface IAgregarMedio : IPaginaBase
    {
        object Medio { get; set; }

        Stream Imagen { get; set; }

        FileInfo InformacionImagen { get; set; }

        void Mensaje(string mensaje);
    }
}
