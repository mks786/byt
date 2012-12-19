using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Departamento
    {

        #region Atributos

        private string _id;
        private string _descripcion;
        private IList<Usuario> _usuarios;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Departamento() { }

        /// <summary>
        /// Constructor que inicializa el Id del Departamento
        /// </summary>
        /// <param name="id">Id del departamento</param>
        public Departamento(string id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Departamento
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del Departamento 
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los usuarios que poseen el Departamento
        /// </summary>
        public virtual IList<Usuario> Usuarios
        {
            get { return this._usuarios; }
            set { this._usuarios = value; }
        }

        #endregion

    }
}
