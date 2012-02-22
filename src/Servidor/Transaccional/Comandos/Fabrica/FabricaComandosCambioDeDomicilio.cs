using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCambioDeDomicilio;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCambioDeDomicilio
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un CambioDeDomicilio
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CambioDeDomicilio cambioDeDomicilio)
        {
            return new ComandoInsertarOModificarCambioDeDomicilio(cambioDeDomicilio);
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
        public static ComandoBase<IList<CambioDeDomicilio>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCambioDeDomicilio();
        }

        public static ComandoBase<CambioDeDomicilio> ObtenerComandoConsultarPorID(CambioDeDomicilio cambioDeDomicilio)
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<bool> ObtenerComandoEliminarCambioDeDomicilio(CambioDeDomicilio cambioDeDomicilio)
        {
            return new ComandoEliminarCambioDeDomicilio(cambioDeDomicilio);
        }

        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCambioDeDomicilio(CambioDeDomicilio cambioDeDomicilio)
        {
            return new ComandoVerificarExistenciaCambioDeDomicilio(cambioDeDomicilio);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarFusionesFiltro
        /// </summary>
        /// <param name="fusion">Fusion a consultar</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>
        public static ComandoBase<IList<CambioDeDomicilio>> ObtenerComandoConsultarCambiosDeDomicilioFiltro(CambioDeDomicilio cambioDeDomicilio)
        {
            return new ComandoConsultarCambiosDeDomicilioFiltro(cambioDeDomicilio);
        }
    }
}
