using System;
namespace Trascend.Bolet.ObjetosComunes.Excepciones.ExcepcionesUsuario
{
    public class ExcepcionInsertarOModificarUsuario : ExcepcionBase
    {
        private readonly string mensaje = "Ocurrió un error insertando el usuario en base de datos";

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
