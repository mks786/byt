using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoArchivo : IDaoBase<Archivo,string>
    {
        Archivo ConsultarContactoPorId(Archivo archivo);
    }
}
