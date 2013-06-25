using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class TipoDocumento
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _tipoDoc;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public TipoDocumento() { }

        /// <summary>
        /// Constructor que inicializa el id del tipo de infobol
        /// </summary>
        /// <param name="id">Id del tipo de infobol</param>
        public TipoDocumento(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del tipo de documento
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del tipo de documento
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del tipo de infobol
        /// </summary>
        public virtual string TipoDoc
        {
            get { return this._tipoDoc; }
            set { this._tipoDoc = value; }
        }

        #endregion
    }
}
