using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Servicio
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _descripcionEntrada;
        private string _proximo;
        private string _proximoEntrada;
        private string _previo;
        private string _previoEntrada;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Servicio() { }

        /// <summary>
        /// Constructor que inicializa el Id del Servicio
        /// </summary>
        /// <param name="id">Id del Servicio</param>
        public Servicio(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion entrada
        /// </summary>
        public virtual string DescripcionEntrada
        {
            get { return this._descripcionEntrada; }
            set { this._descripcionEntrada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el proximo
        /// </summary>
        public virtual string Proximo
        {
            get { return this._proximo; }
            set { this._proximo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el proximo entrada
        /// </summary>
        public virtual string ProximoEntrada
        {
            get { return this._proximoEntrada; }
            set { this._proximoEntrada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el previo
        /// </summary>
        public virtual string Previo
        {
            get { return this._previo; }
            set { this._previo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el previo entrada
        /// </summary>
        public virtual string PrevioEntrada
        {
            get { return this._previoEntrada; }
            set { this._previoEntrada = value; }
        }


        #endregion
    }
}
