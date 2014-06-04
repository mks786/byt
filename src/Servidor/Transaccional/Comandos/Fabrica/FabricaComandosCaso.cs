using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCaso;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCaso
    {
        /// <summary>
        /// Metodo para obtener el Comando para consultar el contenido de toda la tabla de Casos
        /// </summary>
        /// <returns>El Comando que permite realizar la accion determinada</returns>
        public static ComandoBase<IList<Caso>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCasos();
        }

        /// <summary>
        /// Metodo para obtener el Comando para Insertar o actualizar un Caso
        /// </summary>
        /// <param name="caso">Caso a insertar o actualizar</param>
        /// <returns>El Comando que permite realizar la accion determinada</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Caso caso)
        {
            return new ComandoInsertarOModificarCaso(caso);
        }


        /// <summary>
        /// Metodo para obtener el Comando para Consultar Casos por filtro
        /// </summary>
        /// <param name="caso">Filtro para la consulta</param>
        /// <returns>El Comando que permite realizar la accion determinada</returns>
        public static ComandoBase<IList<Caso>> ObtenerComandoConsultarCasosFiltro(Caso caso)
        {
            return new ComandoConsultarCasosFiltro(caso);
        }
    }
}
