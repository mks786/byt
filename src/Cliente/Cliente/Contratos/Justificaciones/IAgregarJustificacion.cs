
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.Justificaciones
{
    interface IAgregarJustificacion : IPaginaBase
    {
        object Justificacion { get; set; }

        void BorrarId();

        void mensaje(string mensaje);

        object Conceptos { get; set; }

        object Concepto { get; set; }
    }
}
