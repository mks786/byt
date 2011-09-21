using System;

namespace Trascend.Bolet.ObjetosComunes.Excepciones.ExcepcionesUsuario
{
    public class ExcepcionConsultarTodosUsuarios : ExcepcionBase
    {
         private readonly string mensaje = "Ocurrió un error consultando todos los usuarios";

        public ExcepcionConsultarTodosUsuarios()
        {
            base.Mensaje = mensaje;
        }

        public ExcepcionConsultarTodosUsuarios(Exception excepcionInterna)
        {
            base.ExcepcionInterna = excepcionInterna;
            base.Mensaje = mensaje;
        }
    }
}
