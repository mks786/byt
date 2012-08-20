using System;

namespace Trascend.Bolet.ObjetosComunes.Excepciones.ExcepcionesObjeto
{
    public class ExcepcionInsertarOModificarObjeto : ExcepcionBase
    {
        private readonly string mensaje = "Ocurrió un error insertando el objeto en base de datos";

        public ExcepcionInsertarOModificarObjeto()
        {
            base.Mensaje = mensaje;
        }

        public ExcepcionInsertarOModificarObjeto(Exception excepcionInterna)
        {
            base.ExcepcionInterna = excepcionInterna;
            base.Mensaje = mensaje;
        }
    }
}

