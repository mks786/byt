
namespace Trascend.Bolet.Cliente.Contratos.Corresponsales
{
    interface IAgregarCorresponsal : IPaginaBase
    {
        object Corresponsal { get; set; }

        object Paises { get; set; }

        object Pais { get; set; }

        object Idioma { get; set; }

        object Idiomas { get; set; }

        void Mensaje(string mensaje);

        void MostrarBotones(string texto);
    }
}
