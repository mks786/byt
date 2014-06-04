using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCasoBase;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCasoBase
    {
        /// <summary>
        /// Metodo que obtiene el comando necesario para insertar o actualizar un Caso Base
        /// </summary>
        /// <param name="casoBase">Caso Base a insertar o actualizar</param>
        /// <returns>Comando necesario para ejecutar la accion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CasoBase casoBase)
        {
            return new ComandoInsertarOModificarCasoBase(casoBase);
        }


        /// <summary>
        /// Metodo que obtiene el comando necesario para eliminar un Caso Base
        /// </summary>
        /// <param name="casoBase">Caso Base a eliminar</param>
        /// <returns>Comando necesario para ejecutar la accion</returns>
        public static ComandoBase<bool> ObtenerComandoEliminar(CasoBase casoBase)
        {
            return new ComandoEliminarCasoBase(casoBase);
        }

        /// <summary>
        /// Metodo que obtiene el comando necesario para obtener los casos base de un caso
        /// </summary>
        /// <param name="casoBase">Caso Base usado como filtro</param>
        /// <returns>Comando necesario para ejecutar la accion</returns>
        public static ComandoBase<IList<CasoBase>> ObtenerComandoConsultarCasosBaseDeCaso(CasoBase casoBase)
        {
            return new ComandoConsultarCasosBaseDeCaso(casoBase);
        }
    }
}
