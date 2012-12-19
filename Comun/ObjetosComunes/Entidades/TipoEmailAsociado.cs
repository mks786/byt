using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class TipoEmailAsociado
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _funcion;
        private Departamento _departamento;
        private string _operacion;
                
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public TipoEmailAsociado() { }

        /// <summary>
        /// Constructor que inicializa el Id del Anexo
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public TipoEmailAsociado(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Anexo
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual string Funcion
        {
            get { return this._funcion; }
            set { this._funcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual Departamento Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }

        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        #endregion
    }
}
