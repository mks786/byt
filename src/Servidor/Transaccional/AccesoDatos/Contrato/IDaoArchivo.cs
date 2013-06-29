using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoArchivo : IDaoBase<Archivo,string>
    {
        Archivo ConsultarArchivoPorId(Archivo archivo);

        Archivo ObtenerArchivoDeMarcaOPatenteInternacional(Archivo archivo);
    }
}
