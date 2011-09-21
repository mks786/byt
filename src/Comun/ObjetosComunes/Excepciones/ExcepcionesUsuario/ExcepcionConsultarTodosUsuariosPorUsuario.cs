using System;
namespace Trascend.Bolet.ObjetosComunes.Excepciones.ExcepcionesUsuario
{
    public class ExcepcionTodosUsuariosPorUsuario : ExcepcionBase
    {
        private readonly string mensaje = "Ocurrió un error al consultar en base de datos los usuarios filtrando por usuario";

        public ExcepcionTodosUsuariosPorUsuario()
        {
            base.Mensaje = mensaje;
        }

        public ExcepcionTodosUsuariosPorUsuario(Exception excepcionInterna)
        {
            base.ExcepcionInterna = excepcionInterna;
            base.Mensaje = mensaje;
        }
    }
}
