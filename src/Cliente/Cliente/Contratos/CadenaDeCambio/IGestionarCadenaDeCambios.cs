using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.CadenaDeCambio
{
    interface IGestionarCadenaDeCambios : IPaginaBase
    {
        object CadenaDeCambios { get; set; }

        string IdCadenaDeCambios { get; set; }

        string CodigoOperacionCadenaCambios { get; set; }

        object TiposCadenaCambios { get; set; }

        object TipoCadenaCambios { get; set; }

        string TextoBotonModificar { get; set; }

        bool HabilitarCampos { set; }

        string IdCarta { get; set; }

        void Mensaje(string mensaje, int opcion);

        void MostarBotonOperaciones();
    }
}
