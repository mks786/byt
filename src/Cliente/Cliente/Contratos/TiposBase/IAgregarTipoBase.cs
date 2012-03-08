
namespace Trascend.Bolet.Cliente.Contratos.TiposBase
{
    interface IAgregarTipoBase : IPaginaBase
    {
        object TipoBase { get; set; }

        void Mensaje(string mensaje);
    }
}