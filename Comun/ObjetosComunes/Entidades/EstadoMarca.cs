using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class EstadoMarca
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _descripcionIngles;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public EstadoMarca() { }

        /// <summary>
        /// Constructor que inicializa el id del Rol
        /// </summary>
        /// <param name="id">Id del Rol</param>
        public EstadoMarca(string id)
        {
            this._id = id;
        }

        /// <summary>
        /// Constructor que inicializa el id del Rol
        /// </summary>
        /// <param name="id">Id del Rol</param>
        public EstadoMarca(string id, string descripcion)
        {
            this._id = id;
            this._descripcion = descripcion;
        }


        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del estado
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del estado
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción en ingles del estado
        /// </summary>
        public virtual string DescripcionIngles
        {
            get { return this._descripcionIngles; }
            set { this._descripcionIngles = value; }
        }

        #endregion
    }
}
