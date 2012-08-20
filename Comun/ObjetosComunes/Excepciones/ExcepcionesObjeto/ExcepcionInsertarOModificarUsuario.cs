using System;

namespace Trascend.Bolet.ObjetosComunes.Excepciones.ExcepcionesObjeto
{
    class ExcepcionInsertarOModificarUsuario : ExcepcionBase
    {
        private readonly string mensaje = "Ocurrió un error insertando el objeto en base de datos";

        public ExcepcionInsertarOModificarUsuario()
        {
            base.Mensaje = mensaje;
        }

        public ExcepcionInsertarOModificarUsuario(Exception excepcionInterna)
        {
            base.ExcepcionInterna = excepcionInterna;
            base.Mensaje = mensaje;
        }
    }
}

