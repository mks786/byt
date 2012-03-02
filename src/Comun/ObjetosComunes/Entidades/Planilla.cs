using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Planilla
    {
        #region Atributos

        private int _id;
        private string _folio;
                
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Planilla() { }

        /// <summary>
        /// Constructor que inicializa el Id del Anexo
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public Planilla(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Anexo
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual string Folio
        {
            get { return this._folio; }
            set { this._folio = value; }
        }

        #endregion
    }
}
