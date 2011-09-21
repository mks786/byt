using System;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosUsuario
{
    public class ComandoConsultarUsuarioPorID : ComandoBase<Usuario>
    {
        Usuario _usuario;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Usuario a consultar</param>
        public ComandoConsultarUsuarioPorID(Usuario usuario)
        {
            this._usuario = usuario;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            throw new NotImplementedException();
        }
    }
}
