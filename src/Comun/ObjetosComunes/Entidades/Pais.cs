using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Pais
    {
        #region Atributos

        private int _id;
        private string _codigo;
        private string _nombreIngles;
        private string _nombreEspanol;
        private string _nacionalidad;
        private string _region;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Pais() { }

        /// <summary>
        /// Constructor que inicializa el Id del Pais
        /// </summary>
        /// <param name="id">Id del Pais</param>
        public Pais(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Pais
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Pais
        /// </summary>
        public virtual string Codigo
        {
            get { return this._codigo; }
            set { this._codigo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre en ingles del Pais
        /// </summary>
        public virtual string NombreIngles
        {
            get { return this._nombreIngles; }
            set { this._nombreIngles = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre en español del Pais
        /// </summary>
        public virtual string NombreEspanol
        {
            get { return this._nombreEspanol; }
            set { this._nombreEspanol = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la nacionalidad de las personas que habitan el Pais
        /// </summary>
        public virtual string Nacionalidad
        {
            get { return this._nacionalidad; }
            set { this._nacionalidad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la región donde se encuentra ubicado el Pais
        /// </summary>
        public virtual string Region
        {
            get { return this._region; }
            set { this._region = value; }
        }

        #endregion
    }
}
