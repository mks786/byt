
namespace Trascend.Bolet.Cliente.Contratos.Anexos
{
    interface IAgregarAnexo : IPaginaBase
    {
        object Anexo { get; set; }

        void Mensaje(string mensaje);
    }
}
