using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Excepciones.ExcepcionesObjeto
{
    public class ExcepcionConsultarTodosObjetosPorObjeto : ExcepcionBase
    {
        private readonly string mensaje = "Ocurrió un error al consultar en base de datos los usuarios filtrando por objetos";

        public ExcepcionConsultarTodosObjetosPorObjeto()
        {
            base.Mensaje = mensaje;
        }

        public ExcepcionConsultarTodosObjetosPorObjeto(Exception excepcionInterna)
        {
            base.ExcepcionInterna = excepcionInterna;
            base.Mensaje = mensaje;
        }
    }
}
