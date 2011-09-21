using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Estatus
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _descripcionIngles;
        private string _statusProximoPaso;
        private string _statusProximoPasoIngles;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Estatus() { }

        /// <summary>
        /// Constructor que inicializa el condigo del status
        /// </summary>
        /// <param name="codigo">Codigo del status</param>
        public Estatus(string codigo)
        {
            this._id = codigo;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Pais
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion del status
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion en ingles del status
        /// </summary>
        public virtual string DescripcionIngles
        {
            get { return this._descripcionIngles; }
            set { this._descripcionIngles = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el proximo paso del status
        /// </summary>
        public virtual string StatusProximoPaso
        {
            get { return this._statusProximoPaso; }
            set { this._statusProximoPaso = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el proximo paso del status en ingles
        /// </summary>
        public virtual string StatusProximoPasoIngles
        {
            get { return this._statusProximoPasoIngles; }
            set { this._statusProximoPasoIngles = value; }
        }

        #endregion
    }
}
