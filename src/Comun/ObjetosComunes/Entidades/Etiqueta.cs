using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Etiqueta
    {
        #region Atributos

        private string _id;
        private string _descripcion1;
        private string _descripcion2;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Etiqueta() { }

        /// <summary>
        /// Constructor que inicializa el condigo de la etiqueta
        /// </summary>
        /// <param name="id">Codigo de la etiqueta</param>
        public Etiqueta(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código de la etiqueta
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion1 de la etiqueta
        /// </summary>
        public virtual string Descripcion1
        {
            get { return this._descripcion1; }
            set { this._descripcion1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion2 de la etiqueta
        /// </summary>
        public virtual string Descripcion2
        {
            get { return this._descripcion2; }
            set { this._descripcion2 = value; }
        }

        #endregion
    }
}
