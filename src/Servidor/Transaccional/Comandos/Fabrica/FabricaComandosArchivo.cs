using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosArchivo;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosArchivo
    {
        /// <summary>
        /// Metodo para obtener el comando para consultar todos los Archivos 
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Archivo>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosArchivos();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Archivo por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Archivo> ObtenerComandoConsultarPorID(Archivo archivo)
        {
            return new ComandoConsultarArchivoPorID(archivo);
        }


        public static ComandoBase<Archivo> ObtenerComandoObtenerArchivoDeMarcaOPatenteInternacional(Archivo archivo)
        {
            return new ComandoObtenerArchivoDeMarcaOPatenteInternacional(archivo);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar un Archivo de marca o de patente
        /// </summary>
        /// <param name="infoBol">Archivo a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Archivo archivo)
        {
            return new ComandoInsertarOModificarArchivo(archivo);
        }

    }
}
