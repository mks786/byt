using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosInstruccionDescuento;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInstruccionDescuento
    {

        public static ComandoBase<IList<InstruccionDescuento>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasInstruccionesDescuento();
        }

        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InstruccionDescuento instruccion)
        {
            return new ComandoInsertarOModificarInstruccionDeDescuento(instruccion);
        }

        public static ComandoBase<IList<InstruccionDescuento>> ObtenerComandoConsultarInstruccionesDeDescuentoMarcaOPatente(InstruccionDescuento instruccionFiltro)
        {
            return new ComandoConsultarInstruccionesDeDescuentoMarcaOPatente(instruccionFiltro);
        }
    }
}
