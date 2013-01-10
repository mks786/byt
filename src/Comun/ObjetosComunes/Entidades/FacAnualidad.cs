using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System;
namespace Trascend.Bolet.ObjetosComunes.Entidades
{
   [Serializable]
    public class FacAnualidad
    {
        #region "Atributos"

        private string _id;
        private string _doc_esp;
        #endregion
        private string _doc_ingl;

        #region "Constructores"
        public FacAnualidad()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacAnualidad
        /// </summary>
        /// <param name="id">Id de la tasa</param>
        public FacAnualidad(string id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la documentacion traduccion
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene docmuento español
        /// </summary>
        public virtual string Doc_Esp
        {
            get { return this._doc_esp; }
            set { this._doc_esp = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene docmuento ingles
        /// </summary>
        public virtual string Doc_Ingl
        {
            get { return this._doc_ingl; }
            set { this._doc_ingl = value; }
        }

        #endregion

    }
}