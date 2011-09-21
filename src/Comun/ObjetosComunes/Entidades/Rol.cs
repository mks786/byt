using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Rol
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private IList<Usuario> _usuarios;
        private IList<Objeto> _objetos;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Rol() { }

        /// <summary>
        /// Constructor que inicializa el id del Rol
        /// </summary>
        /// <param name="id">Id del Rol</param>
        public Rol(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del rol
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del rol
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los usuarios que poseen el rol
        /// </summary>
        public virtual IList<Usuario> Usuarios
        {
            get { return this._usuarios; }
            set { this._usuarios = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los rolesOBjetos que poseen el rol
        /// </summary>
        public virtual IList<Objeto> Objetos
        {
            get { return this._objetos; }
            set { this._objetos = value; }
        }

        #endregion
    }
}
