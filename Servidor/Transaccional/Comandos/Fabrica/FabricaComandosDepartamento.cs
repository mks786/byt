using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosDepartamento;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosDepartamento
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Departamento
        /// </summary>
        /// <param name="departamento">Departamento a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el departamento en la base de datos</returns>
        public static ComandoBase<string> ObtenerComandoAgregar(Departamento departamento)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los departamentos
        /// </summary>
        /// <returns>El Comando para consultar todos los Departamentos</returns>
        public static ComandoBase<IList<Departamento>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosDepartamentos();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Departamento por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Departamento> ObtenerComandoConsultarPorID(Departamento departamento)
        {
            return new ComandoConsultarDepartamentoPorId(departamento);
        }
    }
}
