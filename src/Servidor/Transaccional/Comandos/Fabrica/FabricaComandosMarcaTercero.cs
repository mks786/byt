using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosMarcaTercero;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMarcaTercero
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un marcaTercero
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el marcaTercero en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(MarcaTercero marcaTercero)
        {
            return new ComandoInsertarOModificarMarcaTercero(marcaTercero);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los MarcaTerceros
        /// </summary>
        /// <returns>El Comando para consultar todos los MarcaTerceros</returns>
        public static ComandoBase<IList<MarcaTercero>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosMarcaTerceros();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un MarcaTercero
        /// </summary>
        /// <param name="usuario">MarcaTercero que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarMarcaTercero(MarcaTercero marcaTercero)
        {
            return new ComandoEliminarMarcaTercero(marcaTercero);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un MarcaTercero por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<MarcaTercero> ObtenerComandoConsultarPorID(MarcaTercero marcaTercero)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">MarcaTercero a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaMarcaTercero(MarcaTercero marcaTercero)
        {
            return new ComandoVerificarExistenciaMarcaTercero(marcaTercero);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">MarcaTercero a verificar</param>
     
        public static ComandoBase<IList<MarcaTercero>> ObtenerComandoConsultarMarcasTerceroFiltro(MarcaTercero marcaTercero)
        {
            return new ComandoConsultarMarcasTerceroFiltro(marcaTercero);
        }

        /// <summary>
        /// Método que devuelve el ultimo Id de la letra Enviada
        /// </summary>
        /// <param name="pais">MarcaTercero a verificar</param>
     
        public static ComandoBase<string> ObtenerComandoConsultarMarcaTerceroMaxId(string maxId)
        {
            return new ComandoConsultarMaxId(maxId);
        }

        /// <summary>
        /// Método que devuelve el ultimo Anexo insertado de una marcaTercero
        /// </summary>
        /// <param name="pais">MarcaTercero a verificar</param>
        public static ComandoBase<int> ObtenerComandoConsultarMarcaTerceroMaxAnexo(string maxAnexo)
        {
            return new ComandoConsultarMaxAnexo(maxAnexo);
        }
    }
}
