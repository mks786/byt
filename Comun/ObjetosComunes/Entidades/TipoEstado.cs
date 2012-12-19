using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class TipoEstado
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _descripcionInternacional;
        private string _estadoP;
        private string _estadoPInternacional;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public TipoEstado() { }

        /// <summary>
        /// Constructor que inicializa el Id del TEstado
        /// </summary>
        /// <param name="id">Id del TEstado</param>
        public TipoEstado(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del TEstado
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u la descripción del TEstado
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u la descripción internacional del TEstado
        /// </summary>
        public virtual string DescripcionInternacional
        {
            get { return _descripcionInternacional; }
            set { _descripcionInternacional = value; }
        }

        /// <summary>
        /// Propiedad que asigna u el EstadoP del TEstado
        /// </summary>
        public virtual string EstadoP
        {
            get { return _estadoP; }
            set { _estadoP = value; }
        }

        /// <summary>
        /// Propiedad que asigna u el EstadoP internacional del TEstado
        /// </summary>
        public virtual string EstadoPInternacional
        {
            get { return _estadoPInternacional; }
            set { _estadoPInternacional = value; }
        }


        #endregion
    }
}
