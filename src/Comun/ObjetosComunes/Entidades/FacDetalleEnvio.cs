using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable()]
    public class FacDetalleEnvio
    {
        #region "Atributos"

        private string _id;
        #endregion
        private string _descripcion;

        #region "Constructores"
        public FacDetalleEnvio()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la DetalleEnvio
        /// </summary>
        /// <param name="id">Id de la tasa</param>
        public FacDetalleEnvio(string id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la DetalleEnvio
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene docmuento español
        // ''' </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        #endregion

    }
}