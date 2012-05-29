using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoUsuario : IDaoBase<Usuario, string>
    {
        /// <summary>
        /// Método que autentica un usuario
        /// </summary>
        /// <param name="usuario">usuario a autenticar</param>
        /// <returns>Usuario autenticado</returns>
        Usuario Autenticar(Usuario usuario);


        /// <summary>
        /// Metodo que obtiene el usuario solicitado por sus iniciales
        /// </summary>
        /// <param name="iniciales">String de iniciales</param>
        /// <returns>El usuario con esas iniciales</returns>
        Usuario ObtenerUsuarioPorIniciales(string iniciales);
    }
}
