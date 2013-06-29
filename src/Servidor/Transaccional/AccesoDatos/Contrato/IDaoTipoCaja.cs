using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoTipoCaja : IDaoBase<TipoCaja, string>
    {
        /// <summary>
        /// Metodo para obtener los tipos de cajas ya sea por Marca o por Patente
        /// </summary>
        /// <param name="parametroFiltro">Filtro para Marca o Patente</param>
        /// <returns>Lista de Tipos de Cajas por Marca o Por Patente</returns>
        IList<TipoCaja> ObtenerTiposCajasMarcaOPatente(string parametroFiltro);
    }
}
