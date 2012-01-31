using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCesion;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCesion
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Cesion
        /// </summary>
        /// <param name="cesion">Cesion a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Cesion cesion)
        {
            return new ComandoInsertarOModificarCesion(cesion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Objeto objeto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Cesion>> ObtenerComandoConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<Cesion> ObtenerComandoConsultarPorID(Cesion cesion)
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<bool> ObtenerComandoEliminarCesion(Cesion cesion)
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCesion(Cesion cesion)
        {
            throw new NotImplementedException();
        }
    }
}
