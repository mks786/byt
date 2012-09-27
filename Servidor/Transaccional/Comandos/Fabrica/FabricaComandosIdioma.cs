using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosIdioma;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosIdioma
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un idioma
        /// </summary>
        /// <param name="idioma">Idioma a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Idioma idioma)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idioma"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarIdioma(Idioma idioma)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devulve una lista con todas los idiomas
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Idioma>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosIdiomas();
        }
    }
}
