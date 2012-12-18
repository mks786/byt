using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosAsociado;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosAsociado
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un asociado
        /// </summary>
        /// <param name="asociado">Asociado a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Asociado asociado)
        {
            return new ComandoInsertarOModificarAsociado(asociado);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un asociado
        /// </summary>
        /// <param name="asociado">Asociado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarAsociado(Asociado asociado)
        {
            return new ComandoEliminarAsociado(asociado);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los asociados
        /// </summary>
        /// <returns>Lista con todos los asociados</returns>
        public static ComandoBase<IList<Asociado>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosAsociados();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un asociado con todas sus referencias
        /// </summary>
        /// <returns>Asocaido con todo</returns>
        public static ComandoBase<Asociado> ObtenerComandoConsultarAsociadoConTodo(Asociado asociado)
        {
            return new ComandoConsultarAsociadoConTodo(asociado);
        }



        public static ComandoBase<IList<Asociado>> ObtenerComandoConsultarAsociadosFiltro(Asociado asociado)
        {
            return new ComandoConsultarAsociadosFiltro(asociado);
        }

        public static ComandoBase<bool> ObtenerComandoVerificarCartasPorAsociado(Asociado asociado)
        {
            return new ComandoVerificarCartasDeAsociado(asociado);
        }

        public static ComandoBase<IList<ContactosDelAsociadoVista>> ObtenerComandoConsultarContactosDelAsociado(Asociado asociado, bool todos)
        {
            return new ComandoConsultarContactosDelAsociado(asociado, todos);
        }

        public static ComandoBase<IList<EmailAsociado>> ObtenerComandoConsultarEmailsDelAsociado(Asociado asociado)
        {
            return new ComandoConsultarEmailsDelAsociado(asociado);
        }
    }
}