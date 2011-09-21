using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class TipoFecha
    {
        #region Atributos

        private string _id;
        private string _descripcion;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public TipoFecha() { }

        /// <summary>
        /// Constructor que inicializa el id del tipo de fecha
        /// </summary>
        /// <param name="id">Id del tipo de fecha</param>
        public TipoFecha(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del tipo de fecha
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del tipo de fecha
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        #endregion
    }
}
