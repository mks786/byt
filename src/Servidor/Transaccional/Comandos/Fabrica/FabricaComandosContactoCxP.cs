using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosContactoCxP;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosContactoCxP
    {
        /// <summary>
        /// Metodo que obtiene el comando para consultar todos los registros de la tabla FAC_CONTACTO_CXP
        /// </summary>
        /// <returns>Comando para consultar todos los registros de la tabla FAC_CONTACTO_CXP</returns>
        public static ComandoBase<IList<ContactoCxP>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosContactoCxP();
        }

        /// <summary>
        /// Metodo que obtiene el comando para insertar o modificar un ContactoCxP
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP a insertar o actualizar</param>
        /// <returns>Comando para insertar o actualizar un contacto cxp</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(ContactoCxP contactoCxP)
        {
            return new ComandoInsertarOModificarContactoCxP(contactoCxP);
        }

        /// <summary>
        /// Metodo que obtiene el comando para consultar por filtro 
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP filtro</param>
        /// <returns>Comando para consultar por filtro</returns>
        public static ComandoBase<IList<ContactoCxP>> ObtenerComandoConsultarContactoCxPFiltro(ContactoCxP contactoCxP)
        {
            return new ComandoConsultarContactoCxPFiltro(contactoCxP);
        }
    }
}
