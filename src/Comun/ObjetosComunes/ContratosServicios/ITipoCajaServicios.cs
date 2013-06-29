using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ITipoCajaServicios: IServicioBase<TipoCaja>
    {

        /// <summary>
        /// Servicio para obtener los tipos de cajas de Marcas o los tipos de cajas de Patentes
        /// </summary>
        /// <param name="parametro1">Char para indicar Marca o Patente Nacional</param>
        /// <param name="parametro2">Char para indicar Marca o Patente Internacional</param>
        /// <returns></returns>
        IList<TipoCaja> ObtenerTipoCajaMarcaOPatente(String parametro1);

    }
}
