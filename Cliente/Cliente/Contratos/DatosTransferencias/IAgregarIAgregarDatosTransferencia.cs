
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.DatosTransferencias
{
    interface IAgregarDatosTransferencia : IPaginaBase
    {
        object DatosTransferencia { get; set; }
    }
}
