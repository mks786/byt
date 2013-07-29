using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInfoAdicional : IDaoBase<InfoAdicional, string>
    {

        /// <summary>
        /// Metod que obtiene todas las InfoAdicionales por Distingue en Ingles
        /// </summary>
        /// <param name="infoAdicional">InfoAdicional que sirve para la consulta</param>
        /// <returns>Lista de InfoAdicionales</returns>
        IList<InfoAdicional> ObtenerInfoAdicionalDistingueInglesFiltro(InfoAdicional infoAdicional);


    }
}
