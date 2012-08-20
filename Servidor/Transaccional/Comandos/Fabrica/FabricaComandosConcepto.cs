using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosConcepto;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosConcepto
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una moneda
        /// x
        /// </summary>
        /// <param name="moneda">Moneda a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Concepto concepto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moneda"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarMoneda(Concepto concepto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devulve una lista con todas las monedas
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Concepto>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosConceptos();
        }
    }
}
