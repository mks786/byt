using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCambioDeDomicilioPatente;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCambioDeDomicilioPatente
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un CambioDeDomicilioPatente
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            return new ComandoInsertarOModificarCambioDeDomicilioPatente(cambioDeDomicilio);
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

        public static ComandoBase<IList<CambioDeDomicilioPatente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCambioDeDomicilioPatente();
        }

        public static ComandoBase<CambioDeDomicilioPatente> ObtenerComandoConsultarPorID(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            throw new NotImplementedException();
        }

        public static ComandoBase<bool> ObtenerComandoEliminarCambioDeDomicilio(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            return new ComandoEliminarCambioDeDomicilioPatente(cambioDeDomicilio);
        }

        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCambioDeDomicilio(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            return new ComandoVerificarExistenciaCambioDeDomicilioPatente(cambioDeDomicilio);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarFusionesFiltro
        /// </summary>
        /// <param name="fusion">Fusion a consultar</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>
        public static ComandoBase<IList<CambioDeDomicilioPatente>> ObtenerComandoConsultarCambiosDeDomicilioPatenteFiltro(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            return new ComandoConsultarCambiosDeDomicilioPatenteFiltro(cambioDeDomicilio);
        }
    }
}
