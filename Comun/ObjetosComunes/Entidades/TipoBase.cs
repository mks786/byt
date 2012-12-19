using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class TipoBase
    {
        #region Atributos

        private string _id;
        private string _descripcion;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public TipoBase() { }

        /// <summary>
        /// Constructor que inicializa el id del Rol
        /// </summary>
        /// <param name="id">Id del Rol</param>
        public TipoBase(string id)
        {
            this._id = id;
        }

        /// <summary>
        /// Constructor que inicializa el id del Rol
        /// </summary>
        /// <param name="id">Id del Rol</param>
        public TipoBase(string id, string descripcion)
        {
            this._id = id;
            this._descripcion = descripcion;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Tipo de Base
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del Tipo de Base
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        #endregion
    }
}
