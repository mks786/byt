using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosContacto;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosContacto
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un contacto
        /// x
        /// </summary>
        /// <param name="moneda">contacto a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Contacto contacto)
        {
            return new ComandoInsertarOModificarContacto(contacto);
        }

        /// <summary>
        /// Método que devuelve el comando para eliminar a un contacto
        /// </summary>
        /// <param name="contacto">contacto a eliminar</param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarContacto(Contacto contacto)
        {
            return new ComandoEliminarContacto(contacto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Contacto>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosContactos();
        }

        /// <summary>
        /// Método que devuelve el comando para consultar todos los contactos de un asociado
        /// </summary>
        /// <param name="asociado"></param>
        /// <returns></returns>
        public static ComandoBase<IList<Contacto>> ObtenerComandoConsultarContactosPorAsociado(Asociado asociado)
        {
            return new ComandoConsultarContactosPorAsociado(asociado);
        }
    }
}
