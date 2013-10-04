using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInstruccionDeDescuentoMarca : IPaginaBase
    {
        
        object InstruccionDescuento { get; set; }

        string CodigoOperacion { get; set; }

        object Servicios { get; set; }

        object Servicio { get; set; }

        string Descuento { get; set; }

        string IdCorrespondencia { get; set; }

        string Observaciones { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ConvertirEnteroMinimoABlanco();

    }
}
