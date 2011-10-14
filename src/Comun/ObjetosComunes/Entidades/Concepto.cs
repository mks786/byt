using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Concepto
    {
        #region Atributos

        private string _id;
        private string _descripcion;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Concepto() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public Concepto(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Concepto
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion
    }
}
