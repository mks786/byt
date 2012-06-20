using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Condicion
    {
        #region Atributos

        private int? _id;
        private string _descripcion;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Condicion() { }

        /// <summary>
        /// Constructor que inicializa el Id del Pais
        /// </summary>
        /// <param name="id">Id del Pais</param>
        public Condicion(int? id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id de la condición
        /// </summary>
        public virtual int? Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el id de la condición
        /// </summary>
        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }



        #endregion
    }
}
