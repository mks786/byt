
namespace Trascend.Bolet.Cliente.Contratos.Categorias
{
    interface IAgregarCategoria : IPaginaBase
    {
        object Categoria { get; set; }

        void Mensaje(string mensaje);
    }
}
