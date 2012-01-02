using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InfoAdicional
    {
        #region Atributos

        private string _id;
        private string _nombre;
        private string _email;
        private string _info;
        private string _operacion;
        private bool _insertarOModificar;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public InfoAdicional() { }

        /// <summary>
        /// Constructor que inicializa el condigo del idioma
        /// </summary>
        /// <param name="codigo">Codigo del status</param>
        public InfoAdicional(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código del idioma
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion del idioma
        /// </summary>
        public virtual string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion del idioma
        /// </summary>
        public virtual string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion del idioma
        /// </summary>
        public virtual string Info
        {
            get { return this._info; }
            set { this._info = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la operacion que se va a realizar 
        /// sea CREATE MODIFY o DELETE 
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si se esta insertando o modificando
        /// </summary>
        public virtual bool InsertarOModificar
        {
            get { return _insertarOModificar; }
            set { _insertarOModificar = value; }
        }

        #endregion
    }
}
