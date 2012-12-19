using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCambioPeticionarioPatente;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCambioPeticionarioPatente
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un CambioPeticionarioPatente
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CambioPeticionarioPatente cambioPeticionario)
        {
            return new ComandoInsertarOModificarCambioPeticionarioPatente(cambioPeticionario);
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
        /// Método que devuelve el Comando para consultar todos los CambioPeticionarioPatentes
        /// </summary>
        /// <returns>El Comando para consultar todos los CambioPeticionarioPatentes</returns>
        public static ComandoBase<IList<CambioPeticionarioPatente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCambioPeticionarioPatente();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un CambioPeticionarioPatente por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<CambioPeticionarioPatente> ObtenerComandoConsultarPorID(CambioPeticionarioPatente cambioPeticionario)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un cambio Peticionario
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCambioPeticionarioPatente(CambioPeticionarioPatente cambioPeticionario)
        {
            return new ComandoEliminarCambioPeticionarioPatente(cambioPeticionario);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCambioPeticionarioPatente(CambioPeticionarioPatente cambioPeticionario)
        {
            return new ComandoVerificarExistenciaCambioPeticionarioPatente(cambioPeticionario);
        }

        /// <summary>
        /// Metodo que obtiene el comando ComandoConsultarCambiosPeticionarioFiltro
        /// </summary>
        /// <param name="cambioPeticionario">CambioPeticionarioPatente a consultar</param>
        /// <returns>Lista de CambioPeticionarioPatentes que cumplan con el filtro</returns>
        public static ComandoBase<IList<CambioPeticionarioPatente>> ObtenerComandoConsultarCambioPeticionarioPatenteFiltro(CambioPeticionarioPatente cambioPeticionario)
        {
            return new ComandoConsultarCambiosPeticionarioPatenteFiltro(cambioPeticionario);
        }
    }
}
