using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Objetos
{
    interface IAgregarObjeto : IPaginaBase
    {
        object Objeto { get; set; }

        string Id { get; }

        string Descripcion { get; }
    }
}
