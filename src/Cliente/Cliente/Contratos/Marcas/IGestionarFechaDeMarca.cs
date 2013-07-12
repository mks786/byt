using System.Windows.Controls;
using System.Windows;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarFechaDeMarca : IPaginaBase
    {
        object FechaMarca { get; set; }

        object TiposDeFechas { get; set; }

        object TipoDeFecha { get; set; }

        string IdCorrespondencia { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
