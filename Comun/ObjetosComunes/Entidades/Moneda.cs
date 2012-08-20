using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Moneda
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _operacon;
        private float? _tasa;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Moneda() { }

        /// <summary>
        /// Constructor que inicializa el condigo de la moneda
        /// </summary>
        /// <param name="id">Codigo de la moneda</param>
        public Moneda(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código de la moneda
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion de moneda
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la operacion de la moneda
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacon; }
            set { _operacon = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la tasa de la moneda
        /// </summary>
        public virtual float? Tasa
        {
            get { return _tasa; }
            set { _tasa = value; }
        }
        #endregion
    }
}
