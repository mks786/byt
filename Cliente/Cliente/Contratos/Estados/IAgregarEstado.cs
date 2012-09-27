
namespace Trascend.Bolet.Cliente.Contratos.Estados
{
    interface IAgregarEstado : IPaginaBase
    {
        object Estado { get; set; }

        void Mensaje(string mensaje);
    }
}