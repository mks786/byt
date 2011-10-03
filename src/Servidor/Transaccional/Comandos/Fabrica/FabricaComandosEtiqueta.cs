using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosEtiqueta;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosEtiqueta
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una etiqueta
        /// </summary>
        /// <param name="etiqueta">Etiqueta a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Etiqueta etiqueta)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="etiqueta"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarEtiqueta(Etiqueta etiqueta)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devulve una lista con todas las etiquetas
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Etiqueta>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosEtiquetas();
        }
    }
}
