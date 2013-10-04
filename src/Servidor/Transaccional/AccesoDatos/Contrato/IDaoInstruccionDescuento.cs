using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInstruccionDescuento : IDaoBase<InstruccionDescuento,int>
    {
        IList<InstruccionDescuento> ObtenerInstruccionesDeDescuentoMarcaOPatente(InstruccionDescuento instruccionDescuento);
    }
}
