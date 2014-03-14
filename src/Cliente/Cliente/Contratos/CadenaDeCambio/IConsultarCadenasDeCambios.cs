using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.CadenaDeCambio
{
    interface IConsultarCadenasDeCambios : IPaginaBase
    {
        object CadenaDeCambios { get; set; }

        object TiposCadenasDeCambios { get; set; }

        object TipoCadenaDeCambios { get; set; }

        string TotalHits { set; }

        object Resultados { get; set; }

        object CadenaCambioSeleccionada { get; set; }

        string IdCadenaCambios { get; set; }

        string CodigoOperacionCadenaCambios { get; set; }

        string FechaCadenaCambios { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
