using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInteresadoPatente;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInteresadoPatente
    {
        /// <summary>
        /// Metodo que obtiene el comando para poder obtener todo el contenido de la tabla MYP_INTERESADOS_PAT
        /// </summary>
        /// <returns>Comando para obtener todos los interesados de la tabla MYP_INTERESADOS_PAT</returns>
        public static ComandoBase<IList<InteresadoPatente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInteresadosDePatente();
        }


        /// <summary>
        /// Metodo que obtiene el comando para consultar los interesados de una patente especifica
        /// </summary>
        /// <param name="patente">Patente a consultar</param>
        /// <returns>Comando para obtener los interesados de una patente especifica</returns>
        public static ComandoBase<IList<InteresadoPatente>> ObtenerComandoConsultarInteresadosDePatente(Patente patente)
        {
            return new ComandoConsultarInteresadosDePatente(patente);
        }



        /// <summary>
        /// Metodo que devuelve el comando necesario para insertar o actualizar un objeto de la entidad InteresadoPatente
        /// </summary>
        /// <param name="interesadoPatente">Objeto InteresadoPatente a insertar o actualizar</param>
        /// <returns>Comando para insertar o actualizar un objeto de la entidad InteresadoPatente</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InteresadoPatente interesadoPatente)
        {
            return new ComandoInsertarOModificarInteresadoPatente(interesadoPatente);
        }
    }
}
