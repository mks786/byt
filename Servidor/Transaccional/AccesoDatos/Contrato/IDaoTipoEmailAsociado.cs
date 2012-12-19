using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoTipoEmailAsociado : IDaoBase<TipoEmailAsociado, string>
    {


        /// <summary>
        /// Metodo que inserta una carata en la base de datos
        /// </summary>
        /// <param name="TipoEmailAsociado">parametro a insertar</param>
        /// <param name="transaccion">objetoITransicion indica si se realizo el Commit</param>
        /// <returns>Bool si se inserto correctamente, de lo contrario false</returns>
        bool Insertar(TipoEmailAsociado TipoEmailAsociado, ITransaction transaccion);

    }
}
