using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Accion
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private IList<Caso> _casos;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Accion() { }

        /// <summary>
        /// Constructor que inicializa el id de la Accion
        /// </summary>
        /// <param name="id">Id del tipo de caso</param>
        public Accion(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del tipo de caso
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del tipo de caso
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        public virtual IList<Caso> Casos
        {
            get { return this._casos; }
            set { this._casos = value; }
        }

        #endregion
    }
}

