using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IUsuarioServicios: IServicioBase<Usuario>
    {

        /// <summary>
        /// Servicio que autentica a un usuario contra la Base de Datos
        /// </summary>
        /// <param name="usuario">Usuario a Autenticar</param>
        /// <returns>Usuario autenticado</returns>
        Usuario Autenticar(Usuario usuario);


        /// <summary>
        /// Servicio que cierra la sesión del usuario
        /// </summary>
        /// <param name="hash">Hash del usuario a cerrar sesión</param>
        void CerrarSession(int hash);
    }
}
