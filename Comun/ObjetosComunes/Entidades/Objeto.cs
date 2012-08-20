using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Objeto
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private IList<Rol> _roles;


        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del objeto
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del objeto 
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los rolesOBjetos que poseen el rol
        /// </summary>
        public virtual IList<Rol> Roles
        {
            get { return this._roles; }
            set { this._roles = value; }
        }

        #endregion
    }
}
