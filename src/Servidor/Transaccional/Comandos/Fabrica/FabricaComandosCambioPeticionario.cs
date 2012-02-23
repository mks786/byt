using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCambioPeticionario;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCambioPeticionario
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un CambioPeticionario
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionario a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CambioPeticionario cambioPeticionario)
        {
            return new ComandoInsertarOModificarCambioPeticionario(cambioPeticionario);
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
        public static ComandoBase<IList<CambioPeticionario>> ObtenerComandoConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<CambioPeticionario> ObtenerComandoConsultarPorID(CambioPeticionario cambioPeticionario)
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<bool> ObtenerComandoEliminarCambioPeticionario(CambioPeticionario cambioPeticionario)
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCambioPeticionario(CambioPeticionario cambioPeticionario)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo que obtiene el comando ComandoConsultarCambiosPeticionarioFiltro
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionario a consultar</param>
        /// <returns>Lista de CambioPeticionarios que cumplan con el filtro</returns>
        public static ComandoBase<IList<CambioPeticionario>> ObtenerComandoConsultarCambioPeticionarioFiltro(CambioPeticionario cambioPeticionario)
        {
            return new ComandoConsultarCambiosPeticionarioFiltro(cambioPeticionario);
        }
    }
}
