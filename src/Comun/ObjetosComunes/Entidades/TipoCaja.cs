using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class TipoCaja
    {
        #region Atributos

        private int _id;
        private string _descripcion;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public TipoCaja() { }

        /// <summary>
        /// Constructor que inicializa el id del tipo de caja
        /// </summary>
        /// <param name="id">Id del tipo de caja</param>
        public TipoCaja(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del tipo de caja
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del tipo de caja
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }
               

        #endregion
    }
}
