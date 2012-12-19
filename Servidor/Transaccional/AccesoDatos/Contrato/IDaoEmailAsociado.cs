using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoEmailAsociado : IDaoBase<EmailAsociado, int>
    {


        /// <summary>
        /// Metodo que inserta una cara en la base de datos
        /// </summary>
        /// <param name="EmailAsociado">parametro a insertar</param>
        /// <param name="transaccion">objetoITransicion indica si se realizo el Commit</param>
        /// <returns>Bool si se inserto correctamente, de lo contrario false</returns>
        bool Insertar(EmailAsociado EmailAsociado, ITransaction transaccion);

    }
}
