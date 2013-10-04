using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInstruccionNoTipificadaMarca : IPaginaBase
    {
        object InstruccionNoTipificada { get; set; }

        string CodigoOperacion { get; set; }

        string Descripcion { get; set; }

        string IdCorrespondencia { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
