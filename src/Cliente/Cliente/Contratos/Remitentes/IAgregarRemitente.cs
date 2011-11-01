
namespace Trascend.Bolet.Cliente.Contratos.Remitentes
{
    interface IAgregarRemitente : IPaginaBase
    {
        object Remitente { get; set; }

        char TipoRemitente { get; }

        object Paises { get; set; }

        object Pais { get; set; }
    }
}
