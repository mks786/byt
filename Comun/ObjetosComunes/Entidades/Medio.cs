using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Medio
    {
        #region Atributos

        private string _id;
        private string _nombre;
        private string _formato;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Medio() { }

        /// <summary>
        /// Constructor que inicializa el Id del Medio
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public Medio(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Medio
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre del Medio
        /// </summary>
        public virtual string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el formato del Medio
        /// </summary>
        public virtual string Formato
        {
          get { return _formato; }
          set { _formato = value; }
        }



        #endregion
    }
}
