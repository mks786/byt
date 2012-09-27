using System;

namespace Trascend.Bolet.ObjetosComunes.Excepciones.ExcepcionesObjeto
{
    [Serializable]
    public class ExcepcionConsultarTodosObjetos: PruebaEx
    {
         private readonly string mensaje = "Ocurrió un error consultando todos los objetos";

         public ExcepcionConsultarTodosObjetos()
             : base("uno", "dsadas")
        {
        }

        public ExcepcionConsultarTodosObjetos(string excepcionInterna)
        {
            base.Mensaje = excepcionInterna;
        }
    }
}
