using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosMaterialSapi;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMaterialSapi
    {
        /// <summary>
        /// Metodo para obtener el comando para consultar todos los Materiales Sapi
        /// </summary>
        /// <returns>Comando para consultar todos los Materiales Sapi</returns>
        public static ComandoBase<IList<MaterialSapi>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosMateriales();
        }

        /// <summary>
        /// Metodo para obtener el comando para insertar o actualizar un Material Sapi
        /// </summary>
        /// <param name="material">Material sapi a actualizar</param>
        /// <returns>Comando para insertar o actualizar un material sapi</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(MaterialSapi material)
        {
            return new ComandoInsertarOModificarMaterial(material);
        }

        /// <summary>
        /// Metodo para obtener el comando para eliminar un Material Sapi
        /// </summary>
        /// <param name="material">Material Sapi a eliminar</param>
        /// <returns>Comando para eliminar el material</returns>
        public static ComandoBase<bool> ObtenerComandoEliminar(MaterialSapi material)
        {
            return new ComandoEliminarMaterial(material);
        }

        /// <summary>
        /// Metodo para obtener el comando para consultar todos los Materiales Sapi a traves de un filtro
        /// </summary>
        /// <param name="material">Material Sapi usado como filtro</param>
        /// <returns>Comando para realizar la consulta por filtro</returns>
        public static ComandoBase<IList<MaterialSapi>> ObtenerComandoConsultarMaterialesSapiFiltro(MaterialSapi material)
        {
            return new ComandoConsultarMaterialesSapiFiltro(material);
        }
    }
}
